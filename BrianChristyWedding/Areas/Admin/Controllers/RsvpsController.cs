using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrianChristyWedding.Models;

namespace BrianChristyWedding.Areas.Admin.Controllers
{
    [BasicAuthentication("admin", "wedding1", BasicRealm = "BrianChristyWedding")]
    public class RsvpsController : Controller
    {
        private WeddingContext db = new WeddingContext();

        // GET: Admin/Rsvps
        public ActionResult Index()
        {
            var rsvps = db.Rsvps.Include(r => r.Invitation);
            return View(rsvps.ToList());
        }

        // GET: Admin/Rsvps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rsvp rsvp = db.Rsvps.Find(id);
            if (rsvp == null)
            {
                return HttpNotFound();
            }
            return View(rsvp);
        }

        // GET: Admin/Rsvps/Create
        public ActionResult Create()
        {
            ViewBag.InvitationID = new SelectList(db.Invitations, "ID", "FullName");
            return View();
        }

        // POST: Admin/Rsvps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvitationID,Created,Updated")] Rsvp rsvp)
        {
            if (ModelState.IsValid)
            {
                db.Rsvps.Add(rsvp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvitationID = new SelectList(db.Invitations, "ID", "Name", rsvp.InvitationID);
            return View(rsvp);
        }

        // GET: Admin/Rsvps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rsvp = db.Rsvps.Find(id);
            if (rsvp == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvitationID = new SelectList(db.Invitations, "ID", "Name", rsvp.InvitationID);
            return View(rsvp);
        }

        // POST: Admin/Rsvps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvitationID,Created,Updated")] Rsvp rsvp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rsvp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvitationID = new SelectList(db.Invitations, "ID", "Name", rsvp.InvitationID);
            return View(rsvp);
        }

        // GET: Admin/Rsvps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rsvp rsvp = db.Rsvps.Find(id);
            if (rsvp == null)
            {
                return HttpNotFound();
            }
            return View(rsvp);
        }

        // POST: Admin/Rsvps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rsvp rsvp = db.Rsvps.Find(id);
            db.Rsvps.Remove(rsvp);
            db.SaveChanges();
            return RedirectToAction("Index");
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
