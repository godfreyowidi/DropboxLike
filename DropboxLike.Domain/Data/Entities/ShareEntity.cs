﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Data.Entities;

public class ShareEntity
{
    [Key]
    public int ShareId { get; set; }
    public string UserId { get; set; }
    public string FileId { get; set; }

    public UserEntity User { get; set; }
}