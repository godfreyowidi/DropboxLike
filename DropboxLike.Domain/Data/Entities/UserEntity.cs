using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropboxLike.Domain.Data.Entities;

public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [MaxLength(30)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;

    public FolderEntity RootFolder { get; set; } = null!;
    public IEnumerable<FileShareEntity> FileShares { get; set; } = null!;
    public IEnumerable<FolderShareEntity> FolderShares { get; set; } = null!;
}