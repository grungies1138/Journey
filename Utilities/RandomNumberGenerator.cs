using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.Utilities
{
    public static class RandomNumberGenerator
    {
        public static double GenerateRandomFloat(double lower, double upper)
        {
            var random = new Random();
            var rDouble = random.NextDouble();
            var result = rDouble * (upper - lower) + lower;
            return result;
        }

        public static int GenerateRandomInteger(int lower, int upper)
        {
            var random = new Random();
            return random.Next(lower, upper+1);
        }
    }
}
