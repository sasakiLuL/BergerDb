#pragma once

#include <QObject>
#include <QSqlDatabase>
#include <QSqlRecord>
#include <QSqlError>
#include <Core/Error.h>

namespace Infrastructure
{
    class DbWorker : public QObject
    {
        Q_OBJECT
    public:
        explicit DbWorker(const QString &connString);

    public slots:
        void executeQuery(const QString &sql, const QVariantList &params, const QString &correlationId);

    signals:
        void queryExecuted(const QList<QSqlRecord> &records, const QString &correlationId);
        void queryFailed(const Domain::Error &error, const QString &correlationId);

    private:
        QString m_connectionString;
    };
}
