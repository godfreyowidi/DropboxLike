using System.Net;
using DropboxLike.Domain.Data;
using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using DropboxLike.Domain.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Repositories.Share;

public class ShareFolderRepository : IShareFolderRespository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public ShareFolderRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

   public async Task<OperationResult<string>> ShareFolderWithUserAsync(string folderId, string userId, string shareWithUserId)
    {
        try
        {
            var existingSharedFolder = await _applicationDbContext.SharedFolders!
                .FirstOrDefaultAsync(sf => sf.UserId == shareWithUserId && sf.FolderId == folderId);

            if (existingSharedFolder != null)
            {
                return OperationResult<string>.Fail("Folder is already shared with the user.", HttpStatusCode.Conflict);
            }

            var shareFolder = new ShareFolder
            {
                FolderId = folderId,
                UserId = shareWithUserId
            };

            _applicationDbContext.SharedFolders!.Add(shareFolder);
            await _applicationDbContext.SaveChangesAsync();

            return OperationResult<string>.Success("Folder shared successfully.");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail("An error occurred while sharing the folder. Try again later.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<OperationResult<bool>> UnshareFolderWithUserAsync(string folderId, string userId, string unshareWithUserId)
    {
        try
        {
            var folder = await _applicationDbContext.Folders!
                .FirstOrDefaultAsync(f => f.FolderId == folderId && f.UserId == userId);

            if (folder == null)
            {
                return OperationResult<bool>.Fail("Folder not found or user lacks permission to unshare it.", HttpStatusCode.Unauthorized);
            }

            var shareFolder = await _applicationDbContext.SharedFolders!
                .FirstOrDefaultAsync(sf => sf.FolderId == folderId && sf.UserId == unshareWithUserId);

            if (shareFolder == null)
            {
                return OperationResult<bool>.Fail("Shared folder entry not found.", HttpStatusCode.NotFound);
            }

            _applicationDbContext.SharedFolders!.Remove(shareFolder);
            await _applicationDbContext.SaveChangesAsync();

            return OperationResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
    
}