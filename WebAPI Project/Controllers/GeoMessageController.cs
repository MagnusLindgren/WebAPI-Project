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
   
    [ApiController]
    [Route("api/[controller]")]
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
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoMessageDTO>>> Get()
        {       
            return await _context.GeoMessages.Select(m => m.GeoMessDTO()).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<GeoMessageDTO>> PostGeoComment(GeoMessageDTO geoMessageDTO)
        {
            var geoMessage = geoMessageDTO.ToModel();
            _context.GeoMessages.Add(geoMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeoComment", new { id = geoMessage.Id }, geoMessageDTO);
        }
    }
}
