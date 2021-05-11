using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Project.Models;
using WebAPI_Project.Models.V1;
using WebAPI_Project.Models.V2;


namespace WebAPI_Project.Data
{
    public class GeoMessageDbContext : IdentityDbContext<User>
    {
        public GeoMessageDbContext(DbContextOptions<GeoMessageDbContext> options) : base(options)
        {

        }

        public DbSet<GeoMessage> GeoMessages { get; set; }
        //public DbSet<GeoMessageV2> GeoMessageV2 { get; set; }
        public DbSet<User> User { get; set; }

        public async Task Seed(UserManager<User> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            User testUser = new User
            {
                UserName = "testUser",
                FirstName = "Tester",
                LastName = "Userson",
            };
            await userManager.CreateAsync(testUser, "Passw0rd!");

            var message = new GeoMessage
            {
                Latitude = 57.69,
                Longitude = 12.85,
                Message = "This is a drill. Do not worry!"
            };
            await AddAsync(message);
            
            var messagev2 = new GeoMessage
            {
                Title = "Testtitle",
                Author = "Test Author",
                Body = "This is for testing V2",
                Longitude = 51.69,
                Latitude = 12.85
            };
            await AddAsync(messagev2);

            await SaveChangesAsync();
        }
    }   
}
