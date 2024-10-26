using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Contracts;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryModels
{
    public class BookmarkRepository : RepositoryBase<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBookmark(int userId, int tweetId)
        {
            Bookmark bookmark = new Bookmark()
            {
                UserId = userId,
                TweetId = tweetId
                ,
                BookmarkedAt = DateTime.UtcNow
            };
            Create(bookmark);
            
        }

        public void DeleteBookmark(Bookmark bookmark)
       => Delete(bookmark);

        public async Task<Bookmark> GetBookmark(int userId, int tweetId, bool trackChanges)
        {
            var bookmark=await FindByCondition(x => x.UserId.Equals(userId) && x.TweetId.Equals(tweetId), trackChanges).SingleOrDefaultAsync();
            return bookmark!;
        }

        public async Task<PagedList<Bookmark>> GetBookmarksByUser(int UserId,BookmarkParameters bookmarkParameters, bool trackChanges)
        {
            var bookmarks= FindByCondition(x => x.UserId.Equals(UserId), trackChanges).Sort<Bookmark>(bookmarkParameters.OrderBy!);
            return await PagedList<Bookmark>
                   .ToPagedListAsync(bookmarks, bookmarkParameters.PageNumber,
                   bookmarkParameters.PageSize);
        }
        
    }
}
