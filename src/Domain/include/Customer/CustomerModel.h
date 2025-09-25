#pragma once

#include <optional>
#include <Customer/Address.h>
#include <Customer/Email.h>
#include <Customer/Name.h>
#include <Customer/Notation.h>
#include <Customer/Prefix.h>
#include <Customer/ZipCode.h>
#include <QUuid>
#include <QDateTime>

namespace Domain::Customer
{
    enum class EntryType
    {
        GE,
        EE
    };

    enum class MemberType
    {
        Apothecary,
        LayPerson,
        Doctor,
        NonmedicalPractitioner
    };

    enum class PaymentType
    {
        Billing,
        DirectDebiting,
    };

    enum class Sex
    {
        Male,
        Female
    };

    class CustomerAggregate;

    class CustomerModel
    {
    public:
        CustomerModel(
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
            const Name &institution);

        QUuid id() const { return _id; }
        long personalId() const { return _personalId; }
        Prefix prefix() const { return _prefix; }
        Name firstName() const { return _firstName; }
        Name lastName() const { return _lastName; }
        Sex sex() const { return _sex; }
        Email email() const { return _emailAddress; }
        QDateTime registeredOnUtc() const { return _registeredOnUtc; }
        std::optional<QDateTime> terminatedOnUtc() const { return _terminatedOnUtc; }
        Notation notation() const { return _notation; }
        Address street() const { return _street; }
        Address city() const { return _city; }
        ZipCode zipCode() const { return _zipCode; }
        PaymentType paymentType() const { return _paymentType; }
        MemberType memberType() const { return _memberType; }
        EntryType entryType() const { return _entryType; }
        double subscriptionCost() const { return _subscriptionCost; }
        Name institution() const { return _institution; }

    private:
        QUuid _id;
        long _personalId;
        Prefix _prefix;
        Name _firstName;
        Name _lastName;
        Sex _sex;
        Email _emailAddress;
        QDateTime _registeredOnUtc;
        std::optional<QDateTime> _terminatedOnUtc;
        Notation _notation;
        Address _street;
        Address _city;
        ZipCode _zipCode;
        PaymentType _paymentType;
        MemberType _memberType;
        EntryType _entryType;
        double _subscriptionCost;
        Name _institution;

        friend class CustomerAggregate;
    };
} // namespace Domain::Customer