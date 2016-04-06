using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ExcuteActionSpanAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.dtStart = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.dtEnd = DateTime.Now;
            var dtSpan = (DateTime)filterContext.Controller.ViewBag.dtEnd
                - (DateTime)filterContext.Controller.ViewBag.dtStart;

            filterContext.Controller.ViewBag.dtSpan = dtSpan;

            base.OnActionExecuted(filterContext);
        }

    }
}