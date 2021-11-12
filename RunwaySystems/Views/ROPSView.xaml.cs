using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RunwaySystems.Views
{
    public partial class ROPSView : UserControl
    {
        private enum UI_MODE
        {
            ROSE = 0,
            ARC = 1,
            PLAN = 2
        }

        private List<RadioButton> rowRadioButtons;

        private Dictionary<UI_MODE, BitmapSource> ndUIs = new Dictionary<UI_MODE, BitmapSource>();
        private UI_MODE currentUI = UI_MODE.ROSE;

        public ROPSView()
        {
            InitializeComponent();

            rowRadioButtons = new List<RadioButton>();
            rowRadioButtons.Add(MAX);
            rowRadioButtons.Add(NotMAX);

            ndUIs[UI_MODE.ROSE] = new BitmapImage(new Uri("pack://application:,,,/Resources/ND_Display/ROSE_UI.png"));
            ndUIs[UI_MODE.ARC] = new BitmapImage(new Uri("pack://application:,,,/Resources/ND_Display/ARC_UI.png"));
            ndUIs[UI_MODE.PLAN] = new BitmapImage(new Uri("pack://application:,,,/Resources/ND_Display/PLAN_UI.png"));
        }

        private void UpdateNDUI()
        {
            ND_UI.Fill = new ImageBrush(ndUIs[currentUI]);
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
            //_backgroundTransforms.Children.Add(new ScaleTransform(scale, scale));
            //_backgroundTransforms.Children.Add(new TranslateTransform(xOffset, yOffset));

            NDCanvas.Background.Transform = _backgroundTransforms;
        }

        private void Mode_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            switch(button.Content)
            {
                case "ROSE":
                    currentUI = UI_MODE.ROSE;
                    break;
                case "ARC":
                    currentUI = UI_MODE.ARC;
                    break;
                case "PLAN":
                    currentUI = UI_MODE.PLAN;
                    break;
            }
            UpdateNDUI();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Fire a message to ViewModel about 
        }
    }
}
