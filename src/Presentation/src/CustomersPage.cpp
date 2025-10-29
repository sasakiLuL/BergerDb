#include <Pages/Customers/CustomersPage.hpp>

Presentation::CustomersPage::CustomersPage(CustomersPageModel *model, QWidget *parent)
    : QWidget(parent), m_model(model)
{
    m_layout = new QVBoxLayout(this);
    m_layout->setAlignment(Qt::AlignTop);
    setLayout(m_layout);

    m_pageLabel = new QLabel("Customers", this);
    QFont font = m_pageLabel->font();
    font.setPointSize(56);
    font.setBold(true);
    m_pageLabel->setFont(font);

    m_layout->addWidget(m_pageLabel);

    //m_model->loadTable();
}


