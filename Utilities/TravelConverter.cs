using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.Utilities
{
    public static class TravelConverter
    {
        public const double FPS = 1.466667;
        public static double FeetPerSecond(int mph)
        {
            return mph * FPS;
        }
    }
}
