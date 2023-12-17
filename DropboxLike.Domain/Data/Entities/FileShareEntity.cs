
namespace DropboxLike.Domain.Data.Entities;

<<<<<<<< HEAD:DropboxLike.Domain/Data/Entities/ShareFile.cs
public class ShareFile
========
public class FileShareEntity
>>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6:DropboxLike.Domain/Data/Entities/FileShareEntity.cs
{
    public string UserId { get; set; }
    public string FileId { get; set; }

    public UserEntity User { get; set; } = null!;
    public virtual FileEntity File { get; set; } = null!;
}