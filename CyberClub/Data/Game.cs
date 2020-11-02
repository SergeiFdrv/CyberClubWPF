namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            Subscriptions = new HashSet<Subscription>();
            Genres = new HashSet<Genre>();
        }

        public int GameID { get; set; }

        [Required]
        [StringLength(50)]
        public string GameName { get; set; }

        public int? MadeBy { get; set; }

        [StringLength(255)]
        public string GameLink { get; set; }

        public int? GamePic { get; set; }

        public bool? Singleplayer { get; set; }

        public bool? Multiplayer { get; set; }

        public virtual Dev Dev { get; set; }

        public virtual Pic Pic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
