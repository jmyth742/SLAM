#include <iostream>
#include <pcl/io/pcd_io.h>
#include <pcl/point_types.h>
#include <pcl/registration/icp.h>
#include <pcl/features/normal_3d.h>

#include <boost/thread/thread.hpp>
#include <pcl/filters/voxel_grid.h> // This is used for filtering the point clouds

#include <Eigen/Core> // Eigen matrices

#include <pcl/conversions.h>


#include "zhelpers.hpp"
#include "ZmqUtility.h"
#include "MIRoZmqSubscriber.h"
#include "MIRoZmqPublisher.h"
#include <zmq.hpp>
#include <string>
#include <iostream>
#include <fstream>

using namespace std;
using namespace MiroZmq;
MiroZmq::Zmq3DPoints points;

// Add normals to PCD. Note it changes the type to pcl::PointXYZRGBNormal
void addNormal(pcl::PointCloud<pcl::PointXYZRGB>::Ptr cloud,
			   pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr cloud_with_normals)
{
	// Output dataset
	pcl::PointCloud<pcl::Normal>::Ptr normals(new pcl::PointCloud<pcl::Normal>);

	// Create empty KdTree
	pcl::search::KdTree<pcl::PointXYZRGB>::Ptr searchTree(new pcl::search::KdTree<pcl::PointXYZRGB>);
	searchTree->setInputCloud(cloud);

	// Create normal estimation class
	pcl::NormalEstimation<pcl::PointXYZRGB, pcl::Normal> normalEstimator;
	normalEstimator.setInputCloud(cloud);
	normalEstimator.setSearchMethod(searchTree);
	normalEstimator.setKSearch(15);
	normalEstimator.compute(*normals);

	// Concatenate the RGBNormals fields
	pcl::concatenateFields(*cloud, *normals, *cloud_with_normals);
}

// Downsample pointclouds of type XYZ
void filter(pcl::PointCloud<pcl::PointXYZRGB>::Ptr cloudInput, float &filterSize) {
	pcl::VoxelGrid<pcl::PointXYZRGB> sor;
	// grid.setLeafSize(1.0f, 1.0f, 1.0f); // This is in meters.
	sor.setLeafSize(filterSize, filterSize, filterSize); // This is in meters.
	sor.setInputCloud(cloudInput);
	sor.filter(*cloudInput);
}

// Downsample pointclouds of type XYZRGBNormal
void filterWithNormals(pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr cloudInput, float &filterSize) {
	pcl::VoxelGrid<pcl::PointXYZRGBNormal> grid;
	grid.setLeafSize(0.5f, 0.5f, 0.5f); // This is in meters.
	grid.setInputCloud(cloudInput);
	grid.filter(*cloudInput);
}

// Load point clouds
int loadFile(pcl::PointCloud<pcl::PointXYZRGB>::Ptr pointCloud,
			 const std::string filepath) {
	if (pcl::io::loadPCDFile<pcl::PointXYZRGB>(filepath, *pointCloud) == -1)
	{
		PCL_ERROR("Couldn't read the file\n");
		return (-1);
	}
	return(0);
}

// Perform ICP with normals
void performIcpWithNormals(pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr &cloudTargetNormals,
						   pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr &cloudInputNormals,
						   Eigen::Matrix4f &local,
						   int &maxIter,
						   float &transformationEpsilon,
						   float &euclideanFitnessEpsilon) {
	pcl::IterativeClosestPointWithNormals<pcl::PointXYZRGBNormal, pcl::PointXYZRGBNormal>::Ptr icpNormal(new pcl::IterativeClosestPointWithNormals<pcl::PointXYZRGBNormal, pcl::PointXYZRGBNormal>());
	icpNormal->setMaximumIterations(maxIter);
	icpNormal->setTransformationEpsilon(transformationEpsilon);
	icpNormal->setEuclideanFitnessEpsilon(euclideanFitnessEpsilon);
	icpNormal->setInputSource(cloudInputNormals);
	icpNormal->setInputTarget(cloudTargetNormals);

	// Align both and override the input with the aligned cloud
	icpNormal->align(*cloudInputNormals);

	local = icpNormal->getFinalTransformation();
}

