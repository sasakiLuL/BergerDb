#include <Customer/Address.h>

using namespace Domain::Customer;

const QRegularExpression Address::formatPattern(R"(^[\p{L}0-9 ;,\.\/\\!@#$%&*+()_-]*$)");
const Error Address::Errors::TooLong("Address.TooLong", "The address name is too long.");
const Error Address::Errors::InvalidFormat("Address.InvalidFormat", "The address name has an invalid format.");

Result<Address> Address::create(const QString &value)
{
    if (value.length() > Address::maximumLength)
    {
        return Result<Address>::failure(Errors::TooLong);
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Result<Address>::failure(Errors::InvalidFormat);
    }

    return Result<Address>::success(Address(value));
}

QString Address::value() const
{
    return _value;
}