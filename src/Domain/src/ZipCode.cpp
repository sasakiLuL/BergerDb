#include <Customer/ZipCode.h>

using namespace Domain::Customer;

const QRegularExpression Prefix::formatPattern(R"(^\d+$)");
const Error Prefix::Errors::TooLong("Customer.ZipCode.TooLong", "The zip code is too long.");
const Error Prefix::Errors::InvalidFormat("Customer.ZipCode.InvalidFormat", "The zip code has an invalid format.");

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