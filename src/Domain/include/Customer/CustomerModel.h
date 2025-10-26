#pragma once

#include <optional>
#include <QUuid>
#include <QDateTime>

#include <ValueObjects/Address.h>
#include <ValueObjects/Email.h>
#include <ValueObjects/Name.h>
#include <ValueObjects/Notation.h>
#include <ValueObjects/Prefix.h>
#include <ValueObjects/ZipCode.h>

namespace Domain
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
            const Name &institution);

        int id() const;
        int personalId() const;
        Prefix prefix() const;
        Name firstName() const;
        Name lastName() const;
        Sex sex() const;
        Email email() const;
        QDateTime registeredOn() const;
        std::optional<QDateTime> terminatedOn() const;
        Notation notation() const;
        Address street() const;
        Address city() const;
        ZipCode zipCode() const;
        PaymentType paymentType() const;
        MemberType memberType() const;
        EntryType entryType() const;
        double subscriptionCost() const;
        Name institution() const;

    private:
        int m_id;
        int m_personalId;
        Prefix m_prefix;
        Name m_firstName;
        Name m_lastName;
        Sex m_sex;
        Email m_email;
        QDateTime m_registeredOn;
        std::optional<QDateTime> m_terminatedOn;
        Notation m_notation;
        Address m_street;
        Address m_city;
        ZipCode m_zipCode;
        PaymentType m_paymentType;
        MemberType m_memberType;
        EntryType m_entryType;
        double m_subscriptionCost;
        Name m_institution;

        friend class CustomerAggregate;
    };
}
