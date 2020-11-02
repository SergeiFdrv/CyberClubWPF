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
            Feedbacks = new HashSet<Feedback>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(64)]
        public string EMail { get; set; }

        [StringLength(200)]
        public string Info { get; set; }

        public int? Authority { get; set; }

        [StringLength(255)]
        public string Passwd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Hierarchy Hierarchy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
