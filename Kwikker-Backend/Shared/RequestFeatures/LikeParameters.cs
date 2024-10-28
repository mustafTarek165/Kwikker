using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class LikeParameters:RequestParameters
    {
        public LikeParameters() => OrderBy = "TweetId";
    }
}
