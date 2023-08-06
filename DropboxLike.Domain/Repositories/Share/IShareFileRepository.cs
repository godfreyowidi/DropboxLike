using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.Share;

public interface IShareFileRepository
{
    Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId);
}