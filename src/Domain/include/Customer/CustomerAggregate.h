#pragma once

#include <Customer/CustomerModel.h>

namespace Domain
{
    class CustomerAggregate
    {
    public:
        CustomerAggregate(const CustomerModel &model);
        CustomerModel model() const;

    private:
        CustomerModel m_model;
    };
}