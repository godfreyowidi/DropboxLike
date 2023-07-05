using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.User;

public interface IUserRepository
{
    Task<OperationResult<UserEntity>> RegisterUserAsync(string email, string password);
}