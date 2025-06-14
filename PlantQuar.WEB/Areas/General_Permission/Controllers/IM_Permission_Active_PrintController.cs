using PlantQuar.DTO.DTO.General_Permissions.Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static PlantQuar.DTO.DTO.Im_Permissions.ActivePrintDTO;


namespace PlantQuar.WEB.Areas.General_Permission.Controllers
{
    public class IM_Permission_Active_PrintController : Controller
    {
        // GET: General_Permission/IM_Permission_Active_Print
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPermissionsList(decimal? ImPermission_Number
            , int Isacceppted,int OperationCode)
        {

            User_Session user_Session = User_Session.GetInstance;

            var res = APIHandeling.getData("ListGeneral_PermissionAPI?List=1&ImPermission_Number=" + ImPermission_Number+ "&OperationCode=" + OperationCode);
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

                activePrintDto.User_Creation_Id = (short)Session["UserId"];
                var res = APIHandeling.Put("ListGeneral_PermissionAPI", activePrintDto);

                //#region Log_Station
                //var dto2 = new DAL.Table_Action_Log_Station();
                //dto2.ID_TableActionValue = Station_Id;
                //dto2.Station_ID = Station_Id;
                //dto2.User_Creation_Id = (short)Session["UserId"];
                //dto2.User_Creation_Date = DateTime.Now;
                //if (IsActive == true)
                //{
                //    dto2.ID_Table_Action = 36;
                //    dto2.NOTS = " تم الموافقة علي المحطة ";
                //}
                //else
                //{
                //    dto2.ID_Table_Action = 37;
                //    dto2.NOTS = " تم رفض المحطة  ";
                //}
                //dto2.User_Type_ID = 127;
                //dto2.Type_log_ID = 135;
                //APIHandeling.Put("Log_CheckRequest_API?Station_Log=1", dto2);
                //#endregion

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