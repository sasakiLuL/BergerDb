#include <Customer/Notation.h>

using namespace Domain::Customer;

const Error Notation::Errors::TooLong("Notation.TooLong", "The notation is too long.");

Result<Notation> Notation::create(const QString &value)
{
    if (value.length() > Notation::maximumLength)
    {
        return Result<Notation>::failure(Errors::TooLong);
    }

    return Result<Notation>::success(Notation(value));
}

QString Notation::value() const
{
    return _value;
}