using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static PlantQuar.DTO.DTO.Im_Permissions.ActivePrintDTO;

namespace PlantQuar.WEB.Areas.Im_Permissions.Controllers
{
    public class IM_Permission_Active_PrintController : BaseController
    {
        // GET: Im_Permissions/IM_Permission_Active_Print
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPermissionsList(decimal? ImPermission_Number
            , int Isacceppted)
        {

            User_Session user_Session = User_Session.GetInstance;

            var res = APIHandeling.getData("ListIm_PermissionAPI?List=1&ImPermission_Number=" + ImPermission_Number   );
            var Lst = res.Content.ReadAsAsync<List<ImPermissionsListDTO>>().Result;
            //Lst[0].CanPrint = user_Session.CanPrint;
            //return View(Lst.ToPagedList(pageNumber, pageSize));
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult UpdateUserDatal(ActivePrintDto activePrintDto)
        {
            try
            {
               // if (ModelState.IsValid)
               // {
            User_Session user_Session = User_Session.GetInstance;

                activePrintDto.User_Creation_Id = (short)Session["UserId"] ;
                var res = APIHandeling.Put("ListIm_PermissionAPI", activePrintDto);


                #region Log_Permission
                var dto2 = new DAL.Table_Action_Log();
                dto2.ID_TableActionValue = activePrintDto.ImPermission_Number_ID;
                dto2.Im_PermissionRequest_ID = activePrintDto.ImPermission_Number_ID; 
                dto2.User_Creation_Id = (short)Session["UserId"];
                dto2.User_Creation_Date = DateTime.Now;
                if (activePrintDto.IS_Print_Ar == true)
                {
                    dto2.ID_Table_Action = 45;
                    dto2.NOTS = activePrintDto.NOTS_AR ;
                }
                else {
                    dto2.ID_Table_Action = 45;
                    dto2.NOTS = activePrintDto.NOTS_AR;
                }
 
                dto2.User_Type_ID = 127;
                dto2.Type_log_ID = 135;
                APIHandeling.Put("Log_CheckRequest_API?Permission_Log=1", dto2);

                var dto3 = new DAL.Table_Action_Log();
                dto3.ID_TableActionValue = activePrintDto.ImPermission_Number_ID;
                dto3.Im_PermissionRequest_ID = activePrintDto.ImPermission_Number_ID;
                dto3.User_Creation_Id = (short)Session["UserId"];
                dto3.User_Creation_Date = DateTime.Now;
             if (activePrintDto.IS_Print_EN == true)
                {
                    dto3.ID_Table_Action = 46;
                    dto3.NOTS = activePrintDto.NOTS_EN;
                }
                dto3.User_Type_ID = 127;
                dto3.Type_log_ID = 135;
                APIHandeling.Put("Log_CheckRequest_API?Permission_Log=1", dto3);
                #endregion

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