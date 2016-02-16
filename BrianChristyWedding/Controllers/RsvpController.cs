using BrianChristyWedding.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrianChristyWedding.Controllers
{
    public class RsvpController : Controller
    {
        private readonly WeddingContext db = new WeddingContext();

        public ActionResult Index(string shortcode)
        {
            if (string.IsNullOrWhiteSpace(shortcode))
            {
                return View("Search");
            }
            else
            {
                //Retrieve RSVP info
                var invitation = db.Invitations.FirstOrDefault(x => x.Shortcode == shortcode);
                if (invitation == null)
                {
                    ModelState.AddModelError(
                        "Shortcode",
                        "Unable to find invitation code \"" + shortcode + ".\"  Please check your code and try again.");
                    return View("Search");
                }
                var rsvpVM = new RsvpEntryViewModel(invitation);
                return View("Welcome", rsvpVM);
            }
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Shortcode")] RsvpSearchViewModel searchVM)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { shortcode = searchVM.Shortcode });
            }
            return View("Search");
        }

        [HttpPost]
        public ActionResult Submit([Bind(Include = "InvitationID,Attending,NumGuests,Guests")] RsvpEntryViewModel rsvpVM)
        {
            //Persist the Invitation information across requests
            var invitation = db.Invitations.Find(rsvpVM.InvitationID);
            if(invitation == null)
                return RedirectToAction("Index");
            rsvpVM.Invitation = invitation;

            if (ModelState.IsValid)
            {
                var rsvp = new Rsvp()
                {
                    InvitationID = rsvpVM.InvitationID,
                    Guests = rsvpVM.Guests,
                    Attending = rsvpVM.Attending.GetValueOrDefault()
                };
                AddOrUpdateRsvp(rsvp);
                db.SaveChanges();
                return View("Success");
            }
            return View("Welcome", rsvpVM);
        }

        private void AddOrUpdateRsvp(Rsvp rsvp)
        {
            var rsvpToUpdate = db.Rsvps.Find(rsvp.InvitationID);
            if (rsvpToUpdate != null)
            {
                db.Entry(rsvpToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                db.Rsvps.Add(rsvp);
            }
            AddOrUpdateGuests(rsvp.InvitationID, rsvp.Guests);
        }

        private void AddOrUpdateGuests(int invitationID, ICollection<Guest> guests)
        {
            var guestsToRemove = db.Guests.Where(x => x.RsvpID == invitationID);
            db.Guests.RemoveRange(guestsToRemove);

            if(guests == null)
                return;
            foreach (var guest in guests)
            {
                guest.RsvpID = invitationID;
                db.Guests.Add(guest);
            }
        }
    }
}