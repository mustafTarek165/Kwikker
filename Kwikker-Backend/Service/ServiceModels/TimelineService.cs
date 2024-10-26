using AutoMapper;
using Contracts;
using Service.Contracts;
using Service.Contracts.Contracts;
using Shared.DTOs;
using Shared.RequestFeatures;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.ServiceModels
{
    public class TimelineService : ITimelineService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisCache;  // Use Redis cache

        private readonly IFollowService _followService;
        private readonly ITweetService _tweetService;
       

        private  string CacheKey = "TimeLine";  // Cache key
        private readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);  // Cache expiration time

        public TimelineService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IConnectionMultiplexer redisConnection,
            IFollowService followService, ITweetService tweetService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisConnection.GetDatabase();  // Get the Redis database
            _followService = followService;
            _tweetService = tweetService;
           
        }

        //for followers news
        public async Task<List<ExpandoObject>> GetHomeTimeline(int UserId )
        {
            CacheKey +="follow"+UserId;
            var cachedTimeline=await _redisCache.StringGetAsync(CacheKey);
            if(!cachedTimeline.HasValue)
            {
                FollowingParameters followingParameters = new FollowingParameters();
                var randomFollowees = await _followService.GetUserFollowees(UserId, followingParameters, trackChanges: false);


                TweetParameters tweetParameters = new TweetParameters();

                List<ExpandoObject> homeTimeline = new List<ExpandoObject>();
                foreach (var followee in randomFollowees.followees)
                {

                    var randomTweets = await _tweetService.GetTweetsByUser(followee.FolloweeID, tweetParameters, trackChanges: false);
                    homeTimeline.AddRange(randomTweets.tweets);

                }
                List<ExpandoObject> randomeHomeTimeline = new List<ExpandoObject>();
                Random rnd = new Random();
                randomeHomeTimeline = homeTimeline.OrderBy(x => rnd.Next()).ToList();


                var serializedTimeline = JsonSerializer.Serialize(randomeHomeTimeline);

                await _redisCache.StringSetAsync(CacheKey, serializedTimeline, CacheExpiration);

                return randomeHomeTimeline;
            }
            var retreived = JsonSerializer.Deserialize<List<ExpandoObject>>(cachedTimeline!);

            return retreived!;

        }

        //for profiles
        public async Task<IEnumerable<ExpandoObject>> GetUserTimeline(int UserId)
        {

            CacheKey += "profile" + UserId;
            var cachedTimeline = await _redisCache.StringGetAsync(CacheKey);
            if(!cachedTimeline.HasValue)
            {
                TweetParameters tweetParameters = new TweetParameters();

                var tweets = await _tweetService.GetTweetsByUser(UserId, tweetParameters, trackChanges: false);
                return tweets.tweets;
            }

            var retreived = JsonSerializer.Deserialize<List<ExpandoObject>>(cachedTimeline!);

            return retreived!;
        }

        // random not related to followers or user
        public async Task<List<ExpandoObject>> GetRandomTimeline(int UserId)
        {

            CacheKey += "random" + UserId;
            var cachedTimeline = await _redisCache.StringGetAsync(CacheKey);
            if (!cachedTimeline.HasValue)
            {

                IEnumerable<int> availableUsers = new List<int>();

                availableUsers = await _followService.GetRandomUsers(UserId);

                List<ExpandoObject> randomTimeline = new List<ExpandoObject>();
                foreach (var userId in availableUsers)
                {
                    TweetParameters tweetParameters = new TweetParameters();
                    var randomTweets = await _tweetService.GetTweetsByUser(userId, tweetParameters, trackChanges: false);
                    randomTimeline.AddRange(randomTweets.tweets);

                }
                return randomTimeline;
            }
            var retreived = JsonSerializer.Deserialize<List<ExpandoObject>>(cachedTimeline!);

            return retreived!;

        }

    }
}
