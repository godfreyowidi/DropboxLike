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
    // One method per entity/collection/table.
    ConfigureUserEntity(modelBuilder);
    ConfigureShareEntity(modelBuilder);
    ConfigureFileEntity(modelBuilder);
    
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
    modelBuilder.Entity<ShareEntity>()
      .HasKey(share=> new { share.UserId, share.FileId });

    modelBuilder.Entity<ShareEntity>()
      .HasOne<UserEntity>(share => share.User)
      .WithMany(user => user.FileShares)
      .OnDelete(DeleteBehavior.Cascade);
  }

  private static void ConfigureFileEntity(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<FileEntity>()
      .HasIndex(f => new { f.FileName })
      .IsUnique();

    modelBuilder.Entity<FileEntity>()
      .HasMany(f => f.SharedWithUsers)
      .WithOne(s => s.File)
      .HasForeignKey(s => s.FileId);

    modelBuilder.Entity<FileEntity>()
      .HasMany(fr => fr.SharedWithUsers)
      .WithOne(sf => sf.File)
      .HasForeignKey(sf => sf.FileId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}