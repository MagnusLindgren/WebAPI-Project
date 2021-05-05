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
        public DbSet<User> User { get; set; }

        public async Task Seed(UserManager<User> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            User testUser = new User()
            {
                UserName = "testUser",
                FirstName = "Tester",
                LastName = "Userson",
            };
            await userManager.CreateAsync(testUser, "Passw0rd!");

            GeoMessage message = new GeoMessage()
            {
                Latitude = 123.2,
                Longitude = 423.1,
                Message = "This is a drill. Do not worry!"
            };
            await AddAsync(message);

            await SaveChangesAsync();
        }
    }   
}
