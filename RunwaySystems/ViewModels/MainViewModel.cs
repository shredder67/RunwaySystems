using System;
using System.Windows.Input;

namespace RunwaySystems.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private ViewModel _selectedViewModel = new ROPSViewModel();

        public ViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = new ViewModel(); }
        }

        public ICommand UpdateViewCommand { get; set; }
    }
}
