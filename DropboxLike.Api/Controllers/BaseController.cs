using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

public class BaseController : ControllerBase
{
    protected string GetUserIdFromClaim()
    {
        var userIdClaim = HttpContext.User.FindFirst("userId");
        return userIdClaim?.Value ?? ""; 
    }

}