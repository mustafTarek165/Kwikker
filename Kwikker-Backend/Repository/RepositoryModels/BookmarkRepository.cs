﻿using Entities.Models;
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

        public async Task<List<int>> GetBookmarksByUser(int UserId, bool trackChanges)
        {
            // Build the query with a join and filtering logic
            var bookmarks = await FindByCondition(b => b.UserId == UserId, trackChanges)
                .Select(x=>x.TweetId).ToListAsync();

            return bookmarks;
        }
        
    }
}
