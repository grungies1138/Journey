﻿using Journey.Data;
using SNC.Utilities;
using System.ComponentModel.DataAnnotations;


namespace SNC.Models
{
    public class Car : Vehicle, IVehicle
    {
        public readonly int lowerSpeedLimit;
        public readonly int upperSpeedLimit;
        public readonly int maximumTurn;

        public string Manufacturer { get; set; }
        public uint ModelYear { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public Fuel Fuel { get; set; }

        public Car(string descriptor, string manufacturer, uint modelYear, BodyStyle bodyStyle, VehicleData vehicleData) 
        {
            this.Descriptor = descriptor;
            this.Manufacturer = manufacturer;
            this.ModelYear = modelYear;
            this.BodyStyle = bodyStyle;
            this.Fuel = (Fuel)vehicleData.Fuel; // For brevity, allowing nulls here. Normally would be addressed.
            this.Identifier = "CAR";
            Waypoints = [];
            lowerSpeedLimit = vehicleData.MinimumSpeed; 
            upperSpeedLimit = vehicleData.MaximumSpeed;
            maximumTurn = vehicleData.MaximumTurn;
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
                latitude = RandomNumberGenerator.GenerateRandomFloat(-90, 90);
                longitude = RandomNumberGenerator.GenerateRandomFloat(-180, 180);
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

        // Save results to a car.jny file
        public void Export()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using StreamWriter outputFile = new(Path.Combine(docPath, $"{Identifier.ToLower()}.jny"));
            outputFile.WriteLine($"{Identifier},{Descriptor},{Weight},{Width},{Height},{Length},{Manufacturer},{ModelYear},{BodyStyle},{Fuel}");

            foreach (var waypoint in Waypoints)
            {
                outputFile.WriteLine(waypoint.ToString());
            }
        }
    }
}
