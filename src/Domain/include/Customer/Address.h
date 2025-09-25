#pragma once

#include <QRegularExpression>
#include <Core/Result.h>

namespace Domain::Customer
{
    using Domain::Core::Error;
    using Domain::Core::Result;

    class Address
    {
    public:
        struct Errors
        {
            const static Error TooLong;
            const static Error InvalidFormat;
        };

        const static int maximumLength = 255;
        static const QRegularExpression formatPattern;

        static Result<Address> create(const QString &value);
        QString value() const;

        Address(const QString &value) : _value(value) {}

    private:
        QString _value;
    };
} // namespace Domain::Customer
