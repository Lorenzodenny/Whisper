namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Avatars
    {
        [Key]
        public int AvatarId { get; set; } 
        public string Foto { get; set; } 
    }
}
