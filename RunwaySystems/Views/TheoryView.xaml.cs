using System;
using System.Windows.Controls;
using RunwaySystems.ViewModels;

namespace RunwaySystems.Views
{
    /// <summary>
    /// Логика взаимодействия для TheoryView.xaml
    /// </summary>
    public partial class TheoryView : UserControl
    {
        public TheoryView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
