# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.10

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /usr/bin/cmake

# The command to remove a file.
RM = /usr/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /mnt/c/Users/Javi/Desktop/SLAM/SLAM

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /mnt/c/Users/Javi/Desktop/SLAM/SLAM/build

# Include any dependencies generated for this target.
include CMakeFiles/slam.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/slam.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/slam.dir/flags.make

CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o: CMakeFiles/slam.dir/flags.make
CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o: ../MIRoZmqPublisher.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/mnt/c/Users/Javi/Desktop/SLAM/SLAM/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o -c /mnt/c/Users/Javi/Desktop/SLAM/SLAM/MIRoZmqPublisher.cpp

CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /mnt/c/Users/Javi/Desktop/SLAM/SLAM/MIRoZmqPublisher.cpp > CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.i

CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /mnt/c/Users/Javi/Desktop/SLAM/SLAM/MIRoZmqPublisher.cpp -o CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.s

CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.requires:

.PHONY : CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.requires

CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.provides: CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.requires
	$(MAKE) -f CMakeFiles/slam.dir/build.make CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.provides.build
.PHONY : CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.provides

CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.provides.build: CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o


CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o: CMakeFiles/slam.dir/flags.make
CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o: ../MIRoZmqSubscriber.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/mnt/c/Users/Javi/Desktop/SLAM/SLAM/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Building CXX object CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o -c /mnt/c/Users/Javi/Desktop/SLAM/SLAM/MIRoZmqSubscriber.cpp

CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /mnt/c/Users/Javi/Desktop/SLAM/SLAM/MIRoZmqSubscriber.cpp > CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.i

CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /mnt/c/Users/Javi/Desktop/SLAM/SLAM/MIRoZmqSubscriber.cpp -o CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.s

CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.requires:

.PHONY : CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.requires

CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.provides: CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.requires
	$(MAKE) -f CMakeFiles/slam.dir/build.make CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.provides.build
.PHONY : CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.provides

CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.provides.build: CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o


CMakeFiles/slam.dir/ZmqUtility.cpp.o: CMakeFiles/slam.dir/flags.make
CMakeFiles/slam.dir/ZmqUtility.cpp.o: ../ZmqUtility.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/mnt/c/Users/Javi/Desktop/SLAM/SLAM/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_3) "Building CXX object CMakeFiles/slam.dir/ZmqUtility.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/slam.dir/ZmqUtility.cpp.o -c /mnt/c/Users/Javi/Desktop/SLAM/SLAM/ZmqUtility.cpp

CMakeFiles/slam.dir/ZmqUtility.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/slam.dir/ZmqUtility.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /mnt/c/Users/Javi/Desktop/SLAM/SLAM/ZmqUtility.cpp > CMakeFiles/slam.dir/ZmqUtility.cpp.i

CMakeFiles/slam.dir/ZmqUtility.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/slam.dir/ZmqUtility.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /mnt/c/Users/Javi/Desktop/SLAM/SLAM/ZmqUtility.cpp -o CMakeFiles/slam.dir/ZmqUtility.cpp.s

CMakeFiles/slam.dir/ZmqUtility.cpp.o.requires:

.PHONY : CMakeFiles/slam.dir/ZmqUtility.cpp.o.requires

CMakeFiles/slam.dir/ZmqUtility.cpp.o.provides: CMakeFiles/slam.dir/ZmqUtility.cpp.o.requires
	$(MAKE) -f CMakeFiles/slam.dir/build.make CMakeFiles/slam.dir/ZmqUtility.cpp.o.provides.build
.PHONY : CMakeFiles/slam.dir/ZmqUtility.cpp.o.provides

CMakeFiles/slam.dir/ZmqUtility.cpp.o.provides.build: CMakeFiles/slam.dir/ZmqUtility.cpp.o


CMakeFiles/slam.dir/icp.cpp.o: CMakeFiles/slam.dir/flags.make
CMakeFiles/slam.dir/icp.cpp.o: ../icp.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/mnt/c/Users/Javi/Desktop/SLAM/SLAM/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_4) "Building CXX object CMakeFiles/slam.dir/icp.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/slam.dir/icp.cpp.o -c /mnt/c/Users/Javi/Desktop/SLAM/SLAM/icp.cpp

