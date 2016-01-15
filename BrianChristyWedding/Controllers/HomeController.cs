using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrianChristyWedding.Models;

namespace BrianChristyWedding.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeddingContext db = new WeddingContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Rsvp(string shortcode)
        {
            ViewBag.Shortcode = shortcode;

            if (!string.IsNullOrWhiteSpace(shortcode))
            {
                var invitation = db.Invitations.FirstOrDefault(x => x.Shortcode == shortcode);
                return View(invitation);
            }
            return View();
        }
    }
}