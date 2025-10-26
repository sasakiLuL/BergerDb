#pragma once

#include <QObject>
#include <Customer/CustomerAggregate.h>
#include <DbClient.hpp>

namespace Infrastructure
{
    enum EntityState
    {
        Default,
        Updated,
        Added,
        Removed
    };

    class CustomerDbContext : public QObject
    {
        enum Operation
        {
            AddToReplica,
            UpdateInReplica,

            Get,
            Add,
            Remove,
            Update
        };

        struct OperationContext
        {
            Operation operation;
            int affectedCustomerId;
        };

        const static QString selectSql;
        const static QString selectAllSql;
        const static QString insertSql;
        const static QString deleteSql;
        const static QString updateSql;

        Q_OBJECT
    public:
        CustomerDbContext(DbClient *dbClient, QObject *parent = nullptr);

        const QVector<Domain::CustomerAggregate*> &customersReplica() const;
        void get();
        void add(const Domain::CustomerModel &customer);
        void remove(Domain::CustomerAggregate* customer);

    signals:
        void customerUpdated(int id);
        void customerAdded(int id);
        void customerRemoved(int id);

        void replicaUpdated(int index, EntityState state);
        void operationFailed(const Domain::Error &error);

    private slots:
        void onCustomerUpdated(Domain::CustomerAggregate* customer);

        void onQueryExecuted(const QList<QSqlRecord> &records, const QString &correlationId);
        void onQueryFailed(const Domain::Error &error, const QString &correlationId);

    private:
        Domain::CustomerAggregate* fromRecord(const QSqlRecord &record);

        void addCustomerOperationHandler(const QSqlRecord &record);
        void removeCustomerOperationHandler(int id);
        void updateCustomerOperationHandler(int id);
        void getOperationHandler(const QList<QSqlRecord> &records);

        void updatedInReplicaOperationHandler(const QSqlRecord &record);
        void addedToReplicaOperationHandler(const QSqlRecord &record);

        QVector<Domain::CustomerAggregate*> m_replica;
        QHash<QString, OperationContext> m_pendingOperation;
        DbClient *m_dbClient;

    };

}
