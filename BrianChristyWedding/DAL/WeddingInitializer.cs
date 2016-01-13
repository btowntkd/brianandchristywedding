using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrianChristyWedding.Models;

namespace BrianChristyWedding.DAL
{
    public class WeddingInitializer : System.Data.Entity.DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var shortcode = new ShortcodeGenerator(Invitation.ShortcodeKeyspace, Invitation.ShortcodeLength);
            var invitations = new List<Invitation>()
            {
                new Invitation(){ MaxAllowedGuests = 4, Name = "Daniel and Jacqueline Freeman" , Shortcode = shortcode.Generate()},
                new Invitation(){ MaxAllowedGuests = 2, Name = "Cynthia Pratt" , Shortcode = shortcode.Generate()},
                new Invitation(){ MaxAllowedGuests = 2, Name = "John and Shari Coyle", Shortcode = shortcode.Generate() }
            };
            invitations.ForEach(x => context.Invitations.Add(x));
            context.SaveChanges();

            var rsvps = new List<Rsvp>()
            {
                new Rsvp(){ InvitationID = 1 }
            };
            rsvps.ForEach(x => context.Rsvps.Add(x));
            context.SaveChanges();

            var guests = new List<Guest>()
            {
                new Guest(){ RsvpID = 1, Name = "Dan" },
                new Guest(){ RsvpID = 1, Name = "Jacque" },
                new Guest(){ RsvpID = 1, Name = "Alex" },
                new Guest(){ RsvpID = 1, Name = "Max" }
            };
            guests.ForEach(x => context.Guests.Add(x));
            context.SaveChanges();
        }
    }
}