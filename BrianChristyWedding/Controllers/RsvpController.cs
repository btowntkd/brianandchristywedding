using BrianChristyWedding.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
        public ActionResult Submit([Bind(Include = "InvitationID,Attending,NumGuests,Guests,SongRequest")] RsvpEntryViewModel rsvpVM)
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
                    Attending = rsvpVM.Attending.GetValueOrDefault(),
                    SongRequest = rsvpVM.SongRequest,
                    Invitation = rsvpVM.Invitation
                };
                AddOrUpdateRsvp(rsvp);
                db.SaveChanges();
                return View("Success", rsvpVM);
            }
            else
            {
                //Pad the number of guests, with empties
                while(rsvpVM.Guests.Count < invitation.MaxAllowedGuests)
                    rsvpVM.Guests.Add(new Guest());
            }
            return View("Welcome", rsvpVM);
        }

        private void AddOrUpdateRsvp(Rsvp rsvp)
        {
            var rsvpToUpdate = db.Rsvps.Find(rsvp.InvitationID);
            if (rsvpToUpdate != null)
            {
                rsvpToUpdate.Attending = rsvp.Attending;
                rsvpToUpdate.SongRequest = rsvp.SongRequest;
                db.Entry(rsvpToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                db.Rsvps.Add(rsvp);
            }
            AddOrUpdateGuests(rsvp.InvitationID, rsvp.Guests);
            SendEmailNotification(rsvp);

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

        private void SendEmailNotification(Rsvp rsvp)
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var mail = new MailMessage();
                    mail.To.Add("bcpratt@live.com");
                    mail.To.Add("chrispy115@gmail.com");
                    mail.Subject = "Wedding RSVP Submitted or Updated";
                    mail.ReplyToList.Clear();
                    mail.ReplyToList.Add("no-reply@BrianAndChristyWedding.com");

                    var messageBody = new StringBuilder();
                    messageBody.AppendFormat("<p>An RSVP has been submitted (or updated) from <strong>{0}</strong>.</p>", rsvp.Invitation.EffectiveName);
                    messageBody.Append("<dl>");
                    messageBody.AppendFormat("<dt>Attending?:</dt><dd>{0}</dd>", rsvp.Attending ? "Yes" : "No");
                    if (rsvp.Attending)
                    {
                        messageBody.Append("<d><dt>Guests:</dt><dd><ul>");
                        foreach (var guest in rsvp.Guests)
                        {
                            messageBody.AppendFormat("<li>{0} ({1})</li>", guest.FirstName + " " + guest.LastName, guest.Age);
                        }
                        messageBody.Append("</ul></dd>");
                        messageBody.AppendFormat("<dt>Song request:</dt><dd>{0}</dd>", string.IsNullOrWhiteSpace(rsvp.SongRequest) ? "[None]" : rsvp.SongRequest);
                    }
                    messageBody.Append("</dl>");



                    mail.Body = messageBody.ToString();
                    mail.IsBodyHtml = true;

                    smtp.Send(mail);
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
        }
    }
}