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

        public DemoAnimationViewModel(ExecutionMode ExecutionMode)
        {
            this.ExecutionMode = ExecutionMode;
        }

        private ExecutionMode _ExecutionMode;
        public ExecutionMode ExecutionMode
        {
            get => _ExecutionMode;
            set => Set(ref _ExecutionMode, value); 
        }

        public void Run()
        {
            StartTimer();

        }

        private void StartTimer()
        {
            StopTimer();
            Timer = new Timer(x => Timer_Tick(), null, 0, 100 / AnimationSpeed);
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
