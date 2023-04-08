using DropboxLike.Domain.Contracts;
using DropboxLike.Domain.Repositors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFileRepository, FileRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();