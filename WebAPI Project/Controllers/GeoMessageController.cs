﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Data;
using WebAPI_Project.Models;
using WebAPI_Project.Models.V1;
using WebAPI_Project.Models.V2;
//using GeoMessageDTO = WebAPI_Project.Models.V1.GeoMessageDTO;



namespace WebAPI_Project.Controllers
{
    namespace V2
    {
        [ApiController]
        [ApiVersion("2.0")]
        [Route("api/v{version:apiVersion}/geo-comments")]
        
        public class GeoMessageController : ControllerBase
        {
            private readonly GeoMessageDbContext _context;
            private readonly UserManager<User> _userManager;
            public GeoMessageController(GeoMessageDbContext context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            // GET api/Geomessage/{id}
            /// <summary>
            /// Gets a specific comment based on id
            /// </summary>
            /// <param name="id">id represents wich comment to get</param>
            /// <returns>Returns a JSON object with a specific comment</returns>
            [HttpGet("{id}")]
            public async Task<ActionResult<GetMessageDTO>> GetGeoComment(int id)
            {
                var geoTag = await _context.GeoMessages.FirstOrDefaultAsync(g => g.Id == id);

                if (geoTag == null)
                {
                    return NoContent();
                }

                var geoMessageDto = new GetMessageDTO
                {
                    Message = new MessageDTO { Title = geoTag.Title, Body = geoTag.Body, Author = geoTag.Author },
                    Latitude = geoTag.Latitude,
                    Longitude = geoTag.Longitude
                };

                CheckTitle(geoMessageDto);

                return Ok(geoMessageDto);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<GetMessageDTO>>> Get()
            {
                var geoTags = await _context.GeoMessages.Select(m =>
                    new GetMessageDTO
                    {
                        Message = new MessageDTO { Title = m.Title, Body = m.Body, Author = m.Author },
                        Latitude = m.Latitude,
                        Longitude = m.Longitude
                    }
                    ).ToListAsync();

                foreach (var item in geoTags)
                {
                    CheckTitle(item);
                }

                return geoTags;
            }

            //Kollar om titel är null och lägger isåfall till titel från första meningen i body
            static GetMessageDTO CheckTitle(GetMessageDTO check)
            {
                if (check.Message.Title == null)
                {
                    check.Message.Title = check.Message.Body.Split(new[] { '.' }).FirstOrDefault();
                }
                return check;
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
            private readonly UserManager<User> _userManager;
            public GeoMessageController(GeoMessageDbContext context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
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
                var geoTag = await _context.GeoMessages.FirstOrDefaultAsync(g => g.Id == id);

                if (geoTag == null)
                {
                    return NotFound();
                }

            var geoMessageDto = new GeoMessageDTO
            {
                Message = geoTag.Body,
                Latitude = geoTag.Latitude,
                Longitude = geoTag.Longitude
            };

                return Ok(geoMessageDto);
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
                return await _context.GeoMessages.Select(m => 
                    new GeoMessageDTO
                    {
                        Message = m.Body,
                        Longitude = m.Longitude,
                        Latitude = m.Latitude
                    }
                    ).ToListAsync();
            }
            /// <summary>
            /// Post a message to the specific location. Need to be logged in to post
            /// </summary>
            /// <param name="geoMessageDTO"></param>
            /// <returns></returns>
            [Authorize]
            // POST api/Geomessage
            [HttpPost]
            public async Task<ActionResult<GeoMessageDTO>> PostGeoComment(GeoMessageDTO geoMessageDTO)
            {
                var user = await _userManager.GetUserAsync(this.User);

                var geoMessage = geoMessageDTO.ToModel(user);
                _context.GeoMessages.Add(geoMessage);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGeoComment", new { id = geoMessage.Id }, geoMessageDTO);
            }
        }
    }
}
