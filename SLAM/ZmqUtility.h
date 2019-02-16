#ifndef ZMQ_UTILITY_H
#define ZMQ_UTILITY_H


#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <inttypes.h>
#include <cstring>

/*	ZMQ COMUNICATION PROTOCOL

Subscriber Adress : "tcp://127.0.0.1:Port"
Publisher Adress : "tcp://*:Port"


*/

namespace MiroZmq
{

	typedef struct
	{
		unsigned char _type;	// 16
	        int _rows;
			int _cols;
			int _maxIter;
			float _transformationEpsilon;
			float _euclideanFitnessEpsilon;
			float _filterSize;
			float _tx;
			float _ty;
			float _tz;
			float _rx;
			float _ry;
			float _rz;
		 	float *_xPoints;				// measured in millimeters
		 	float *_yPoints;
		 	float *_zPoints;
			unsigned int *_rgb;
       		unsigned long _pkgSize;

        void SetData(int rows, int cols, int maxIter, float transformationEpsilon, float euclideanFitnessEpsilon, float filterSize, float tx, float ty, float tz, float rx, float ry, float rz, float *xPoints, float *yPoints, float *zPoints, unsigned int *rgb)

		{
	    _pkgSize = 0;
            _type = 16;
            _pkgSize += sizeof(unsigned char);
            _rows = rows;
	     //printf("\n_rows %d \n", _rows);
            _pkgSize += sizeof(int);
            _cols = cols;
						_pkgSize += sizeof(int);
            _maxIter = maxIter;
						_pkgSize += sizeof(float);
            _transformationEpsilon = transformationEpsilon;
						_pkgSize += sizeof(float);
            _euclideanFitnessEpsilon = euclideanFitnessEpsilon;
						_pkgSize += sizeof(float);
            _filterSize = filterSize;

						_pkgSize += sizeof(float);
            _tx = tx;
						_pkgSize += sizeof(float);
            _ty = ty;
						_pkgSize += sizeof(float);
            _tz = tz;
						_pkgSize += sizeof(float);
            _rx = rx;
						_pkgSize += sizeof(float);
            _ry = ry;
						_pkgSize += sizeof(float);
            _rz = rz;

            _pkgSize += sizeof(int);
            _xPoints = xPoints;

            _pkgSize += sizeof(float) * _rows * _cols;
            _yPoints = yPoints;

            _pkgSize += sizeof(float) * _rows * _cols;
            _zPoints = zPoints;
	    	_pkgSize += sizeof(float) * _rows * _cols;
            _rgb = rgb;

						_pkgSize += sizeof(unsigned int) * _rows;
		}

	} Zmq3DPoints;

}


class ZmqUtility
{


public:
	ZmqUtility();
	~ZmqUtility();

	static MiroZmq::Zmq3DPoints Deserialize3DPoints(char *buf, int size);

	static int Serialize3DPoints(MiroZmq::Zmq3DPoints points, char *outBuf);

};

#endif
