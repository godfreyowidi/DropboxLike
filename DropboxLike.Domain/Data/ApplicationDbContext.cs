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
  public DbSet<FileShareEntity>? SharedFiles { get; set; }
  
  public DbSet<FolderEntity>? Folders { get; set; }
  public DbSet<ShareFolder>? ShareFolders { get; set; }
  public DbSet<FileFolder>? FileFolders { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    ConfigureUserEntity(modelBuilder);
    ConfigureShareEntity(modelBuilder);
    ConfigureFileEntity(modelBuilder);
    
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.Entity<FileFolder>()
      .HasKey(ff => new { ff.FileId, ff.FolderId });

    modelBuilder.Entity<FileFolder>()
      .HasOne(ff => ff.File)
      .WithMany(f => f.FileFolders)
      .HasForeignKey(ff => ff.FileId);

    modelBuilder.Entity<FileFolder>()
      .HasOne(ff => ff.Folder)
      .WithMany(f => f.FileFolders)
      .HasForeignKey(ff => ff.FolderId);
  }

  private static void ConfigureUserEntity(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserEntity>()
      .HasIndex(u => u.Email)
      .IsUnique();
  }

  private static void ConfigureShareEntity(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<FileShareEntity>()
      .HasKey(share=> new { share.UserId, share.FileId });

    modelBuilder.Entity<FileShareEntity>()
      .HasOne<UserEntity>(share => share.User)
      .WithMany(user => user.FileShares)
      .OnDelete(DeleteBehavior.Cascade);
  }
  
  private static void ConfigureShareFolder(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ShareFolder>()
      .HasKey(share=> new { share.UserId, share.FolderId });

    modelBuilder.Entity<ShareFolder>()
      .HasOne<UserEntity>(share => share.User)
      .WithMany(user => user.FolderShares)
      .OnDelete(DeleteBehavior.Cascade);
  }


  private static void ConfigureFileEntity(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<FileEntity>()
      .HasIndex(f => new { f.FileName })
      .IsUnique();

    modelBuilder.Entity<FileEntity>()
      .HasMany(f => f.Shares)
      .WithOne(s => s.File)
      .HasForeignKey(s => s.FileId);

    modelBuilder.Entity<FileEntity>()
      .HasMany(fr => fr.Shares)
      .WithOne(sf => sf.File)
      .HasForeignKey(sf => sf.FileId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}

