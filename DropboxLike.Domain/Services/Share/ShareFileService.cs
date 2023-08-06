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
    
    public Task<OperationResult<string>> ShareFileServiceAsync(FileEntity file)
    {
        return _shareFileRepository.SharedFileWithEmail(file);
    }
}