CMakeFiles/slam.dir/icp.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/slam.dir/icp.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /mnt/c/Users/Javi/Desktop/SLAM/SLAM/icp.cpp > CMakeFiles/slam.dir/icp.cpp.i

CMakeFiles/slam.dir/icp.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/slam.dir/icp.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /mnt/c/Users/Javi/Desktop/SLAM/SLAM/icp.cpp -o CMakeFiles/slam.dir/icp.cpp.s

CMakeFiles/slam.dir/icp.cpp.o.requires:

.PHONY : CMakeFiles/slam.dir/icp.cpp.o.requires

CMakeFiles/slam.dir/icp.cpp.o.provides: CMakeFiles/slam.dir/icp.cpp.o.requires
	$(MAKE) -f CMakeFiles/slam.dir/build.make CMakeFiles/slam.dir/icp.cpp.o.provides.build
.PHONY : CMakeFiles/slam.dir/icp.cpp.o.provides

CMakeFiles/slam.dir/icp.cpp.o.provides.build: CMakeFiles/slam.dir/icp.cpp.o


# Object files for target slam
slam_OBJECTS = \
"CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o" \
"CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o" \
"CMakeFiles/slam.dir/ZmqUtility.cpp.o" \
"CMakeFiles/slam.dir/icp.cpp.o"

# External object files for target slam
slam_EXTERNAL_OBJECTS =

