using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class RsvpLookupViewModel
    {
        [Required]
        [Display(Name = "Invitation code")]
        public string Shortcode { get; set; }

        RsvpEntryViewModel RsvpEntry { get; set; }
    }
}