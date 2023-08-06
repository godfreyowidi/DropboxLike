namespace DropboxLike.Domain.Repositories.Share;

public interface IShareFileRepository
{
    Task<List<FileEntity>> GetSharedFilesByUserId(string userId);
}