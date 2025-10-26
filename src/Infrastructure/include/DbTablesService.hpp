#pragma once

#include <QObject>

#include <DbClient.hpp>

namespace Infrastructure
{
    class DbTablesService : public QObject
    {
        Q_OBJECT
    public:
        DbTablesService(DbClient *client, const QString &filePath, QObject *parent = nullptr);

    public slots:
        void createIfNotExist();

    signals:
        void progressUpdated(float progress);
        void creationFinished();
        void creationFailed(const Domain::Error &error);

    private slots:
        void onQueryExecuted(const QList<QSqlRecord> &records, const QString &correlationId);
        void onQueryFailed(const Domain::Error &error, const QString &correlationId);

    private:
        QString readSqlFile(const QString &filePath);

        QString m_filePath;
        DbClient *m_dbClient;

        QHash<QString, int> m_pendingQueries;
    };
}


