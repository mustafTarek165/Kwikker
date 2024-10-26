using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Follow
    {
        public int FollowerID { get; set; } // Foreign Key, references User
        public int FolloweeID { get; set; } // Foreign Key, references User
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public User Follower { get; set; } = null!;  // The user who follows
        public User Followee { get; set; } = null!;  // The user being followed
    }
}
