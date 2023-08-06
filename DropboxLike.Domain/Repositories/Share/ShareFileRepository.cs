using DropboxLike.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Repositories.Share;

public class ShareFileRepository : IShareFileRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ShareFileRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<List<FileEntity>> GetSharedFilesByUserId(string userId)
    {
        return _applicationDbContext.SharedFiles!
            .Where(file => file.UserId == userId)
            .Join(
                _applicationDbContext.FileModels,
                sharedFile => sharedFile.FileId,
                file => file.FileKey,
                (sharedFile, file) => file)
            .ToListAsync();
    }
}