using SNC.Data;
using SNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Data
{
    public class VehicleData
    {
        public VehicleType Type { get; set; }
        public int MinimumSpeed { get; set; }
        public int MaximumSpeed { get; set; }
        public int MaximumTurn { get; set; }
        public Power? Power { get; set; }
        public Fuel? Fuel { get; set; }
    }
}
