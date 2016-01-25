using System;
using System.Data.Entity;
using System.Linq;

namespace BrianChristyWedding.Models
{
    public class WeddingContext : DbContext
    {
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public ShortcodeGenerator ShortcodeGenerator { get; private set; }

        public WeddingContext() : base("DefaultConnection")
        {
            ShortcodeGenerator = new ShortcodeGenerator(Invitation.ShortcodeKeyspace, Invitation.ShortcodeLength);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invitation>()
                .HasOptional(x => x.Rsvp)
                .WithRequired(y => y.Invitation)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Rsvp>()
                .HasKey(x => x.InvitationID);

            modelBuilder.Entity<Guest>()
                .HasRequired(x => x.Rsvp)
                .WithMany(y => y.Guests)
                .HasForeignKey(y => y.RsvpID)
                .WillCascadeOnDelete(true);
        }

        public override int SaveChanges()
        {
            UpdateTimestampEntities();
            GenerateMissingShortcodes();
            return base.SaveChanges();
        }

        private void GenerateMissingShortcodes()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Invitation && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var item in entities)
            {
                var invitation = item.Entity as Invitation;
                if (string.IsNullOrWhiteSpace(invitation.Shortcode))
                {
                    invitation.Shortcode = ShortcodeGenerator.Generate();
                }
            }
        }

        private void UpdateTimestampEntities()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is ITimestamps && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var item in entities)
            {
                var tstamp = item.Entity as ITimestamps;
                var now = DateTime.UtcNow;
                if (item.State == EntityState.Added || tstamp.Created == default(DateTime))
                {
                    tstamp.Created = now;
                }
                tstamp.Updated = now;
            }
        }
    }
}