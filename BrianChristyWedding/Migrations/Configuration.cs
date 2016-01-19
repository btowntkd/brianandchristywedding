namespace BrianChristyWedding.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeddingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeddingContext context)
        {
            var shortcodes = new ShortcodeGenerator(Invitation.ShortcodeKeyspace, Invitation.ShortcodeLength);
            context.Invitations.AddOrUpdate(
                x => x.ID,
                new Invitation() { ID = 1, MaxAllowedGuests = 4, Name = "Daniel and Jacqueline Freeman", Shortcode = shortcodes.Generate() },
                new Invitation() { ID = 2, MaxAllowedGuests = 2, Name = "Cynthia Pratt", Shortcode = shortcodes.Generate() },
                new Invitation() { ID = 3, MaxAllowedGuests = 2, Name = "John and Shari Coyle", Shortcode = shortcodes.Generate() });
            context.SaveChanges();

            context.Rsvps.AddOrUpdate(
                x => x.InvitationID,
                new Rsvp() { InvitationID = 1 });
            context.SaveChanges();

            context.Guests.AddOrUpdate(
                x => x.RsvpID,
                new Guest() { RsvpID = 1, Name = "Dan" },
                new Guest() { RsvpID = 1, Name = "Jacque" },
                new Guest() { RsvpID = 1, Name = "Alex" },
                new Guest() { RsvpID = 1, Name = "Max" });
            context.SaveChanges();
        }
    }
}
