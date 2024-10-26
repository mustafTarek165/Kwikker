using AutoMapper;
using Contracts;
using Entities.ExceptionModels;
using Service.Contracts.Contracts;
using Service.DataShaping;
using Shared.DTOs;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceModels
{
    internal sealed class RetweetService : IRetweetService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly INotificationService _notification;
        public RetweetService(IRepositoryManager repository, ILoggerManager
        logger, IMapper mapper, INotificationService notification)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _notification = notification;   
        }
        public async Task CreateRetweet(int userId, int tweetid, bool trackChanges)
        {
            var user =await _repository.UserRepository.GetUser(userId, trackChanges);
            if (user is null) throw new ForeignKeyNotFoundException(userId, "Likes", "User");

            var tweet = await _repository.TweetRepository.GetTweet(tweetid, trackChanges);
            if (tweet is null) throw new ForeignKeyNotFoundException(tweetid, "Likes", "Tweet");

            _repository.RetweetRepository.CreateRetweet(userId, tweetid);

            //notify user
            await _notification.CreateNotification(userId, "Retweet", tweet.UserID, $"{user.Username} has Retweeted your tweet");

            await _repository.SaveAsync();
        }

        public async Task DeleteRetweet(int userId, int tweetid, bool trackChanges)
        {
            var retweet =await _repository.RetweetRepository.GetRetweet(userId, tweetid, trackChanges: false);
            if (retweet is null) throw new CompositeKeyNotFoundException(userId, tweetid, "Likes", "User", "Tweet");

            _repository.RetweetRepository.DeleteRetweet(retweet);
            await _repository.SaveAsync();
        }
       
   

        public async Task<int> GetTweetRetweetsNumber(int tweetId, bool trackChanges)
        {
            int retweets =await _repository.RetweetRepository.GetTweetRetweetsNumber(tweetId, trackChanges);

            return retweets;
        }
    }
}
