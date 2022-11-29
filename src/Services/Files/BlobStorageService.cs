using System;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using BogusStore.Domain.Files;
using HeyRed.Mime;
using Microsoft.Extensions.Configuration;

namespace BogusStore.Services.Files;

public class BlobStorageService : IStorageService
{
    private readonly string connectionString;

    public Uri BasePath => new Uri("https://hogentdemostorage.blob.core.windows.net/images");

    public BlobStorageService(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("Storage");
    }

    public Uri GenerateImageUploadSas(Image image)
    {
        string containerName = "images";
        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = containerClient.GetBlobClient(image.Filename);

        var blobSasBuilder = new BlobSasBuilder
        {
            ExpiresOn = DateTime.UtcNow.AddMinutes(5),
            BlobContainerName = containerName,
            BlobName = image.Filename,
        };

        blobSasBuilder.SetPermissions(BlobSasPermissions.Create | BlobSasPermissions.Write);
        var sas = blobClient.GenerateSasUri(blobSasBuilder);
        return sas;
    }
}

