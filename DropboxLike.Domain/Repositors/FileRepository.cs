using DropboxLike.Domain.Contracts;
using DropboxLike.Domain.Data;
using File = DropboxLike.Domain.Models.File;

namespace DropboxLike.Domain.Repositors;

public class FileRepository : IRepository<File>
{
  private readonly ApplicationDbContext? _dbContext;

  public async Task<File?> Create(File file)
  {
    try
    {
      if (file != null)
      {
        var obj = await _dbContext.Files.AddAsync(file);
        await _dbContext.SaveChangesAsync();
        return obj.Entity;
      }
      else
      {
        return null;
      }
    }
    catch (Exception)
    {
      throw;
    }
  }
}