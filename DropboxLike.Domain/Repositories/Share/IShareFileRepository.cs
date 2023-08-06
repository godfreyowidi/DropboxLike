using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.Share;

public interface IShareFileRepository
{
    List<ShareEntity> GetSharedFilesForUser(string loggedInUserEmail);
}