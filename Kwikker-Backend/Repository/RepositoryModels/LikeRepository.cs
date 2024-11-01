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
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public  void CreateLike(int userId, int tweetId)
        {
            Like like = new Like()
            {
                TweetId = tweetId,
                UserId = userId,
                LikedAt=DateTime.Now
            };
            var tweet=RepositoryContext.Set<Tweet>().FirstOrDefault(x=>x.ID.Equals(tweetId));
            if (tweet != null) tweet.LikesNumber++;

            Create(like);
            
        }

        public async Task<Like> GetLike(int userId,int tweetId,bool trackChanges)
        {
          var result=  await FindByCondition(x => x.UserId.Equals(userId) && x.TweetId.Equals(tweetId), trackChanges).SingleOrDefaultAsync();
            return result!;
         }
        public void DeleteLike(Like like) {

            var tweet = RepositoryContext.Set<Tweet>().FirstOrDefault(x => x.ID.Equals(like.TweetId));
            if (tweet != null) tweet.LikesNumber--;
            Delete(like);
        }
    


        public async Task<int> GetTweetLikesNumber(int tweetId, bool trackChanges)
        =>await RepositoryContext.Set<Like>().CountAsync();



        public async Task<PagedList<Tweet>> GetLikedTweetsByUser(int UserId, LikeParameters likeParameters, bool trackChanges)
        {
            // Build the query with a join and filtering logic
            var likes = FindByCondition(b => b.UserId == UserId, trackChanges)
                .Sort<Like>(likeParameters.OrderBy!)
                .Join(
                    RepositoryContext.Set<Tweet>(), // Join with Tweets table
                    b => b.TweetId,                 // Foreign key in Like
                    t => t.ID,                      // Primary key in Tweet
                    (b, t) => t                  // Select entire Tweet entity
                );


            return await PagedList<Tweet>
                   .ToPagedListAsync(likes, likeParameters.PageNumber,
                   likeParameters.PageSize);
        }
    }
}
