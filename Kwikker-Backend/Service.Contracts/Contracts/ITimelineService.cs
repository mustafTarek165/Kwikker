using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface ITimelineService
    {
        //for prfiles
        Task<List<ExpandoObject>> GetHomeTimeline(int UserId);
        //for followers news
        Task<IEnumerable<ExpandoObject>>  GetUserTimeline(int UserId);

        //for random not related to followers or user
        Task<List<ExpandoObject>> GetRandomTimeline(int UserId);
    }
}
