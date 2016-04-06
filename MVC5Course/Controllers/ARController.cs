using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return PartialView("Index");
        }

        public ActionResult FileTest(int? dl)
        {
            if (dl.HasValue)
            {
                return File(Server.MapPath("~/Image/pkc.png"), "Image/png", "pikachu.png");
            }
            else
            {
                return File(Server.MapPath("~/Image/pkc.png"), "Image/png");
            }
        }

        public ActionResult JsonTest(int id)
        {
            //暫時關閉延遲載入, 避免循環參考
            repoProduct.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            var product = repoProduct.Find(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}