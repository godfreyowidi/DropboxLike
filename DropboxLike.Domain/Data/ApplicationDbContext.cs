using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options)
  {
  }
    
  public DbSet<FileEntity>? FileModels { get; set; }
  public DbSet<UserEntity>? AppUsers { get; set;  }
  public DbSet<ShareEntity>? SharedFiles { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserEntity>()
      .HasIndex(u => u.Email)
      .IsUnique();
    
    modelBuilder.Entity<ShareEntity>()
      .HasKey(s => s.ShareId);
    
    modelBuilder.Entity<ShareEntity>()
      .HasIndex(k => new { k.UserId, k.FileId })
      .IsUnique();
    
    base.OnModelCreating(modelBuilder);
  }
}