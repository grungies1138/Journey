using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Data
{
    public class ImportZones
    {
        public static List<Zone> ImportData()
        {
            using (StreamReader r = new StreamReader(@"Data\boatZones.json"))
            {
                string json = r.ReadToEnd();
                List<Zone> items = JsonConvert.DeserializeObject<List<Zone>>(json);
                return items;
            }
        }
    }
}
