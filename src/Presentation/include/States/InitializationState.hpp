#pragma once

#include <QLabel>
#include <QProgressBar>
#include <QWidget>

#include <DbTablesService.hpp>
#include <AppStateController.hpp>
#include <States/FatalErrorState.hpp>

namespace Presentation
{
    class InitializationState : public QWidget
    {
        Q_OBJECT
    public:
        InitializationState(AppStateController *appStateController, FatalErrorState *fatalErrorState, Infrastructure::DbTablesService *tablesService, QWidget *parent = nullptr);

    private:
        AppStateController *m_appStateController;
        FatalErrorState *m_fatalErrorState;
        Infrastructure::DbTablesService *m_tableService;

        QLabel *m_label;
        QProgressBar *m_progressBar;
    };
}
