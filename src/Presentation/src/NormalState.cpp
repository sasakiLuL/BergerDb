#include <States/NormalState.hpp>

#include <QVBoxLayout>

Presentation::NormalState::NormalState(AppStateController *appStateController, PageController *pageController, QWidget *parent)
    : QWidget{parent}, m_appStateController(appStateController), m_pageController(pageController)
{
    auto centralLayout = new QVBoxLayout(this);
    setLayout(centralLayout);

    centralLayout->setAlignment(Qt::AlignCenter);

    centralLayout->addWidget(pageController->stackedWidget());

    pageController->setCurrentPage(Page::Customers);
}
