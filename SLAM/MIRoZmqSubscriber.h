#pragma once

#include <iostream>
#include <thread>
#include <mutex>
#include <functional>


#include <zmq.hpp>
#include "zhelpers.hpp"

#include "ZmqUtility.h"

#define MAX_MESSAGE_SIZE 10000000

#define DEBUG_MESSAGE 0
class Manager{
public:
	void ReadZmq(char* buf, int size);
};
class MIRoZmqSubscriber
{
	typedef std::function<void(char*, int)> Callback;


public:
	MIRoZmqSubscriber(std::string zmqAddress, int timeout, Callback fp);
	~MIRoZmqSubscriber();

	void setActive(bool flag);

private:

	void runSubscriber();

	std::thread m_thread;
	Callback m_callback;
	
	// The m context zmq stuff
	zmq::context_t m_context;
	zmq::socket_t *m_subscriber;
	std::string m_address;
	int socketConflate = 1;
	const char *socketFilter = "";

	char *m_buffer = new char[MAX_MESSAGE_SIZE];
	
	bool m_active = false;
	std::mutex m_activeMutex;

	int msTimeoutWithoutMessage = 0;
	int timeoutTimes = 0;
};

