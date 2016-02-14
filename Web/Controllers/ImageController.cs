using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace Web.Controllers
{    
    [OutputCache(Duration = 21600, VaryByParam = "None")]
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index(string id)
        {
            return ImageToFile(Res.GetImage(id));
        }

        public ActionResult Subject(int aid, int bid)
        {
            var imgName = string.Format("Subject{0:D2}Pic{1:D2}", aid, bid);
            return ImageToFile(Res.GetImage(imgName));
        }

        public ActionResult Article(int aid, int bid)
        {
            var imgName = string.Format("Article{0:D2}Pic{1:D2}", aid, bid);
            return ImageToFile(Res.GetImage(imgName));
        }

        private FileContentResult ImageToFile(Bitmap img)
        {
            using (var memStream = new System.IO.MemoryStream())
            {
                img.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return File(memStream.GetBuffer(), "image/jpeg");
            }
        }
    }
}
