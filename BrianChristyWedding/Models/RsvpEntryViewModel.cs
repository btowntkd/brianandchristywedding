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
                    NumGuests = rsvp.Guests.Count;
                    Attending = NumGuests != 0;
                    Guests = new List<Guest>(rsvp.Guests);
                }
                else
                {
                    NumGuests = null;
                    Attending = null;
                    Guests = new List<Guest>();
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
    }
}