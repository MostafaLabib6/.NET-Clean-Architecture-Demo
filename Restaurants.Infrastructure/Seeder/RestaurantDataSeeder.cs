using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeder;

public interface IRestaurantDataSeeder
{
    Task SeedAsync();
}

internal class RestaurantDataSeeder(RestaurantDb context) : IRestaurantDataSeeder
{
    public async Task SeedAsync()
    {
        if (!await context.Restaurants.AnyAsync())
        {
            var restaurants = GetRestaurants();
            context.Restaurants.AddRange(restaurants);
            
            await context.SaveChangesAsync();
        }
    }


    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                Email = "contact@kfc.com",
                HasDelivery = true,
                PhoneNumber = "+44 20 1234 5678",
                Dishes =
                [
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Email = "contact@mcdonald.com",
                HasDelivery = true,
                PhoneNumber = "+44 20 1234 5678",
                Address = new Address()
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