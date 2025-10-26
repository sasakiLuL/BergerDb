#pragma once

#include <QObject>
#include <DbWorker.hpp>

namespace Infrastructure
{
    class DbClient : public QObject
    {
        Q_OBJECT

    public:
        explicit DbClient(const QString &connString, QObject *parent = nullptr);
        ~DbClient();

        static QString generateCorrelationId();

    public slots:
        void executeQuery(const QString &sql, const QVariantList &params, const QString &correlationId);

    signals:
        void queryExecuted(const QList<QSqlRecord> &records, const QString &correlationId);
        void queryFailed(const Domain::Error &error, const QString &correlationId);

    private:
        QThread *m_thread;
        DbWorker *m_worker;
    };
}
