using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.IM_Committee;
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
using PlantQuar.DTO.DTO.Log;

namespace PlantQuar.WEB.Areas.Im_CheckRequests.Controllers
{
    public class Im_CheckRequestDetailsController : BaseController
    {
        // GET: Im_CheckRequests/Im_CheckRequestDetails
        public ActionResult Index(string ImCheckRequest_Number, int Outlet_ID, int Hide_Button = 0)
        {
            var res = APIHandeling.getData("Im_CheckRequests_API?ImCheckRequest_Number=" + ImCheckRequest_Number + "&Outlet_ID=" + Outlet_ID);
            var Lst = res.Content.ReadAsAsync<ImRequestDetailsDTO>().Result;

            var reasons = APIHandeling.getData("Im_CheckRequests_API?List=1&refuse=1");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ListReasons = reasonsList;
            ViewBag.Hide_Button = Hide_Button;


            ////noura   return 2 lists

            //var res1 = APIHandeling.getData("ImCommittee_Final_Result_API?ImCheckRequest_Number=" + ImCheckRequest_Number);

            //var Lst1 = res1.Content.ReadAsAsync<Fees_ALL>().Result;

            //return View(Lst1);
            //noura

            return View(Lst);
        }

        [HttpPost]
        public ActionResult saveReasons(List<short> listIDs, long checkReqId)
        {
            ReasonsListReqIdDTO dto = new ReasonsListReqIdDTO();
            dto.checkReqId = checkReqId;
            dto.refuseReasonsIds = listIDs;
            User_Session Current = User_Session.GetInstance;
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            APIHandeling.Post("Im_CheckRequests_API?listt=1", dto);
            return Json("succ");
        }

        [HttpPost]
        public ActionResult acceptRequest(ImRequestDetailsDTO model)
        {
            try
            {
                Im_CheckRequestDTO dto = new Im_CheckRequestDTO();
                dto.ID = model.Im_CheckRequest_ID;
                dto.IsAccepted = (bool)model.IsAccepted;
                dto.IsAccepted_Date = DateTime.Now;
                dto.IsActive= (bool)model.IsAccepted;
                dto.User_Updation_Id = (short)Session["UserId"];
                dto.User_Updation_Date = DateTime.Now;
                APIHandeling.Put("Im_CheckRequests_API?approve=1", dto);


                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();                
                dto2.ID_TableActionValue = model.Im_CheckRequest_ID;
                dto2.Im_CheckRequest_ID = model.Im_CheckRequest_ID;
                dto2.User_Creation_Id = (short)Session["UserId"];
                dto2.User_Creation_Date = DateTime.Now;
                if (model.IsAccepted == true)
                {
                    dto2.ID_Table_Action = 12;
                    dto2.NOTS = " تم الموافقة علي الطلب ";
                }
                else
                {
                    dto2.ID_Table_Action = 23;
                    dto2.NOTS = " تم رفض الطلب  ";
                }
                dto2.User_Type_ID = 127;
                dto2.Type_log_ID = 135;
                APIHandeling.Put("Log_CheckRequest_API?itemFees=1", dto2);

                return RedirectToAction("Index", "List_ImCheckRequest");
            }
            catch (Exception ex)
            {
                return null;
               // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
            }
           
        }

        [HttpPost]
        public JsonResult saveItemFees(long? itemId,long Im_CheckRequest_ID, decimal? fees)
        {
            Items_checkReq item = new Items_checkReq();
            item.ImcheckReqItem_ID = itemId;
            item.Fees = fees;
            item.User_Updation_Id = (short)Session["UserId"];
            item.User_Updation_Date = DateTime.Now;
            APIHandeling.Put("Im_CheckRequests_API?itemFees=1", item);
            //Eslam
            Log_CheckRequest_DTO dto = new Log_CheckRequest_DTO();
            dto.ID_Table_Action = 15;
            dto.ID_TableActionValue = itemId;
            dto.Im_CheckRequest_ID = Im_CheckRequest_ID;
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            dto.NOTS = " تعديل رسوم النبات من خلال الحجر الزراعي";
            dto.User_Type_ID = 127;
            dto.Type_log_ID = 135;
            APIHandeling.Put("Log_CheckRequest_API?itemFees=1", dto);
            return Json("succ");
        }

        public ActionResult GetReport(string path1)
        {
            try
            {

                Session["Path_Server"] = path1;// @"\plant\Import\Im_CheckRequests\2021\10\Im_CheckRequestFiles595\1234.jfif";
                return Redirect("~/ASP/DisplayImge.aspx");

            }
            catch (Exception ex)
            {

                return null;

            }

        }
    }
}