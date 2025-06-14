using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using Privilages.DAL;

namespace PlantQuar.WEB.Areas.EX_User.Controllers
{
    public class Add_UserController : BaseController
    {
        string apiName = "AddUser_API";
        // GET: EX_User/Add_User
        public ActionResult Index(int? ChangeOneUser=0)
        {
            try
            {
                ViewBag.AdminOrNot = ChangeOneUser;
                if (ChangeOneUser == 0)
                {
                    var Fees_Process = APIHandeling.getData(apiName + "?Outlet=-1");
                    var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                    ViewBag.ddd = lst;
                }
                else
                {
                    ViewBag.Outlet_Name = Session["Outlet_Name"].ToString();
                    ViewBag.UserId = Session["UserId"].ToString();
                    ViewBag.FullName = Session["FullName"].ToString();
                }

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UsersOutlet(long outletID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User=" + outletID + "");
                var lst = Fees_Process.Content.ReadAsAsync<List<User>>().Result;
                //  ViewBag.sss = lst;

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
        public ActionResult Insert_PR_User(short Id, string user_name, string pass, string email,bool IS_Change_Password)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Id=" + Id + "&user_name=" + user_name + "&pass=" + pass 
                    + "&email=" + email + "&IS_Change_Password="+ IS_Change_Password);
                //var lst = Fees_Process.Content.ReadAsAsync<User>().Result;
                //  ViewBag.sss = lst;
                var lst = Fees_Process.Content.ReadAsAsync<int>().Result;
                //return Json("", JsonRequestBehavior.AllowGet);
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
        public ActionResult Get_Email_PR_User(short Id)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User_Id=" + Id + "");
                var result = Fees_Process.Content.ReadAsAsync<User>().Result;
                

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }




    }
}
