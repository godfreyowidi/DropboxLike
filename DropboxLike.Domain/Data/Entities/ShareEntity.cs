﻿
namespace DropboxLike.Domain.Data.Entities;

public class ShareEntity
{
    public string UserId { get; set; }
    public string FileId { get; set; }

    public UserEntity User { get; set; } = null!;
    public FileEntity File { get; set; } = null!;
}