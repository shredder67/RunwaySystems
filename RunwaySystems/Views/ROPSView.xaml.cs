using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RunwaySystems.Views
{
    public partial class ROPSView : UserControl
    {
        private List<RadioButton> ROWRadioButtons;
        public ROPSView()
        {
            InitializeComponent();
            ROWRadioButtons = new List<RadioButton>();
            ROWRadioButtons.Add(MAX);
            ROWRadioButtons.Add(NotMAX);

        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            var selectedMode = sender as RadioButton;

            if(selectedMode.GroupName == "ROW" && selectedMode == RWYTooShortOverrun)
            {
                turnOnROWButtons();
                ROWRadioButtons[0].IsChecked = true;
            }
        }

        private void Mode_UnChecked(object sender, RoutedEventArgs e)
        {
            var unSelectedMode = sender as RadioButton;

            if (unSelectedMode.GroupName == "ROW" && unSelectedMode == RWYTooShortOverrun)
            {
                turnOffROWButtons();
            }
        }

        private void turnOffROWButtons()
        {
            ROWRadioButtons.ForEach((button) => {
                button.IsChecked = false;
                button.IsEnabled = false; 
            });
        }

        private void turnOnROWButtons()
        {
            ROWRadioButtons.ForEach((button) => button.IsEnabled = true);
        }
    }
}
