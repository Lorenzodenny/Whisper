namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notifications
    {
        [Key]
        public int NotificationID { get; set; }

        public int UserID { get; set; }

        public int? TriggeredByUserID { get; set; }

        public int? PostID { get; set; }

        public int? CommentID { get; set; }

        public int? ConversationID { get; set; }

        public int? FriendshipID { get; set; }

        public int? LikeID { get; set; }


        [StringLength(255)]
        public string NotificationType { get; set; }

        public bool? ReadStatus { get; set; }

        public DateTime? NotificationDate { get; set; }

        public virtual Comments Comments { get; set; }

        public virtual Conversations Conversations { get; set; }

        public virtual Friendships Friendships { get; set; }

        public virtual Posts Posts { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }

        public virtual Likes Like { get; set; }
    }
}
