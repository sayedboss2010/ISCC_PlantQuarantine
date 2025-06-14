using PlantQuar.DTO.DTO.General_Permissions;
using PlantQuar.DTO.DTO.General_Permissions.Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.General_Permission.Controllers
{
    public class Im_permissionDetailController : BaseController
    {
        // GET: General_Permission/Im_permissionDetail
        public ActionResult Index(decimal? ImPermission_Number)
        {
            var res = APIHandeling.getData("ListGeneral_PermissionAPI?detail=1&ImPermission_Number=" + ImPermission_Number);
            var Lst = res.Content.ReadAsAsync<ImPermissionPrintDetailsDTO>().Result;
            //
              var res2 = APIHandeling.getData("ListGeneral_PermissionAPI?ImPermissionRequestId=" + ImPermission_Number);
            var Lst2 = res2.Content.ReadAsAsync<List<Im_Permission_RefuseReasonDTO>>().Result;

            var reasons = APIHandeling.getData("ListGeneral_PermissionAPI?List=1&refuse=1");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;

            ViewBag.ListReasons = reasonsList;

            ViewBag.RefuseReasons = Lst2;
    
            return View(Lst);
        } 
       
        
        [HttpPost]
        public JsonResult GetTypes(int v1)
        {           
            int v = 1;
            var res = APIHandeling.getData("ListGeneral_PermissionAPI?id=" + v1+"&detail1=" + v);
            var Lst = res.Content.ReadAsAsync<List<ImPermissionPrintDetailsDTO>>().Result;

            //  var reasons = APIHandeling.getData("Im_CheckRequests_API?List=1&refuse=1");
            //  var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            //  ViewBag.ListReasons = reasonsList;
            for (int i=0;i<Lst.Count;i++)
            {
                Lst[i].Enrollment_type_AR= (Lst[i].Enrollment_Start)?.ToString("yyyy-MM-dd");
                Lst[i].Enrollment_type_EN = (Lst[i].Enrollment_End)?.ToString("yyyy-MM-dd");
            }
            return Json(Lst,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult saveReasons(List<short> listIDs, long Im_PermissionRequest_Id)
        {           
                Im_PermissionRequest_RefuseReasonDTO dto = new Im_PermissionRequest_RefuseReasonDTO();
                dto.Im_PermissionRequest_Id = Im_PermissionRequest_Id;
                dto.refuseReasonsIds = listIDs;
                User_Session Current = User_Session.GetInstance;
                dto.User_Creation_Id = (short)Session["UserId"];
                dto.User_Creation_Date = DateTime.Now;
                APIHandeling.Post("ListGeneral_PermissionAPI?listt=1", dto);
           
            return Json("succ");
        }
        //approve
        public ActionResult acceptRequest(long Im_PermissionRequest_Id )
        {
            ImPermissionPrintDetailsDTO model = new ImPermissionPrintDetailsDTO();
            model.Im_PermissionRequest_ID= Im_PermissionRequest_Id; 
            model.IsAcceppted = true;
            model.User_Creation_Id = (short)Session["UserId"];
            APIHandeling.Put("ListGeneral_PermissionAPI?approve=1", model);
            return RedirectToAction("Index", "ListIm_Permission_Filter");
        }

        public ActionResult GetReport(string path1)
        {
            try
            {
                Session["Path_Server"] = path1;
                return Redirect("~/ASP/DisplayImge.aspx");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Save_NoPaid( long Im_PermissionRequest_Id)
        {
            ImPermissionPrintDetailsDTO dto = new ImPermissionPrintDetailsDTO();
            dto.Im_PermissionRequest_ID = Im_PermissionRequest_Id;
            dto.IsAcceppted = true;
            dto.IsPaid = true;
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;

            APIHandeling.Post("ListGeneral_PermissionAPI?NoPaid=1", dto);
            return Json("succ");
        }
    }
}