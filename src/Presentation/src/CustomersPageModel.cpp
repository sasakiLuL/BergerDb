#include <Pages/Customers/CustomersPageModel.hpp>

Presentation::CustomersPageModel::CustomersPageModel(PageController* pageController, Infrastructure::CustomerDbContext *dbContext,  QObject *parent)
    : PageModel(pageController, parent), m_dbContext(dbContext)
{

}

void Presentation::CustomersPageModel::loadTable()
{
    m_dbContext->get();
}


