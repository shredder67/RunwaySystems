using System;
using System.Windows.Input;
using RunwaySystems.Commands;
using RunwaySystems.Models;

namespace RunwaySystems.ViewModels
{
    public class MainViewModel : ViewModel
    {

        #region Fields

        private ViewModel _selectedViewModel = new ROPSViewModel();

        public ViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set => Set(ref _selectedViewModel, value);
        }

        #endregion

        #region Commands

        public ICommand SwitchSubViewCommand { get; }
        private bool CanSwitchSubViewCommandExecute(object p) => true;
        private void OnSwitchSubViewCommandExecuted(object p)
        {
            string windowName = p.ToString();
            if(windowName != null)
            {
                switch (windowName)
                {
                    case "ROPSView":
                        if (!(_selectedViewModel is ROPSViewModel))
                            SelectedViewModel = new ROPSViewModel();
                        break;
                    case "RAASView":
                        if (!(_selectedViewModel is RAASViewModel))
                            SelectedViewModel = new RAASViewModel();
                        break;
                    case "TheoryView":
                        if (!(_selectedViewModel is TheoryViewModel))
                            SelectedViewModel = new TheoryViewModel();
                        break;
                }
            }
        }

        #endregion

        public MainViewModel()
        {
            SwitchSubViewCommand = new RelayCommand(
                OnSwitchSubViewCommandExecuted,
                CanSwitchSubViewCommandExecute
            );
        }
    }
}
