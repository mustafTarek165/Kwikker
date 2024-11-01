using AutoMapper;
using Contracts;
using Service.Contracts.Contracts;

using Shared.DTOs;
using Entities.ExceptionModels;
using Microsoft.AspNetCore.SignalR;
using Shared.RequestFeatures;
namespace Service.ServiceModels
{
    internal sealed class LikeService : ILikeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly INotificationService _notification;
        private readonly IHubContext<NotificationHub> _hubContext;
        public LikeService(IRepositoryManager repository, ILoggerManager
        logger, IMapper mapper, IHubContext<NotificationHub> hubContext, INotificationService notification)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _notification = notification;
            _hubContext = hubContext;   
        }

        public async Task CreateLike(int userId, int tweetid,bool trackChanges)
        {
            var user =await _repository.UserRepository.GetUser(userId,trackChanges);
            if (user is null) throw new ForeignKeyNotFoundException(userId, "Likes", "User");

            var tweet =await _repository.TweetRepository.GetTweet(tweetid, trackChanges);
            if (tweet is null) throw new ForeignKeyNotFoundException(tweetid, "Likes", "Tweet");

           _repository.LikeRepository.CreateLike(userId, tweetid);


            // Notify user
            string notificationMessage = $"{user.Username} has liked your tweet";
            await _notification.CreateNotification(userId, "Like", tweet.UserID, notificationMessage);

            // Send real-time notification via SignalR
          
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationMessage);

            await _repository.SaveAsync();
        }

        public async Task DeleteLike(int userId, int tweetid, bool trackChanges)
        {
            var like =await _repository.LikeRepository.GetLike(userId, tweetid, trackChanges: false);
            if (like is null) throw new CompositeKeyNotFoundException(userId, tweetid, "Likes", "User", "Tweet");

            _repository.LikeRepository.DeleteLike(like);
            await _repository.SaveAsync();
        }

        public async Task<int> GetTweetLikesNumber(int tweetId, bool trackChanges)
        {
            int likes=await _repository.LikeRepository.GetTweetLikesNumber(tweetId,trackChanges);
            
            return likes;
        }

        public async Task<IEnumerable<TweetDTO>> GetUserLikedTweets(int userId, bool trackChanges)
        {
            var likedTweetsWithMetaData = await _repository.LikeRepository.GetLikedTweetsByUser(userId,  trackChanges);


            if (!likedTweetsWithMetaData.Any())
            {
                return Enumerable.Empty<TweetDTO>();
            }

            var likedTweetsDTOs = _mapper.Map<IEnumerable<TweetDTO>>(likedTweetsWithMetaData);

            return likedTweetsDTOs;
        }

    }
}
