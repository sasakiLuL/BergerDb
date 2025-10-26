#include <DbTablesService.hpp>

#include <QFile>

Infrastructure::DbTablesService::DbTablesService(DbClient *client, const QString &filePath, QObject *parent) :
    QObject(parent), m_dbClient(client), m_filePath(filePath)
{
    connect(m_dbClient, &DbClient::queryExecuted, this, &DbTablesService::onQueryExecuted);
    connect(m_dbClient, &DbClient::queryFailed, this, &DbTablesService::onQueryFailed);
}

void Infrastructure::DbTablesService::createIfNotExist()
{
    QString script = readSqlFile(m_filePath);

    if (script.isEmpty())
    {
        return;
    }

    auto statements = script.split(';', Qt::SkipEmptyParts);

    int progressStep = 100 / statements.count();

    for (int i = 0; i < statements.count(); i++)
    {
        QString trimmed = statements[i].trimmed();

        if (trimmed.isEmpty())
            continue;

        auto correlationId = DbClient::generateCorrelationId();
        m_dbClient->executeQuery(trimmed, {}, correlationId);
        m_pendingQueries[correlationId] = progressStep * (i + 1);
    }
}

void Infrastructure::DbTablesService::onQueryExecuted(const QList<QSqlRecord> &records, const QString &correlationId)
{
    auto progressIterator = m_pendingQueries.find(correlationId);

    if (progressIterator == m_pendingQueries.end())
    {
        qDebug() << "Operation with the given correlation id was not found";
        return;
    }

    emit progressUpdated(*progressIterator);

    m_pendingQueries.remove(correlationId);

    if (m_pendingQueries.isEmpty())
    {
        emit creationFinished();
    }
}

void Infrastructure::DbTablesService::onQueryFailed(const Domain::Error &error, const QString &correlationId)
{
    if (!m_pendingQueries.remove(correlationId))
    {
        qDebug() << "Operation with the given correlation id was not found";
        return;
    }

    emit creationFailed(error);
}

QString Infrastructure::DbTablesService::readSqlFile(const QString &filePath)
{
    QFile file(filePath);

    if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
    {
        qWarning() << "Failed to open SQL file:" << file.errorString();
        return {};
    }
    QTextStream in(&file);
    return in.readAll();
}
