using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropboxLike.Domain.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<FileEntity>? FileModels { get; set; }
    public DbSet<UserEntity>? AppUsers { get; set; }
    public DbSet<ShareFile>? SharedFiles { get; set; }
    public DbSet<FolderEntity>? Folders { get; set; }
    public DbSet<ShareFolder>? SharedFolders { get; set; }

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

        modelBuilder.Entity<ShareFile>()
            .HasOne(share => share.User)
            .WithMany(user => user.FileShares)
            .HasForeignKey(share => share.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShareFile>()
            .HasOne(share => share.File)
            .WithMany(file => file.SharedWithUsers)
            .HasForeignKey(share => share.FileId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureShareFolder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShareFolder>()
            .HasKey(share => new { share.UserId, share.FolderId });

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
            .OnDelete(DeleteBehavior.Cascade);

        // A parent-child relationship within folders
        modelBuilder.Entity<FolderEntity>()
            .HasOne(folder => folder.ParentFolder)
            .WithMany(parent => parent.ChildFolders)
            .HasForeignKey(folder => folder.ParentFolderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}