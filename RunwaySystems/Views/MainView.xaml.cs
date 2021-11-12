using System;
using System.Windows;
using RunwaySystems.ViewModels;

namespace RunwaySystems
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
