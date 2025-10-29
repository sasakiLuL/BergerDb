#pragma once

#include <QHBoxLayout>
#include <QLabel>
#include <QPushButton>
#include <QTableView>
#include <QWidget>

#include <Pages/Customers/CustomersPageModel.hpp>

namespace Presentation
{
    class CustomersPage : public QWidget
    {
        Q_OBJECT

    public:
        CustomersPage(CustomersPageModel *model, QWidget *parent = nullptr);

    private:
        CustomersPageModel* m_model;

        QVBoxLayout *m_layout;
        QLabel *m_pageLabel;
    };
}
