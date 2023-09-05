using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using DropboxLike.Domain.Repositories.User;

namespace DropboxLike.Domain.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult<string>> RegisterUserAsync(string email, string password)
    {
        return await _userRepository.RegisterUserAsync(email, password);
    }

    public async Task<OperationResult<string>> GetUserIdByEmailAddressAsync(string email)
    {
        return await _userRepository.GetUserIdByEmailAddressAsync(email);
    }
}