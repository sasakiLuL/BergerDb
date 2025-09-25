#pragma once

#include <Core/Result.h>
#include <QRegularExpression>

namespace Domain::Customer
{
    using Core::Error;
    using Core::Result;

    class ZipCode
    {
    public:
        struct Errors
        {
            const static Error TooLong;
            const static Error InvalidFormat;
        };

        const static int maximumLength = 5;
        static const QRegularExpression formatPattern;

        static Result<ZipCode> create(const QString &value);
        QString value() const;

        ZipCode(const QString &value) : _value(value) {}

    private:
        QString _value;
    };
} // namespace Domain::Customer