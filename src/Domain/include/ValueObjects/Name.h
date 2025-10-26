#pragma once

#include <QRegularExpression>

#include <Core/Error.h>

namespace Domain
{
    class Name
    {
    public:
        struct Errors
        {
            const static Error TooLong;
            const static Error InvalidFormat;
        };

        const static int maximumLength = 256;
        static const QRegularExpression formatPattern;

        static std::variant<Error, Name> create(const QString &value);
        QString value() const;

        Name(const QString &value);

    private:
        QString m_value;
    };
}
