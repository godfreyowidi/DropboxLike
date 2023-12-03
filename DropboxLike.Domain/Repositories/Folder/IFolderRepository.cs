using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.Folder;

public interface IFolderRepository
{
    Task<OperationResult<FolderEntity>> CreateFolderAsync(string folderName, string userId);
    Task<OperationResult<string>> AddFileToFolderAsync(string fileId, string folderId);
    Task<OperationResult<string>> RemoveFileFromFolderAsync(string fileId, string folderId);


}