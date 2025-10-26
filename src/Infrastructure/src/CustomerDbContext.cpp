#include <Features/Customer/CustomerDbContext.hpp>

#include <QTimeZone>

const QString Infrastructure::CustomerDbContext::selectSql = R"(
    SELECT * FROM customers WHERE id = ?
)";

const QString Infrastructure::CustomerDbContext::selectAllSql = R"(
    SELECT * FROM customers
)";

const QString Infrastructure::CustomerDbContext::insertSql = R"(
    INSERT INTO customers(
        personalId, prefix, firstName, lastName, sex, email,
        registeredOn, terminatedOn, notation, street, city, zipCode,
        paymentType, memberType, entryType, subscriptionCost, institution
    )
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    RETURNING id
)";

const QString Infrastructure::CustomerDbContext::deleteSql = R"(
    DELETE FROM customers WHERE id = ?
)";

const QString Infrastructure::CustomerDbContext::updateSql = R"(
    UPDATE customers SET
        personalId = ?, prefix = ?, firstName = ?, lastName = ?, sex = ?, email = ?,
        registeredOn = ?, terminatedOn = ?, notation = ?, street = ?, city = ?, zipCode = ?,
        paymentType = ?, memberType = ?, entryType = ?, subscriptionCost = ?, institution = ?
    WHERE id = ?
)";

Infrastructure::CustomerDbContext::CustomerDbContext(DbClient *dbClient, QObject *parent) :
    QObject(parent), m_dbClient(dbClient)
{
    connect(m_dbClient, &DbClient::queryFailed, this, &CustomerDbContext::onQueryFailed);
    connect(m_dbClient, &DbClient::queryExecuted, this, &CustomerDbContext::onQueryExecuted);
}

const QVector<Domain::CustomerAggregate*> &Infrastructure::CustomerDbContext::customersReplica() const
{
    return m_replica;
}

void Infrastructure::CustomerDbContext::get()
{
    QString correlationId = DbClient::generateCorrelationId();
    m_pendingOperation[correlationId] = OperationContext{Operation::Get, -1};
    m_dbClient->executeQuery(selectAllSql, {}, correlationId);
}

void Infrastructure::CustomerDbContext::add(const Domain::CustomerModel &customer)
{
    QString correlationId = DbClient::generateCorrelationId();
    m_pendingOperation[correlationId] = OperationContext{Operation::Add, -2};

    m_dbClient->executeQuery(
        insertSql,
        {
            customer.personalId(),
            customer.prefix().value(),
            customer.firstName().value(),
            customer.lastName().value(),
            static_cast<int>(customer.sex()),
            customer.email().value(),
            customer.registeredOn().toUTC().toMSecsSinceEpoch(),
            customer.terminatedOn().has_value() ? customer.terminatedOn().value() : QVariant(),
            customer.notation().value(),
            customer.street().value(),
            customer.city().value(),
            customer.zipCode().value(),
            static_cast<int>(customer.paymentType()),
            static_cast<int>(customer.memberType()),
            static_cast<int>(customer.entryType()),
            customer.subscriptionCost(),
            customer.institution().value()
        },
        correlationId);
}

void Infrastructure::CustomerDbContext::remove(Domain::CustomerAggregate* customer)
{
    QString correlationId = DbClient::generateCorrelationId();
    int id = customer->model().id();
    m_pendingOperation[correlationId] = OperationContext{Operation::Remove, id};
    m_dbClient->executeQuery(
        deleteSql,
        {
            id
        },
        correlationId);
}

