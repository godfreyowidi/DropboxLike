namespace DropboxLike.Domain.Data.Entities;

public class FolderShareEntity
{
    public string UserId { get; set; }
    public string FolderId { get; set; }

    public UserEntity User { get; set; } = null!;
<<<<<<< Updated upstream
    public virtual FolderEntity Folder { get; set; } = null!;
=======
<<<<<<< HEAD:DropboxLike.Domain/Data/Entities/ShareFolder.cs
    public virtual FolderEntity Folder { get; set; } = null!;
=======
    public FolderEntity Folder { get; set; } = null!;
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6:DropboxLike.Domain/Data/Entities/FolderShareEntity.cs
>>>>>>> Stashed changes
}