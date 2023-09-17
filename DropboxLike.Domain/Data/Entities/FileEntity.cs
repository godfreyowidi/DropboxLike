
using System.ComponentModel.DataAnnotations;
public class FileEntity
{
  [Key]
  public string FileKey { get; set; }

  public string FileName { get; set; } = string.Empty;
  public string? FileSize { get; set; }
  public string? FilePath { get; set; }
  public string? ContentType { get; set; }
  public string? TimeStamp { get; set; }
}