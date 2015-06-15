using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class Guest
    {
        public int GuestID { get; set; }
        public string Name { get; set; }

        public int RsvpID { get; set; }
        public virtual Rsvp Rsvp { get; set; }
    }
}