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

            //One to zero-or-one relationship between invitation and RSVP
            modelBuilder.Entity<Rsvp>().HasKey(x => x.InvitationID);
            modelBuilder.Entity<Rsvp>()
                .HasRequired(x => x.Invitation)
                .WithOptional(y => y.Rsvp);

            //One to many relationship between RSVP and Guests
            modelBuilder.Entity<Guest>()
                .HasRequired(x => x.Rsvp)
                .WithMany(y => y.Guests)
                .HasForeignKey(z => z.RsvpID);
        }

        public override int SaveChanges()
        {
            UpdateTimestampEntities();
            return base.SaveChanges();
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