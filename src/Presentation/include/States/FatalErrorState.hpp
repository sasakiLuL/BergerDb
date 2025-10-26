#pragma once

#include <QWidget>
#include <QLabel>

#include <AppStateController.hpp>
#include <Core/Error.h>

namespace Presentation
{
    class FatalErrorState : public QWidget
    {
        Q_OBJECT
    public:
        FatalErrorState(AppStateController *appStateController, QWidget *parent = nullptr);

    public slots:
        void setError(const Domain::Error &error);

    private:
        AppStateController *m_appStateController;
        Domain::Error m_error;

        QLabel *m_label;
        QLabel *m_errorLabel;
    };
}
