using DropboxLike.Domain.Services.Share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShareFileController : BaseController
{
    private readonly IShareFileService _shareFileService;

    public ShareFileController(IShareFileService shareFileService)
    {
        _shareFileService = shareFileService;
    }

    [HttpPost]
    [Route("share")]
    public async Task<IActionResult> ShareFileWithUserAsync([FromForm] string userId, [FromForm] string fileKey)
    {
        var response = await _shareFileService.ShareFileWithUserAsync(userId, fileKey);

        if (response.IsSuccessful) return Ok();
        
        var message = $"Failed to create shared file for user with ID {userId} and file with key {fileKey} due to '{response.FailureMessage ?? "<>"}'";
        return StatusCode(response.StatusCode, message);
    }

    [HttpGet]
    [Route("sharedfile")]
    public async Task<IActionResult> GetSharedFilesByUserIdAsync()
    {
        var userId = GetUserIdFromClaim();

        var response = await _shareFileService.GetSharedFilesByUserId(userId);
        
        if (response.IsSuccessful) return Ok(response.Value);
        
        var message = $"Failed to get shared files for user with ID {userId} due to '{response.FailureMessage ?? "<>"}'";
        return StatusCode(response.StatusCode, message);

    }
}