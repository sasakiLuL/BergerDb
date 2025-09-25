#include <Customer/ZipCode.h>

using namespace Domain::Customer;

const QRegularExpression ZipCode::formatPattern(R"(^\d+$)");
const Error ZipCode::Errors::TooLong("Customer.ZipCode.TooLong", "The zip code is too long.");
const Error ZipCode::Errors::InvalidFormat("Customer.ZipCode.InvalidFormat", "The zip code has an invalid format.");

Result<ZipCode> ZipCode::create(const QString &value)
{
    if (value.length() > ZipCode::maximumLength)
    {
        return Result<ZipCode>::failure(Errors::TooLong);
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Result<ZipCode>::failure(Errors::InvalidFormat);
    }

    return Result<ZipCode>::success(ZipCode(value));
}

QString ZipCode::value() const
{
    return _value;
}