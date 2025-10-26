#include <Customer/CustomerAggregate.h>

Domain::CustomerAggregate::CustomerAggregate(CustomerModel model, QObject *parent) :
    QObject(parent), m_model(model) {}

Domain::CustomerModel Domain::CustomerAggregate::model() const
{
    return m_model;
}

void Domain::CustomerAggregate::updatePersonalId(int value)
{
    if (m_model.personalId() != value)
    {
        m_model.m_personalId = value;
        emit personalIdChanged(value);
    }
}

void Domain::CustomerAggregate::updatePrefix(const Domain::Prefix &value)
{
    if (m_model.prefix().value() != value.value())
    {
        m_model.m_prefix = value;
        emit prefixChanged(value);
    }
}

void Domain::CustomerAggregate::updateFirstName(const Domain::Name &value)
{
    if (m_model.firstName().value() != value.value())
    {
        m_model.m_firstName = value;
        emit firstNameChanged(value);
    }
}

void Domain::CustomerAggregate::updateLastName(const Domain::Name &value)
{
    if (m_model.lastName().value() != value.value())
    {
        m_model.m_lastName = value;
        emit lastNameChanged(value);
    }
}

void Domain::CustomerAggregate::updateSex(Domain::Sex value)
{
    if (m_model.sex() != value)
    {
        m_model.m_sex = value;
        emit sexChanged(value);
    }
}

void Domain::CustomerAggregate::updateEmail(const Domain::Email &value)
{
    if (m_model.email().value() != value.value())
    {
        m_model.m_email = value;
        emit emailChanged(value);
    }
}

void Domain::CustomerAggregate::updateRegisteredOn(const QDateTime &value)
{
    if (m_model.registeredOn() != value)
    {
        m_model.m_registeredOn = value;
        emit registeredOnChanged(value);
    }
}

void Domain::CustomerAggregate::updateTerminatedOn(const std::optional<QDateTime> &value)
{
    if (m_model.terminatedOn() != value)
    {
        m_model.m_terminatedOn = value;
        emit terminatedOnChanged(value);
    }
}

void Domain::CustomerAggregate::updateNotation(const Domain::Notation &value)
{
    if (m_model.notation().value() != value.value())
    {
        m_model.m_notation = value;
        emit notationChanged(value);
    }
}

void Domain::CustomerAggregate::updateStreet(const Domain::Address &value)
{
    if (m_model.street().value() != value.value())
    {
        m_model.m_street = value;
        emit streetChanged(value);
    }
}

void Domain::CustomerAggregate::updateCity(const Domain::Address &value)
{
    if (m_model.city().value() != value.value())
    {
        m_model.m_city = value;
        emit cityChanged(value);
    }
}

void Domain::CustomerAggregate::updateZipCode(const Domain::ZipCode &value)
{
    if (m_model.zipCode().value() != value.value())
    {
        m_model.m_zipCode = value;
        emit zipCodeChanged(value);
    }
}

void Domain::CustomerAggregate::updatePaymentType(Domain::PaymentType value)
{
    if (m_model.paymentType() != value)
    {
        m_model.m_paymentType = value;
        emit paymentTypeChanged(value);
    }
}

void Domain::CustomerAggregate::updateMemberType(Domain::MemberType value)
{
    if (m_model.memberType() != value)
    {
        m_model.m_memberType = value;
        emit memberTypeChanged(value);
    }
}

void Domain::CustomerAggregate::updateEntryType(Domain::EntryType value)
{
    if (m_model.entryType() != value)
    {
        m_model.m_entryType = value;
        emit entryTypeChanged(value);
    }
}

void Domain::CustomerAggregate::updateSubscriptionCost(double value)
{
    if (!qFuzzyCompare(m_model.subscriptionCost(), value))
    {
        m_model.m_subscriptionCost = value;
        emit subscriptionCostChanged(value);
    }
}

void Domain::CustomerAggregate::updateInstitution(const Domain::Name &value)
{
    if (m_model.institution().value() != value.value())
    {
        m_model.m_institution = value;
        emit institutionChanged(value);
    }
}
