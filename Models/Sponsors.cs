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
        [StringLength(120, ErrorMessage = "Il contenuto può essere lungo al massimo 120 caratteri.")]
        public string Description { get; set; }

        public string Foto { get; set; }

        public int UserId { get; set; }

        public virtual Users Users { get; set; }
    }
}
