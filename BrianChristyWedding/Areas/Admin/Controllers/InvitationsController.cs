using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrianChristyWedding.Models;
using System.Text;

namespace BrianChristyWedding.Areas.Admin.Controllers
{
    [BasicAuthentication("admin", "wedding1", BasicRealm = "BrianChristyWedding")]
    public class InvitationsController : Controller
    {
        private WeddingContext db = new WeddingContext();

        // GET: Admin/Invitations
        public ActionResult Index()
        {
            var invitations = db.Invitations.Include(i => i.Rsvp);
            return View(invitations.OrderBy(x => x.LastName).ToList());
        }

        // GET: Admin/Invitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Admin/Invitations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,CustomAddressLabelName,MaxAllowedGuests,Address")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Invitations.Add(invitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invitation);
        }

        // GET: Admin/Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Rsvps, "InvitationID", "InvitationID", invitation.ID);
            return View(invitation);
        }

        // POST: Admin/Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,CustomAddressLabelName,MaxAllowedGuests,Shortcode,Address")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Rsvps, "InvitationID", "InvitationID", invitation.ID);
            return View(invitation);
        }

        // GET: Admin/Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Admin/Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export(string format = "csv")
        {
            if (format == "csv")
            {
                var invitations = db.Invitations.Include(i => i.Rsvp);
                var csvResult = new StringBuilder();
                csvResult.AppendLine(
                    string.Join(",",
                        "ID",
                        "FirstName",
                        "LastName",
                        "CustomLabelName",
                        "Shortcode",
                        "MaxAllowedGuests",
                        "AddressLine1",
                        "AddressLine2",
                        "City",
                        "State",
                        "Zip"));
                foreach (var invitation in invitations)
                {
                    csvResult.AppendLine(
                        string.Join(",",
                            StringToCsvCell(invitation.ID.ToString()),
                            StringToCsvCell(invitation.FirstName),
                            StringToCsvCell(invitation.LastName),
                            StringToCsvCell(invitation.CustomAddressLabelName),
                            StringToCsvCell(invitation.Shortcode),
                            StringToCsvCell(invitation.MaxAllowedGuests.ToString()),
                            StringToCsvCell(invitation.Address.AddressLine1),
                            StringToCsvCell(invitation.Address.AddressLine2),
                            StringToCsvCell(invitation.Address.City),
                            StringToCsvCell(invitation.Address.State),
                            StringToCsvCell(invitation.Address.Zip)));
                }
                return File(System.Text.Encoding.UTF8.GetBytes(csvResult.ToString()), "text/csv", "Invitations.csv");
            }
            return HttpNotFound();
        }

        /// <summary>
        /// Turn a string into a CSV cell output
        /// </summary>
        /// <param name="str">String to output</param>
        /// <returns>The CSV cell formatted string</returns>
        private static string StringToCsvCell(string str)
        {
            if(string.IsNullOrWhiteSpace(str))
                return string.Empty;

            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
