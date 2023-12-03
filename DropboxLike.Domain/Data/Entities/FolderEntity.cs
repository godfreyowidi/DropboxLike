
using System.ComponentModel.DataAnnotations;

namespace DropboxLike.Domain.Data.Entities;

public class FolderEntity
{
    [Key]
    public string FolderId { get; set; }
    public string FolderName { get; set; } = string.Empty;
    public string UserId { get; set; }

    public ICollection<FileFolder> FileFolders { get; set; } = new List<FileFolder>();
}