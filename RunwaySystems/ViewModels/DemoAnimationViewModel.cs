using System;
using System.Threading;

namespace RunwaySystems.ViewModels
{
    class DemoAnimationViewModel : ViewModel
    {

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

        public DemoAnimationViewModel(ROWExecutionMode rOWExecutionMode, ROPExecutionMode? rOPExecutionMode = null)
        {
            this.ROWExecutionMode = rOWExecutionMode;
            this.ROPExecutionMode = rOPExecutionMode;
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

        public int AnimationSpeed { get; private set; }

        public void Run()
        {
            StartTimer();

        }

        private void StartTimer()
        {
            StopTimer();
            Timer = new Timer(x => Timer_Tick(), null, 0, 100 / AnimationSpeed);
        }

        private void Timer_Tick()
        {
            throw new NotImplementedException();
        }

        private void StopTimer()
        {
            if (Timer != null)
            {
                Timer.Dispose();
                Timer = null;
            }
        }

    }
}
