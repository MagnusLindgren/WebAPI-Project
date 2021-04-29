using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Models;

namespace WebAPI_Project.Data
{
    public class GeoMessageDbContext : IdentityDbContext<User>
    {
        public GeoMessageDbContext(DbContextOptions<GeoMessageDbContext> options) : base(options)
        {

        }

        public DbSet<GeoMessage> GeoMessages { get; set; }
    }
}
