#include <States/InitializationState.hpp>

#include <QVBoxLayout>

Presentation::InitializationState::InitializationState(
    AppStateController *appStateController,
    FatalErrorState *fatalErrorState,
    Infrastructure::DbTablesService *tablesService,
    QWidget *parent) :
        QWidget{parent},
        m_appStateController(appStateController),
        m_tableService(tablesService),
        m_fatalErrorState(fatalErrorState)
{
    auto centralLayout = new QVBoxLayout(this);
    setLayout(centralLayout);

    centralLayout->setAlignment(Qt::AlignCenter);

    m_label = new QLabel("Initializing", this);
    m_progressBar = new QProgressBar(this);
    m_progressBar->setRange(0, 100);

    centralLayout->addWidget(m_label);
    centralLayout->addWidget(m_progressBar);

    connect(m_tableService, &Infrastructure::DbTablesService::progressUpdated, m_progressBar, &QProgressBar::setValue);

    connect(m_tableService, &Infrastructure::DbTablesService::creationFinished, this, [this](){
        m_appStateController->showState(AppState::Normal);
    });
    connect(m_tableService, &Infrastructure::DbTablesService::creationFailed, this, [this](const Domain::Error &error){
        m_fatalErrorState->setError(error);
        m_appStateController->showState(AppState::Normal);
    });

    m_tableService->createIfNotExist();
}
