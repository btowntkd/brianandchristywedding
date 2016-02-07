using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public enum GuestAge
    {
        Adult = 0,
        Child
    }

    public class Guest
    {
        public int GuestID { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        public GuestAge Age { get; set; }

        public int RsvpID { get; set; }
        public virtual Rsvp Rsvp { get; set; }
    }
}