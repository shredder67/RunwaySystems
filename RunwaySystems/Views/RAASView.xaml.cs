using RunwaySystems.ViewModels;
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
using System.Windows.Threading;

namespace RunwaySystems.Views
{
    public partial class RAASView : UserControl
    {

        private DispatcherTimer videoTimer = new DispatcherTimer();
        private bool isPaused;

        public RAASView()
        {
            InitializeComponent();
            this.PreviewMouseUp += new MouseButtonEventHandler(timelineSlider_MouseUp);
            this.DataContext = new RAASViewModel();
            videoTimer.Tick += new EventHandler(VideoTimer_Tick);
            videoTimer.Interval = new TimeSpan(0, 0, 1);
            isPaused = true;
        }

        private void RAASPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            RAASPlayer.Play();
            videoTimer.Start();
            isPaused = false;
        }

        private void RAASPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = RAASPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void VideoTimer_Tick(object sender, EventArgs e)
        {
            timelineSlider.Value += 1;

            if (RAASPlayer.Source != null)
            {
                if (RAASPlayer.NaturalDuration.HasTimeSpan)
                    lblStatus.Content = String.Format("{0} / {1}", RAASPlayer.Position.ToString(@"mm\:ss"), RAASPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lblStatus.Content = "No file selected...";
        }

        private void PlayerLoadFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Video couldn't load, something went wrong!\n\n" + e.ToString() + "\n\n" + RAASPlayer.Source);
        }

        private void RAASPlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isPaused)
            {
                videoTimer.Stop();
                this.RAASPlayer.Pause();
                isPaused = true;
            }
            else
            {
                videoTimer.Start();
                this.RAASPlayer.Play();
                isPaused = false;
            }    
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RAASPlayer.Volume = (double)VolumeSlider.Value;
        }

        private bool _timeManualChange = false;
        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(_timeManualChange)
            {
                int sliderValue = (int)timelineSlider.Value;
                TimeSpan ts = sliderValue * videoTimer.Interval;
                RAASPlayer.Position = ts;
                _timeManualChange = false;
            }
        }

        private void timelineSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _timeManualChange = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timelineSlider.Value = timelineSlider.Minimum;
            _timeManualChange = true;
            timelineSlider_ValueChanged(this, null);
        }
    }
}
