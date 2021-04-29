using Microsoft.AspNetCore.Identity;
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

        public async Task Seed(UserManager<User> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            User testuser = new User()
            {
                FirstName = "Tester",
                LastName = "Userson",               
            };
            await userManager.CreateAsync(testuser);

            GeoMessage message = new GeoMessage()
            {
                latitude = 123.2,
                longitude = 423.1,
                message = "This is a drill. Do not worry!"
            };
            await AddAsync(message);

            await SaveChangesAsync();
        }
    }   
}
