#include "ZmqUtility.h"


ZmqUtility::ZmqUtility()
{
}


ZmqUtility::~ZmqUtility()
{
}

MiroZmq::Zmq3DPoints ZmqUtility::Deserialize3DPoints(char *buf, int size)
{

	MiroZmq::Zmq3DPoints points;
    unsigned int i = 0;
    unsigned char type = buf[i];
    i += sizeof(unsigned char);
    int *incomingRows = (int*)&buf[i];
    i += sizeof(int);
    int *incomingCols = (int*)&buf[i];
    i += sizeof(int);
		int *incomingMaxIter = (int*)&buf[i];
    i += sizeof(int);
		float *incomingTransformationEpsilon = (float*)&buf[i];
    i += sizeof(float);
		float *incomingEuclideanFitnessEpsilon = (float*)&buf[i];
    i += sizeof(float);
		float *incomingFilterSize = (float*)&buf[i];
    i += sizeof(float);

		float *incomingTx = (float*)&buf[i];
    i += sizeof(float);
		float *incomingTy = (float*)&buf[i];
    i += sizeof(float);
		float *incomingTz = (float*)&buf[i];
    i += sizeof(float);

		float *incomingRx = (float*)&buf[i];
    i += sizeof(float);
		float *incomingRy = (float*)&buf[i];
    i += sizeof(float);
		float *incomingRz = (float*)&buf[i];
    i += sizeof(float);

    float *incomingXPoints = (float*)&buf[i];
    i += sizeof(float) * (*incomingRows) * (*incomingCols);
    float *incomingYPoints = (float*)&buf[i];
    i += sizeof(float) * (*incomingRows) * (*incomingCols);
    float *incomingZPoints = (float*)&buf[i];
    i += sizeof(float) * (*incomingRows) * (*incomingCols);
		unsigned int *incomingCPoints = (unsigned int*)&buf[i];
    i += sizeof(unsigned int) * (*incomingRows);
    points.SetData(*incomingRows, *incomingCols, *incomingMaxIter, *incomingTransformationEpsilon, *incomingEuclideanFitnessEpsilon, *incomingFilterSize, *incomingTx, *incomingTy, *incomingTz, *incomingRx, *incomingRy, *incomingRz, incomingXPoints, incomingYPoints, incomingZPoints, incomingCPoints);
    return points;
}

int ZmqUtility::Serialize3DPoints(MiroZmq::Zmq3DPoints points, char *outBuf)
{
	int size = 0;
	std::memcpy(outBuf + size, &points._type, sizeof(unsigned char));				// type of variable
	size += sizeof(unsigned char);

        std::memcpy(outBuf + size, &points._rows, sizeof(int));							// n. of rows
	size += sizeof(int);

	std::memcpy(outBuf + size, &points._cols, sizeof(int));							// n. of cols
	size += sizeof(int);

	std::memcpy(outBuf + size, &points._maxIter, sizeof(int));
	size += sizeof(int);

	std::memcpy(outBuf + size, &points._transformationEpsilon, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._euclideanFitnessEpsilon, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._filterSize, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._tx, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._ty, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._tz, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._rx, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._ry, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, &points._rz, sizeof(float));
	size += sizeof(float);

	std::memcpy(outBuf + size, points._xPoints, sizeof(float) * points._rows * points._cols);		// X Point data
	size += (sizeof(float)* points._rows * points._cols);

	std::memcpy(outBuf + size, points._yPoints, sizeof(float) * points._rows * points._cols);		// Y Point data
	size += (sizeof(float)* points._rows * points._cols);

	std::memcpy(outBuf + size, points._zPoints, sizeof(float) * points._rows * points._cols);		// Z Point data
	size += (sizeof(float) * points._rows * points._cols);

				std::memcpy(outBuf + size, points._rgb, sizeof(unsigned int)* points._rows);         // Z Point data
        size += (sizeof(unsigned int)* points._rows);


    return size;
}
