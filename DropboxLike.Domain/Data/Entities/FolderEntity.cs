using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropboxLike.Domain.Data.Entities;

public class FolderEntity
{
    [Key]
<<<<<<< HEAD
    public string? FolderId { get; set; }
    public string FolderName { get; set; } = string.Empty;
    public string UserId { get; set; }
    public virtual UserEntity User { get; set; }
    
    // Folder hierarchy
    public string? ParentFolderId { get; set; }
    public virtual FolderEntity ParentFolder { get; set; } = null!;
    public virtual ICollection<FolderEntity> ChildFolders { get; set; } = new List<FolderEntity>();

    // Files in the Folder
    public virtual ICollection<FileEntity> Files { get; set; } = new List<FileEntity>();

    // Shared information
    public virtual ICollection<ShareFolder> SharedWithUsers { get; set; } = new List<ShareFolder>();
=======
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string FolderId { get; set; }
    public string UserId { get; set; }
    public string FolderName { get; set; } = string.Empty;
    
    public UserEntity Owner { get; set; } = null!;
    public FolderEntity? Parent { get; set; }
    public IEnumerable<FileEntity> Files { get; set; } = new List<FileEntity>();
    public IEnumerable<FolderEntity> Folders { get; set; } = new List<FolderEntity>();
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6
}