using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Data
{
    public static class ImportVehicles
    {
        public static List<VehicleData> ImportData()
        {
            using (StreamReader r = new StreamReader(@"Data\data.json"))
            {
                string json = r.ReadToEnd();
                List<VehicleData> items = JsonConvert.DeserializeObject<List<VehicleData>>(json);
                return items;
            }
        }
    }
}
