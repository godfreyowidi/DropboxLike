using DropboxLike.Domain.Contracts;
using DropboxLike.Domain.Data;
using File = DropboxLike.Domain.Models.File;

namespace DropboxLike.Domain.Repositors;

public class IFileRepository //: IRepository<File>
{
  private readonly ApplicationDbContext? _dbContext;

  public async Task<File> Create(File file) // UPLOAD
  {
    try
    {
      if (file != null)
      {
        var obj = _dbContext.Add<File>(file);
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