int main()
{
	char *m_buffer = new char[MAX_MESSAGE_SIZE];
		MIRoZmqPublisher MIRoZmqPublisher("tcp://127.0.0.1:5557");
	ZmqUtility zmqUtility;
	//  Prepare our context and subscriber
	zmq::context_t context(1);
	zmq::socket_t subscriber(context, ZMQ_SUB);
	subscriber.connect("tcp://127.0.0.1:35781");
	subscriber.setsockopt(ZMQ_SUBSCRIBE, "", 0);
	bool pointsAvail = true;

	//cloudTarget and cloudInput are the first cloud and the second to apply ICP.
	pcl::PointCloud<pcl::PointXYZRGB>::Ptr cloudTarget(new pcl::PointCloud<pcl::PointXYZRGB>);
	pcl::PointCloud<pcl::PointXYZRGB>::Ptr cloudInput(new pcl::PointCloud<pcl::PointXYZRGB>);

	pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr cloudTargetNormals(new pcl::PointCloud<pcl::PointXYZRGBNormal>());
	pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr cloudInputNormals(new pcl::PointCloud<pcl::PointXYZRGBNormal>());

	pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr cloudInputBufferNormals(new pcl::PointCloud<pcl::PointXYZRGBNormal>());
	pcl::PointCloud<pcl::PointXYZRGB>::Ptr cloudInputBuffer(new pcl::PointCloud<pcl::PointXYZRGB>());

	pcl::PointCloud<pcl::PointXYZRGB>::Ptr globalCloud(new pcl::PointCloud<pcl::PointXYZRGB>());
	pcl::PointCloud<pcl::PointXYZRGBNormal>::Ptr globalCloudNormals(new pcl::PointCloud<pcl::PointXYZRGBNormal>());

	// Define the matrices needed
	Eigen::Matrix4f global = Eigen::Matrix4f::Identity(); // Global movement produced
	Eigen::Matrix4f local = Eigen::Matrix4f::Identity(); // Local movement between clouds
	Eigen::Matrix4f mirrorX; // Mirror over x-axis. Unity is left-handed, whilst PCL is right-handed
	mirrorX << -1.0, 0.0, 0.0, 0.0,
	            0.0, 1.0, 0.0, 0.0,
	            0.0, 0.0, 1.0, 0.0,
	            0.0, 0.0, 0.0, 1.0;

	// System corrected output transformation matrix (must match a left handed system)
	Eigen::Matrix4f outputGlobal = Eigen::Matrix4f::Identity();

	float tx, ty, tz, rx, ry, rz;

	int maxIter;
	float transformationEpsilon;
	float euclideanFitnessEpsilon;
	float filterSize;

	const int pollItems = 1;
	zmq_pollitem_t items[pollItems];
	items[0].socket = subscriber;
	items[0].events = ZMQ_POLLIN;
	int timeoutTimes=0;

	std::cout << "Starting SLAM..." << std::endl;

	for (int i = 0; i < 300; i++) {
		clock_t start = clock();    // start

		// Load points into the point cloud.
		while(pointsAvail){
			//here we poll until there is an item ready for us.
			int rc = zmq_poll(items, pollItems, 1000); // (array, arrayItems, timeout);
			if (rc < 0) {
			timeoutTimes++;
			}
			if (items[0].revents & ZMQ_POLLIN) {
				int size = subscriber.recv(m_buffer, MAX_MESSAGE_SIZE, ZMQ_DONTWAIT); // blocking
				if(m_buffer[0] == 16){
					points = zmqUtility.Deserialize3DPoints(m_buffer, sizeof(m_buffer));
					cloudInput->width    = points._rows;
					cloudInput->height   = points._cols;
					cloudInput->is_dense = false;
					cloudInput->points.resize (cloudInput->width * cloudInput->height);
					maxIter = points._maxIter;
					transformationEpsilon = points._transformationEpsilon;
					euclideanFitnessEpsilon = points._euclideanFitnessEpsilon;
					filterSize = points._filterSize;
						 for (size_t i = 0; i < cloudInput->points.size (); ++i)
							  {
							    cloudInput->points[i].x = points._xPoints[i];
							    cloudInput->points[i].y = points._yPoints[i];
							    cloudInput->points[i].z = points._zPoints[i];
							    cloudInput->points[i].rgb = (float)points._rgb[i];
							  }
				pointsAvail=false;
				}
			}
		}

		// If not in the first iteration, reuse the buffer of cloudInput as the new target cloud
		if (i != 0) {
			pcl::copyPointCloud(*cloudInputBufferNormals, *cloudTargetNormals);
		}
		else {
			if(i == 0){ // For the first iteration use the same cloud as input and target.
			pcl::copyPointCloud(*cloudInput, *cloudTarget);
			}
			else{
				pcl::copyPointCloud(*cloudTarget, *cloudInput);
			}
			// Downsample the target cloud
			filter(cloudTarget, filterSize);
			std::cout << "Target cloud downsampled to " << cloudTarget->size() << " data points." << std::endl;

			// Mirror over the x-axis
			pcl::transformPointCloud(*cloudTarget, *cloudTarget, mirrorX);

			// Add the normals for point-to-plane ICP
			addNormal(cloudTarget, cloudTargetNormals);
			std::cout << "Normals added to target point cloud." << std::endl;
		}

		std::cout << "Input cloud loaded with " << cloudInput->size() << " data points." << std::endl;

		// Downsample the input cloud
		filter(cloudInput, filterSize);
		std::cout << "Input cloud downsampled to " << cloudInput->size() << " data points." << std::endl;

		// Mirror over the x-axis
		pcl::transformPointCloud(*cloudInput, *cloudInput, mirrorX);

		// Add the normals for point-to-plane ICP
		addNormal(cloudInput, cloudInputNormals);
		std::cout << "Normals added to input point cloud." << std::endl;

		// Create a buffer of the cloudInput to be used as cloudTargetNormals on the next iteration
		pcl::copyPointCloud(*cloudInputNormals, *cloudInputBufferNormals);

		// In the first iteration the target is also the only global cloud
		if (i == 0) {
			*globalCloudNormals = *cloudTargetNormals;
		}

		performIcpWithNormals(cloudTargetNormals, cloudInputNormals, local, maxIter, transformationEpsilon, euclideanFitnessEpsilon);

		// Print local transformation matrix from ICP and update global matrix
		std::cout << "Local: \n" << local << std::endl;
		global = global * local;
		std::cout << "Global: \n" << global << std::endl;

		outputGlobal = global * mirrorX;
		Eigen::Affine3f global3(outputGlobal);
		pcl::getTranslationAndEulerAngles(global3, tx, ty, tz, rx, ry, rz);
		rx = 180/3.14159265358979323846*rx;
		ry = 180/3.14159265358979323846*ry;
		rz = 180/3.14159265358979323846*rz+180;
		std::cout << "Translation (x, y, z)       : " << tx << ", " << ty << ", " << tz << std::endl;
		std::cout << "Rotation (roll, pitch, yaw) : " << rx << ", " << ry << ", " << rz << std::endl;

		// Apply the transformation to the input cloud and concatenate the transformed cloud to the global map
		pcl::transformPointCloudWithNormals(*cloudInputNormals, *cloudInputNormals, global);
		pcl::copyPointCloud(*cloudInputNormals, *cloudInput); // Copy to an XYZRGB point cloud, instead of XYZRGBNormal
		int col = 4;
		int rows = cloudInput->points.size();
		int globWidth = cloudInput->width; //rows
		int globHeight =  cloudInput->height; //cols

		float* xArr = NULL;
		float* yArr = NULL;
		float* zArr = NULL;
		unsigned int* rgbArr = NULL;

		long lonRow=cloudInput->points.size();

		int size = globWidth*globHeight;
		xArr = new float[size];
		yArr = new float[size];
		zArr = new float[size];
		rgbArr = new unsigned int[size];

		for (size_t i = 0; i < globWidth*globHeight; ++i){
			xArr[i] = cloudInput->points[i].x;
			yArr[i] = cloudInput->points[i].y;
			zArr[i] = cloudInput->points[i].z;
			rgbArr[i] = (unsigned int)cloudInput->points[i].rgb; //Add unsigned int cast
		}

		// SetData(int rows, int cols, int maxIter, float transformationEpsilon, float euclideanFitnessEpsilon, float filterSize, float tx, float ty, float tz, float rx, float ry, float rz, float *xPoints, float *yPoints, float *zPoints, unsigned int *rgb)
		points.SetData(globWidth, globHeight, maxIter,transformationEpsilon, euclideanFitnessEpsilon, filterSize, tx, ty, tz, rx, ry, rz, xArr, yArr, zArr, rgbArr);
		if(xArr != NULL) {
			delete[] xArr;
		}
		if(yArr != NULL) {
			delete[] yArr;
		}
		if(zArr != NULL) {
			delete[] zArr;
		}
		if(rgbArr != NULL) {
			delete[] rgbArr;
		}
		xArr = NULL;
		yArr = NULL;
		zArr = NULL;
		rgbArr = NULL;
    MIRoZmqPublisher.publish3DPoints(points);
	 	pointsAvail = true;

		clock_t end = clock();     // end
		std::cout << "Elapsed time = " << (double)(end - start) / CLOCKS_PER_SEC << "sec.\n\n";
	}

	return (0);
}
