#include <States/NormalState.hpp>

#include <QVBoxLayout>

Presentation::NormalState::NormalState(AppStateController *appStateController, QWidget *parent)
    : QWidget{parent}, m_appStateController(appStateController)
{
    auto centralLayout = new QVBoxLayout(this);
    setLayout(centralLayout);

    centralLayout->setAlignment(Qt::AlignCenter);

    m_label = new QLabel("Normal state", this);

    centralLayout->addWidget(m_label);
}