void Infrastructure::CustomerDbContext::onCustomerUpdated(Domain::CustomerAggregate* customer)
{
    QString correlationId = DbClient::generateCorrelationId();
    Domain::CustomerModel model = customer->model();
    m_pendingOperation[correlationId] = OperationContext{Operation::Update, model.id()};
    m_dbClient->executeQuery(
        updateSql,
        {
            model.personalId(),
            model.prefix().value(),
            model.firstName().value(),
            model.lastName().value(),
            static_cast<int>(model.sex()),
            model.email().value(),
            model.registeredOn().toUTC().toMSecsSinceEpoch(),
            model.terminatedOn().has_value() ? model.terminatedOn().value() : QVariant(),
            model.notation().value(),
            model.street().value(),
            model.city().value(),
            model.zipCode().value(),
            static_cast<int>(model.paymentType()),
            static_cast<int>(model.memberType()),
            static_cast<int>(model.entryType()),
            model.subscriptionCost(),
            model.institution().value()
        },
        correlationId);
}

void Infrastructure::CustomerDbContext::onQueryExecuted(const QList<QSqlRecord> &records, const QString &correlationId)
{
    auto contextIterator = m_pendingOperation.find(correlationId);

    if (contextIterator == m_pendingOperation.end())
    {
        qDebug() << "Operation with the given correlation id was not found";
        return;
    }

    auto context = *contextIterator;

    switch (context.operation)
    {
    case Operation::Add:
        addCustomerOperationHandler(records.first());
        break;
    case Operation::Remove:
        removeCustomerOperationHandler(context.affectedCustomerId);
        break;
    case Operation::Update:
        updateCustomerOperationHandler(context.affectedCustomerId);
        break;
    case Operation::Get:
        getOperationHandler(records);
        break;
    case Operation::UpdateInReplica:
        updatedInReplicaOperationHandler(records.first());
        break;
    case Operation::AddToReplica:
        addedToReplicaOperationHandler(records.first());
        break;
    default:
        qDebug() << "Undefinded operation";
        break;
    }
}

void Infrastructure::CustomerDbContext::onQueryFailed(const Domain::Error &error, const QString &correlationId)
{
    if (!m_pendingOperation.remove(correlationId))
    {
        qDebug() << "Operation with the given correlation id was not found";
        return;
    }

    emit operationFailed(error);
}

Domain::CustomerAggregate* Infrastructure::CustomerDbContext::fromRecord(const QSqlRecord &record)
{
    int id = record.value("id").toInt();
    int personalId = record.value("personalId").toInt();
    Domain::Prefix prefix(record.value("prefix").toString());
    Domain::Name firstName(record.value("firstName").toString());
    Domain::Name lastName(record.value("lastName").toString());
    Domain::Sex sex = static_cast<Domain::Sex>(record.value("sex").toInt());
    Domain::Email email(record.value("email").toString());
    QDateTime registeredOn = QDateTime::fromMSecsSinceEpoch(record.value("registeredOn").toLongLong(), QTimeZone::UTC);

    std::optional<QDateTime> terminatedOn;
    if (!record.value("terminatedOn").isNull()) {
        terminatedOn = QDateTime::fromMSecsSinceEpoch(record.value("terminatedOn").toLongLong(), QTimeZone::UTC);
    }

    Domain::Notation notation(record.value("notation").toString());
    Domain::Address street(record.value("street").toString());
    Domain::Address city(record.value("city").toString());
    Domain::ZipCode zipCode(record.value("zipCode").toString());
    Domain::PaymentType paymentType = static_cast<Domain::PaymentType>(record.value("paymentType").toInt());
    Domain::MemberType memberType = static_cast<Domain::MemberType>(record.value("memberType").toInt());
    Domain::EntryType entryType = static_cast<Domain::EntryType>(record.value("entryType").toInt());
    double subscriptionCost = record.value("subscriptionCost").toDouble();
    Domain::Name institution(record.value("institution").toString());

    auto model = Domain::CustomerModel(
        id,
        personalId,
        prefix,
        firstName,
        lastName,
        sex,
        email,
        registeredOn,
        terminatedOn,
        notation,
        street,
        city,
        zipCode,
        paymentType,
        memberType,
        entryType,
        subscriptionCost,
        institution
        );

    auto aggregate = new Domain::CustomerAggregate(std::move(model), this);

    const auto connectField = [this, &aggregate](auto signal){
        QObject::connect(aggregate, signal, this, [this, aggregate](){
            onCustomerUpdated(aggregate);
        });
    };

    connectField(&Domain::CustomerAggregate::personalIdChanged);
    connectField(&Domain::CustomerAggregate::prefixChanged);
    connectField(&Domain::CustomerAggregate::firstNameChanged);
    connectField(&Domain::CustomerAggregate::lastNameChanged);
    connectField(&Domain::CustomerAggregate::sexChanged);
    connectField(&Domain::CustomerAggregate::emailChanged);
    connectField(&Domain::CustomerAggregate::registeredOnChanged);
    connectField(&Domain::CustomerAggregate::terminatedOnChanged);
    connectField(&Domain::CustomerAggregate::notationChanged);
    connectField(&Domain::CustomerAggregate::streetChanged);
    connectField(&Domain::CustomerAggregate::cityChanged);
    connectField(&Domain::CustomerAggregate::zipCodeChanged);
    connectField(&Domain::CustomerAggregate::paymentTypeChanged);
    connectField(&Domain::CustomerAggregate::memberTypeChanged);
    connectField(&Domain::CustomerAggregate::entryTypeChanged);
    connectField(&Domain::CustomerAggregate::subscriptionCostChanged);
    connectField(&Domain::CustomerAggregate::institutionChanged);

    return aggregate;
}

