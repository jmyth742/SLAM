# SLAM
This is a Unity based game for Industry 4.0 to allow the user to understand how SLAM (Simultaneous Localisation and Mapping) works. The user can move around a mine, capture pointclouds using a simulated time of flight (ToF) camera and send them over to the ICP algorithm to reconstruct the mine in real time. This allows the user to map and localise itself on an unstructured environment. Take a look at our [video demo](https://www.youtube.com/watch?v=ntKkwUbOkWc).

We provide a built working version that uses the Windows Subsystem of Linux (WSL) to run the backend, along with a working executable of the game for Windows 10 64-bits.

Nevertheless, there are some build dependencies which need to be completed to compile the system.

## Dependencies
First you need to ensure you have the PCL library installed and its dependencies. For the WSL or on Ubuntu 18.04:
```shell
sudo apt-get install libpcl-dev
```

More info: [PCL for linux](http://www.pointclouds.org/downloads/linux.html).

We also need to install ZeroMQ and the cpp bindings for ZMQ see:

[Download ZeroMQ](http://zeromq.org/area:download).

[Cpp bindings](http://zeromq.org/bindings:cpp).

To build ZeroMQ on WSL it is recommended to build it from source. It is enough to follow the build instructions on their [main repository](https://github.com/zeromq/cppzmq).

Once this has completed and the installations are sucessful you can clone this repository:
```shell
git clone https://github.com/jmyth742/SLAM.git
```
In case you need to build the back end:
```shell
cd SLAM/ && mkdir build && cmake .. && make
```
Note: you might not need to rebuild it if you use the binaries we already built for you. Check if the system matches.

For the game itself you can use the prebuilt executable provided inside the bin folder, or open the project on Unity and build an executable yourself.

## Usage
Simply run the built back end from shell and then the executable of the game itself.
