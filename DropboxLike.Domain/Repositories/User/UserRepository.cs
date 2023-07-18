using System.Net;
using System.Security.Cryptography;
using System.Text;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using DropboxLike.Domain.Configuration;
using DropboxLike.Domain.Data;
using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DropboxLike.Domain.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IAmazonS3 _awsS3Client;
    private readonly string? _bucketName;


    public UserRepository(ApplicationDbContext applicationDbContext, IOptions<AwsConfiguration> options)
    {
        var configuration = options.Value;
        _applicationDbContext = applicationDbContext;
        _bucketName = configuration.BucketName;
        _awsS3Client = new AmazonS3Client(configuration.AwsAccessKey, configuration.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(configuration.Region));
    }

    public async Task<OperationResult<string>> RegisterUserAsync(string email, string password)
    {
        try
        {
            if (_applicationDbContext.AppUsers != null)
            {
                var existingUser = await _applicationDbContext.AppUsers.FirstOrDefaultAsync(u => u.Email == email);

                if (existingUser != null)
                {
                    return OperationResult<string>.Fail("User already exist");
                }
            }

            var hashedPassword = HashPassword(password);

            var newUser = new UserEntity
            {
                Email = email,
                Password = hashedPassword,
            };

            _applicationDbContext.AppUsers?.Add(newUser);
            await _applicationDbContext.SaveChangesAsync();

            try
            {
                await CreateS3BucketFolder(newUser.Id!.ToString());
            }
            catch (Exception ex)
            {
                _applicationDbContext.AppUsers?.Remove(newUser);
                await _applicationDbContext.SaveChangesAsync();

                throw ex;
            }
            return OperationResult<string>.Success(newUser.Id!.ToString(), HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Fail(ex.Message, HttpStatusCode.InternalServerError);
        }

    }

    private async Task CreateS3BucketFolder(string userId)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = $"user_{userId}/",
            ContentBody = string.Empty
        };
        await _awsS3Client.PutObjectAsync(request);
    }
    private static string HashPassword(string password)
    {
        using (var md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashedPassword;
        }
    }
}