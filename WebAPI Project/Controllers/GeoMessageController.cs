using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Data;
using WebAPI_Project.Models;
using WebAPI_Project.Models.V1;
using WebAPI_Project.Models.v2;
using GeoMessageDTO = WebAPI_Project.Models.V1.GeoMessageDTO;



namespace WebAPI_Project.Controllers
{
    namespace V2
    {
        [ApiController]
        [ApiVersion("2.0")]
        [Route("api/v{version:apiVersion}/[controller]")]
        
        public class GeoMessageController : ControllerBase
        {
            // Testkod för att testa versionering
            private readonly GeoMessageDbContext _context;
            public GeoMessageController(GeoMessageDbContext context)
            {
                _context = context;
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Models.V2.GeoMessageDTO>> GetGeoComment(int id)
            {
                var geoTag = await _context.GeoMessages.FindAsync(id);

                if (geoTag == null)
                {
                    return NoContent();
                }

                return Ok(geoTag.GeoMessDTO());
            }
        }
    }

    namespace V1
    { 
        [ApiController]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/geo-comments")]
        public class GeoMessageController : ControllerBase
        {
            private readonly GeoMessageDbContext _context;
            public GeoMessageController(GeoMessageDbContext context)
            {
                _context = context;
            }

            // GET api/Geomessage/{id}
            /// <summary>
            /// Gets a specific comment based on id
            /// </summary>
            /// <param name="id">id represents wich comment to get</param>
            /// <returns>Returns a JSON object with a specific comment</returns>
            [HttpGet("{id}")]
            public async Task<ActionResult<GeoMessageDTO>> GetGeoComment(int id)
            {
                var geoTag = await _context.GeoMessages.FindAsync(id);

                if (geoTag == null)
                {
                    return NotFound();
                }

                return Ok(geoTag.GeoMessDTO());
            }
            // GET api/Geomessage
            /// <summary>
            /// Gets a list of geo-comments
            /// </summary>
            /// 
            /// <returns>Returns a JSON object with a list of geo-comments</returns>
            [HttpGet]
            public async Task<ActionResult<IEnumerable<GeoMessageDTO>>> Get()
            {       
                return await _context.GeoMessages.Select(m => m.GeoMessDTO()).ToListAsync();
            }

            [Authorize]
            // POST api/Geomessage
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
}
