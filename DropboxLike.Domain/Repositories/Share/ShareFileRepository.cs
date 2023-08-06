using System.Net.Mime;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using DropboxLike.Domain.Configuration;
using DropboxLike.Domain.Data;
using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using Microsoft.Extensions.Options;

namespace DropboxLike.Domain.Repositories.Share;

public class ShareFileRepository : IShareFileRepository
{
    private readonly string? _bucketName;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IAmazonS3 _awsS3Client;

    public ShareFileRepository(IOptions<AwsConfiguration> options, ApplicationDbContext applicationDbContext)
    {
        var configuration = options.Value;
        _bucketName = configuration.BucketName;
        _awsS3Client = new AmazonS3Client(configuration.AwsAccessKey, configuration.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(configuration.Region));
        _applicationDbContext = applicationDbContext;
    }

    public List<ShareEntity> GetSharedFilesForUser(string loggedInUserEmail)
    {
        // conditions
        // relate email to the user id
        using var dbContext = new ApplicationDbContext();
        return dbContext.SharedFiles!
            .Where(file => file.RecipientEmail == loggedInUserEmail)
            .ToList();
    }
}