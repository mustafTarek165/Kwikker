using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Contracts;
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

        public void CreateLike(int userId, int tweetId)
        {
            Like like = new Like()
            {
                TweetId = tweetId,
                UserId = userId,
                LikedAt=DateTime.Now
            };
            Create(like);
            
        }

        public async Task<Like> GetLike(int userId,int tweetId,bool trackChanges)
        {
          var result=  await FindByCondition(x => x.UserId.Equals(userId) && x.TweetId.Equals(tweetId), trackChanges).SingleOrDefaultAsync();
            return result!;
         }
        public void DeleteLike(Like like)=> Delete(like);


        public async Task<int> GetTweetLikesNumber(int tweetId, bool trackChanges)
        =>await RepositoryContext.Set<Like>().CountAsync();
    }
}
