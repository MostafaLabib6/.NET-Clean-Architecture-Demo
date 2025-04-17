namespace Restaurants.Infrastructure.Authorization.Constants;

public enum RestaurantClaimTypes
{
    Nationality,
    BirthDate,
}

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
}