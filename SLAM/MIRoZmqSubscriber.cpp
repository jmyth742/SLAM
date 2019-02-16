#include "MIRoZmqSubscriber.h"


MIRoZmqSubscriber::MIRoZmqSubscriber(std::string zmqAddress, int timeout, Callback fp)
{
	m_address = zmqAddress;
	m_callback = fp;
	msTimeoutWithoutMessage = timeout;

	m_context = zmq::context_t(1);
	
	setActive(true);
}


MIRoZmqSubscriber::~MIRoZmqSubscriber()
{
	setActive(false);
}

void MIRoZmqSubscriber::setActive(bool flag) {
	
	if (flag && !m_active) {
		m_thread = std::thread(&MIRoZmqSubscriber::runSubscriber, this);
	}

	if (!flag && m_active) {
		std::unique_lock<std::mutex> locker(m_activeMutex);
		m_active = false;
		locker.unlock();
		m_thread.join();
		m_thread.~thread();
	}
	
}

void MIRoZmqSubscriber::runSubscriber() {
	
	m_subscriber = new zmq::socket_t(m_context, ZMQ_SUB);
	m_subscriber->setsockopt(ZMQ_CONFLATE, &socketConflate, sizeof(socketConflate));
	m_subscriber->connect(m_address);
	m_subscriber->setsockopt(ZMQ_SUBSCRIBE, socketFilter, strlen(socketFilter));
	
	// Initialize poll set
	// I use it for timeout but it's usefull to read from more sockets
	const int pollItems = 1;
	zmq_pollitem_t items[pollItems];
	items[0].socket = *m_subscriber;
	items[0].events = ZMQ_POLLIN;

	// don't actually take the locks yet
	std::unique_lock<std::mutex> locker(m_activeMutex);
	m_active = true;
	while (m_active) {
		locker.unlock();

		int rc = zmq_poll(items, pollItems, 1000); // (array, arrayItems, timeout);  
		if (rc < 0) {
			timeoutTimes++;
		}

		if (items[0].revents & ZMQ_POLLIN) {
			
			int size = m_subscriber->recv(m_buffer, MAX_MESSAGE_SIZE, ZMQ_DONTWAIT); // blocking
			if (size > 0) {
				m_callback(m_buffer, size);
			}

		}
		locker.lock();		
	}

	m_subscriber->~socket_t();

}

void Manager::ReadZmq(char* buf, int size){

}
