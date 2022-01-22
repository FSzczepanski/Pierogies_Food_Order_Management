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


                // var unseededSeeds = seeds
                //     .Where(user => !context.MyUsers.Any(u => u.Id == user.Id))
                //     .ToList();
                await context.MyUsers.AddRangeAsync(seeds);
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Items =
                    {
                        new TodoItem {Title = "Apples", Done = true},
                        new TodoItem {Title = "Milk", Done = true},
                        new TodoItem {Title = "Bread", Done = true},
                        new TodoItem {Title = "Toilet paper"},
                        new TodoItem {Title = "Pasta"},
                        new TodoItem {Title = "Tissues"},
                        new TodoItem {Title = "Tuna"},
                        new TodoItem {Title = "Water"}
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}