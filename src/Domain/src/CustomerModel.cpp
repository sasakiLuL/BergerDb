#include <Customer/CustomerModel.h>

Domain::CustomerModel::CustomerModel(
    int id,
    int personalId,
    const Prefix &prefix,
    const Name &firstName,
    const Name &lastName,
    Sex sex,
    const Email &email,
    const QDateTime &registeredOn,
    const std::optional<QDateTime> &terminatedOn,
    const Notation &notation,
    const Address &street,
    const Address &city,
    const ZipCode &zipCode,
    PaymentType paymentType,
    MemberType memberType,
    EntryType entryType,
    double subscriptionCost,
    const Name &institution) :
        m_id(id),
        m_personalId(personalId),
        m_prefix(prefix),
        m_firstName(firstName),
        m_lastName(lastName),
        m_sex(sex),
        m_email(email),
        m_registeredOn(registeredOn),
        m_terminatedOn(terminatedOn),
        m_notation(notation),
        m_street(street),
        m_city(city),
        m_zipCode(zipCode),
        m_paymentType(paymentType),
        m_memberType(memberType),
        m_entryType(entryType),
        m_subscriptionCost(subscriptionCost),
        m_institution(institution) {}

int Domain::CustomerModel::id() const
{
    return m_id;
}

int Domain::CustomerModel::personalId() const
{
    return m_personalId;
}

QDateTime Domain::CustomerModel::registeredOn() const
{
    return m_registeredOn;
}

std::optional<QDateTime> Domain::CustomerModel::terminatedOn() const
{
    return m_terminatedOn;
}

double Domain::CustomerModel::subscriptionCost() const
{
    return m_subscriptionCost;
}

Domain::Name Domain::CustomerModel::institution() const
{
    return m_institution;
}

Domain::EntryType Domain::CustomerModel::entryType() const
{
    return m_entryType;
}

Domain::MemberType Domain::CustomerModel::memberType() const
{
    return m_memberType;
}

Domain::PaymentType Domain::CustomerModel::paymentType() const
{
    return m_paymentType;
}

Domain::ZipCode Domain::CustomerModel::zipCode() const
{
    return m_zipCode;
}

Domain::Address Domain::CustomerModel::city() const
{
    return m_city;
}

Domain::Address Domain::CustomerModel::street() const
{
    return m_street;
}

Domain::Notation Domain::CustomerModel::notation() const
{
    return m_notation;
}

Domain::Email Domain::CustomerModel::email() const
{
    return m_email;
}

Domain::Sex Domain::CustomerModel::sex() const
{
    return m_sex;
}

Domain::Name Domain::CustomerModel::lastName() const
{
    return m_lastName;
}

Domain::Name Domain::CustomerModel::firstName() const
{
    return m_firstName;
}

Domain::Prefix Domain::CustomerModel::prefix() const
{
    return m_prefix;
}