slam: CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o
slam: CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o
slam: CMakeFiles/slam.dir/ZmqUtility.cpp.o
slam: CMakeFiles/slam.dir/icp.cpp.o
slam: CMakeFiles/slam.dir/build.make
slam: /usr/lib/x86_64-linux-gnu/libboost_system.so
slam: /usr/lib/x86_64-linux-gnu/libboost_filesystem.so
slam: /usr/lib/x86_64-linux-gnu/libboost_thread.so
slam: /usr/lib/x86_64-linux-gnu/libboost_date_time.so
slam: /usr/lib/x86_64-linux-gnu/libboost_iostreams.so
slam: /usr/lib/x86_64-linux-gnu/libboost_serialization.so
slam: /usr/lib/x86_64-linux-gnu/libboost_chrono.so
slam: /usr/lib/x86_64-linux-gnu/libboost_atomic.so
slam: /usr/lib/x86_64-linux-gnu/libboost_regex.so
slam: /usr/lib/x86_64-linux-gnu/libpthread.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_common.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_octree.so
slam: /usr/lib/libOpenNI.so
slam: /usr/lib/libOpenNI2.so
slam: /usr/lib/x86_64-linux-gnu/libfreetype.so
slam: /usr/lib/x86_64-linux-gnu/libz.so
slam: /usr/lib/x86_64-linux-gnu/libexpat.so
slam: /usr/lib/x86_64-linux-gnu/libpython2.7.so
slam: /usr/lib/libvtkWrappingTools-6.3.a
slam: /usr/lib/x86_64-linux-gnu/libjpeg.so
slam: /usr/lib/x86_64-linux-gnu/libpng.so
slam: /usr/lib/x86_64-linux-gnu/libtiff.so
slam: /usr/lib/x86_64-linux-gnu/libproj.so
slam: /usr/lib/x86_64-linux-gnu/hdf5/openmpi/libhdf5.so
slam: /usr/lib/x86_64-linux-gnu/libsz.so
slam: /usr/lib/x86_64-linux-gnu/libdl.so
slam: /usr/lib/x86_64-linux-gnu/libm.so
slam: /usr/lib/x86_64-linux-gnu/openmpi/lib/libmpi.so
slam: /usr/lib/x86_64-linux-gnu/libnetcdf_c++.so
slam: /usr/lib/x86_64-linux-gnu/libnetcdf.so
slam: /usr/lib/x86_64-linux-gnu/libgl2ps.so
slam: /usr/lib/x86_64-linux-gnu/libtheoraenc.so
slam: /usr/lib/x86_64-linux-gnu/libtheoradec.so
slam: /usr/lib/x86_64-linux-gnu/libogg.so
slam: /usr/lib/x86_64-linux-gnu/libxml2.so
slam: /usr/lib/x86_64-linux-gnu/libjsoncpp.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_io.so
slam: /usr/lib/x86_64-linux-gnu/libflann_cpp_s.a
slam: /usr/lib/x86_64-linux-gnu/libpcl_kdtree.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_search.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_sample_consensus.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_filters.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_features.so
slam: /usr/lib/x86_64-linux-gnu/libboost_system.so
slam: /usr/lib/x86_64-linux-gnu/libboost_filesystem.so
slam: /usr/lib/x86_64-linux-gnu/libboost_thread.so
slam: /usr/lib/x86_64-linux-gnu/libboost_date_time.so
slam: /usr/lib/x86_64-linux-gnu/libboost_iostreams.so
slam: /usr/lib/x86_64-linux-gnu/libboost_serialization.so
slam: /usr/lib/x86_64-linux-gnu/libboost_chrono.so
slam: /usr/lib/x86_64-linux-gnu/libboost_atomic.so
slam: /usr/lib/x86_64-linux-gnu/libboost_regex.so
slam: /usr/lib/x86_64-linux-gnu/libpthread.so
slam: /usr/lib/libOpenNI.so
slam: /usr/lib/libOpenNI2.so
slam: /usr/lib/x86_64-linux-gnu/libflann_cpp_s.a
slam: /usr/lib/x86_64-linux-gnu/libfreetype.so
slam: /usr/lib/x86_64-linux-gnu/libz.so
slam: /usr/lib/x86_64-linux-gnu/libvtkDomainsChemistry-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libexpat.so
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersGeneric-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersHyperTree-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersParallelFlowPaths-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersParallelGeometry-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersParallelImaging-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersParallelMPI-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersParallelStatistics-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersProgrammable-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersPython-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libpython2.7.so
slam: /usr/lib/libvtkWrappingTools-6.3.a
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersReebGraph-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersSMP-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersSelection-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersVerdict-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkverdict-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libjpeg.so
slam: /usr/lib/x86_64-linux-gnu/libpng.so
slam: /usr/lib/x86_64-linux-gnu/libtiff.so
slam: /usr/lib/x86_64-linux-gnu/libvtkGUISupportQtOpenGL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkGUISupportQtSQL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkGUISupportQtWebkit-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkViewsQt-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libproj.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOAMR-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/hdf5/openmpi/libhdf5.so
slam: /usr/lib/x86_64-linux-gnu/libsz.so
slam: /usr/lib/x86_64-linux-gnu/libdl.so
slam: /usr/lib/x86_64-linux-gnu/libm.so
slam: /usr/lib/x86_64-linux-gnu/openmpi/lib/libmpi.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOEnSight-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libnetcdf_c++.so
slam: /usr/lib/x86_64-linux-gnu/libnetcdf.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOExport-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingGL2PS-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingContextOpenGL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libgl2ps.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOFFMPEG-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOMovie-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libtheoraenc.so
slam: /usr/lib/x86_64-linux-gnu/libtheoradec.so
slam: /usr/lib/x86_64-linux-gnu/libogg.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOGDAL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOGeoJSON-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOImport-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOInfovis-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libxml2.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOMINC-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOMPIImage-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOMPIParallel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOParallel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIONetCDF-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libjsoncpp.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOMySQL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOODBC-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOPLY-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOParallelExodus-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOParallelLSDyna-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOParallelNetCDF-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOParallelXML-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOPostgreSQL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOVPIC-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkVPIC-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOVideo-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOXdmf2-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkxdmf2-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingMath-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingMorphological-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingStatistics-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingStencil-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkInteractionImage-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkLocalExample-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkParallelMPI4Py-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingExternal-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingFreeTypeFontConfig-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingImage-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingLOD-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingMatplotlib-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingParallel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingParallelLIC-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingQt-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingVolumeAMR-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingVolumeOpenGL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkTestingGenericBridge-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkTestingIOSQL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkTestingRendering-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkViewsContext2D-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkViewsGeovis-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkWrappingJava-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libpcl_common.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_octree.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_io.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_kdtree.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_search.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_sample_consensus.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_filters.so
slam: /usr/lib/x86_64-linux-gnu/libpcl_features.so
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersFlowPaths-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libtheoraenc.so
slam: /usr/lib/x86_64-linux-gnu/libtheoradec.so
slam: /usr/lib/x86_64-linux-gnu/libogg.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOExodus-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkexoIIc-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libnetcdf_c++.so
slam: /usr/lib/x86_64-linux-gnu/libnetcdf.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOLSDyna-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libxml2.so
slam: /usr/lib/x86_64-linux-gnu/hdf5/openmpi/libhdf5.so
slam: /usr/lib/x86_64-linux-gnu/libsz.so
slam: /usr/lib/x86_64-linux-gnu/libdl.so
slam: /usr/lib/x86_64-linux-gnu/libm.so
slam: /usr/lib/x86_64-linux-gnu/openmpi/lib/libmpi.so
slam: /usr/lib/x86_64-linux-gnu/libvtkWrappingPython27Core-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkPythonInterpreter-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libpython2.7.so
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersParallel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkParallelMPI-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingLIC-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersTexture-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkGUISupportQt-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libQt5Widgets.so.5.9.5
slam: /usr/lib/x86_64-linux-gnu/libQt5Gui.so.5.9.5
slam: /usr/lib/x86_64-linux-gnu/libQt5Core.so.5.9.5
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersAMR-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkParallelCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOLegacy-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingOpenGL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libGLU.so
slam: /usr/lib/x86_64-linux-gnu/libSM.so
slam: /usr/lib/x86_64-linux-gnu/libICE.so
slam: /usr/lib/x86_64-linux-gnu/libX11.so
slam: /usr/lib/x86_64-linux-gnu/libXext.so
slam: /usr/lib/x86_64-linux-gnu/libXt.so
slam: /usr/lib/x86_64-linux-gnu/libvtkIOSQL-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkViewsInfovis-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkChartsCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingContext2D-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersImaging-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingLabel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkGeovisCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOXML-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOGeometry-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOXMLParser-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkInfovisLayout-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkInfovisBoostGraphAlgorithms-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkInfovisCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkViewsCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkInteractionWidgets-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersHybrid-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingGeneral-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingSources-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersModeling-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkInteractionStyle-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingHybrid-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOImage-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkDICOMParser-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkIOCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkmetaio-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libz.so
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingAnnotation-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingFreeType-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkftgl-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libfreetype.so
slam: /usr/lib/x86_64-linux-gnu/libGL.so
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingColor-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingVolume-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkRenderingCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonColor-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersExtraction-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersStatistics-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingFourier-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkImagingCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkalglib-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersGeometry-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersSources-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersGeneral-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkFiltersCore-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonExecutionModel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonComputationalGeometry-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonDataModel-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonMisc-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonTransforms-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonMath-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonSystem-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libvtksys-6.3.so.6.3.0
slam: /usr/lib/x86_64-linux-gnu/libproj.so
slam: /usr/lib/x86_64-linux-gnu/libvtkCommonCore-6.3.so.6.3.0
slam: /usr/local/lib/libzmq.so.5.2.2
slam: CMakeFiles/slam.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/mnt/c/Users/Javi/Desktop/SLAM/SLAM/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_5) "Linking CXX executable slam"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/slam.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/slam.dir/build: slam

