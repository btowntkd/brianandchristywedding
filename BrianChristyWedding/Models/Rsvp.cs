using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class Rsvp : ITimestamps
    {
        public int InvitationID { get; set; }
        public virtual Invitation Invitation { get; set; }

        [Display(Name = "Received")]
        public DateTime Created { get; set; }
        [Display(Name = "Last updated")]
        public DateTime Updated { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}