using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisper.Models
{
    
        public class PostViewModel
        {
            public Posts Post { get; set; }
            public bool LikedByUser { get; set; }
            public int TotalLikes { get; set; }
            public List<Comments> Comments { get; set; }
        }

    
}