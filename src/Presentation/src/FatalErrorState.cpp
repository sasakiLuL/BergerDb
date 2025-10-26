#include <States/FatalErrorState.hpp>

#include <QVBoxLayout>

Presentation::FatalErrorState::FatalErrorState(AppStateController *appStateController, QWidget *parent)
    : QWidget{parent}, m_appStateController(appStateController), m_error("", "")
{
    auto centralLayout = new QVBoxLayout(this);
    setLayout(centralLayout);

    centralLayout->setAlignment(Qt::AlignCenter);

    m_label = new QLabel("Fatal Error", this);
    m_errorLabel = new QLabel(this);

    centralLayout->addWidget(m_label);
    centralLayout->addWidget(m_errorLabel);
}

void Presentation::FatalErrorState::setError(const Domain::Error &error)
{
    m_error = error;
    m_errorLabel->setText("Code" + m_error.code() + ". Message: " + m_error.message());
}
