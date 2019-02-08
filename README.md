# SLAM
This is a Unity based game for Industry 4.0 to allow the user to understand how SLAM (Simultaneous Localisation and Mapping) works. The user can move around a mine, capture pointclouds using a simulated time of flight (ToF) camera and send them over to the ICP algorithm to reconstruct the mine in real time. This allows the user to map and localise itself on an unstructured environment.

We provide a built working version that uses Windows Subsystem of Linux (WSL) to run the backend, and a working executable of the game for Windows 10 64 bits.

Nevertheless, there are some build dependencies which need to be completed to compile the system.

First you need to ensure you have the PCL library installed and its dependencies. For the WSL or on Ubuntu 18.04 you can simply sudo apt-get install libpcl-dev

http://www.pointclouds.org/downloads/linux.html

We also need to install ZMQ and the cpp bindings for ZMQ see : http://zeromq.org/area:download

To build ZeroMQ on WSL it is recommended to build it from source. It is enough to follow the build instructions on https://github.com/zeromq/cppzmq


Cpp bindings : http://zeromq.org/bindings:cpp

Once this has completed and the installations are sucessful. you must do the following to build

git clone https://github.com/jmyth742/SLAM.git

cd SLAM/ && mkdir build && cmake .. && make
