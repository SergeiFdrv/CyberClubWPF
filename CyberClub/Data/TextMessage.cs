namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TextMessage
    {
        [Key]
        public int MessageID { get; set; }

        [Column("Sender")]
        public int? SenderID { get; set; }

        [Column("Reciever")]
        public int? RecieverID { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortText { get; set; }

        [StringLength(255)]
        public string LongText { get; set; }

        public DateTime DT { get; set; }

        public bool? IsRead { get; set; }

        public virtual User Reciever { get; set; }

        public virtual User Sender { get; set; }
    }
}
