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
    public class RetweetRepository : RepositoryBase<Retweet>, IRetweetRepository
    {
        public RetweetRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateRetweet(int userId, int tweetId)
        {
            Retweet retweet = new Retweet()
            {
                TweetId = tweetId,
                UserId = userId,
                RetweetedAt = DateTime.Now
            };

            var tweet = RepositoryContext.Set<Tweet>().FirstOrDefault(x => x.ID.Equals(tweetId));
            if (tweet != null) tweet.RetweetsNumber++;

            Create(retweet);

        }

        public async Task< Retweet> GetRetweet(int userId, int tweetId, bool trackChanges)

        {
           var result= await FindByCondition(x => x.UserId.Equals(userId) && x.TweetId.Equals(tweetId), trackChanges).SingleOrDefaultAsync();
            
            return result!;
        }

        public void DeleteRetweet(Retweet retweet) => Delete(retweet);

        public async Task<int> GetTweetRetweetsNumber(int tweetId, bool trackChanges)
     => await FindByCondition(x => x.TweetId.Equals(tweetId), trackChanges).Include(x => x.User).CountAsync();

        public async Task<PagedList<Tweet>> GetRetweetsByUser(int UserId, TweetParameters tweetParameters, bool trackChanges)
        {
            var Retweets = FindByCondition(x => x.UserId.Equals(UserId), trackChanges)
                .Sort<Retweet>(tweetParameters.OrderBy!).Join(
                    RepositoryContext.Set<Tweet>(), // Join with Tweets table
                    b => b.TweetId,                 // Foreign key in Retweet
                    t => t.ID,                      // Primary key in Tweet
                    (b, t) => t                     // Select entire Tweet entity
                );
            return await PagedList<Tweet>
                   .ToPagedListAsync(Retweets, tweetParameters.PageNumber,
                   tweetParameters.PageSize);
        }

       
    }
}
