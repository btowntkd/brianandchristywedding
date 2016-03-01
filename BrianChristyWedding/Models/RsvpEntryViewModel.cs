using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class RsvpEntryViewModel
    {
        public RsvpEntryViewModel() : this(null)
        { }

        public RsvpEntryViewModel(Invitation invitation)
        {
            if (invitation != null)
            {
                Invitation = invitation;
                InvitationID = invitation.ID;

                var rsvp = Invitation.Rsvp;
                if (rsvp != null)
                {
                    Attending = rsvp.Attending;
                    NumGuests = rsvp.Attending ? (int?)rsvp.Guests.Count : null;
                    Guests = rsvp.Guests.ToList();
                    SongRequest = rsvp.SongRequest;
                }
                else
                {
                    NumGuests = null;
                    Attending = null;
                    Guests = new List<Guest>();
                    SongRequest = string.Empty;
                }

                while (Guests.Count < Invitation.MaxAllowedGuests)
                {
                    Guests.Add(new Guest());
                }
            }
        }

        public Invitation Invitation { get; set; }

        public int InvitationID { get; set; }

        public bool? Attending { get; set; }

        public int? NumGuests { get; set; }

        public List<Guest> Guests { get; set; }

        public string SongRequest { get; set; }
    }
}