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
            var tweet = RepositoryContext.Set<Tweet>().FirstOrDefault(x => x.ID.Equals(tweetId));
            if (tweet != null) tweet.BookmarksNumber++;
            Create(bookmark);
            
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            var tweet = RepositoryContext.Set<Tweet>().FirstOrDefault(x => x.ID.Equals(bookmark.TweetId));
            if (tweet != null) tweet.BookmarksNumber--;
            Delete(bookmark);
        }
 

        public async Task<Bookmark> GetBookmark(int userId, int tweetId, bool trackChanges)
        {
            var bookmark=await FindByCondition(x => x.UserId.Equals(userId) && x.TweetId.Equals(tweetId), trackChanges).SingleOrDefaultAsync();
            return bookmark!;
        }

        public async Task<PagedList<Tweet>> GetBookmarksByUser(int UserId,BookmarkParameters bookmarkParameters, bool trackChanges)
        {
            // Build the query with a join and filtering logic
            var bookmarks = FindByCondition(b => b.UserId == UserId, trackChanges)
                .Sort<Bookmark>(bookmarkParameters.OrderBy!)
                .Join(
                    RepositoryContext.Set<Tweet>(), // Join with Tweets table
                    b => b.TweetId,                 // Foreign key in Bookmark
                    t => t.ID,                      // Primary key in Tweet
                    (b, t) => t                     // Select entire Tweet entity
                );


            return await PagedList<Tweet>
                   .ToPagedListAsync(bookmarks, bookmarkParameters.PageNumber,
                   bookmarkParameters.PageSize);
        }
        
    }
}
