using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Models.V1;

namespace WebAPI_Project.Models
{
    namespace V2
    {
        public class GetMessageDTO
        {
            public MessageDTO Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
        public class MessageDTO
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Author { get; set; }

        }
        public class AddMessageDTO
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Author { get; set; }
        }


    }

    namespace V1
    {
        public class GeoMessageDTO 
        {
            public string Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public GeoMessage ToModel(User user)
            {
                return new GeoMessage
                {
                    Author = $"{user.FirstName} {user.LastName}",
                    Body = this.Message,
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
        public class GeoMessage
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public string Author { get; set; }
            public string Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }    
}
