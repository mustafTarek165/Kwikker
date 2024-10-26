using Shared.DTOs;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface IBookmarkService
    {
        Task CreateBookmark(int userId,int tweetId, bool trackChanges);
        Task DeleteBookmark(int userId,int tweetId, bool trackChanges);
        Task<(IEnumerable<BookmarkDTO> bookmarks, MetaData metaData)> GetUserBookmarks(int userId, BookmarkParameters bookmarkParameters, bool trackChanges);
    }
}
