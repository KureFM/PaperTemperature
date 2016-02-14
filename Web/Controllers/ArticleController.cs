using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index(int? id)
        {
            if (id <= 4 && id > 0)
            {
                ViewBag.SubjectID = 3;
            }
            else if (id >= 5 && id <= 8)
            {
                ViewBag.SubjectID = 6;
            }
            else if (id >= 9 && id <= 12)
            {
                ViewBag.SubjectID = 7;
            }
            ViewBag.ArticleID = id;
            return View();
        }

    }
}
