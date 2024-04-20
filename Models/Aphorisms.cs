namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Aphorisms
    {
        [Key]
        public int AphorismId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Il contenuto può essere lungo al massimo 500 caratteri.")]
        public string Frase { get; set; }
    }
}
