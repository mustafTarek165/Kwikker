using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface ITweetRepository
    {
        Task<PagedList<Tweet>> GetTweetsByUser(int UserId,TweetParameters tweetParameters,bool trackChanges);
         Task<Tweet> GetTweet(int id, bool trackChanges);
        void CreateTweet(int UserId, Tweet tweet); //child resource
        void DeleteTweet(Tweet tweet);
    }
}
