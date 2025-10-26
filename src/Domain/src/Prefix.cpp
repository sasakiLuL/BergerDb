#include <ValueObjects/Prefix.h>

const QRegularExpression Domain::Prefix::formatPattern(R"(^[\p{L}0-9, ./-]*$)");
const Domain::Error Domain::Prefix::Errors::TooLong("Prefix.TooLong", "The prefix is too long.");
const Domain::Error Domain::Prefix::Errors::InvalidFormat("Prefix.InvalidFormat", "The prefix has an invalid format.");

std::variant<Domain::Error, Domain::Prefix> Domain::Prefix::create(const QString &value)
{
    if (value.length() > Prefix::maximumLength)
    {
        return Errors::TooLong;
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Errors::InvalidFormat;
    }

    return Prefix(value);
}

QString Domain::Prefix::value() const
{
    return m_value;
}

Domain::Prefix::Prefix(const QString &value) : m_value(value) {}
