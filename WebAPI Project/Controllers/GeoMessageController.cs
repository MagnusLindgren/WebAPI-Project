using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Route("api/geo-comments")]
    [ApiController]
    public class GeoMessageController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoMessage>> GetGeoComment(int id)
        {
            var message = await
            return Ok();
        }
    }
}
