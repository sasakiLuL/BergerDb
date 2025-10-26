cmake_minimum_required(VERSION 3.16)

project(Domain LANGUAGES CXX)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 REQUIRED COMPONENTS Core)

add_library(Domain STATIC
    include/Core/Error.h src/Error.cpp

    include/ValueObjects/Address.h src/Address.cpp
    include/ValueObjects/Email.h src/Email.cpp
    include/ValueObjects/Name.h src/Name.cpp
    include/ValueObjects/Notation.h src/Notation.cpp
    include/ValueObjects/Prefix.h src/Prefix.cpp
    include/ValueObjects/ZipCode.h src/ZipCode.cpp

    include/Customer/CustomerModel.h src/CustomerModel.cpp
    include/Customer/CustomerAggregate.h src/CustomerAggregate.cpp)

target_include_directories(Domain 
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/include
    PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/src)

target_link_libraries(Domain PRIVATE Qt6::Core)
