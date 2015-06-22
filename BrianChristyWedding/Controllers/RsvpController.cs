using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrianChristyWedding.DAL;

namespace BrianChristyWedding.Controllers
{
    public class RsvpController : Controller
    {
        private WeddingContext db = new WeddingContext();

        // GET: Rsvp
        public ActionResult Index()
        {
            return View();
        }

        // GET: Rsvp
        public ActionResult Rsvp(string shortcode)
        {
            var invitation = db.Invitations.FirstOrDefault(x => x.Shortcode == shortcode);
            return View(invitation);
        }
    }
}