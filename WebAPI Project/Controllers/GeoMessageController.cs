using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Data;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Route("api/v1/geo-comments")]
    [ApiController]
    public class GeoMessageController : ControllerBase
    {
        private readonly GeoMessageDbContext _context;
        public GeoMessageController(GeoMessageDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeoMessage>> GetGeoComment(int id)
        {
            var geoTag = await _context.GeoMessages.FindAsync(id);

            if (geoTag == null)
            {
                return NotFound();
            }

            return Ok(geoTag);
        }
        //GET api/GeoMessage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoMessage>>> Get()
        {
       
            return await _context.GeoMessages.ToListAsync();
        }
    }
}
