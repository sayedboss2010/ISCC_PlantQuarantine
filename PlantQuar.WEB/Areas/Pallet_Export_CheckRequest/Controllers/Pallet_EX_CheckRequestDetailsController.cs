

using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Pallet_Export_CheckRequest.Controllers
{
    public class Pallet_EX_CheckRequestDetailsController : BaseController
    {
        // GET: Pallet_Export_CheckRequest/Pallet_EX_CheckRequestDetails

        public ActionResult Index(string ImCheckRequest_Number, int Hide_Button = 0)
        {
            var res = APIHandeling.getData("Pallet_EX_CheckRequestDetails_API?Pallet_ExCheckRequest_Number=" + ImCheckRequest_Number);
            //var Lst = res.Content.ReadAsAsync<Pallet_EXRequestDetailsDTO>().Result;
            /// var Lst = res.Content.ReadAsAsync<>().Result;
            var Lst = res.Content.ReadAsAsync<List<Pallet_EXRequestDetailsDTO>>().Result;

            var reasons = APIHandeling.getData("Pallet_EX_CheckRequestDetails_API?List=1&refuse=1");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ListReasons = reasonsList;
            ViewBag.Hide_Button = Hide_Button;
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
            APIHandeling.Post("Pallet_EX_CheckRequests_API?listt=1", dto);
            return Json("succ");
        }

        [HttpPost]
        public ActionResult acceptRequest(Pallet_EXRequestDetailsDTO model)
        {
            try
            {
                EX_CheckRequestDTO dto = new EX_CheckRequestDTO();
                dto.ID = model.Ex_CheckRequest_ID;
                dto.IsAccepted = (bool)model.IsAccepted;

                APIHandeling.Put("Pallet_EX_CheckRequests_API?approve1=1", dto);

                return RedirectToAction("Index", "Pallet_List_EXCheckRequest");
            }
            catch (Exception ex)
            {
                return null;
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
            }

        }

        [HttpPost]
        public JsonResult saveItemFees(long? itemId, decimal? fees)
        {
            Items_checkReq item = new Items_checkReq();
            item.Ex_Items_checkReqID = itemId;
            item.Fees = fees;
            APIHandeling.Put("Pallet_EX_CheckRequests_API?itemFees=1", item);
            return Json("succ");
        }

        public ActionResult GetReport(string path1)
        {
            try
            {

                Session["Path_Server"] = path1;// @"\plant\Import\Ex_CheckRequests\2021\10\Ex_CheckRequestFiles595\1234.jfif";
                return Redirect("~/ASP/DisplayImge.aspx");

            }
            catch (Exception ex)
            {

                return null;

            }

        }
    }
}