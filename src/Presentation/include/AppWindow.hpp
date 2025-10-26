#pragma once

#include <AppStateController.hpp>
#include <QMainWindow>

namespace Presentation
{
    class AppWindow : public QMainWindow
    {
        Q_OBJECT

    public:
        AppWindow(AppStateController* appStateController, QWidget *parent = nullptr);
        ~AppWindow();

    private:
        AppStateController *m_appStateController;
    };
}
