namespace CyberClub.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext() : base("name=ConnString")
        {
        }

        public virtual DbSet<Dev> Devs { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Hierarchy> Hierarchy { get; set; }
        public virtual DbSet<Pic> Pics { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dev>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Dev)
                .HasForeignKey(e => e.MadeBy);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.Game1)
                .HasForeignKey(e => e.Game);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Games)
                .Map(m => m.ToTable("gamegenre").MapLeftKey("game").MapRightKey("genre"));

            modelBuilder.Entity<Hierarchy>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Hierarchy)
                .HasForeignKey(e => e.Authority);

            modelBuilder.Entity<Pic>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.Pic)
                .HasForeignKey(e => e.GamePic);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Who);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Who);
        }
    }
}
