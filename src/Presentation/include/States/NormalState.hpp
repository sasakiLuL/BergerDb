#pragma once

#include <QWidget>
#include <QLabel>

#include <AppStateController.hpp>

namespace Presentation
{
    class NormalState : public QWidget
    {
        Q_OBJECT
    public:
        NormalState(AppStateController *appStateController, QWidget *parent = nullptr);

    private:
        AppStateController *m_appStateController;

        QLabel *m_label;
    };
}


