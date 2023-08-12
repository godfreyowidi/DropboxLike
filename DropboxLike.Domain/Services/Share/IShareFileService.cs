using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Services.Share;

public interface IShareFileService
{
    Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId);
    Task<OperationResult<string>> ShareFileWithUserAsync(string userId, string fileKey);

}