#include <ValueObjects/Notation.h>

const Domain::Error Domain::Notation::Errors::TooLong("Notation.TooLong", "The notation is too long.");

std::variant<Domain::Error, Domain::Notation> Domain::Notation::create(const QString &value)
{
    if (value.length() > Notation::maximumLength)
    {
        return Errors::TooLong;
    }

    return Notation(value);
}

QString Domain::Notation::value() const
{
    return m_value;
}

Domain::Notation::Notation(const QString &value) : m_value(value) {}
