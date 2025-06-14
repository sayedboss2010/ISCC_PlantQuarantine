using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlantQuar.WEB.Areas.Employee
{
    public class ChangePasswordController : Controller
    {
        
        // GET: Employee/ChangePassword
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult checkUserData(CustomUserLogin data)
        
        
        
        {
            try
            {
                var Fees_Process = APIHandeling.getData("ChangePassword_API?" +
                    "LoginName=" + data.LoginName+"&Password="+data.Password);
                var lst = Fees_Process.Content.ReadAsAsync<int>().Result;
                  

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult UpdateUserDatal(CustomUserLogin Dto)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    //model.User_Updation_Id=(short)Session["Language"];
                    //model.User_Updation_Date = DateTime.Now;

                    //check Repeated Data
                    Dto.Emp_ID =(short)Session["UserId"];
                     var res = APIHandeling.Put("ChangePassword_API", Dto);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateCompanyNational");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
       
        public ActionResult Logout()
        {
            User_Session current = User_Session.GetInstance;
            current.LogOut();
            // Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home", new { area = "" });
        }

    }
}