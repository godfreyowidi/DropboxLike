using DropboxLike.Domain.Services.Share;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShareFileController : ControllerBase
{
    private readonly IShareFileService _shareFileService;

    public ShareFileController(IShareFileService shareFileService)
    {
        _shareFileService = shareFileService;
    }

    [HttpGet]
    [Route("share")]
    public async Task<IActionResult> ShareFileAsync(FileEntity file)
    {

        var response = await _shareFileService.ShareFileServiceAsync(file);

        return Ok(response);
    }
}