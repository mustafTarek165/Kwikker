using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface ILikeRepository
    {
        Task<int> GetTweetLikesNumber(int tweetId,bool trackChanges);
        void CreateLike(int userId, int tweetId);
        void DeleteLike(Like like);
        Task<Like> GetLike(int userId, int tweetId,bool trackChanges);

        Task<PagedList<Tweet>> GetLikedTweetsByUser(int UserId, LikeParameters likeParameters, bool trackChanges);
    }
}
