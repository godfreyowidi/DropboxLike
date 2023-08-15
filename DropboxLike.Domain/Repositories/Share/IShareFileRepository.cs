using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.Share;

public interface IShareFileRepository
{
    Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId, AccessPermission requiredPermission);
    Task<OperationResult<string>> ShareFileWithUserAsync(string userId, string fileKey, AccessPermission requiredPermission);

}