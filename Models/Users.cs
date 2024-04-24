namespace Whisper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Comments = new HashSet<Comments>();
            Conversations = new HashSet<Conversations>();
            Conversations1 = new HashSet<Conversations>();
            Friendships = new HashSet<Friendships>();
            Friendships1 = new HashSet<Friendships>();
            Likes = new HashSet<Likes>();
            Notifications = new HashSet<Notifications>();
            Notifications1 = new HashSet<Notifications>();
            Posts = new HashSet<Posts>();
            Reports = new HashSet<Reports>();
            Sponsors = new HashSet<Sponsors>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        
        [StringLength(255, MinimumLength = 8)] 
        [DataType(DataType.Password)]
        [RegularExpression("(?=.*[A-Z]).{8,}", ErrorMessage = "La password deve essere lunga almeno 8 caratteri e contenere almeno una lettera maiuscola.")] //[RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*]).{8,}", ErrorMessage = "La password deve essere lunga almeno 8 caratteri e contenere almeno una lettera maiuscola, un numero e un carattere speciale.")]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress] 
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Inserisci un indirizzo email valido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "User"; //Admin

        [Required]
        [StringLength(50)]
        public string Stato { get; set; } = "Attivo"; // Bannato

        [Required]
        [StringLength(16)]
        [DisplayName("Codice Fiscale")]
        public string CodiceFiscale { get; set; }

        public int? AvatarId { get; set; }

        public bool IsDeleted { get; set; } = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversations> Conversations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversations> Conversations1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Friendships> Friendships { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Friendships> Friendships1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likes> Likes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notifications> Notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notifications> Notifications1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Posts> Posts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sponsors> Sponsors { get; set; }

        public virtual Avatars Avatars { get; set; }
    }
}
