using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Services.Token;

public interface ITokenManager
{
    Task<OperationResult<string>> Authenticate(string email, string password);
    string NewToken(string email, string userId);
}