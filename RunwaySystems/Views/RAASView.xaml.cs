﻿using RunwaySystems.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RunwaySystems.Views
{
    public partial class RAASView : UserControl
    {
        public RAASView()
        {
            InitializeComponent();
            this.DataContext = new RAASViewModel();
        }

        private void PlayerLoadFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Video couldn't load, something went wrong!\n\n" + e.ToString());
        }
    }
}