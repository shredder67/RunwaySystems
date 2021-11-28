using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using RunwaySystems.Commands;

namespace RunwaySystems.ViewModels
{
    class RAASViewModel : ViewModel
    {

        #region Fields

        private string _RAASSource = "Resources/DemoResources/RAAS_video.wmv";

        public string RAASSource
        {
            get => _RAASSource;
            set => Set(ref _RAASSource, value);
        }

        #endregion

        #region Commands

        #region Select Source Video Command

        public ICommand SwitchRAASSourceVideoCommand { get; }
        private bool CanSwitchRAASSourceVideoCommandExecute(object p) => true;
        private void OnSwitchRAASSourceVideoCommandExecuted(object p)
        {
            //TODO: Do switch source command
        }

        #endregion



        #endregion

        public RAASViewModel()
        {
            SwitchRAASSourceVideoCommand = new RelayCommand(
                OnSwitchRAASSourceVideoCommandExecuted,
                CanSwitchRAASSourceVideoCommandExecute
            );
        }

    }
}
