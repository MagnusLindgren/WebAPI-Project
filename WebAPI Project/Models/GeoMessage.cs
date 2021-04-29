using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Project.Models
{
    public class GeoMessage
    {
     
            public int id { get; set; }
            public string message { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
      
    }
}
