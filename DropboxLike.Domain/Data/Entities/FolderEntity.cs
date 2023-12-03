using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropboxLike.Domain.Data.Entities;

public class FolderEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string FolderId { get; set; }
    public string UserId { get; set; }
    public string FolderName { get; set; } = string.Empty;
    
    public UserEntity Owner { get; set; } = null!;
    public FolderEntity? Parent { get; set; }
    public IEnumerable<FileEntity> Files { get; set; } = new List<FileEntity>();
    public IEnumerable<FolderEntity> Folders { get; set; } = new List<FolderEntity>();
}