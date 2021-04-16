using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CyberClub.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=ConnString")
        {
        }

        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<TextMessage> TextMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Developer)
                .HasForeignKey(e => e.DeveloperID);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.GameID);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Games)
                .Map(m => m.ToTable("GameGenre").MapLeftKey("Game").MapRightKey("Genre"));

            modelBuilder.Entity<Image>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Image)
                .HasForeignKey(e => e.GameIcon);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Subscriber);

            modelBuilder.Entity<User>()
                .HasMany(e => e.InTextMessages)
                .WithOptional(e => e.Reciever)
                .HasForeignKey(e => e.RecieverID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OutTextMessages)
                .WithOptional(e => e.Sender)
                .HasForeignKey(e => e.SenderID);
        }
    }
}
