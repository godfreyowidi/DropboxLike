using System.Net;
using DropboxLike.Domain.Data;
using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Repositories.Folder;

public class FolderRepository : IFolderRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FolderRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<OperationResult<FolderEntity>> CreateFolderAsync(string folderName, string userId)
    {
        var existingFolder =
            await _applicationDbContext.Folders.FirstOrDefaultAsync(f =>
                f.FolderName == folderName && f.UserId == userId);

        if (existingFolder != null)
        {
            var uniqueFolderName = folderName;
            var counter = 1;

            do
            {
                uniqueFolderName = $"{folderName}({counter})";
                counter++;
                existingFolder =
                    await _applicationDbContext.Folders.FirstOrDefaultAsync(f =>
                        f.FolderName == uniqueFolderName && f.UserId == userId);
            } while (existingFolder != null);

            folderName = uniqueFolderName;
        }

        var newFolder = new FolderEntity
        {
            FolderId = Guid.NewGuid().ToString(),
            FolderName = folderName,
            UserId = userId
        };

        _applicationDbContext.Folders.Add(newFolder);
        await _applicationDbContext.SaveChangesAsync();
        return OperationResult<FolderEntity>.Success(newFolder);
    }

    public async Task<OperationResult<string>> AddFileToFolderAsync(string fileId, string folderId)
    {
        var fileFolder = new FileFolder { FileId = fileId, FolderId = folderId };
        _applicationDbContext.FileFolders.Add(fileFolder);
        await _applicationDbContext.SaveChangesAsync();
        return OperationResult<string>.Success("");
    }

    public async Task<OperationResult<string>> RemoveFileFromFolderAsync(string fileId, string folderId)
    {
        var fileFolder =
            await _applicationDbContext.FileFolders.FirstOrDefaultAsync(ff =>
                ff.FileId == fileId && ff.FolderId == folderId);

        if (fileFolder != null)
        {
            _applicationDbContext.FileFolders.Remove(fileFolder);
            await _applicationDbContext.SaveChangesAsync();
        }

        return OperationResult<string>.Success("");
    }
}