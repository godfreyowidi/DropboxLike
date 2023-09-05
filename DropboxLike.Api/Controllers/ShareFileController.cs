using DropboxLike.Domain.Models;
using DropboxLike.Domain.Services.Share;
using DropboxLike.Domain.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShareFileController : BaseController
{
    private readonly IShareFileService _shareFileService;
    private readonly IUserService _userService;

    public ShareFileController(IShareFileService shareFileService, IUserService userService)
    {
        _shareFileService = shareFileService;
        _userService = userService;
    }

    [HttpPost]
    [Route("share")]
    public async Task<IActionResult> ShareFileWithUserAsync([FromForm] string userId, [FromForm] string fileKey)
    {
        var response = await _shareFileService.ShareFileWithUserAsync(userId, fileKey);

        if (response.IsSuccessful) return Ok();

        var message =
            $"Failed to create shared file for user with ID {userId} and file with key {fileKey} due to '{response.FailureMessage ?? "<>"}'";
        return StatusCode(response.StatusCode, message);
    }

    [HttpGet]
    [Route("sharedfile")]
    public async Task<IActionResult> GetSharedFilesByUserIdAsync()
    {
        var userId = GetUserIdFromClaim();

        var response = await _shareFileService.GetSharedFilesByUserId(userId);

        if (response.IsSuccessful) return Ok(response.Value);

        var message =
            $"Failed to get shared files for user with ID {userId} due to '{response.FailureMessage ?? "<>"}'";
        return StatusCode(response.StatusCode, message);

    }

    [HttpPost]
    [Route("shareByEmail")]
    public async Task<IActionResult> ShareWithUserByEmailAsync([FromBody] Share model)
    {
        var result = await _userService.GetUserIdByEmailAddressAsync(model.Email);

        if (!result.IsSuccessful)
        {
            return BadRequest("User with that email is not a registered user.");
        }

        var userId = result.Value;

        var shareResult = await _shareFileService.ShareFileWithUserAsync(userId, model.FileId);

        if (shareResult.IsSuccessful)
        {
            return Ok("File Share Successfully.");
        }

        return BadRequest("An error occurred while sharing the file. Try again later.");
    }
}