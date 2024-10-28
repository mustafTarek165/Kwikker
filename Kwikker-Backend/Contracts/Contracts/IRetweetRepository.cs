using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface IRetweetRepository
    {
        Task<int> GetTweetRetweetsNumber(int tweetId, bool trackChanges);
        void CreateRetweet(int userId, int tweetId);
        void DeleteRetweet(Retweet retweet);
        Task<Retweet> GetRetweet(int userId, int tweetId, bool trackChanges);

        Task<PagedList<Tweet>> GetRetweetsByUser(int UserId, TweetParameters tweetParameters, bool trackChanges);
    }
}
