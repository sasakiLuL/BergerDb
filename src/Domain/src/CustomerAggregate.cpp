#include <Customer/CustomerAggregate.h>

using namespace Domain::Customer;

CustomerAggregate::CustomerAggregate(const CustomerModel &model) : _model(model)
{
}