using Microsoft.EntityFrameworkCore;
using File = DropboxLike.Domain.Models.File;

namespace DropboxLike.Domain.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<File>? Files { get; set; }
}