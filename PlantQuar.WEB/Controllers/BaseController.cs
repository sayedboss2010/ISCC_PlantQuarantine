using PlantQuar.WEB.App_Start;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

namespace PlantQuar.WEB.Controllers
{
    public class BaseController : Controller
    {
        //public class RedirectingAction : ActionFilterAttribute
        //{
        //    public override void OnActionExecuting(ActionExecutingContext context)
        //    {
        //        base.OnActionExecuting(context);
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-Eg");
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-Eg");
        //        User_Session Current = User_Session.GetInstance;

        //        if ((long)(short)Session["UserId"] == 0)
        //        {
        //            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //            {
        //                controller = "Home",
        //                action = "Login"
        //            }));
        //        }
        //        else
        //        {

        //            if (Session["Language"].ToString() == "ar-Eg")
        //            {
        //                Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-Eg");
        //                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-Eg");
        //            }
        //            else
        //            {
        //                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        //            }
        //        }
        //    }
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                User_Session Current = User_Session.GetInstance;
                var userId = Session["UserId"];

                if (Session["UserId"] == null)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-Eg");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-Eg");
                   filterContext.Result = new RedirectResult("~/Home/Login");
                    //filterContext.Result = RedirectToAction("Login", "Home");
//                    filterContext.Result = new RedirectToRouteResult(
//    new RouteValueDictionary
//    {
//        {"controller", "Home"},
//        {"action", "Login"}
//    }
//);
                    return;
                }
                else
                {

                    if (Session["Language"].ToString() == "ar-Eg")
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-Eg");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-Eg");
                    }
                    else
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    }
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
        }

        public ActionResult ChangeLanguage()
        {
            User_Session Current = User_Session.GetInstance;

            //Language_IsAr = 1; ar ->true
           // User_Session.Set_Language((Current.Language_IsAr == 1 ? false : true));
            User_Session.Set_Language(Session["Language"].ToString() == "ar-Eg" ? false : true);
            
            return Redirect(Request.UrlReferrer.ToString());

        }
    }
}