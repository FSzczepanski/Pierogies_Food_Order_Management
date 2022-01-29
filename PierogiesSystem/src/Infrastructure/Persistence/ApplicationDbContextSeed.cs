using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace CleanArchitecture.Infrastructure.Persistence
{
    using System.Collections.Generic;

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(ApplicationDbContext context)
        {
            if (!context.MyUsers.Any())
            {
                var seeds = new List<User>
                {

                    new()
                    {
                        FirstName = "Admin", LastName = "Folwark", Username = "AdminFolwark",
                        PasswordHash = BC.HashPassword("admin.2021")
                    },
                    new()
                    {
                        FirstName = "Test", LastName = "User", Username = "Admin",
                        PasswordHash = BC.HashPassword("admin.2021")
                    },
                };

                
                await context.MyUsers.AddRangeAsync(seeds);
                await context.SaveChangesAsync();
            }
        }
        
        public static async Task SeedDefaultSystemSettings(ApplicationDbContext context)
        {
            if (!context.SystemSettings.Any())
            {
                var seed = new SystemSettings()
                {
                    Name = "Folwark Tumiany Pokoje & Restauracja",
                    Description = "Restauracja na Warmii!",
                    Nip = "",
                    PhoneNumber = "508128336",
                    Location = new Location()
                    {
                        Name = "Folwark Tumiany", Description = "", Street = "Tumiany 7", CityName = "Tumiany",
                        CountryName = "Polska", IsDefault = true, ZipCode = "11-010"
                    },
                    MaxKmFromLocation = 50,
                    GlobalDeliveryPrice = 20
                };


                context.SystemSettings.Add(seed);
                await context.SaveChangesAsync();
            }
        }
        
    }
}