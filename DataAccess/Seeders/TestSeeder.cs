using Domain.Lookup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seeders;

internal class TestSeeder 
{
    public void TestMetod()
    {
        string jsonData = string.Empty;
        var apartmentTypes = JsonConvert.DeserializeObject<List<ApartmentType>>(jsonData);
    }
}
