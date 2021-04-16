namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Subscriptions = new HashSet<Subscription>();
            InTextMessages = new HashSet<TextMessage>();
            OutTextMessages = new HashSet<TextMessage>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(255)]
        public string About { get; set; }

        [EnumDataType(typeof(UserLevel))]
        [Required]
        public UserLevel UserLevel { get; set; } = UserLevel.Admin;

        [StringLength(255)]
        public string UserPass { get; set; }

        public DateTime? LastLogin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TextMessage> InTextMessages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TextMessage> OutTextMessages { get; set; }
    }
}
