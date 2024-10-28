using Shared.DTOs;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface ILikeService
    {

        Task<int> GetTweetLikesNumber(int tweetId, bool trackChanges);

        Task CreateLike(int userId, int tweetid,bool trackChanges);

        Task DeleteLike(int userId, int tweetid, bool trackChanges);
        Task<(IEnumerable<TweetDTO> likedTweets, MetaData metaData)> GetUserLikedTweets(int userId, LikeParameters likeParameters, bool trackChanges);
    }
}
