using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.EX_User.Controllers
{
    public class User_PrivilageController : BaseController
    {
        // GET: EX_User/User_Privilage
        string apiName = "User_Privilage_API";
        public ActionResult Index()
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Outlet=-1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.ddd = lst;

                var group_Process = APIHandeling.getData(apiName + "?Group=-1");
                var lst2 = group_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.group = lst2;




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
        public JsonResult Module_AddEDIT(long PR_GroupId)
        {

            try
            {
                var Module_Process = APIHandeling.getData(apiName + "?PR_GroupId=" + PR_GroupId);
                var lst = Module_Process.Content.ReadAsAsync<List<CustomOption>>().Result;

                //var res = APIHandeling.getData("MainClassification_API?AddEdit=1&ItemType_ID=" + ItemType_ID);
                //var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "MainClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Menu_AddEDIT(long PR_ModuleId, long PR_GroupId)
        {

            try
            {

                var Menu_Process = APIHandeling.getData(apiName + "?PR_ModuleId=" + PR_ModuleId
                    + "&PR_GroupId=" + PR_GroupId);
                var lst = Menu_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                //ViewBag.Menu = lst4;

                //var Module_Process = APIHandeling.getData(apiName + "?PR_GroupId=" + PR_GroupId);
                //var lst = Module_Process.Content.ReadAsAsync<List<CustomOption>>().Result;

                //var res = APIHandeling.getData("MainClassification_API?AddEdit=1&ItemType_ID=" + ItemType_ID);
                //var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //return Json(new { Result = "OK", Options = lst });

                return Json(lst, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "MainClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult InsertEmpPrivilage(short EmpId, List<Emp_New_DTO> menus_new, List<Emp_Old_DTO> menus_old)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    User_PrivilageDTO menus = new User_PrivilageDTO();

                    menus.EmpId = EmpId;
                    menus.List_Emp_New = menus_new;
                    menus.List_Emp_Old = menus_old;
                    var res = APIHandeling.Post(apiName, menus);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = menus })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


        [HttpPost]
        public ActionResult Get_Priv_Emp_Old(short checkedEmpId)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?checkedEmpId=" + checkedEmpId + "");
                var lst = Fees_Process.Content.ReadAsAsync<List<Emp_Old_DTO>>().Result;
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
        public ActionResult DeleteEmpPrivilage(List<User_PrivilageDTO> Check_Delete_id)
        {
            try
            {
                var deleteEmpRows = APIHandeling.getData(apiName + "?Check_Delete_id=" + Check_Delete_id + "");
                var lst = deleteEmpRows.Content.ReadAsAsync<List<User_PrivilageDTO>>().Result;
                //  ViewBag.sss = lst;
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                //return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public ActionResult Get_Priv_Emp_Old2(long Check_Delete_id)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Check_Delete_id=" + Check_Delete_id + "");
                var lst = Fees_Process.Content.ReadAsAsync<List<User_PrivilageDTO>>().Result;
                //  ViewBag.sss = lst;
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }
}