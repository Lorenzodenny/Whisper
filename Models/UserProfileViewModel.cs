using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisper.Models
{
    public class UserProfileViewModel
    {
        public Users Users { get; set; }
        public IEnumerable<Posts> Posts { get; set; }
        public string LoggedInUserId { get; internal set; }

        public bool IsFriend { get; set; }

        public IEnumerable<Friendships> Friendships { get; set; }

    }
}