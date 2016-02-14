using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SubjectController : Controller
    {
        //
        // GET: /Subject/

        public ActionResult Index(int? id)
        {
            ViewBag.SubjectID = id;
            return View();
        }
    }
}
