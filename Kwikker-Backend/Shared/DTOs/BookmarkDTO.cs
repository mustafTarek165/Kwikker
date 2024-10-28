using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record BookmarkDTO(int userId,int tweetId, DateTime bookmarkedAt);
}
