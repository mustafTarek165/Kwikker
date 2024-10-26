using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Like
    {
        public int UserId { get; set; }
        public int TweetId { get; set; }
        public DateTime LikedAt { get; set; }

        public User? User { get; set; } 
        public Tweet? Tweet { get; set; } 

    }
}
