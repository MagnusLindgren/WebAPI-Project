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

        // GET api/Geomessage per id
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoMessageDTO>> GetGeoComment(int id)
        {
            var geoTag = await _context.GeoMessages.FindAsync(id);

            if (geoTag == null)
            {
                return NotFound();
            }

            return geoTag.GeoMessDTO();
        }
        //GET api/GeoMessage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoMessageDTO>>> Get()
        {       
            return await _context.GeoMessages.Select(m => m.GeoMessDTO()).ToListAsync();
        }
    }
}
