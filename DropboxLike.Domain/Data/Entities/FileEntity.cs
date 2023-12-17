
using System.ComponentModel.DataAnnotations;

namespace DropboxLike.Domain.Data.Entities;

public class FileEntity
{
  [Key]
  public string FileKey { get; set; }
  public string FileName { get; set; } = string.Empty;
  public string? FileSize { get; set; }
  public string? FilePath { get; set; }
  public string? ContentType { get; set; }
  public string? TimeStamp { get; set; }
    
  // Reference to the User
  public string UserId { get; set; }
  public virtual UserEntity User { get; set; }

<<<<<<< HEAD
  // Reference to the Folder
  public string? FolderId { get; set; }
  public virtual FolderEntity? Folder { get; set; }

  // Shared information
  public virtual ICollection<ShareFile> SharedWithUsers { get; set; } = new List<ShareFile>();
=======
  public FolderEntity Folder { get; set; } = null!;
  public IEnumerable<FileShareEntity> Shares { get; set; } = new List<FileShareEntity>();
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6
}
