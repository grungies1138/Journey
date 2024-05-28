using Journey.Data;
using SNC.Models;
using SNC.Utilities;

namespace SNC;
public class Program
{
    static void Main(string[] args)
    {
        // Import needed data files
        List<VehicleData> vehicles = ImportVehicles.ImportData();
        List<Zone> zones = ImportZones.ImportData();
        Console.WriteLine("Importing Vehicle data.");

        // Collect Car data from imported files, instantiate and begin processing and export
        var carData = vehicles.Where(x => x.Type == Data.VehicleType.CAR).FirstOrDefault();
        var car = new Car("Outback", "Suburu", 2024, BodyStyle.SUV, carData);
        Console.WriteLine("Exporting Car Waypoints.");
        car.Start();

        // Collect Boat data from imported files, instantiate and begin processing and export
        var boatZone = zones[RandomNumberGenerator.GenerateRandomInteger(0, 3)];
        var boatData = vehicles.Where(x => x.Type == Data.VehicleType.BOAT && x.Power == Power.SAIL).FirstOrDefault();
        var boat = new Boat("420 Inflatable", RandomNumberGenerator.GenerateRandomFloat(1, 10), "MINICAT", boatData, boatZone);
        Console.WriteLine($"Exporting Boat Waypoints in {boatZone.Name}");
        boat.Start();

        Console.WriteLine("Waypoint Generation complete.");
    }
}
