using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RunwaySystems.ViewModels
{
    //Base ViewModel class
    public class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //Handle property update closure
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (field.Equals(value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //private bool canDispose = true;
        //protected bool Dispose()
        //{
        //    return true;
        //}
    }
   
}
