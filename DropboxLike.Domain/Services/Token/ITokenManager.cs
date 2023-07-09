using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Services.Token;

public interface ITokenManager
{
    Task<OperationResult<bool>> Authenticate(string email, string password);
    string NewToken(string email);
}