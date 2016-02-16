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
            //var shortcodes = new ShortcodeGenerator(Invitation.ShortcodeKeyspace, Invitation.ShortcodeLength);
            //context.Invitations.AddOrUpdate(
            //    x => x.ID,
            //    new Invitation() { ID = 1, MaxAllowedGuests = 4, FirstName = "Daniel and Jacqueline", LastName = "Freeman"},
            //    new Invitation() { ID = 2, MaxAllowedGuests = 2, FirstName = "Cynthia", LastName = "Pratt"},
            //    new Invitation() { ID = 3, MaxAllowedGuests = 2, FirstName = "John and Shari", LastName = "Coyle"});
            //context.SaveChanges();

            //context.Rsvps.AddOrUpdate(
            //    x => x.InvitationID,
            //    new Rsvp() { InvitationID = 1 });
            //context.SaveChanges();

            //context.Guests.AddOrUpdate(
            //    x => x.GuestID,
            //    new Guest() { GuestID = 1, RsvpID = 1, FirstName = "Dan", LastName = "Freeman" },
            //    new Guest() { GuestID = 2, RsvpID = 1, FirstName = "Jacque", LastName = "Freeman" },
            //    new Guest() { GuestID = 3, RsvpID = 1, FirstName = "Alex", LastName = "Freeman" },
            //    new Guest() { GuestID = 4, RsvpID = 1, FirstName = "Max", LastName = "Freeman" });
            //context.SaveChanges();
        }
    }
}
