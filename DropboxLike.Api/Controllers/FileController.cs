using DropboxLike.Domain.Data;
using DropboxLike.Domain.Repositors;
using Microsoft.AspNetCore.Mvc;
using File = DropboxLike.Domain.Models.File;


namespace DropboxLike.Api.Controllers;

public class Filecontroller : ControllerBase
{
  private readonly ApplicationDbContext _dbContext;
  private readonly IFileRepository _repository;

  public Filecontroller(ApplicationDbContext dbContext, IFileRepository repository)
  {
    _dbContext = dbContext;
    _repository = repository;
  }

  [HttpPost]
  public async Task<List<File>> PostAsync(IFormFile files)
  {
    //Getting FileName
    var fileName = Path.GetFileName(files.FileName);

    //Getting file Extension
    var fileExtension = Path.GetExtension(fileName);

    // concatenating  FileName + FileExtension
    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

    var objFiles = new File()
    {
      Id = 0,
      Name = newFileName,
      FileType = fileExtension,
      CreatedOn = DateTime.Now
    };

    using (var target = new MemoryStream())
    {
      files.CopyTo(target);
      objFiles.DataFiles = target.ToArray();
    }
    var savedFiles = _repository.Create(objFiles);
    // return await savedFiles.
    // Route to the HOME page - this is for now! :smirk:
    // Return the list of items - files for now

  }
}