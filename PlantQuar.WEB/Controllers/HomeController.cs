using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using Privilages.DAL;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;

namespace PlantQuar.WEB.Controllers
{
    public class HomeController :Controller
    {

        //dbPrivilageEntities db = new dbPrivilageEntities();
        //[RedirectingAction]
        public ActionResult Index()
        {
            try
            {
                ViewBag.Outlet_Name = Session["Outlet_Name"].ToString();
                return View();
            }
            catch (System.Exception ex)
            {

                return null;
            }
           
        }

        // GET: AccountOperation
        //[RedirectingAction]
        public ActionResult Login()
        {
            Models.CustomUserLogin user = new Models.CustomUserLogin();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(CustomUserLogin user)
        {
            //// if  // DEBUG
            bool isFoundOnActive = false; //fnValidateUser(user.LoginName, user.Password);
            bool isChanged = true; //fnValidateUser(user.LoginName, user.Password);
            #region Old Code

            int IsVaild = User_Session.LogIn(user);
 
            switch (IsVaild)
            {
                case (int)Enums.User_Login.Valid:
                    {
                        isFoundOnActive = true;
                     

                        break;
                    }
                case (int)Enums.User_Login.InvalidCredential:
                    {
                        ViewBag.msg = "InvalidCredential";
                          //  Enums.GetEnumDescription<Enums.Error>(Enums.User_Login.InvalidCredential);
                        break;
                    }

                case (int)Enums.User_Login.LoginFromAnotherDevice:
                    {
                        ViewBag.msg = "LoginFromAnotherDevice";
                     //   ViewBag.msg = Enums.GetEnumDescription<Enums.Error>(Enums.User_Login.LoginFromAnotherDevice);
                        break;
                    }
                case (int)Enums.User_Login.ValidNotPasswordChanged:
                    {
                        isChanged = false;
                          
                        break;
                    }
            }

            if (isChanged)
            {
                return (isFoundOnActive == true) ?
               (ActionResult)RedirectToAction
               ("Index", "Home", new { area = "" }) : View();
            }
            else
            {
                //return      (ActionResult)RedirectToAction
                //    ("Index", "ChangePassword", new { area = "" });
                //  return View("~/Employee/Views/ChangePassword/Index.cshtml");
                // return  View("~/Views/Employee/ChangePassword/Index");
                return RedirectToAction("Index", "Employee/ChangePassword");

            }
            #endregion

        }

     
        public ActionResult LogOut()
        {
            User_Session current = User_Session.GetInstance;
            current.LogOut();
            // Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}