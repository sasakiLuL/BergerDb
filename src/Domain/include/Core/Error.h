#pragma once

#include <QString>

namespace Domain
{
    class Error
    {
    public:
        Error(const QString &code, const QString &message);
        QString code() const;
        QString message() const;

    private:
        QString m_code;
        QString m_message;
    };
}