using System.Net.Mime;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using DropboxLike.Domain.Configuration;
using DropboxLike.Domain.Data;
using Microsoft.Extensions.Options;

namespace DropboxLike.Domain.Repositories.Share;

public class ShareFileRepository
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

    public string GeneratePreSignedURL(FileEntity file)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = file.FileName,
            ContentType = file.ContentType,
            Expires = DateTime.UtcNow.AddHours(1)
        };
        var preSignedUrl = _awsS3Client.GetPreSignedURL(request);

        return preSignedUrl;
    }
}