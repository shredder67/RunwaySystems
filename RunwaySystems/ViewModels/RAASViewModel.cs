using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using RunwaySystems.Commands;

namespace RunwaySystems.ViewModels
{

    public class VideoSource
    {
        public string ReadableName { get; set; }
        public string PathToSource { get; set; }
    }

    class RAASViewModel : ViewModel
    {

        #region Fields

        private VideoSource _RAASSource;

        public VideoSource RAASSource
        {
            get => _RAASSource;
            set => Set(ref _RAASSource, value);
        }

        public ObservableCollection<VideoSource> Sources { get; }

        #endregion

        #region Commands

        #region Select Source Video Command

        //public ICommand SwitchRAASSourceVideoCommand { get; }
        //private bool CanSwitchRAASSourceVideoCommandExecute(object p) => true;
        //private void OnSwitchRAASSourceVideoCommandExecuted(object p)
        //{
        //    RAASSource = (p as VideoSource).PathToSource;
        //}

        #endregion


        #endregion

        public RAASViewModel()
        {
            Sources = new ObservableCollection<VideoSource>()
            {
                new VideoSource { ReadableName = "Введение в RAAS", PathToSource = "Resources/DemoResources/RAAS_intro.wmv" },
                new VideoSource { ReadableName = "RAAS от первого лица", PathToSource = "Resources/DemoResources/RAAS_POV.wmv" },
                new VideoSource { ReadableName = "Схематическое представление RAAS", PathToSource = "Resources/DemoResources/RAAS_video.wmv" },
                new VideoSource { ReadableName = "Обзор RAAS", PathToSource = "Resources/DemoResources/RAAS_video_2.wmv" }
            };
            RAASSource = Sources[0];

            //SwitchRAASSourceVideoCommand = new RelayCommand(
            //    OnSwitchRAASSourceVideoCommandExecuted,
            //    CanSwitchRAASSourceVideoCommandExecute
            //);
        }

    }
}
