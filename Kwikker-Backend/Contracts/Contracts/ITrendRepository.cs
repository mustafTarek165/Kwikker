using Entities.DomainModels;
using Entities.Models;

namespace Repository.Contracts.Contracts
{
    public interface ITrendRepository
    {
        Task<IEnumerable<Trend>> GetTopTrends();

        Task<IEnumerable<Tweet>> GetTweetsByTrend(string hashtag);

        Task<IDictionary<string,Trend>> GetExistingTrends(List<string>trends);

        
        Task CreateNewTrendsAsync(List<Trend>trends);   
    }
}
