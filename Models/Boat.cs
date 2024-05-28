using Journey.Data;
using SNC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.Models
{
    internal class Boat : Vehicle, IVehicle
    {
        public readonly int lowerSpeedLimit;
        public readonly int upperSpeedLimit;
        public readonly int maximumTurn;

        public Power Power { get; set; }
        public double Draft { get; set; }
        public string Manufacturer { get; set; }
        public Zone Zone { get; set; }

        public Boat(string descriptor, double draft, string manufacturer, VehicleData vehicleData, Zone zone)
        {
            Descriptor = descriptor;
            Power = (Power)vehicleData.Power; // For brevity, allowing nulls here. Normally it would be addressed.
            Draft = draft;
            Manufacturer = manufacturer;
            Identifier = "BOAT";
            lowerSpeedLimit = vehicleData.MinimumSpeed;
            upperSpeedLimit = vehicleData.MaximumSpeed;
            maximumTurn = vehicleData.MaximumTurn;
            Waypoints = [];
            Zone = zone;
        }

        // Being the data gathering and processing
        public void Start()
        {
            // Get random number of waypoints to collect
            var numWaypoints = RandomNumberGenerator.GenerateRandomInteger(10, 30);

            for (int i = 0; i < numWaypoints; i++)
            {
                GetSpeed();
                GetBearing();

                Waypoint newWaypoint;
                if (i == 0)
                {
                    // Get starting Waypoint
                    newWaypoint = GetWaypoint(0, 0, 0, 0);
                }
                else
                {
                    // Get last recorded waypoint for comparison and distance tracking
                    var lastWaypoint = Waypoints.Last();
                    var time = TravelTime();
                    var distance = TravelConverter.FeetPerSecond(CurrentSpeed) * time;
                    newWaypoint = GetWaypoint(lastWaypoint.Latitude, lastWaypoint.Longitude, distance, time);
                }

                Waypoints.Add(newWaypoint);
            }

            Export();
        }

        // Get random speed between the upper an dlower speed limits.
        public void GetSpeed()
        {
            CurrentSpeed = RandomNumberGenerator.GenerateRandomInteger(lowerSpeedLimit, upperSpeedLimit);
        }

        // Get a random bearing, compensating for rolling over or under the 360 degree limits.
        public void GetBearing()
        {
            if (CurrentBearing == 0)
            {
                CurrentBearing = RandomNumberGenerator.GenerateRandomInteger(0, 360);
            }
            else
            {
                var newBearing = RandomNumberGenerator.GenerateRandomInteger(CurrentBearing - maximumTurn, CurrentBearing + maximumTurn);
                if (newBearing < 0)
                {
                    CurrentBearing = 360 + newBearing;
                }
                else if (newBearing > 360)
                {
                    CurrentBearing = newBearing - 360;
                }
                else
                {
                    CurrentBearing = newBearing;
                }

            }
        }

        // Generate and return a random number of seconds for travel between waypoints
        public double TravelTime() => RandomNumberGenerator.GenerateRandomFloat(1, 100);

        // Get the new waypoint based on bearing and speed and return Waypoint object with time Delta.
        public Waypoint GetWaypoint(double latitude, double longitude, double distance, double time)
        {
            if (latitude == 0 && longitude == 0)
            {
                latitude = RandomNumberGenerator.GenerateRandomFloat(Zone.LowerLatitude, Zone.UpperLatitude);
                longitude = RandomNumberGenerator.GenerateRandomFloat(Zone.LowerLongitude, Zone.UpperLongitude);
            }
            GeoCalc.GetEndingCoordinates(latitude, longitude, CurrentBearing, distance, out double newLatitude, out double newLongitude);

            var lastWaypoint = Waypoints.LastOrDefault();

            double delta = lastWaypoint is not null ? lastWaypoint.Delta : 0;
            var newWaypoint = new Waypoint
            {
                Latitude = newLatitude,
                Longitude = newLongitude,
                Delta = delta + time
            };

            return newWaypoint;
        }

        // Save results to a boat.jny file
        public void Export()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using StreamWriter outputFile = new(Path.Combine(docPath, $"{Identifier.ToLower()}.jny"));
            outputFile.WriteLine($"{Identifier},{Descriptor},{Weight},{Width},{Height},{Length},{Power},{Draft},{Manufacturer}");

            foreach (var waypoint in Waypoints)
            {
                outputFile.WriteLine(waypoint.ToString());
            }
        }
    }
}
