#pragma once

#include <QRegularExpression>

#include <Core/Error.h>

namespace Domain
{
    class Prefix
    {
    public:
        struct Errors
        {
            const static Error TooLong;
            const static Error InvalidFormat;
        };

        const static int maximumLength = 50;
        static const QRegularExpression formatPattern;

        static std::variant<Error, Prefix> create(const QString &value);
        QString value() const;

        Prefix(const QString &value);

    private:
        QString m_value;
    };
}
