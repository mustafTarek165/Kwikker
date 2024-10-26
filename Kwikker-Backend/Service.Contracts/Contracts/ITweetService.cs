using Shared.DTOs;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface ITweetService
    {
        Task<(IEnumerable<ExpandoObject> tweets, MetaData metaData)> GetTweetsByUser(int UserId,TweetParameters tweetParameters, bool trackChanges);
        
         Task<TweetDTO> GetTweet(int id, bool trackChanges);
        Task<TweetDTO> CreateTweet(int UserId,TweetForCreationDTO tweet, bool trackChanges);
        Task DeleteTweet(int id,bool trackChanges);
        Task<TweetDTO> UpdateTweet(TweetForUpdateDTO tweetForUpdateDTO);

    }
}
