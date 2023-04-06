namespace DropboxLike.Domain.Models;

public class File
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? FileType { get; set; }
  public byte[]? DataFiles { get; set; }
  public DateTime?  CreatedOn { get; set; }
}