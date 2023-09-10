using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.User;

public interface IUserRepository
{
    Task<OperationResult<string>> RegisterUserAsync(string email, string password);
    Task<List<string>> GetUserIdByEmailAddressAsync(List<string> emails);
}