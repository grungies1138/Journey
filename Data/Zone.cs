using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Data
{
    public class Zone
    {
        public string Name { get; set; }
        public double LowerLatitude { get; set; }
        public double UpperLatitude { get; set; }
        public double LowerLongitude { get; set; }
        public double UpperLongitude { get; set; }
    }
}
