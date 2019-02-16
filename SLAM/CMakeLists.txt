cmake_minimum_required(VERSION 2.6 FATAL_ERROR)
set (CMAKE_CXX_STANDARD 14)

project(slam)

find_package(PCL 1.3 REQUIRED COMPONENTS common io features)

find_package(Boost REQUIRED COMPONENTS system)

find_package(cppzmq)

find_package(VTK REQUIRED)
include(${VTK_USE_FILE})

include_directories(${PCL_INCLUDE_DIRS})
link_directories(${PCL_LIBRARY_DIRS})
add_definitions(${PCL_DEFINITIONS})

file(GLOB source_files
        "*.h"
        "*.cpp"
        "*.hpp"
        )

add_executable(slam ${source_files})
target_link_libraries(slam ${PCL_LIBRARIES} cppzmq)
#list(REMOVE_ITEM PCL_ALL_REQUIRED_LIBRARIES "debug")
