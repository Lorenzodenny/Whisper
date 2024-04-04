namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sponsors
    {
        [Key]
        public int SponsorId { get; set; }

        [Required]
        [StringLength(255)]
        public string Titolo { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public string Foto { get; set; }

        public int UserId { get; set; }

        public virtual Users Users { get; set; }
    }
}
