using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeder;

public interface IRestaurantDataSeeder
{
    Task SeedAsync();
}

internal class RestaurantDataSeeder(RestaurantDbContext context) : IRestaurantDataSeeder
{
    public async Task SeedAsync()
    {
        if (context.Database.GetPendingMigrations().Any())
        {
            await context.Database.MigrateAsync();
        }

        if (!await context.Restaurants.AnyAsync())
        {
            var restaurants = GetRestaurants();
            context.Restaurants.AddRange(restaurants);

            await context.SaveChangesAsync();
        }

        if (!context.Roles.Any())
        {
            var roles = Enum.GetValues<RolesEnum>()
                .Select(role => new IdentityRole()
                {
                    Name = role.ToString(),
                    NormalizedName = role.ToString().ToUpper()
                })
                .ToList();
            ;
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }
    }


    private IEnumerable<Restaurant> GetRestaurants()
    {
        var owner = new User()
        {
            Email = "seed-data@admin.com",
        };
        List<Restaurant> restaurants =
        [
            new()
            {
                Owner = owner,
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                Email = "contact@kfc.com",
                HasDelivery = true,
                PhoneNumber = "+44 20 1234 5678",
                Dishes =
                [
                    new Dish
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M
                    },

                    new Dish
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M
                    }
                ],
                Address = new Address
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new()
            {
                Owner = owner,
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Email = "contact@mcdonald.com",
                HasDelivery = true,
                PhoneNumber = "+44 20 1234 5678",
                Address = new Address
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];

        return restaurants;
    }
}