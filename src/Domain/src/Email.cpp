#include <ValueObjects/Email.h>

const QRegularExpression Domain::Email::formatPattern(R"(^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$)");
const Domain::Error Domain::Email::Errors::TooLong("Email.TooLong", "The email address is too long.");
const Domain::Error Domain::Email::Errors::InvalidFormat("Email.InvalidFormat", "The email address has an invalid format.");

std::variant<Domain::Error, Domain::Email> Domain::Email::create(const QString &value)
{
    if (value.length() > Email::maximumLength)
    {
        return Errors::TooLong;
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Errors::InvalidFormat;
    }

    return Email(value);
}

QString Domain::Email::value() const
{
    return m_value;
}

Domain::Email::Email(const QString &value) : m_value(value) {}
