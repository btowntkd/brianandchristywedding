using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrianChristyWedding.Models;

namespace BrianChristyWedding.DAL
{
    public class WeddingInitializer : System.Data.Entity.DropCreateDatabaseAlways<WeddingContext>
    {
        protected override void Seed(WeddingContext context)
        {
            var invitations = new List<Invitation>()
            {
                new Invitation(){ MaxAllowedGuests = 4, Name = "Daniel and Jacqueline Freeman" },
                new Invitation(){ MaxAllowedGuests = 2, Name = "Cynthia Pratt" },
                new Invitation(){ MaxAllowedGuests = 2, Name = "John and Shari Coyle" }
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