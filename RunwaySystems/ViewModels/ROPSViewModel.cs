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
    public enum ExecutionMode
    {
        PerfectLanding,
        IfWetRWYTooShort,
        RWYTooShort,
        MaxBreakingMaxReverse,
        KeepMaxReverse
    }

    public class ROPSViewModel : ViewModel
    {
        #region Fields

        // Режим проигрывания анимации
        private ExecutionMode _selectedExecutionMode;
        public ExecutionMode SelectedExectionMode
        {
            get => _selectedExecutionMode;
            set => Set(ref _selectedExecutionMode, value);
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

        #region Switch Execution Mode
        // TODO: Finish Execution Mode Selection
        public ICommand SwitchExecutionModeCommand { get; }
        private bool CanSwitchExecutionModeCommandExecute(object p)
        {
            return true;
        }
        private void OnSwitchExecutionModeCommandExecuted(object p)
        {

        }

        #endregion

        #region Execute Demo

        public ICommand ExecuteDemoCommand { get; }
        private bool CanExecuteDemoCommandExecute(object p) => true;
        private void OnExecuteDemoCommandExecuted(object p)
        {
            AnimationViewModel.ExecutionMode = SelectedExectionMode;
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
            _AnimationViewModel = new DemoAnimationViewModel(SelectedExectionMode);
            // TODO: Figure out a way to disable a Play button while playing animation

            SwitchNDDisplayCommand = new RelayCommand(
                OnSwitchNDDisplayCommandExecuted,
                CanSwitchNDDisplayCommandExecute
            );

            SwitchExecutionModeCommand = new RelayCommand(
                 OnSwitchExecutionModeCommandExecuted,
                 CanSwitchExecutionModeCommandExecute
            );

            ExecuteDemoCommand = new RelayCommand(
                OnExecuteDemoCommandExecuted,
                CanExecuteDemoCommandExecute
            );
        }

    }
}
