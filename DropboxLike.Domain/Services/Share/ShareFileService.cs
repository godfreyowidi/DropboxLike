using DropboxLike.Domain.Data.Entities;
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
    
    public Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId, AccessPermission requiredPermission)
    {
        return _shareFileRepository.GetSharedFilesByUserId(userId, requiredPermission);
    }

    public Task<OperationResult<string>> ShareFileWithUserAsync(string userId, string fileKey, AccessPermission requiredPermission)
    {
        return _shareFileRepository.ShareFileWithUserAsync(userId, fileKey, requiredPermission);
    }
}