#include <DbClient.hpp>

#include <QThread>
#include <QUuid>

namespace Infrastructure
{

DbClient::DbClient(const QString &connString, QObject *parent)
    : m_worker(new DbWorker(connString)), m_thread(new QThread(this))
{
    m_worker->moveToThread(m_thread);

    connect(m_thread, &QThread::finished, m_worker, &QObject::deleteLater, Qt::ConnectionType::QueuedConnection);
    connect(m_worker, &DbWorker::queryExecuted, this, &DbClient::queryExecuted);
    connect(m_worker, &DbWorker::queryFailed, this, &DbClient::queryFailed);

    m_thread->start();
}

DbClient::~DbClient()
{
    m_thread->quit();
    m_thread->wait();
}

QString DbClient::generateCorrelationId()
{
    return QUuid::createUuid().toString(QUuid::WithoutBraces);
}

void DbClient::executeQuery(const QString &sql, const QVariantList &params, const QString &correlationId)
{
    QMetaObject::invokeMethod(
        m_worker, "executeQuery", Qt::QueuedConnection, Q_ARG(QString, sql), Q_ARG(QVariantList, params), Q_ARG(QString, correlationId));
}

}
