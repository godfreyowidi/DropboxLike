using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Services.Token;
using DropboxLike.Domain.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace DropboxLike.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly ITokenManager _tokenManager;
    private readonly IUserService _userService;

    public AuthenticateController(ITokenManager tokenManager, IUserService userService)
    {
        _tokenManager = tokenManager;
        _userService = userService;
    }
    
    public async Task<IActionResult> Authenticate(string email, string password)
    {
        var result = await _tokenManager.Authenticate(email, password);
        if (result.IsSuccessful)
        {
            return Ok(new { Token = _tokenManager.NewToken(email, result.Value) });
        }

        if (result.FailureMessage != null) ModelState.AddModelError("Unauthorized", result.FailureMessage);
        return Unauthorized(ModelState);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser(UserEntity request)
    {
        var result = await _userService.RegisterUserAsync(request.Email!, request.Password!);

        if (result.IsSuccessful)
        {
            var userId = result.Value;
            return Ok(new {Id = userId});
        }

        return StatusCode(result.StatusCode, result.FailureMessage);
    }
}