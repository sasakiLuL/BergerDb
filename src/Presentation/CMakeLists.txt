cmake_minimum_required(VERSION 3.16)

project(Presentation LANGUAGES CXX)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt${QT_VERSION_MAJOR} REQUIRED COMPONENTS
    Widgets
    Sql)

find_package(Qt6 REQUIRED COMPONENTS Core)
find_package(Qt6 REQUIRED COMPONENTS Core)

add_library(Presentation STATIC
    include/AppWindow.hpp src/AppWindow.cpp
    include/AppStateController.hpp src/AppStateController.cpp

    include/States/InitializationState.hpp src/InitializationState.cpp
    include/States/NormalState.hpp src/NormalState.cpp
    include/States/FatalErrorState.hpp src/FatalErrorState.cpp
    include/PageController.hpp src/PageController.cpp
    include/PageModel.hpp src/PageModel.cpp
)

target_include_directories(Presentation
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/include
    PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/src)

target_link_libraries(Presentation PRIVATE
    Qt${QT_VERSION_MAJOR}::Widgets
    Qt${QT_VERSION_MAJOR}::Sql
    Domain
    Infrastructure)
target_link_libraries(Presentation PRIVATE Qt6::Core)
target_link_libraries(Presentation PRIVATE Qt6::Core)
