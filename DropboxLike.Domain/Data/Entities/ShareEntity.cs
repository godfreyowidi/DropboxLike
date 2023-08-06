namespace DropboxLike.Domain.Data.Entities;

public class ShareEntity
{
    public string FileId { get; set; }
    public string UserId { get; set; }
    public string SenderEmail { get; set; }
    public string RecipientEmail { get; set; }
    public string AccessPermissions { get; set; }
}
// userid - foreign id