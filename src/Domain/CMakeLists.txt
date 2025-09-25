cmake_minimum_required(VERSION 3.16)

project(Domain LANGUAGES CXX)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 REQUIRED COMPONENTS Core)

add_library(Domain STATIC
    src/Address.cpp
    src/Email.cpp
    src/Name.cpp
    src/Notation.cpp
    src/Prefix.cpp
    src/ZipCode.cpp
)

target_include_directories(Domain 
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/include
    PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/src)

target_link_libraries(Domain PRIVATE Qt6::Core)