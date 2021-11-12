using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RunwaySystems.Models
{
    class Plane : INotifyPropertyChanged
    {
        private double x;
        private double y;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public double X { get; }
        public double Y { get; }

        public Plane(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void Update(double x, double y)
        {
            this.x = x;
            this.y = y;
            OnPropertyChanged("position");
        }
    }
}
