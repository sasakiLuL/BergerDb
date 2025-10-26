#pragma once

#include <QRegularExpression>

#include <Core/Error.h>

namespace Domain
{
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

        static std::variant<Error, ZipCode> create(const QString &value);
        QString value() const;

        ZipCode(const QString &value);

    private:
        QString m_value;
    };
}
