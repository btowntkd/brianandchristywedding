using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrianChristyWedding.Models;
using System.Net.Mail;

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

        [HttpPost]
        public ActionResult Contact([Bind(Include = "Name,Email,Subject,Body")] ContactViewModel contactForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        var mail = new MailMessage();
                        mail.To.Add("bcpratt@live.com");
                        mail.To.Add("chrispy115@gmail.com");
                        mail.Subject = "Message from Wedding Website";
                        mail.ReplyToList.Clear();
                        mail.ReplyToList.Add(contactForm.Email);
                        mail.Body = string.Format("<p>Message from {0} ({1})</p><p>Subject: {2}</p><p></p><p>{3}</p>",
                            contactForm.Name,
                            contactForm.Email,
                            contactForm.Subject,
                            contactForm.Body);
                        mail.IsBodyHtml = true;

                        smtp.Send(mail);
                    }
                    return View("ContactSuccess");
                }
                catch (Exception)
                {
                    return View("ContactError");
                }
            }
            return View();
        }
    }
}