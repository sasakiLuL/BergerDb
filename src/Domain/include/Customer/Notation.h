#pragma once

#include <Core/Result.h>

namespace Domain::Customer
{
    using Domain::Core::Error;
    using Domain::Core::Result;

    class Notation
    {
    public:
        struct Errors
        {
            const static Error TooLong;
        };

        const static int maximumLength = 3000;

        static Result<Notation> create(const QString &value);
        QString value() const;

        Notation(const QString &value) : _value(value) {}

    private:
        QString _value;
    };
} // namespace Domain::Customer