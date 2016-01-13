using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BrianChristyWedding.Models;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BrianChristyWedding.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private const string _shortcodeKeyspace = "b0acdfe8761234zv59";
        private const int _shortcodeOffset = 1234;
        private readonly ShortcodeGenerator _encoder = new ShortcodeGenerator(_shortcodeKeyspace, _shortcodeOffset);

        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One to zero-or-one relationship between invitation and RSVP
            modelBuilder.Entity<Rsvp>().HasKey(x => x.InvitationID);
            modelBuilder.Entity<Rsvp>()
                .HasRequired(x => x.Invitation)
                .WithOptional(y => y.Rsvp);

            //One to many relationship between RSVP and Guests
            modelBuilder.Entity<Guest>()
                .HasRequired(x => x.Rsvp)
                .WithMany(y => y.Guests)
                .HasForeignKey(z => z.RsvpID);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}