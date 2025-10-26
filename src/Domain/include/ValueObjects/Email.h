#pragma once

#include <QRegularExpression>

#include <Core/Error.h>

namespace Domain
{
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

        static std::variant<Error, Email> create(const QString &value);
        QString value() const;

        Email(const QString &value);

    private:
        QString m_value;
    };
}
