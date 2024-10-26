using Microsoft.EntityFrameworkCore.Storage;
using Repository.Contracts.Contracts;
using Repository.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        ITweetRepository TweetRepository { get; }
        IRetweetRepository RetweetRepository { get; }   
        ILikeRepository LikeRepository { get; }
        IFollowRepository FollowRepository { get; } 
   
         IBookmarkRepository BookmarkRepository { get;}
        ITrendRepository TrendRepository { get; }   

        ITweetTrendRepository TweetTrendRepository { get; } 

        INotificationRepository NotificationRepository { get; }
        Task SaveAsync();
      

    }
}
