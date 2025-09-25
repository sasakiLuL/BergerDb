#include <Customer/Name.h>

using namespace Domain::Customer;

const QRegularExpression Name::formatPattern(R"(^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$)");
const Error Name::Errors::TooLong("Name.TooLong", "The customer name istoo long.");
const Error Name::Errors::InvalidFormat("Name.InvalidFormat", "The customer name has an invalid format.");

Result<Name> Name::create(const QString &value)
{
    if (value.length() > Name::maximumLength)
    {
        return Result<Name>::failure(Errors::TooLong);
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Result<Name>::failure(Errors::InvalidFormat);
    }

    return Result<Name>::success(Name(value));
}

QString Name::value() const
{
    return _value;
}