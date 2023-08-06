namespace DropboxLike.Domain.Models;

public class FileMetadata
{
    public string? FileKey { get; set; }
    public string? FileName { get; set; }
    public string? FileSize { get; set; }
    public string? FilePath { get; set; }
    public string? ContentType { get; set; }
    public string? TimeStamp { get; set; }
}