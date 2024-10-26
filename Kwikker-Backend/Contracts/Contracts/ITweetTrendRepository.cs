using Entities.DomainModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface ITweetTrendRepository
    {
        Task CreateNewTweetTrendsAsync(List<TweetTrend> tweetTrends);
    }
}
