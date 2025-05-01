namespace Restaurants.Infrastructure.ServiceConfiguration;

public class BlogStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string ContainerName { get; set; } = default!;

    public string AccountKey { get; set; } = default!;
    public string AccountName { get; set; } = default!;
}