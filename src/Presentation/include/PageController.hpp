#pragma once

#include <QObject>
#include <QStackedWidget>

namespace Presentation
{
    enum class Page
    {
        Customers,
        Customer,
        AddCustomer
    };

    class PageController : public QObject
    {
        Q_OBJECT
    public:
        PageController(QObject *parent = nullptr);
        ~PageController();

        QStackedWidget *stackedWidget();

        void registerPage(Page page, QWidget* pageWidget);

        Page currentPage();
        void setCurrentPage(Page page);

    signals:
        void pageChanged(Page page);

    private:
        Page m_currentPage;
        QStackedWidget *m_pagesWidget;

        QHash<Page, int> m_pagesIndices;
    };
}


