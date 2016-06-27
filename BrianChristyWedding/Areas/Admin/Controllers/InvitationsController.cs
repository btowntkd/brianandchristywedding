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
                var invitations = db.Invitations;
                var csvResult = CsvGenerator.Generate(
                    invitations,
                    new CsvColumn<Invitation>("ID", x => x.ID),
                    new CsvColumn<Invitation>("FirstName", x => x.FirstName),
                    new CsvColumn<Invitation>("LastName", x => x.LastName),
                    new CsvColumn<Invitation>("CustomLabelName", x => x.CustomAddressLabelName),
                    new CsvColumn<Invitation>("Shortcode", x => x.Shortcode.ToUpper()),
                    new CsvColumn<Invitation>("MaxAllowedGuests", x => x.MaxAllowedGuests),
                    new CsvColumn<Invitation>("AddressLine1", x => x.Address.AddressLine1),
                    new CsvColumn<Invitation>("AddressLine2", x => x.Address.AddressLine2),
                    new CsvColumn<Invitation>("City", x => x.Address.City),
                    new CsvColumn<Invitation>("State", x => x.Address.State),
                    new CsvColumn<Invitation>("Zip", x => x.Address.Zip),
                    new CsvColumn<Invitation>("ShortcodeLink", x => "http://BrianAndChristyWedding.com/RSVP/" + x.Shortcode.ToUpper()),
                    new CsvColumn<Invitation>("EffectiveAddressName", x => string.IsNullOrWhiteSpace(x.CustomAddressLabelName) ? x.FirstName + " " + x.LastName : x.CustomAddressLabelName));
                
                return File(System.Text.Encoding.UTF8.GetBytes(csvResult), "text/csv", "Invitations.csv");
            }
            return HttpNotFound();
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
