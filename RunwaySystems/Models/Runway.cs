using System;
using System.Collections.Generic;
using System.Text;

namespace RunwaySystems.Models
{
    class Runway
    {
        private double length;
        private Plane landingPlane;

        public double Length { get; }

        public Runway(double length, Plane plane)
        {
            this.length = length;
            landingPlane = plane;
        }
    }
}
