using DropboxLike.Domain.Models;
using DropboxLike.Domain.Repositories.Share;

namespace DropboxLike.Domain.Services.Share;

public class ShareFileService : IShareFileService
{
    private readonly IShareFileRepository _shareFileRepository;

    public ShareFileService(IShareFileRepository shareFileRepository)
    {
        _shareFileRepository = shareFileRepository;
    }
    
    public Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId)
    {
        return _shareFileRepository.GetSharedFilesByUserId(userId);
    }

    public Task<OperationResult<string>> ShareFileWithUsersAsync(IEnumerable<string> userIds, string fileId)
    {
        return _shareFileRepository.ShareFileWithUsersAsync(userIds, fileId);
    }
}