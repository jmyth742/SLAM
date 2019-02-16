#include "MIRoZmqPublisher.h"


MIRoZmqPublisher::MIRoZmqPublisher(std::string zmqAddress)
{
	m_address = zmqAddress;
	m_context = zmq::context_t(1);
	m_publisher = new zmq::socket_t(m_context, ZMQ_PUB);
	m_publisher->bind(m_address);
	std::cout << "creating connection...\n" << std::endl;
}


MIRoZmqPublisher::~MIRoZmqPublisher()
{
}

bool MIRoZmqPublisher::publish3DPoints(MiroZmq::Zmq3DPoints points2Send)
{
	char *outBuffer = new char[MAX_MESSAGE_SIZE];
    int size = ZmqUtility::Serialize3DPoints(points2Send, outBuffer);
    if (size == points2Send._pkgSize)
    {
        zmq::message_t message(outBuffer, size);
        m_publisher->send(message);
    }
    else
    {
        std::cout << "!!!!!!!!!!!!!!!!!! PACKAGE 3D POINTS SIZE ERROR !!!!!!!!!!!!!!!!!!!!" << std::endl;
        delete outBuffer;
        return 1;
    }

    delete outBuffer;

    return 0;

}
