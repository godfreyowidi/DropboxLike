namespace DropboxLike.Domain.Data.Entities;

public class FileFolder
{
    public string FileId { get; set; }
    public FileEntity File { get; set; }
    
    public string FolderId { get; set; }
    public FolderEntity Folder { get; set; }
}