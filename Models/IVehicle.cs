using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.Models
{
    // Interface used for setting methods required in all vehicle classes
    public interface IVehicle
    {
        void Start();
        void GetSpeed();
        void GetBearing();
        double TravelTime();
        Waypoint GetWaypoint(double latitude, double longitude, double distance, double time);
        void Export();

    }
}
