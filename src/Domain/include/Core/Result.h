#pragma once

#include <variant>
#include <initializer_list>
#include <QVector>
#include "Core/Error.h"

namespace Domain::Core
{
    template <typename TValue>
    class Result
    {
    public:
        const TValue &Value() const
        {
            if (!std::holds_alternative<TValue>(_state))
            {
                throw std::runtime_error("Cannot access value of a failure or empty success result.");
            }
            return std::get<TValue>(_state);
        }

        bool isSuccess() const { return std::holds_alternative<std::monostate>(_state) || std::holds_alternative<TValue>(_state); }

        bool isFailure() const { return !isSuccess(); }

        static Result success() { return Result(std::monostate{}); }

        static Result success(TValue value) { return Result(std::move(value)); }

        static Result failure(const Error &error) { return Result(QVector<Error>({error})); }

        static Result failure(std::initializer_list<Error> errors) { return Result(QVector<Error>(errors)); }

    private:
        std::variant<std::monostate, TValue, QVector<Error>> _state;

        explicit Result(std::variant<std::monostate, TValue, QVector<Error>> state)
            : _state(std::move(state))
        {
            if (std::holds_alternative<QVector<Error>>(_state) && std::get<QVector<Error>>(_state).empty())
            {
                throw std::invalid_argument("Failure result must have at least one error.");
            }
        }
    };
}