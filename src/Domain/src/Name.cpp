#include <ValueObjects/Name.h>

const QRegularExpression Domain::Name::formatPattern(R"(^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$)");
const Domain::Error Domain::Name::Errors::TooLong("Name.TooLong", "The customer name istoo long.");
const Domain::Error Domain::Name::Errors::InvalidFormat("Name.InvalidFormat", "The customer name has an invalid format.");

std::variant<Domain::Error, Domain::Name> Domain::Name::create(const QString &value)
{
    if (value.length() > Name::maximumLength)
    {
        return Errors::TooLong;
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Errors::InvalidFormat;
    }

    return Name(value);
}

QString Domain::Name::value() const
{
    return m_value;
}

Domain::Name::Name(const QString &value) : m_value(value) {}
