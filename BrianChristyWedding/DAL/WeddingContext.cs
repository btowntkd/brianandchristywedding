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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One to zero-or-one relationship between invitation and RSVP
            modelBuilder.Entity<Rsvp>().HasKey(x => x.InvitationID);

            modelBuilder.Entity<Rsvp>()
                .HasRequired(x => x.Invitation)
                .WithOptional(y => y.Rsvp);
        }
    }
}