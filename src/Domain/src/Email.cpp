#include <Customer/Email.h>

using namespace Domain::Customer;

const QRegularExpression Email::formatPattern(R"(^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$)");
const Error Email::Errors::TooLong("Email.TooLong", "The email address is too long.");
const Error Email::Errors::InvalidFormat("Email.InvalidFormat", "The email address has an invalid format.");

Result<Email> Email::create(const QString &value)
{
    if (value.length() > Email::maximumLength)
    {
        return Result<Email>::failure(Errors::TooLong);
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Result<Email>::failure(Errors::InvalidFormat);
    }

    return Result<Email>::success(Email(value));
}

QString Email::value() const
{
    return _value;
}