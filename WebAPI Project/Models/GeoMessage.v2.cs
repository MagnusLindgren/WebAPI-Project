using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Project.Models
{
    namespace v2
    {
        public class GeoMessage : GeoMessageDTO
        {
            public int Id { get; set; }
        }
        public class GeoMessageDTO
        {
            public string Body { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public GeoMessage ToModel()
            {
                return new GeoMessage
                {
                    Body = this.Body,
                    Title = this.Title,
                    Author = this.Author,
                    Longitude = this.Longitude,
                    Latitude = this.Latitude,

                };
            }
            public GeoMessageDTO GeoMessDTO()
            {
                return new GeoMessageDTO
                {
                    Body = this.Body,
                    Title = this.Title,
                    Author = this.Author,
                    Longitude = this.Longitude,
                    Latitude = this.Latitude,
                };
            }

        }
    }
}
