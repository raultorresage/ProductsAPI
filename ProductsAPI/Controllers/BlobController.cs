using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Attributes;
using ProductsAPI.Models;

[ApiController]
[Route("api/blob")]
[Produces("application/json")]
[Tracker]
[Auth]
public class BlobController : ControllerBase
{
    private readonly BlobStorageService _blobStorageService;

    public BlobController(BlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [HttpPost("{jwt}/upload")]
    public async Task<IActionResult> UploadBlob(IFormFile file, [FromRoute] string jwt)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload a valid file.");
        }

        string? userId = Jwt.GetUserIdFromToken(jwt);

        if (userId == null)
        {
            return BadRequest("Invalid token.");
        }

        using (var stream = file.OpenReadStream())
        {
            var uri = await _blobStorageService.UploadBlobAsync($"{userId}.{Guid.NewGuid().ToString()}", stream);
            return Ok(new { BlobUri = uri });
        }
    }

    [HttpGet("{jwt}/download/{fileName}")]
    public async Task<IActionResult> DownloadBlob(string fileName)
    {
        var blobStream = await _blobStorageService.DownloadBlobAsync(fileName);
        return File(blobStream, "application/octet-stream", fileName);
    }

    [HttpDelete("{jwt}/delete/{fileName}")]
    public async Task<IActionResult> DeleteBlob(string fileName)
    {
        await _blobStorageService.DeleteBlobAsync(fileName);
        return NoContent();
    }
}
