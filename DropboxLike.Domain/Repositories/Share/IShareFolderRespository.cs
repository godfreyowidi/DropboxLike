﻿using DropboxLike.Domain.Data.Entities;
using DropboxLike.Domain.Models;
using DropboxLike.Domain.Models.Requests;

namespace DropboxLike.Domain.Repositories.Share;

public interface IShareFolderRespository
{
    Task<OperationResult<string>> ShareFolderWithUserAsync(IEnumerable<string> userIds, string folderId);
}