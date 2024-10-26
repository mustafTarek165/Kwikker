using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DomainModels
{
    public class TweetTrend
    {
        public int TweetId { get; set; }
        public Tweet? Tweet { get; set; }

        public int TrendId { get; set; }
        public Trend? Trend { get; set; }
    }
}
