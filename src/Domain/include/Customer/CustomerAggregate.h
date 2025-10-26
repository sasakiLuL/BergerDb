#pragma once

#include <Customer/CustomerModel.h>

namespace Domain
{
    class CustomerAggregate : public QObject
    {
        Q_OBJECT
    public:
        CustomerAggregate(CustomerModel model, QObject *parent = nullptr);
        CustomerModel model() const;

    public slots:
        void updatePersonalId(int value);
        void updatePrefix(const Domain::Prefix &value);
        void updateFirstName(const Domain::Name &value);
        void updateLastName(const Domain::Name &value);
        void updateSex(Domain::Sex value);
        void updateEmail(const Domain::Email &value);
        void updateRegisteredOn(const QDateTime &value);
        void updateTerminatedOn(const std::optional<QDateTime> &value);
        void updateNotation(const Domain::Notation &value);
        void updateStreet(const Domain::Address &value);
        void updateCity(const Domain::Address &value);
        void updateZipCode(const Domain::ZipCode &value);
        void updatePaymentType(Domain::PaymentType value);
        void updateMemberType(Domain::MemberType value);
        void updateEntryType(Domain::EntryType value);
        void updateSubscriptionCost(double value);
        void updateInstitution(const Domain::Name &value);

    signals:
        void personalIdChanged(int value);
        void prefixChanged(const Domain::Prefix &value);
        void firstNameChanged(const Domain::Name &value);
        void lastNameChanged(const Domain::Name &value);
        void sexChanged(Domain::Sex value);
        void emailChanged(const Domain::Email &value);
        void registeredOnChanged(const QDateTime &value);
        void terminatedOnChanged(const std::optional<QDateTime> &value);
        void notationChanged(const Domain::Notation &value);
        void streetChanged(const Domain::Address &value);
        void cityChanged(const Domain::Address &value);
        void zipCodeChanged(const Domain::ZipCode &value);
        void paymentTypeChanged(Domain::PaymentType value);
        void memberTypeChanged(Domain::MemberType value);
        void entryTypeChanged(Domain::EntryType value);
        void subscriptionCostChanged(double value);
        void institutionChanged(const Domain::Name &value);

    private:
        CustomerModel m_model;
    };
}
