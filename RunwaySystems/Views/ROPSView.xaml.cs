using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RunwaySystems.ViewModels;

namespace RunwaySystems.Views
{
    public partial class ROPSView : UserControl
    {
        public ROPSView()
        {
            InitializeComponent();
            this.DataContext = new ROPSViewModel();
        }
    }
}
