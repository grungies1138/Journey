using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.Models
{
    public class Waypoint
    {
        public double  Latitude { get; set; }
        public double Longitude { get; set; }
        public double Delta { get; set; }

        public override string ToString()
        {
            return $"{Latitude},{Longitude},{Delta}";
        }
    }
}
