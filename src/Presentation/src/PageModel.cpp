#include <PageModel.hpp>

Presentation::PageModel::PageModel(PageController *pageController, QObject *parent)
    : QObject{parent}, m_pageController(pageController)
{}
