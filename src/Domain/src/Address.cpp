#include <ValueObjects/Address.h>

const QRegularExpression Domain::Address::formatPattern(R"(^[\p{L}0-9 ;,\.\/\\!@#$%&*+()_-]*$)");
const Domain::Error Domain::Address::Errors::TooLong("Address.TooLong", "The address name is too long.");
const Domain::Error Domain::Address::Errors::InvalidFormat("Address.InvalidFormat", "The address name has an invalid format.");

std::variant<Domain::Error, Domain::Address> Domain::Address::create(const QString &value)
{
    if (value.length() > Address::maximumLength)
    {
        return Errors::TooLong;
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Errors::InvalidFormat;
    }

    return Address(value);
}

QString Domain::Address::value() const
{
    return m_value;
}

Domain::Address::Address(const QString &value) : m_value(value) {}
