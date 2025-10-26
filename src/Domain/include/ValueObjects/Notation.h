#pragma once

#include <Core/Error.h>

namespace Domain
{
    class Notation
    {
    public:
        struct Errors
        {
            const static Error TooLong;
        };

        const static int maximumLength = 3000;

        static std::variant<Error, Notation> create(const QString &value);
        QString value() const;

        Notation(const QString &value);

    private:
        QString m_value;
    };
}
