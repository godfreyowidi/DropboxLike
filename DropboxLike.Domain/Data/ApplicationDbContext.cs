using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
<<<<<<< HEAD
    public DbSet<FileEntity>? FileModels { get; set; }
    public DbSet<UserEntity>? AppUsers { get; set; }
    public DbSet<ShareFile>? SharedFiles { get; set; }
    public DbSet<FolderEntity>? Folders { get; set; }
    public DbSet<ShareFolder>? SharedFolders { get; set; }
=======
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
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Existing configurations
        ConfigureUserEntity(modelBuilder);
        ConfigureShareEntity(modelBuilder);
        ConfigureFileEntity(modelBuilder);
        ConfigureShareFolder(modelBuilder);

        // New folder configurations
        ConfigureFolderEntity(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

<<<<<<< HEAD
    private static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }

    private static void ConfigureShareEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShareFile>()
            .HasKey(share => new { share.UserId, share.FileId });
=======
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
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6

        modelBuilder.Entity<ShareFile>()
            .HasOne(share => share.User)
            .WithMany(user => user.FileShares)
            .HasForeignKey(share => share.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShareFile>()
            .HasOne(share => share.File)
            .WithMany(file => file.SharedWithUsers)
            .HasForeignKey(share => share.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureShareFolder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShareFolder>()
            .HasKey(share => new { share.UserId, share.FolderId });

<<<<<<< HEAD
        modelBuilder.Entity<ShareFolder>()
            .HasOne(share => share.User)
            .WithMany(user => user.FolderShares)
            .HasForeignKey(share => share.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShareFolder>()
            .HasOne(share => share.Folder)
            .WithMany(folder => folder.SharedWithUsers)
            .HasForeignKey(share => share.FolderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
=======
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
>>>>>>> 18c886d4669ada5ddf808932b2237bb9b3c62ac6

    private static void ConfigureFileEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileEntity>()
            .HasIndex(f => new { f.FileName })
            .IsUnique();

        modelBuilder.Entity<FileEntity>()
            .HasOne(file => file.User)
            .WithMany(user => user.Files)
            .HasForeignKey(file => file.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FileEntity>()
            .HasOne(file => file.Folder)
            .WithMany(folder => folder.Files)
            .HasForeignKey(file => file.FolderId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureFolderEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FolderEntity>()
            .HasOne(folder => folder.User)
            .WithMany(user => user.Folders)
            .HasForeignKey(folder => folder.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // A parent-child relationship within folders
        modelBuilder.Entity<FolderEntity>()
            .HasOne(folder => folder.ParentFolder)
            .WithMany(parent => parent.ChildFolders)
            .HasForeignKey(folder => folder.ParentFolderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}