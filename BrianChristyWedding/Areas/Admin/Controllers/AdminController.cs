﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrianChristyWedding.Areas.Admin.Controllers
{
    [BasicAuthentication("admin", "wedding1", BasicRealm = "BrianChristyWedding")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}