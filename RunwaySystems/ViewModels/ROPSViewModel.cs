using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunwaySystems.ViewModels
{
    public class ROPSViewModel : ViewModel
    {

        //Режим проигрывания анимации
        private string _selectedExecutionMode;
        public string SelectedExectionMode
        {
            get => _selectedExecutionMode;
            set => Set(ref _selectedExecutionMode, value);
        }


        //Выбранный режим работы ND ()
        private string _ND_DisplayMode;
        public string ND_DisplayMode
        {
            get => _ND_DisplayMode;
            set => Set(ref _ND_DisplayMode, value);
        }


        private double _ND_Scale;
        public double ND_Scale
        {
            get => _ND_Scale;
            set => Set(ref _ND_Scale, value);
        }

    }
}
