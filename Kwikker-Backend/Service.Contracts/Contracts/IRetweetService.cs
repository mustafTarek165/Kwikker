using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface IRetweetService
    {
        
        Task<int> GetTweetRetweetsNumber(int tweetId, bool trackChanges);
        Task CreateRetweet(int userId, int tweetid, bool trackChanges);

        Task DeleteRetweet(int userId, int tweetid, bool trackChanges);
    }
}
