using Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Contracts.Contracts;
using Repository.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public  class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ITweetRepository> _tweetRepository;
        private readonly Lazy<IRetweetRepository> _retweetRepository;
        private readonly Lazy<ILikeRepository> _likeRepository;
        private readonly Lazy<IFollowRepository> _followRepository;
        private readonly Lazy<IBookmarkRepository> _bookmarkRepository;
        private readonly Lazy<ITrendRepository> _trendRepository;
        private readonly Lazy<ITweetTrendRepository> _tweetTrendRepository;
        private readonly Lazy<INotificationRepository> _notificationRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _userRepository = new Lazy<IUserRepository>(() => new
            UserRepository(repositoryContext));

            _tweetRepository = new Lazy<ITweetRepository>(() => new
            TweetRepository(repositoryContext));
            _retweetRepository = new Lazy<IRetweetRepository>(() => new
          RetweetRepository(repositoryContext));

            _likeRepository = new Lazy<ILikeRepository>(() => new
            LikeRepository(repositoryContext));
            _followRepository = new Lazy<IFollowRepository>(() => new
          FollowRepository(repositoryContext));
            
            _bookmarkRepository=new Lazy<IBookmarkRepository> (()=>new 
            BookmarkRepository(repositoryContext));


            _trendRepository = new Lazy<ITrendRepository>(() => new
          TrendRepository(repositoryContext));

            _tweetTrendRepository = new Lazy<ITweetTrendRepository>(() => new
            TweetTrendRepository(repositoryContext));

            _notificationRepository = new Lazy<INotificationRepository>(() => new
           NotificationRepository(repositoryContext));

        }

        public IUserRepository UserRepository => _userRepository.Value;

        public ITweetRepository TweetRepository => _tweetRepository.Value;

        public IRetweetRepository RetweetRepository => _retweetRepository.Value;

        public ILikeRepository LikeRepository => _likeRepository.Value;

        public IFollowRepository FollowRepository => _followRepository.Value;

      

        public IBookmarkRepository BookmarkRepository => _bookmarkRepository.Value;

        public ITrendRepository TrendRepository => _trendRepository.Value;

        public ITweetTrendRepository TweetTrendRepository => _tweetTrendRepository.Value;

        public INotificationRepository NotificationRepository => _notificationRepository.Value;

       

        public Task SaveAsync() =>_repositoryContext.SaveChangesAsync();

        
    }
}
