using Service.Contracts.Contracts;
using Contracts;
using AutoMapper;
using Shared.DTOs;
using Shared.RequestFeatures;
using Entities.ExceptionModels;
namespace Service.ServiceModels
{
    public class BookmarkService:IBookmarkService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public BookmarkService(IRepositoryManager repository, ILoggerManager
        logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;   
        }

        public async Task CreateBookmark(int userId, int tweetId, bool trackChanges)
        {
            
            var user = await _repository.UserRepository.GetUser(userId, trackChanges);
            if (user is null) throw new NotFoundException(userId, "User");

            var tweet = await _repository.TweetRepository.GetTweet(tweetId, trackChanges);
            if (tweet is null) throw new NotFoundException(tweetId, "Tweets");



            _repository.BookmarkRepository.CreateBookmark(userId, tweetId);
            await _repository.SaveAsync();

        }

        public async Task DeleteBookmark(int userId, int tweetId, bool trackChanges)
        {
            var bookmark = await _repository.BookmarkRepository.GetBookmark(userId, tweetId, trackChanges);
            if (bookmark is null) throw new CompositeKeyNotFoundException(userId, tweetId, "Follows", "User", "User");
            _repository.BookmarkRepository.DeleteBookmark(bookmark);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<BookmarkDTO> bookmarks, MetaData metaData)> GetUserBookmarks(int userId, BookmarkParameters bookmarkParameters, bool trackChanges)
        {
            var bookmarksWithMetaData = await _repository.BookmarkRepository.GetBookmarksByUser(userId, bookmarkParameters, trackChanges);

            if (bookmarksWithMetaData.Count() == 0) throw new ForeignKeyNotFoundException(userId, "Bookmarks", "User");

            var bookmarksDTOs = _mapper.Map<IEnumerable<BookmarkDTO>>(bookmarksWithMetaData);

            return (bookmarks: bookmarksDTOs, metaData: bookmarksWithMetaData.MetaData);
        }
    }
}
