using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RunwaySystems.Views
{
    public partial class ROPSView : UserControl
    {
        private List<RadioButton> rowRadioButtons;
        private BitmapSource ndBackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Resources/ND_Background.png"));


        public ROPSView()
        {
            InitializeComponent();

            rowRadioButtons = new List<RadioButton>();
            rowRadioButtons.Add(MAX);
            rowRadioButtons.Add(NotMAX);
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            var selectedMode = sender as RadioButton;

            if(selectedMode.GroupName == "ROW" && selectedMode == RWYTooShortOverrun)
            {
                turnOnROWButtons();
                rowRadioButtons[0].IsChecked = true; // so the button is always enabled
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
            rowRadioButtons.ForEach((button) => {
                button.IsChecked = false;
                button.IsEnabled = false; 
            });
        }

        private void turnOnROWButtons()
        {
            rowRadioButtons.ForEach((button) => button.IsEnabled = true);
        }

        private void NDBackground_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)sender;

            double scale = slider.Value;
            int xOffset = (int)((1 - slider.Value) * NDCanvas.Width / 2);
            int yOffset = (int)((1 - slider.Value) * NDCanvas.Height);

            var _backgroundTransforms = new TransformGroup();
            _backgroundTransforms.Children.Add(new ScaleTransform(scale, scale));
            _backgroundTransforms.Children.Add(new TranslateTransform(xOffset, yOffset));

            NDCanvas.Background.Transform = _backgroundTransforms;
        }
    }
}
