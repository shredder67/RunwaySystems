using RunwaySystems.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RunwaySystems.ViewModels
{
    public enum ROWExecutionMode
    {
        PerfectLanding,
        IfWetRWYTooShort,
        RWYTooShort
    }

    public enum ROPExecutionMode
    {
        MaxBreakingMaxReverse,
        KeepMaxReverse
    }

    public class ROPSViewModel : ViewModel
    {
        #region Fields

        // Режим проигрывания анимации

        private bool _EnableROPModes = false;
        public bool EnableROPModes
        {
            get => _EnableROPModes;
            set => Set(ref _EnableROPModes, value);
        }

        private ROWExecutionMode _SelectedROWExecutionMode;
        public ROWExecutionMode SelectedROWExectionMode
        {
            get => _SelectedROWExecutionMode;
            set => Set(ref _SelectedROWExecutionMode, value);
        }

        private ROPExecutionMode? _SelectedROPExecutionMode;
        public ROPExecutionMode? SelectedROPExectionMode
        {
            get => _SelectedROPExecutionMode;
            set => Set(ref _SelectedROPExecutionMode, value);
        }


        // Режим отображаемого ND интерфейса
        private enum ND_Overlay
        {
            ROSE = 0,
            ARC = 1,
            PLAN = 2
        }
        private Dictionary<ND_Overlay, Uri> displaySources = new Dictionary<ND_Overlay, Uri>();

        private Uri _CurrentND_Display;
        public Uri CurrentND_Display
        {
            get => _CurrentND_Display;
            set => Set(ref _CurrentND_Display, value);
        }


        // Масштаб отображения ND дисплея
        private double _ND_Scale;
        public double ND_Scale
        {
            get => _ND_Scale;
            set => Set(ref _ND_Scale, value);
        }

        // Демонстрация посадки самолета
        private DemoAnimationViewModel _AnimationViewModel;
        private DemoAnimationViewModel AnimationViewModel
        {
            get => _AnimationViewModel;
            set => Set(ref _AnimationViewModel, value);
        }

        #endregion

        #region Commands

        #region Switch ND Display
        public ICommand SwitchNDDisplayCommand { get; }
        private bool CanSwitchNDDisplayCommandExecute(object p) => true;
        private void OnSwitchNDDisplayCommandExecuted(object p)
        {
            var display = (ND_Overlay)Enum.Parse(typeof(ND_Overlay), (string)p);
            CurrentND_Display = displaySources[display];
        }
        #endregion

        #region Switch ROW Execution Mode
        public ICommand SwitchROWExecutionModeCommand { get; }
        private bool CanSwitchROWExecutionModeCommandExecute(object p) => true;
        private void OnSwitchROWExecutionModeCommandExecuted(object p)
        {
            var mode = (ROWExecutionMode) Enum.Parse(typeof(ROWExecutionMode), p.ToString());
            if (mode != ROWExecutionMode.PerfectLanding)
                EnableROPModes = true;
            else
            {
                EnableROPModes = false;
                SelectedROPExectionMode = null;
            }
            SelectedROWExectionMode = mode;
        }

        #endregion

        #region Switch ROP Execution Mode
 
        public ICommand SwitchROPExecutionModeCommand { get; }
        private bool CanSwitchROPExecutionModeCommandExecute(object p) => EnableROPModes;
        private void OnSwitchROPExecutionModeCommandExecuted(object p)
        {
            var mode = (ROPExecutionMode)Enum.Parse(typeof(ROPExecutionMode), p.ToString());
            SelectedROPExectionMode = mode;
        }

        #endregion

        #region Execute Demo

        public ICommand ExecuteDemoCommand { get; }
        private bool CanExecuteDemoCommandExecute(object p) => true;
        private void OnExecuteDemoCommandExecuted(object p)
        {
            AnimationViewModel.ROWExecutionMode = SelectedROWExectionMode;
            AnimationViewModel.Run();
        }

        #endregion

        #endregion

        public ROPSViewModel()
        {
            displaySources[ND_Overlay.ROSE] = new Uri("pack://application:,,,/Resources/ND_Display/ROSE_UI.png");
            displaySources[ND_Overlay.ARC] = new Uri("pack://application:,,,/Resources/ND_Display/ARC_UI.png");
            displaySources[ND_Overlay.PLAN] = new Uri("pack://application:,,,/Resources/ND_Display/PLAN_UI.png");
            _CurrentND_Display = displaySources[ND_Overlay.ROSE];
            _AnimationViewModel = new DemoAnimationViewModel(
                SelectedROWExectionMode, 
                SelectedROPExectionMode);
            // TODO: Figure out a way to disable a Play button while playing animation

            SwitchNDDisplayCommand = new RelayCommand(
                OnSwitchNDDisplayCommandExecuted,
                CanSwitchNDDisplayCommandExecute
            );

            SwitchROWExecutionModeCommand = new RelayCommand(
                 OnSwitchROWExecutionModeCommandExecuted,
                 CanSwitchROWExecutionModeCommandExecute
            );

            SwitchROPExecutionModeCommand = new RelayCommand(
                 OnSwitchROPExecutionModeCommandExecuted,
                 CanSwitchROPExecutionModeCommandExecute
            );

            ExecuteDemoCommand = new RelayCommand(
                OnExecuteDemoCommandExecuted,
                CanExecuteDemoCommandExecute
            );
        }

    }
}
