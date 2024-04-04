namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reports
    {
        [Key]
        public int ReportId { get; set; }

        public int? PostId { get; set; }

        public int? CommentId { get; set; }

        public int? MessageId { get; set; }

        public int ReportedByUserId { get; set; }

        [Required]
        [StringLength(255)]
        public string DescrizioneReport { get; set; }

        public DateTime? ReportDate { get; set; }

        public virtual Comments Comments { get; set; }

        public virtual Comments Comments1 { get; set; }

        public virtual Messages Messages { get; set; }

        public virtual Posts Posts { get; set; }

        public virtual Users Users { get; set; }
    }
}
