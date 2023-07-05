using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Services.User;

public interface IUserService
{
    Task<OperationResult<UserEntity>> RegisterUserAsync(string email, string password);
}