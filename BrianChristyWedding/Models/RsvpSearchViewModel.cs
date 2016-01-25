using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class RsvpSearchViewModel
    {
        [Required]
        [Display(Name = "Invitation code")]
        public string Shortcode { get; set; }
    }
}