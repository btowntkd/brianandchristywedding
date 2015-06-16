using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class Rsvp
    {
        public int InvitationID { get; set; }
        public virtual Invitation Invitation { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}