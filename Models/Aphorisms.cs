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
        public string Frase { get; set; }
    }
}
