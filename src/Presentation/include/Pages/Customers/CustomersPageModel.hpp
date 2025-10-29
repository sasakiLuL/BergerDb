#pragma once

#include <QObject>

#include <PageModel.hpp>

#include <Customer/CustomerDbContext.hpp>

namespace Presentation
{
    class CustomersPageModel : public PageModel
    {
        Q_OBJECT

    public:
        CustomersPageModel(PageController* pageController, Infrastructure::CustomerDbContext *dbContext,  QObject *parent = nullptr);

    public slots:
        void loadTable();

    private:
        Infrastructure::CustomerDbContext *m_dbContext;
    };
}
