using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Services.File;

public interface IFileService
{
    Task<OperationResult<object>> UploadSingleFileAsync(IFormFile file, string userId);
    Task<OperationResult<Models.File>> DownloadSingleFileAsync(string fileId);
    Task<OperationResult<object>> DeleteSingleFileAsync(string fileId);
    Task<OperationResult<List<FileMetadata>>> ListBucketFilesAsync(string userId);
}