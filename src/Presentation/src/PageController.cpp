#include <PageController.hpp>

Presentation::PageController::PageController(QObject *parent)
    : QObject{parent}
{
    m_pagesWidget = new QStackedWidget();
}

Presentation::PageController::~PageController()
{
    delete m_pagesWidget;
}

QStackedWidget *Presentation::PageController::stackedWidget()
{
    return m_pagesWidget;
}

void Presentation::PageController::registerPage(Page page, QWidget *pageWidget)
{
    m_pagesIndices[page] = m_pagesWidget->addWidget(pageWidget);
}

void Presentation::PageController::setCurrentPage(Page page)
{
    m_currentPage = page;

    int index = m_pagesIndices[m_currentPage];
    m_pagesWidget->setCurrentIndex(index);
    emit pageChanged(m_currentPage);
}

Presentation::Page Presentation::PageController::currentPage()
{
    return m_currentPage;
}
