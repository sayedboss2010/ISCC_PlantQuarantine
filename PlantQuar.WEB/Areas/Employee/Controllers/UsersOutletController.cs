using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.DTO.Employee;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Employee.Controllers
{
    public class UsersOutletController : Controller
    {
        // GET: Employee/UsersOutlet
        string apiName = "Employee_Track_API";
        public ActionResult Index(long outletId, int Operation_Type, string Start_Date, string End_Date, long Emp_ID, long Company_ID, byte Committee_TypeId, int PageNo, string request_number)
        {
            try
            {
                //var OutLit_ID = Session["Outlet_ID"].ToString();
                var Fees_Process = APIHandeling.getData(apiName + "?outletId=" + outletId + "&Operation_Type=" + Operation_Type + "&Start_Date=" + Start_Date + "&End_Date=" + End_Date
                    + "&Emp_ID=" + Emp_ID + "&Company_ID=" + Company_ID + "&Committee_TypeId=" + Committee_TypeId + "&PageNo=" + PageNo + "&request_number=" + request_number);
                var lst = Fees_Process.Content.ReadAsAsync<List<Employee_TrackDTO>>().Result;
                ViewBag.page = PageNo;

                return View(lst);// Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



        public JsonResult DeleteEmployeeConfirm(long Committee_ID, long Employee_ID,bool Addmin_Confirm)
        {
            try
            {
                //var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("Employee_Track_API" + "?Committee_ID=" + Committee_ID + "&Employee_ID=" + Employee_ID+ "&Addmin_Confirm="+ Addmin_Confirm);
                var data = res.Content.ReadAsAsync<CommitteeEmployeeDTO>().Result;

                if (data != null)
                {
                    //if (data.User_Deletion_Date == true)
                    //{
                    //    Session["Path_Server"] = data.filePath;
                    //    return Json(new { result = data.result, noteAr = data.noteAr, noteEn = data.noteEn, filePath = data.filePath, labName = data.labName, analysisType = data.analysisType, rejectreason = data.rejectreason, Infection_Name = data.Infection_Name, Result_injury_Name = data.Result_injury_Name, SampleSize = data.SampleSize, farmSampleID = data.farmSampleId }, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    return Json("Su", JsonRequestBehavior.AllowGet);
                    //}
                }
                else
                {
                    return Json("Invalid", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Delete_Emp_Confirm");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }
}