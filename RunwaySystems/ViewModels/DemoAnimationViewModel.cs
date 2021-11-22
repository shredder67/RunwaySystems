using RunwaySystems.Commands;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;

namespace RunwaySystems.ViewModels
{
    // Реализует проигрывание анимации исходя из заданных параметров
    class DemoAnimationViewModel : ViewModel
    {
        #region Fields

        private bool _IsPlaying;
        public bool IsPlaying { 
            get => _IsPlaying;
            set => Set(ref _IsPlaying, value); }

        private Timer _Timer;
        public Timer Timer
        {
            get => _Timer;
            set => Set(ref _Timer, value);
        }

        private int _PlanePositionX;
        public int PlanePositionX
        {
            get => _PlanePositionX;
            set => Set(ref _PlanePositionX, value);
        }

        private int _PlanePositionY;
        public int PlanePositionY
        {
            get => _PlanePositionY;
            set => Set(ref _PlanePositionY, value);
        }

        private ROWExecutionMode _ROWExecutionMode;
        public ROWExecutionMode ROWExecutionMode
        {
            get => _ROWExecutionMode;
            set => Set(ref _ROWExecutionMode, value); 
        }

        private ROPExecutionMode? _ROPExecutionMode;
        public ROPExecutionMode? ROPExecutionMode
        {
            get => _ROPExecutionMode;
            set => Set(ref _ROPExecutionMode, value);
        }

        #endregion

        #region Commands

        #region Execute Demo

        public ICommand ExecuteDemoCommand { get; }
        private bool CanExecuteDemoCommandExecute(object p) => !IsPlaying;
        private void OnExecuteDemoCommandExecuted(object p)
        {
            IsPlaying = true;
            //TODO: Launch animation
        }

        #endregion

        #endregion

        #region Constructor

        public DemoAnimationViewModel()
        {
            PlanePositionX = 40;
            PlanePositionY = 155;

            ExecuteDemoCommand = new RelayCommand(
                OnExecuteDemoCommandExecuted,
                CanExecuteDemoCommandExecute
            );
        }

        #endregion

        #region AnimationLogic

        private Point currentPosition;
        private readonly Point startingPosition = new Point(40, 155);
        private readonly Point rowToRopPosition = new Point(25, 230);
        private readonly Point shortStopPosition = new Point(40, 450);
        private readonly Point midStopPosition = new Point(40, 540);
        private readonly Point farStopPosition = new Point(40, 615);
        

        private void playDemo()
        {
            currentPosition = new Point(PlanePositionX, PlanePositionY);

        }

        #endregion


    }
}
