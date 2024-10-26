using EFCore.BulkExtensions;
using Entities.DomainModels;
using Entities.Models;
using Repository.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryModels
{
    public class TweetTrendRepository : RepositoryBase<TweetTrend>, ITweetTrendRepository
    {
        public TweetTrendRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateNewTweetTrendsAsync(List<TweetTrend> tweetTrends)
        {
            await RepositoryContext.BulkInsertAsync(tweetTrends, options =>
            {
                options.SetOutputIdentity = true; // Ensures IDs are retrieved
            });
        }
    }
}
