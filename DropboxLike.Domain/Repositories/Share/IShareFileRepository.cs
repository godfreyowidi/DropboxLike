﻿using DropboxLike.Domain.Models;

namespace DropboxLike.Domain.Repositories.Share;

public interface IShareFileRepository
{
    Task<OperationResult<List<FileMetadata>>> GetSharedFilesByUserId(string userId);
    Task<OperationResult<string>> ShareFileWithUsersAsync(IEnumerable<string> userIds, string fileId);

}