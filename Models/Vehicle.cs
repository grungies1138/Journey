using SNC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.Models
{
    // Abstract classed used for setting and, in some cases, assigning variables to the Vehicle subclasses.
    public abstract class Vehicle
    {
        public int CurrentSpeed { get; set; }
        public int CurrentBearing { get; set; }
        public List<Waypoint> Waypoints { get; set; }

        public string Identifier { get; set; }
        public string Descriptor { get; set; }
        public double Weight
        {
            get
            {
                return RandomNumberGenerator.GenerateRandomFloat(1000, 20000);
            }
        }

        public double Width
        {
            get
            {
                return RandomNumberGenerator.GenerateRandomFloat(10, 200);
            }
        }

        public double Height 
        { 
            get
            {
                return RandomNumberGenerator.GenerateRandomFloat(10, 100);
            }
        }
        public double Length
        {
            get
            {
                return RandomNumberGenerator.GenerateRandomFloat(20, 200);
            }
        }
    }
}
