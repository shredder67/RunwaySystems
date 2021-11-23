using RunwaySystems.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
        public ROWExecutionMode SelectedROWExecutionMode
        {
            get => _SelectedROWExecutionMode;
            set => Set(ref _SelectedROWExecutionMode, value);
        }

        private ROPExecutionMode? _SelectedROPExecutionMode;
        public ROPExecutionMode? SelectedROPExecutionMode
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
        private double _ND_Scale = 4;
        public double ND_Scale
        {
            get => _ND_Scale;
            set => Set(ref _ND_Scale, value);
        }

        private bool _IsPlaying;
        public bool IsPlaying
        {
            get => _IsPlaying;
            set => Set(ref _IsPlaying, value);
        }

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
            {
                EnableROPModes = true;
                _SelectedROPExecutionMode = ROPExecutionMode.MaxBreakingMaxReverse;
            }
            else
            {
                EnableROPModes = false;
                SelectedROPExecutionMode = null;
            }
            SelectedROWExecutionMode = mode;
        }

        #endregion

        #region Switch ROP Execution Mode
 
        public ICommand SwitchROPExecutionModeCommand { get; }
        private bool CanSwitchROPExecutionModeCommandExecute(object p) => EnableROPModes;
        private void OnSwitchROPExecutionModeCommandExecuted(object p)
        {
            var mode = (ROPExecutionMode)Enum.Parse(typeof(ROPExecutionMode), p.ToString());
            SelectedROPExecutionMode = mode;
        }

        #endregion

        #region Play Demo Animation

        public ICommand ExecuteDemoCommand { get; }

        private bool CanExecuteDemoCommandExecute(object p) => true;
        private void OnExecuteDemoCommandExecuted(object p) => playDemo();

        #endregion

        #endregion

        public ROPSViewModel()
        {
            displaySources[ND_Overlay.ROSE] = new Uri("pack://application:,,,/Resources/ND_Display/ROSE_UI.png");
            displaySources[ND_Overlay.ARC] = new Uri("pack://application:,,,/Resources/ND_Display/ARC_UI.png");
            displaySources[ND_Overlay.PLAN] = new Uri("pack://application:,,,/Resources/ND_Display/PLAN_UI.png");
            _CurrentND_Display = displaySources[ND_Overlay.ROSE];
            _dTimer.Interval = TimeSpan.FromMilliseconds(15);
            _dTimer.Tick += new EventHandler(DiagonalMoveUpdate);


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

        #region Demo Logic

        private static readonly Point startingPosition = new Point(40, 155);
        private static readonly Point rowToRopPosition = new Point(230, 25);
        private static readonly Point shortStopPosition = new Point(450, 25);
        private static readonly Point midStopPosition = new Point(540, 25);
        private static readonly Point farStopPosition = new Point(615, 25);

        private void playDemo()
        {
            

            _start = startingPosition;
            _mid = rowToRopPosition;

            if (SelectedROWExecutionMode == ROWExecutionMode.PerfectLanding)
                _end = shortStopPosition;
            else if (SelectedROPExecutionMode == ROPExecutionMode.KeepMaxReverse)
                _end = farStopPosition;
            else
                _end = midStopPosition;

            _timeCounter = 0;
            _animationSpeed = 4;
            IsPlaying = true;
            PFD_Message = ROWExecutionModeToMessage();
            _dTimer.Start();
        }

        private DispatcherTimer _dTimer = new DispatcherTimer();
        private double _animationSpeed;
        private double _timeCounter;
        private Point _start, _mid, _end;
        private void DiagonalMoveUpdate(object sender, EventArgs e)
        {
            if (PlanePositionX >= _mid.X && PlanePositionY <= _mid.Y)
            {
                _dTimer.Tick -= DiagonalMoveUpdate;
                _dTimer.Tick += HorizontalMoveUpdate;
                _timeCounter = 0;
                PFD_Message = ROPExecutionModeToMessage();
                return;
            }

            _timeCounter += _dTimer.Interval.TotalSeconds;
            var interpol = LinearInterpol(_start, _mid, _timeCounter / _animationSpeed);
            this.PlanePositionX = interpol.X;
            this.PlanePositionY = interpol.Y;
        }

        private void HorizontalMoveUpdate(object sender, EventArgs e)
        {
            if (PlanePositionX >= _end.X)
            {
                _dTimer.Stop();
                _dTimer.Tick += DiagonalMoveUpdate;
                _dTimer.Tick -= HorizontalMoveUpdate;
                IsPlaying = false;
                _timeCounter = 0;
                PFD_Message = "";

                Task.Delay(1000).Wait();
                
                PlanePositionX = startingPosition.X;
                PlanePositionY = startingPosition.Y;
                
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

        private string ROWExecutionModeToMessage()
        {
            if (SelectedROWExecutionMode == ROWExecutionMode.PerfectLanding)
                return "";
            if (SelectedROWExecutionMode == ROWExecutionMode.IfWetRWYTooShort)
                return "IF WET: RWY TOO SHORT";
            return "RUNWAY TOO SHORT";
        }
        private string ROPExecutionModeToMessage()
        {
            if (SelectedROPExecutionMode == ROPExecutionMode.MaxBreakingMaxReverse)
                return "MAX BRAKE\nMAX REVERSE";
            if (SelectedROPExecutionMode == ROPExecutionMode.KeepMaxReverse)
                return "MAX BRAKE\nMAX REVERSE";
            return "";
        }

        #endregion
    }
}
