#pragma once

#include <QObject>

#include <PageController.hpp>

namespace Presentation
{
    class PageModel : public QObject
    {
        Q_OBJECT
    public:
        PageModel(PageController *pageController, QObject *parent = nullptr);

    protected:
        PageController *m_pageController;
    };
}


