using RunwaySystems.Commands;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace RunwaySystems.ViewModels
{
    // Реализует проигрывание анимации исходя из заданных параметров
    class DemoViewModel : ViewModel
    {
        #region Fields

        private bool _IsPlaying;
        public bool IsPlaying { 
            get => _IsPlaying;
            set => Set(ref _IsPlaying, value); }

        private int _PlanePositionX = 40;
        public int PlanePositionX
        {
            get => _PlanePositionX;
            set => Set(ref _PlanePositionX, value);
        }

        private int _PlanePositionY = 155;
        public int PlanePositionY
        {
            get => _PlanePositionY;
            set => Set(ref _PlanePositionY, value);
        }

        private string _PFD_Message = "";
        public string PFD_Message
        {
            get => _PFD_Message;
            set => Set(ref _PFD_Message, value);
        }

        private ROWExecutionMode _ROW_MODE;
        public ROWExecutionMode ROW_MODE
        {
            get => _ROW_MODE;
            set => Set(ref _ROW_MODE, value); 
        }

        private ROPExecutionMode? _ROP_MODE;
        public ROPExecutionMode? ROP_MODE
        {
            get => _ROP_MODE;
            set => Set(ref _ROP_MODE, value);
        }

        #endregion

        #region Commands

        #region Execute Demo

        public ICommand ExecuteDemoCommand { get; }

        private bool CanExecuteDemoCommandExecute(object p) => true;
        private void OnExecuteDemoCommandExecuted(object p) => playDemo();
        #endregion

        #endregion

        #region Constructor

        public DemoViewModel()
        {
            ExecuteDemoCommand = new RelayCommand(
                OnExecuteDemoCommandExecuted,
                CanExecuteDemoCommandExecute
            );

            _dTimer.Interval = TimeSpan.FromMilliseconds(15);
            _dTimer.Tick += new EventHandler(DiagonalMoveUpdate);
        }

        #endregion

        #region AnimationLogic

        private static readonly Point startingPosition = new Point(40, 155);
        private static readonly Point rowToRopPosition = new Point(230, 25);
        private static readonly Point shortStopPosition = new Point(450, 25);
        private static readonly Point midStopPosition = new Point(540, 25);
        private static readonly Point farStopPosition = new Point(615, 25);

        private void playDemo()
        {
            PlanePositionX = startingPosition.X;
            PlanePositionY = startingPosition.Y;
            _animationSpeed = 2;
            IsPlaying = true;

            _start = startingPosition;
            _mid = rowToRopPosition;

            if (ROW_MODE == ROWExecutionMode.PerfectLanding)
                _end = shortStopPosition;
            else if (ROP_MODE == ROPExecutionMode.KeepMaxReverse)
                _end = farStopPosition;
            else
                _end = midStopPosition;

            _dTimer.Start();
        }

        private DispatcherTimer _dTimer = new DispatcherTimer();
        private double _animationSpeed;
        private double _timeCounter = 0;
        private Point _start, _mid, _end;
        private void DiagonalMoveUpdate(object sender, EventArgs e)
        {
            if(PlanePositionX >= _mid.X && PlanePositionY <= _mid.Y)
            {
                _dTimer.Tick -= DiagonalMoveUpdate;
                _dTimer.Tick += HorizontalMoveUpdate;
                _timeCounter = 0;
                return;
            }

            _timeCounter += _dTimer.Interval.TotalSeconds;
            var interpol = LinearInterpol(_start, _mid,  _timeCounter / _animationSpeed);
            this.PlanePositionX = interpol.X;
            this.PlanePositionY = interpol.Y;
        }

        private void HorizontalMoveUpdate(object sender, EventArgs e)
        {
            if (PlanePositionX >= _end.X)
            {
                _dTimer.Tick += DiagonalMoveUpdate;
                _dTimer.Stop();
                IsPlaying = false;
                PFD_Message = "";
                return;
            }

            _timeCounter += _dTimer.Interval.TotalSeconds;
            var interpol = LinearInterpol(_mid, _end, _timeCounter / _animationSpeed);
            this.PlanePositionX = interpol.X;
            this.PlanePositionY = interpol.Y;
        }

        private Point LinearInterpol(Point p1, Point p2, double t)
        {
            var res = new Point
            {
                X = Math.Clamp((int)(((1 - t) * p1.X) + t * p2.X), p1.X, p2.X),
                Y = Math.Clamp((int)(((1 - t) * p1.Y) + t * p2.Y), p2.Y, p1.Y)
            };
            return res;
        }

        #endregion

        #region ROPS Message Logic



        #endregion
    }
}
