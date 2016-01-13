namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BrianChristyWedding.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BrianChristyWedding.DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var shortcode = new ShortcodeGenerator(Invitation.ShortcodeKeyspace, Invitation.ShortcodeLength);
            //var invitations = new List<Invitation>()
            //{
            //    new Invitation(){ MaxAllowedGuests = 4, Name = "Daniel and Jacqueline Freeman" , Shortcode = shortcode.Generate()},
            //    new Invitation(){ MaxAllowedGuests = 2, Name = "Cynthia Pratt" , Shortcode = shortcode.Generate()},
            //    new Invitation(){ MaxAllowedGuests = 2, Name = "John and Shari Coyle", Shortcode = shortcode.Generate() }
            //};
            //invitations.ForEach(x => context.Invitations.Add(x));
            //context.SaveChanges();

            //var rsvps = new List<Rsvp>()
            //{
            //    new Rsvp(){ InvitationID = 1 }
            //};
            //rsvps.ForEach(x => context.Rsvps.Add(x));
            //context.SaveChanges();

            //var guests = new List<Guest>()
            //{
            //    new Guest(){ RsvpID = 1, Name = "Dan" },
            //    new Guest(){ RsvpID = 1, Name = "Jacque" },
            //    new Guest(){ RsvpID = 1, Name = "Alex" },
            //    new Guest(){ RsvpID = 1, Name = "Max" }
            //};
            //guests.ForEach(x => context.Guests.Add(x));
            //context.SaveChanges();
        }
    }
}
