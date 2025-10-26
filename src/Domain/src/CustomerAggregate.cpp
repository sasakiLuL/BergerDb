#include <Customer/CustomerAggregate.h>
#include "CustomerAggregate.h"

using namespace Domain::Customer;

CustomerAggregate::CustomerAggregate(const CustomerModel &model) : m_model(model)
{
}

Domain::CustomerModel Domain::Customer::CustomerAggregate::model() const
{
    return m_model;
}
