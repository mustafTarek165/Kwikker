using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface IFollowRepository
    {
        void CreateFollow(int followerId, int followeeId);
        void DeleteFollow(Follow follow);
       Task<Follow> GetFollow(int followerId, int followeeId,bool trackChanges);
        Task<PagedList<User>> GetUserFollowers(int followeeId, FollowingParameters followingParameters, bool trackChanges);
        Task<PagedList<User>> GetUserFollowees(int followerId, FollowingParameters followingParameters, bool trackChanges);
    }
}
