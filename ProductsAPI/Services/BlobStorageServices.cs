using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public class BlobStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobStorageService(BlobContainerClient blobContainerClient)
    {
        _blobContainerClient = blobContainerClient;
    }

    public async Task<string> UploadBlobAsync(string fileName, Stream fileStream)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "application/octet-stream" });
        return blobClient.Uri.ToString();
    }

    public async Task<Stream> DownloadBlobAsync(string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        BlobDownloadInfo download = await blobClient.DownloadAsync();
        return download.Content;
    }

    public async Task DeleteBlobAsync(string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();
    }
}