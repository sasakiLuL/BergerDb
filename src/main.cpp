#include <QApplication>
#include <QFont>

#include <DbClient.hpp>
#include <DbTablesService.hpp>
#include <AppStateController.hpp>
#include <AppWindow.hpp>
#include <States/FatalErrorState.hpp>
#include <States/InitializationState.hpp>
#include <States/NormalState.hpp>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    QCoreApplication::setOrganizationName("BergerDb");
    QCoreApplication::setApplicationName("BergerDb");

    QFont globalFont("Arial", 12);
    globalFont.setBold(true);
    app.setFont(globalFont);

    auto dbClient = Infrastructure::DbClient{"berger.db", &app};
    auto dbTablesService = Infrastructure::DbTablesService{&dbClient, ":/resources/sql/main_tables.sql", &app};

    auto appStateController = Presentation::AppStateController{&app};

    auto appWindow = Presentation::AppWindow{&appStateController};

    auto fatalErrorState = Presentation::FatalErrorState{&appStateController, &appWindow};
    auto initializingState = Presentation::InitializationState{&appStateController, &fatalErrorState, &dbTablesService, &appWindow};
    auto normalState = Presentation::NormalState{&appStateController, &appWindow};

    appStateController.registerState(Presentation::AppState::Initialization, &initializingState);
    appStateController.registerState(Presentation::AppState::Normal, &normalState);
    appStateController.registerState(Presentation::AppState::FatalError, &fatalErrorState);

    appStateController.showState(Presentation::AppState::Initialization);

    appWindow.show();
    return app.exec();
}
