cmake_minimum_required(VERSION 3.16)

project(Infrastructure LANGUAGES CXX)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt${QT_VERSION_MAJOR} REQUIRED COMPONENTS
    Core
    Sql)

find_package(Qt6 REQUIRED COMPONENTS Core)

add_library(Infrastructure STATIC
    include/DbClient.hpp src/DbClient.cpp
    include/DbWorker.hpp src/DbWorker.cpp

    include/Customer/CustomerDbContext.hpp src/CustomerDbContext.cpp
    include/DbTablesService.hpp include/DbTablesService.cpp
)

target_include_directories(Infrastructure 
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/include
    PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/src)

target_link_libraries(Infrastructure PRIVATE
    Qt${QT_VERSION_MAJOR}::Core
    Qt${QT_VERSION_MAJOR}::Sql
    Domain)
target_link_libraries(Infrastructure PRIVATE Qt6::Core)
