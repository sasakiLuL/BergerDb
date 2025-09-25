#include <Customer/Prefix.h>

using namespace Domain::Customer;

const QRegularExpression Prefix::formatPattern(R"(^[\p{L}0-9, ./-]*$)");
const Error Prefix::Errors::TooLong("Prefix.TooLong", "The prefix is too long.");
const Error Prefix::Errors::InvalidFormat("Prefix.InvalidFormat", "The prefix has an invalid format.");

Result<Prefix> Prefix::create(const QString &value)
{
    if (value.length() > Prefix::maximumLength)
    {
        return Result<Prefix>::failure(Errors::TooLong);
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Result<Prefix>::failure(Errors::InvalidFormat);
    }

    return Result<Prefix>::success(Prefix(value));
}

QString Prefix::value() const
{
    return _value;
}