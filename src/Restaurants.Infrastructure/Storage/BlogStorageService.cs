using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.ServiceConfiguration;

namespace Restaurants.Infrastructure.Storage;

internal class BlogStorageService(IOptions<BlogStorageSettings> blogSettings) : IBlobStorageService
{
    private readonly BlogStorageSettings _blobStorageSettings = blogSettings.Value;

    public async Task<string> UploadAsync(Stream file, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(file);

        var blobUrl = blobClient.Uri.ToString();

        return blobUrl;
    }

    public async Task<string?> GetSaSToken(string? blobUrl)
    {
        if(string.IsNullOrEmpty(blobUrl))
        {
            return null;
        }

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.ContainerName,
            BlobName = blobUrl.Split('/').Last(),
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };
        
        sasBuilder.SetPermissions(BlobSasPermissions.Read);
        
        var sasToken = sasBuilder.ToSasQueryParameters(
            new StorageSharedKeyCredential(_blobStorageSettings.AccountName, _blobStorageSettings.AccountKey));

        return $"{blobUrl}?{sasToken}";

    }
}