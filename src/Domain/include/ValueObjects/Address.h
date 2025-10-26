#pragma once

#include <QRegularExpression>
#include <Core/Error.h>

namespace Domain
{
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

        static std::variant<Error, Address> create(const QString &value);
        QString value() const;

        Address(const QString &value);

    private:
        QString m_value;
    };
}
