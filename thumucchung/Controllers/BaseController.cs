using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace thumucchung.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["username"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);


            // Method Return Previous
            var httpContext = filterContext.HttpContext;

            if (httpContext.Request.RequestType == "GET"
                && !httpContext.Request.IsAjaxRequest()
                && filterContext.IsChildAction == false)    // do no overwrite if we do child action.
            {
                // stop overwriting previous page if we just reload the current page.
                if (Session["CurUrl"] != null
                    && ((Uri)Session["CurUrl"]).Equals(httpContext.Request.Url))
                    return;

                Session["PrevUrl"] = Session["CurUrl"] ?? httpContext.Request.Url;
                Session["CurUrl"] = httpContext.Request.Url;
            }
        }
        public ActionResult RedirectToPrevious(String defaultAction, String defaultController)
        {
            if (Session == null || Session["PrevUrl"] == null)
            {
                return RedirectToAction(defaultAction, defaultController);
            }

            String url = ((Uri)Session["PrevUrl"]).PathAndQuery;

            if (Request.Url != null && Request.Url.PathAndQuery != url)
            {
                return Redirect(url);
            }

            return RedirectToAction(defaultAction, defaultController);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "waring")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}