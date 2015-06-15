using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BrianChristyWedding.Models;

namespace BrianChristyWedding.DAL
{
    public class WeddingContext : DbContext
    {
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}