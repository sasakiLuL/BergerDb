#include "CustomerModel.h"

using namespace Domain::Customer;

CustomerModel::CustomerModel(
    const QUuid &id,
    long personalId,
    const Prefix &prefix,
    const Name &firstName,
    const Name &lastName,
    Sex sex,
    const Email &emailAddress,
    const QDateTime &registeredOnUtc,
    const std::optional<QDateTime> &terminatedOnUtc,
    const Notation &notation,
    const Address &street,
    const Address &city,
    const ZipCode &zipCode,
    PaymentType paymentType,
    MemberType memberType,
    EntryType entryType,
    double subscriptionCost,
    const Name &institution)
    : _id(id),
      _personalId(personalId),
      _prefix(prefix),
      _firstName(firstName),
      _lastName(lastName),
      _sex(sex),
      _emailAddress(emailAddress),
      _registeredOnUtc(registeredOnUtc),
      _terminatedOnUtc(terminatedOnUtc),
      _notation(notation),
      _street(street),
      _city(city),
      _zipCode(zipCode),
      _paymentType(paymentType),
      _memberType(memberType),
      _entryType(entryType),
      _subscriptionCost(subscriptionCost),
      _institution(institution)
{
}