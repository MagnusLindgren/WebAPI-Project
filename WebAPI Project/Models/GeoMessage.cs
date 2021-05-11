using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Project.Models
{
    namespace V2
    {
        public class GeoMessage : GeoMessageDTO
        {          
            public int Id { get; set; }
        }
        public class Message
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Author { get; set; }
        }
        public class GeoMessageDTO
        {
            public Message Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
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
