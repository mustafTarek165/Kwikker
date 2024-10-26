using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface ITrendService
    {
        Task<IEnumerable<TrendDTO>> GetTopTrends();
        Task<IEnumerable<TweetDTO>> GetTweetsByTrend(string hashtag);
        IEnumerable<TrendDTO> GetTrends();
    }
}
