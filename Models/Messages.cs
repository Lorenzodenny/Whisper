namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Messages()
        {
            Reports = new HashSet<Reports>();
        }

        [Key]
        public int MessageId { get; set; }

        public int ConversationId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Testo { get; set; }

        public DateTime? Orario { get; set; }

        public bool ReadStatus { get; set; }

        public virtual Conversations Conversations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports { get; set; }
    }
}
