#pragma once

#include <QMainWindow>

namespace Ui::Views
{
    class MainWindow : public QMainWindow
    {
        Q_OBJECT

    public:
        MainWindow(QWidget *parent = nullptr);
        ~MainWindow();
    };
}
