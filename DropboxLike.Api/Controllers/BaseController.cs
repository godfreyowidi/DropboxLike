using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

public class BaseController : ControllerBase
{
    protected string GetUserIdFromClaim()
    {
        var signedInUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        if (signedInUser is null)
            return "Please sign in to continue.";

        return signedInUser;
    }

}