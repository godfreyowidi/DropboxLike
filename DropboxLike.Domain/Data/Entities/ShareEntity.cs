using System.ComponentModel.DataAnnotations;

namespace DropboxLike.Domain.Data.Entities;

public class ShareEntity
{
    [Key]
    public string UserId { get; set; }
    public string FileId { get; set; }
    public string SenderEmail { get; set; } = "";
    public string RecipientEmail { get; set; } = "";
}