void Infrastructure::CustomerDbContext::addCustomerOperationHandler(const QSqlRecord &record)
{
    int id = record.value("id").toInt();

    emit customerAdded(id);

    QString correlationId = DbClient::generateCorrelationId();
    m_pendingOperation[correlationId] = OperationContext{Operation::AddToReplica, id};
    m_dbClient->executeQuery(
        selectSql,
        {
            id
        },
        correlationId);
}

void Infrastructure::CustomerDbContext::removeCustomerOperationHandler(int id)
{
    emit customerRemoved(id);

    auto recordIterator = std::find_if(m_replica.begin(), m_replica.end(), [id](const auto &agg){
        return agg->model().id() == id;
    });

    int index = std::distance(m_replica.begin(), recordIterator);

    m_replica.erase(recordIterator);

    emit replicaUpdated(index, EntityState::Removed);
}

void Infrastructure::CustomerDbContext::updateCustomerOperationHandler(int id)
{
    emit customerUpdated(id);

    QString correlationId = DbClient::generateCorrelationId();
    m_pendingOperation[correlationId] = OperationContext{Operation::UpdateInReplica, id};
    m_dbClient->executeQuery(
        selectSql,
        {
            id
        },
        correlationId);
}

void Infrastructure::CustomerDbContext::getOperationHandler(const QList<QSqlRecord> &records)
{
    m_replica.clear();
    m_replica.reserve(records.size());

    std::transform(
        records.begin(),
        records.end(),
        std::back_inserter(m_replica),
        [this](const QSqlRecord &record) { return fromRecord(record); });

    emit replicaUpdated(-1, EntityState::Default);
}

void Infrastructure::CustomerDbContext::updatedInReplicaOperationHandler(const QSqlRecord &record)
{
    int id = record.value("id").toInt();

    auto recordIterator = std::find_if(m_replica.begin(), m_replica.end(), [id](const auto &agg){
        return agg->model().id() == id;
    });

    if (recordIterator == m_replica.end())
    {
        qDebug() << "Replica is corrupted. No record with specified id was found in replica";
        return;
    }

    *recordIterator = fromRecord(record);

    emit replicaUpdated(std::distance(m_replica.begin(), recordIterator), EntityState::Updated);
}

void Infrastructure::CustomerDbContext::addedToReplicaOperationHandler(const QSqlRecord &record)
{
    m_replica.push_back(fromRecord(record));

    emit replicaUpdated(m_replica.size() - 1, EntityState::Added);
}
