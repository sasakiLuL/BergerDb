#pragma once

#include <QString>

namespace Domain::Core
{
    struct Error
    {
        Error(const QString &code_, const QString &message_) : message(message_), code(code_) {}

        QString message;
        QString code;
    };

} // namespace Domain