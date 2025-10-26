#pragma once

#include <QStackedWidget>

namespace Presentation
{
    enum class AppState
    {
        Initialization,
        Normal,
        FatalError
    };

    class AppStateController : public QObject
    {
        Q_OBJECT
    public:
        AppStateController(QObject *parent = nullptr);
        ~AppStateController();

        void registerState(AppState type, QWidget *widget);
        void showState(AppState type);

        QStackedWidget *statesWidget();

    signals:
        void stateChanged(AppState current);

    private:
        QStackedWidget *m_statesWidget;
        QHash<AppState, int> m_states;
    };
}


