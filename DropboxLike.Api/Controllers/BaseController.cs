using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

public class BaseController : ControllerBase
{
    protected string GetUserIdFromClaim()
    {
        return HttpContext.User.FindFirst("userId")!.Value;
    }

}