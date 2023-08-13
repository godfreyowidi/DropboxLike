using DropboxLike.Domain.Services.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FileController : BaseController
{
  private readonly IFileService _fileService;

  public FileController(IFileService fileService)
  {
    _fileService = fileService;
  }
  
  [HttpPost]
  [Route("Upload")]
  public async Task<IActionResult> UploadFileAsync(IFormFile file)
  {
    var userId = GetUserIdFromClaim();

    var response = await _fileService.UploadSingleFileAsync(file, userId);

    return StatusCode(response.StatusCode);
  }

  [HttpGet]
  [Route("Download/{fileId}")]
  public async Task<IActionResult> DownloadFileAsync(string fileId)
  {
    var userId = GetUserIdFromClaim();
    
    var response = await _fileService.DownloadSingleFileAsync(fileId);

    if (!response.IsSuccessful)
    {
      var message = $"Failed to download file with ID {fileId} due to '{response.FailureMessage ?? "<>"}'";
      return StatusCode(response.StatusCode, message);
    }
    var file = response.Value;
    return new FileStreamResult(file.FileStream, file.ContentType);
  }

  [HttpGet]
  [Route("List")]
  public async Task<IActionResult> ListFilesAsync()
  {
    // TODO: List files operation needs to be scoped to user, otherwise other users' files will be included in response.
    var userId = GetUserIdFromClaim();
    
    var response = await _fileService.ListBucketFilesAsync();
    
    return Ok(response.Value);
  }

  [HttpDelete]
  [Route("Delete/{fileId}")]
  public async Task<IActionResult> DeleteFileAsync(string fileId)
  {
    var userId = GetUserIdFromClaim();
    
    var response = await _fileService.DeleteSingleFileAsync(fileId);
    if (response.IsSuccessful) return NoContent();
    var message = $"Failed to delete file with ID {fileId} due to '{response.FailureMessage ?? "<>"}'";
    return StatusCode(response.StatusCode, message);
  }
}