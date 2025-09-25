#pragma once

#include <Customer/CustomerModel.h>

namespace Domain::Customer
{
    class CustomerAggregate
    {
    public:
        CustomerAggregate(const CustomerModel &model);
        CustomerModel model() const { return _model; }

    private:
        CustomerModel _model;
    };
}