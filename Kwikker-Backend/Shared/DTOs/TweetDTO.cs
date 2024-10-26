using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record TweetDTO(int id,string content,string mediaUrl ,int userId,DateTime createdAt);
}
