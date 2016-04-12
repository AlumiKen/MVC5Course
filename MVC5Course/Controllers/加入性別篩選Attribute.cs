using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class 加入性別篩選Attribute : ActionFilterAttribute
    {
        public 加入性別篩選Attribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}