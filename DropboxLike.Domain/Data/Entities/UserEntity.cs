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
<<<<<<< HEAD
    
    // Files and Folders owned by the User
    public virtual ICollection<FileEntity> Files { get; set; } = new List<FileEntity>();
    public virtual ICollection<FolderEntity> Folders { get; set; } = new List<FolderEntity>();
 
    // Sharing relationships
    public virtual ICollection<ShareFile> FileShares { get; set; } = new List<ShareFile>();
    public virtual ICollection<ShareFolder> FolderShares { get; set; } = new List<ShareFolder>();
=======

    public FolderEntity RootFolder { get; set; } = null!;
    public IEnumerable<FileShareEntity> FileShares { get; set; } = null!;
    public IEnumerable<FolderShareEntity> FolderShares { get; set; } = null!;
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6
}