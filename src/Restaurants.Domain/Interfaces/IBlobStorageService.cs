namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadAsync(Stream file, string fileName);
    Task<string?> GetSaSToken(string? blobUrl);
}