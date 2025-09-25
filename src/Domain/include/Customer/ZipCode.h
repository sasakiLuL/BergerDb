#pragma once

#include <Core/Result.h>
#include <QRegularExpression>

namespace Domain::Customer
{
    using Core::Error;
    using Core::Result;

    class Prefix
    {
    public:
        struct Errors
        {
            const static Error TooLong;
            const static Error InvalidFormat;
        };

        const static int maximumLength = 5;
        static const QRegularExpression formatPattern;

        static Result<Prefix> create(const QString &value);
        QString value() const;

        Prefix(const QString &value) : _value(value) {}

    private:
        QString _value;
    };
} // namespace Domain::Customer