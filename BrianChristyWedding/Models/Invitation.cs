using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class Invitation
    {
        public const string ShortcodeKeyspace = "0123456789abcdefghijklmnopqrstuvwxyz";
        public const int ShortcodeLength = 4;

        public Invitation()
        {
            Address = new Address();
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Custom address label name")]
        public string CustomAddressLabelName { get; set; }

        [Display(Name = "Guests allowed")]
        public int MaxAllowedGuests { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(4)]
        [Display(Name = "Invitation code")]
        public string Shortcode { get; set; }

        public Address Address { get; set; }

        public virtual Rsvp Rsvp { get; set; }
    }
}