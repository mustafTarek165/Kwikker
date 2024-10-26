using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Bookmark
    {
        public int UserId { get; set; }
        public int TweetId { get; set; }
        public DateTime BookmarkedAt { get; set; }

        public User? User { get; set; }
        public Tweet? Tweet { get; set; }
    }
}
