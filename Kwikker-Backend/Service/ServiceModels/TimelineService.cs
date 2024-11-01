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
        public async Task<List<TweetDTO>> GetHomeTimeline(int UserId )
        {
            CacheKey +="follow"+UserId;
            var cachedTimeline=await _redisCache.StringGetAsync(CacheKey);
            if(!cachedTimeline.HasValue)
            {
                Random rnd = new Random();
                FollowingParameters followingParameters = new FollowingParameters();
                var Followees = await _followService.GetUserFollowees(UserId, followingParameters, trackChanges: false);
                List<GeneralUserDTO> randomFollowees = new List<GeneralUserDTO>();

                randomFollowees=Followees.followees.OrderBy(x => rnd.Next()).ToList();
                 TweetParameters tweetParameters = new TweetParameters();   
   
                List<TweetDTO> homeTimeline = new List<TweetDTO>();
                foreach (var followee in randomFollowees)
                {

                    var randomTweets = await _tweetService.GetTweetsByUser(followee.id, tweetParameters, trackChanges: false);
                    homeTimeline.AddRange(randomTweets.tweets);

                }
                List<TweetDTO> randomeHomeTimeline = new List<TweetDTO>();
               
                randomeHomeTimeline = homeTimeline.OrderBy(x => rnd.Next()).ToList();


                var serializedTimeline = JsonSerializer.Serialize(randomeHomeTimeline);

                await _redisCache.StringSetAsync(CacheKey, serializedTimeline, CacheExpiration);

                return randomeHomeTimeline;
            }
            var retreived = JsonSerializer.Deserialize<List<TweetDTO>>(cachedTimeline!);

            return retreived!;

        }

        // random not related to followers or user
        public async Task<List<TweetDTO>> GetRandomTimeline(int UserId)
        {

            CacheKey += "random" + UserId;
            var cachedTimeline = await _redisCache.StringGetAsync(CacheKey);
            if (!cachedTimeline.HasValue)
            {

                IEnumerable<int> availableUsers = new List<int>();

                availableUsers = await _followService.GetRandomUsers(UserId);

                TweetParameters tweetParameters = new TweetParameters(); 
                List<TweetDTO> randomTimeline = new List<TweetDTO>();
                foreach (var userId in availableUsers)
                {
                   
                    var randomTweets = await _tweetService.GetTweetsByUser(userId, tweetParameters, trackChanges: false);
                    randomTimeline.AddRange(randomTweets.tweets);

                }

                List<TweetDTO> random = new List<TweetDTO>();
                Random rnd = new Random();
                random = randomTimeline.OrderBy(x => rnd.Next()).ToList();

                var serializedTimeline = JsonSerializer.Serialize(random);

                await _redisCache.StringSetAsync(CacheKey, serializedTimeline, CacheExpiration);

                return randomTimeline;
            }
           
            var retreived = JsonSerializer.Deserialize<List<TweetDTO>>(cachedTimeline!);

            return retreived!;

        }
        //for profiles
        public async Task<(IEnumerable<TweetDTO> twts, MetaData data)> GetUserTimeline(int UserId, TweetParameters tweetParameters)
        {
            var tweetsValues = await _tweetService.GetTweetsByUser(UserId, tweetParameters, trackChanges: false);

            return (twts: tweetsValues.tweets, data: tweetsValues.metaData);

        }


    }
}
