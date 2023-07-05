using System.Security.Cryptography;
using System.Text;
using DropboxLike.Domain.Data;
using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<OperationResult<UserEntity>> RegisterUserAsync(string email, string password)
    {
        try
        {
            if (_applicationDbContext.AppUsers != null)
            {
                var existingUser = await _applicationDbContext.AppUsers.FirstOrDefaultAsync(u => u.Email == email);

                if (existingUser != null)
                {
                    return OperationResult<UserEntity>.Fail("User already exist");
                }
            }

            var hashedPassword = HashPassword(password);

            var newUser = new UserEntity
            {
                Email = email,
                Password = hashedPassword,
            };

            _applicationDbContext.AppUsers?.Add(newUser);

            await _applicationDbContext.SaveChangesAsync();

            return OperationResult<UserEntity>.Success(newUser);
        }
        catch (Exception ex)
        {
            return OperationResult<UserEntity>.Fail(ex.Message);
        }

    }

    private static string HashPassword(string password)
    {
        using (var md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashedPassword;
        }
    }
}