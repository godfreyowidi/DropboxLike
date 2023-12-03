
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

  public IEnumerable<ShareEntity> SharedWithUsers { get; set; } = null!;
  public ICollection<FileFolder> FileFolders { get; set; } = new List<FileFolder>();
}
