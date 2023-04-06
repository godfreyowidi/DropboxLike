using DropboxLike.Domain.Contracts;
using File = DropboxLike.Domain.Models.File;


namespace DropboxLike.Infrastrucure.Services;

public class ServiceFile
{
  public readonly IRepository<File>? _fileRepository;
  public ServiceFile(IRepository<File> fileRepository)
  {
    _fileRepository = fileRepository;
  }

  public async Task<File> UploadFile(File file)
  {
    try
    {
      if (file == null)
      {
        throw new ArgumentNullException(nameof(file));
      }
      else
      {
        return await _fileRepository.Create(file);
      }
    }
    catch (Exception)
    {
      throw;
    }
  }
}