cmake_minimum_required(VERSION 3.16)

project(Presentation LANGUAGES CXX)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 REQUIRED COMPONENTS Widgets)
find_package(Qt6 REQUIRED COMPONENTS Core)

add_library(Ui STATIC
    src/MainWindow.cpp include/Views/MainWindow.h
    include/StateController.hpp src/StateController.cpp
)

target_include_directories(Ui
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/include
    PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/src)

target_link_libraries(Ui PRIVATE 
    Qt6::Widgets
    Domain)
target_link_libraries(Ui PRIVATE Qt6::Core)
