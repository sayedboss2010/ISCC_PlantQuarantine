using PlantQuar.DTO.DTO.Im_Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using static PlantQuar.DTO.DTO.Im_Permissions.ActivePrintDTO;

namespace PlantQuar.WEB.Areas.Im_Permissions.Controllers
{
    public class Stopping_PermissionController : BaseController
    {

        //hhhhhh
        // GET: Im_Permissions/Stopping_Permission
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPermissionsList(long? ImPermission_Number, int Isacceppted)
        {           
            var res = APIHandeling.getData("Stopping_PermissionAPI?List=1&ImPermission_Number=" + ImPermission_Number);
            var Lst = res.Content.ReadAsAsync<List<Stopping_PermissionsDTO>>().Result;
            if (Lst != null)
            {
                return Json(Lst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }

        [HttpPost]
        public JsonResult UpdateUserDatal(ActivePrintDto activePrintDto)
        {
            try
            {
                // if (ModelState.IsValid)
                // {
                User_Session user_Session = User_Session.GetInstance;

                activePrintDto.User_Creation_Id = (short)Session["UserId"];
                var res = APIHandeling.Put("Stopping_PermissionAPI", activePrintDto);

                return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
                  : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                //  }
                //  else
                // {
                //   return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                //  }
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
    }
}