.PHONY : CMakeFiles/slam.dir/build

CMakeFiles/slam.dir/requires: CMakeFiles/slam.dir/MIRoZmqPublisher.cpp.o.requires
CMakeFiles/slam.dir/requires: CMakeFiles/slam.dir/MIRoZmqSubscriber.cpp.o.requires
CMakeFiles/slam.dir/requires: CMakeFiles/slam.dir/ZmqUtility.cpp.o.requires
CMakeFiles/slam.dir/requires: CMakeFiles/slam.dir/icp.cpp.o.requires

.PHONY : CMakeFiles/slam.dir/requires

CMakeFiles/slam.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/slam.dir/cmake_clean.cmake
.PHONY : CMakeFiles/slam.dir/clean

CMakeFiles/slam.dir/depend:
	cd /mnt/c/Users/Javi/Desktop/SLAM/SLAM/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /mnt/c/Users/Javi/Desktop/SLAM/SLAM /mnt/c/Users/Javi/Desktop/SLAM/SLAM /mnt/c/Users/Javi/Desktop/SLAM/SLAM/build /mnt/c/Users/Javi/Desktop/SLAM/SLAM/build /mnt/c/Users/Javi/Desktop/SLAM/SLAM/build/CMakeFiles/slam.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/slam.dir/depend
