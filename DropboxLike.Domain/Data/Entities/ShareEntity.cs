using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Data.Entities;

public class ShareEntity
{
    public string UserId { get; set; }
    public string FileId { get; set; }

    public UserEntity User { get; set; } = null!;
}