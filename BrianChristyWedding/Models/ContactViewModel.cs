using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Your name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Your email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Body { get; set; }
    }
}