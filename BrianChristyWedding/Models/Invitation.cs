using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrianChristyWedding.Models
{
    public class Invitation
    {
        private const string _shortcodeKeyspace = "zf13a6c2";
        private const int _shortcodeOffset = 450;
        private static readonly ShortCoder _encoder = new ShortCoder(_shortcodeKeyspace, _shortcodeOffset);

        public Invitation()
        {
            Address = new Address();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxAllowedGuests { get; set; }
        
        public Address Address { get; set; }

        public virtual Rsvp Rsvp { get; set; }

        public string Shortcode
        {
            get { return _encoder.Encode(ID); }
        }

        public static string GetShortcodeFromId(int id)
        {
            return _encoder.Encode(id);
        }

        public static int GetIdFromShortcode(string shortcode)
        {
            return (int)_encoder.Decode(shortcode);
        }

    }
}