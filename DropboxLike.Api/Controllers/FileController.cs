using DropboxLike.Domain.Data;
using DropboxLike.Domain.Repositors;
using Microsoft.AspNetCore.Mvc;
using File = DropboxLike.Domain.Models.File;


namespace DropboxLike.Api.Controllers;

public class Filecontroller : ControllerBase
{
  private readonly ApplicationDbContext _dbContext;
  private readonly FileRepository _repository;

  public Filecontroller(ApplicationDbContext dbContext, FileRepository repository)
  {
    _dbContext = dbContext;
    _repository = repository;
  }

  [HttpPost]
  public async Task<ActionResult<File>> PostAsync(IFormFile file)
  {
    if (file.Length > 0)
    {
      //Getting FileName
      var fileName = Path.GetFileName(file.FileName);

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
        file.CopyTo(target);
        objFiles.DataFiles = target.ToArray();
      }
      await _repository.Create(objFiles);
    }
    return Ok();
  }
}