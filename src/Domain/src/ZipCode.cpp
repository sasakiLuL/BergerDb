#include <ValueObjects/ZipCode.h>

const QRegularExpression Domain::ZipCode::formatPattern(R"(^\d+$)");
const Domain::Error Domain::ZipCode::Errors::TooLong("Customer.ZipCode.TooLong", "The zip code is too long.");
const Domain::Error Domain::ZipCode::Errors::InvalidFormat("Customer.ZipCode.InvalidFormat", "The zip code has an invalid format.");

std::variant<Domain::Error, Domain::ZipCode> Domain::ZipCode::create(const QString &value)
{
    if (value.length() > ZipCode::maximumLength)
    {
        return Errors::TooLong;
    }

    if (formatPattern.match(value).hasMatch() == false)
    {
        return Errors::InvalidFormat;
    }

    return ZipCode(value);
}

QString Domain::ZipCode::value() const
{
    return m_value;
}

Domain::ZipCode::ZipCode(const QString &value) : m_value(value) {}
