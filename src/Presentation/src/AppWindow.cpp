#include <AppWindow.hpp>

#include <QHBoxLayout>

Presentation::AppWindow::AppWindow(AppStateController* appStateController, QWidget *parent)
    : QMainWindow(parent), m_appStateController(appStateController)
{
    resize(1600, 900);
    setMinimumSize(800, 600);

    QWidget *central = new QWidget(this);
    auto *centralLayout = new QHBoxLayout(central);
    centralLayout->setAlignment(Qt::AlignTop);
    setCentralWidget(central);

    auto *stateWidget = appStateController->statesWidget();

    centralLayout->addWidget(stateWidget);
}

Presentation::AppWindow::~AppWindow() {}
