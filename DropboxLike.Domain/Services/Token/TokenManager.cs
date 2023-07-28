using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DropboxLike.Domain.Data;
using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DropboxLike.Domain.Services.Token;

public class TokenManager : ITokenManager
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly byte[] _secretKey;

    public TokenManager(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _tokenHandler = new JwtSecurityTokenHandler();
        _secretKey = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes("rekfjdhabdjekkrnabrisnakelsntjsn")));
    }

    public async Task<OperationResult<string>> Authenticate(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return OperationResult<string>.Fail("Email and password are required");
        }

        var user = await GetUserByEmail(email);

        if (user == null) return OperationResult<string>.Fail("Incorrect Password");
        var hashedPassword = HashPassword(password);

        return hashedPassword == user.Password
            ? OperationResult<string>.Success(user.Id)
            : OperationResult<string>.Fail("Incorrect Password");
    }

    private static string HashPassword(string password)
    {
        using (var md5 = MD5.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = md5.ComputeHash(passwordBytes);
            var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedPassword;
        }
    }

    private async Task<UserEntity?> GetUserByEmail(string email)
    {
        var user = await _applicationDbContext.AppUsers.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public string NewToken(string email, string userId)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.NameIdentifier, userId)
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = "localhost",
            Audience = "DropboxLike",
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = _tokenHandler.CreateToken(tokenDescriptor);
        var jwtString = _tokenHandler.WriteToken(token);
        return jwtString;
    }
}