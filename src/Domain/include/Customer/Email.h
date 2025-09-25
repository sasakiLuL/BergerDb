#pragma once

#include <Core/Result.h>
#include <QRegularExpression>

namespace Domain::Customer
{
    using Domain::Core::Error;
    using Domain::Core::Result;

    class Email
    {
    public:
        struct Errors
        {
            const static Error TooLong;
            const static Error InvalidFormat;
        };

        const static int maximumLength = 320;
        static const QRegularExpression formatPattern;

        static Result<Email> create(const QString &value);
        QString value() const;

        Email(const QString &value) : _value(value) {}

    private:
        QString _value;
    };
} // namespace Domain::Customer