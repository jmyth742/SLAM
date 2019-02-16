#pragma once

#include <iostream>

#include <zmq.hpp>
#include "zhelpers.hpp"

#include "ZmqUtility.h"

#define MAX_MESSAGE_SIZE 10000000

#define DEBUG_MESSAGE 0

class MIRoZmqPublisher
{
public:
	MIRoZmqPublisher(std::string zmqAddress);
	~MIRoZmqPublisher();

    bool publish3DPoints(MiroZmq::Zmq3DPoints points2Send);

private:
	zmq::context_t m_context;
	zmq::socket_t *m_publisher;

	std::string m_address;

	char *m_buffer = new char[MAX_MESSAGE_SIZE];
};

