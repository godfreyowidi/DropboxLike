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

    public async Task<OperationResult<string>> ShareFolderWithUserAsync(IEnumerable<string> userIds, string folderId)
    {
        try
        {
            foreach (var userId in userIds)
            {
                var existingSharedFolder =
                    await _applicationDbContext.ShareFolders.FirstOrDefaultAsync(sf =>
                        sf.UserId == userId && sf.FolderId == folderId);

                if (existingSharedFolder != null)
                {
                    continue;
                }

                var sharedFolder = new ShareFolder()
                {
                    UserId = userId,
                    FolderId = folderId,
                };
                _applicationDbContext.ShareFolders.Add(sharedFolder);
            }

            await _applicationDbContext.SaveChangesAsync();
            return OperationResult<string>.Success("Folder shared successfully.");
        }
        catch (Exception e)
        {
            return OperationResult<string>.Fail("Error occurred while sharing the folder. ",
                HttpStatusCode.InternalServerError);
        }
    }
    
}