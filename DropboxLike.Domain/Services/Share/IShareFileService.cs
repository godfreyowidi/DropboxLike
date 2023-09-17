using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Services.Share;

public interface IShareFileService
{
    Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId);
    Task<OperationResult<string>> ShareFileWithUsersAsync(IEnumerable<string> userIds, string fileId);

}