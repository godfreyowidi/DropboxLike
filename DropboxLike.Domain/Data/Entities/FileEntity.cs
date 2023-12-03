
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

  public FolderEntity Folder { get; set; } = null!;
  public IEnumerable<FileShareEntity> Shares { get; set; } = new List<FileShareEntity>();
}
