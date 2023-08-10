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

    [HttpGet]
    [Route("share")]
    public async Task<IActionResult> GetSharedFilesByUserIdAsync()
    {
        var userId = GetUserIdFromClaim();

        var response = await _shareFileService.GetSharedFilesByUserId(userId);

        return Ok(response);
    }
}