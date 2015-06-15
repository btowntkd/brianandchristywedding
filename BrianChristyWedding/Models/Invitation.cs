using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class Invitation
    {
        public int InvitationID { get; set; }
        public string Name { get; set; }
        public int MaxAllowedGuests { get; set; }

        public virtual Rsvp Rsvp { get; set; }
    }
}