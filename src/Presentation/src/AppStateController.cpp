#include <AppStateController.hpp>

Presentation::AppStateController::AppStateController(QObject *parent)
    : QObject{parent}
{
    m_statesWidget = new QStackedWidget();
}

Presentation::AppStateController::~AppStateController()
{
    delete m_statesWidget;
}

void Presentation::AppStateController::registerState(AppState type, QWidget *widget)
{
    m_states[type] = m_statesWidget->addWidget(widget);
}

void Presentation::AppStateController::showState(AppState type)
{
    m_statesWidget->setCurrentIndex(m_states[type]);
    emit stateChanged(type);
}

QStackedWidget *Presentation::AppStateController::statesWidget()
{
    return m_statesWidget;
}
