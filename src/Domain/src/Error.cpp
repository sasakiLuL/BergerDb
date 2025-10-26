#include <Core/Error.h>

Domain::Error::Error(const QString &code, const QString &message)
    : m_code(code), m_message(message) {}

QString Domain::Error::code() const
{
    return m_code;
}

QString Domain::Error::message() const
{
    return m_message;
}
