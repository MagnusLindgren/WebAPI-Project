using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Models.V1;

namespace WebAPI_Project.Models
{
    namespace V2
    {
        public class GeoMessageV2 : GeoMessage
        {
            public new MessageDTO Message { get; set; }
        }

    }

    namespace V1
    {
        public class GeoMessage : GeoMessageDTO
        {
            public int Id { get; set; }
        }
        public class GeoMessageDTO
        {
            public string Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public GeoMessage ToModel()
            {
                return new GeoMessage
                {
                    Message = this.Message,
                    Longitude = this.Longitude,
                    Latitude = this.Latitude,
                };
            }
            public GeoMessageDTO GeoMessDTO()
            {
                return new GeoMessageDTO
                {
                    Message = this.Message,
                    Longitude = this.Longitude,
                    Latitude = this.Latitude,
                };
            }

        }
    }
}
