#pragma once

#include <QWidget>
#include <QLabel>

#include <PageController.hpp>
#include <AppStateController.hpp>

namespace Presentation
{
    class NormalState : public QWidget
    {
        Q_OBJECT
    public:
        NormalState(AppStateController *appStateController, PageController *pageController, QWidget *parent = nullptr);

    private:
        AppStateController *m_appStateController;
        PageController *m_pageController;
    };
}


