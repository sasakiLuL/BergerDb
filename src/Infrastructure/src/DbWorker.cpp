#include <DbWorker.hpp>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QThread>

namespace Infrastructure
{

DbWorker::DbWorker(const QString &connString)
    : m_connectionString(connString), QObject(nullptr) {}

void DbWorker::executeQuery(const QString &sql, const QVariantList &params, const QString &correlationId)
{
    QThread::msleep(3000);

    QString connectionName = QString("SQLite_%1").arg(reinterpret_cast<quintptr>(QThread::currentThreadId()));

    {
        QSqlDatabase db = QSqlDatabase::addDatabase("QSQLITE", connectionName);
        db.setDatabaseName(m_connectionString);

        if (!db.open())
        {
            emit queryFailed(Domain::Error("Dal.DbClient", "Initialization failed."), correlationId);
            return;
        }

        {
            QSqlQuery query = QSqlQuery{db};
            query.prepare(sql);

            for (int i = 0; i < params.size(); ++i)
            {
                query.bindValue(i, params[i]);
            }

            if (!query.exec())
            {
                emit queryFailed(Domain::Error("Dal.DbClient", "Query execution failed."), correlationId);
                return;
            }

            QList<QSqlRecord> records;

            while (query.next())
            {
                records.append(query.record());
            }

            emit queryExecuted(records, correlationId);
        }

        db.close();
    }

    QSqlDatabase::removeDatabase(connectionName);
}

}

