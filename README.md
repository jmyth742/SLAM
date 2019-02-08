# SLAM
This is a unity based game for industry 4.0 to allow the user to understand how SLAM (Simultaneous Localisation and Mapping) works. The use can move around a mine take pictures using a 3D camera and saving them as pointclouds, this is then sent to an ICP algorithm to reconstruct the mine in real time whilst the user moves around, whilst localising the the drone at the same time.

There are some build dependencies which need to be completed to compile the system.

First you need to ensure you have the PCL library installed and its dependencies.

http://www.pointclouds.org/downloads/linux.html

We also need to install ZMQ and the cpp bindings for ZMQ see : http://zeromq.org/area:download

Cpp bindings : http://zeromq.org/bindings:cpp

Once this has completed and the installations are sucessful. you must do the following to build

git clone https://github.com/jmyth742/SLAM.git

cd SLAM/ && mkdir build && cmake .. && make
