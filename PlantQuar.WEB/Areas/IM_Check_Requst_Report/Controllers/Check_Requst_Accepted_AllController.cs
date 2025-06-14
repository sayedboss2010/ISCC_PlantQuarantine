using PlantQuar.DTO.DTO.IM_Check_Requst_Report;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.IM_Check_Requst_Report.Controllers
{
    public class Check_Requst_Accepted_AllController : BaseController
    {
        // GET: IM_Check_Requst_Report/Check_Requst_Accepted_All      
        string api = "Check_Requst_Accepted_All_API";
        // string _ImCheckRequest_Number = "";

        public ActionResult Index(string ImCheckRequest_Number, int opt, int opt1, int opt2, int opt3, int opt4, int opt5, int opt6, int opt7, int opt8, int opt9, int opt10, int opt11)
        {
            Log_CheckRequest_DTO dto = new Log_CheckRequest_DTO();
            dto.ID_Table_Action = 25;
            dto.ID_TableActionValue = long.Parse(ImCheckRequest_Number);
            dto.Im_CheckRequest_ID = long.Parse(ImCheckRequest_Number);
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            dto.NOTS = " طلب طباعه استماره للفحص خلال الحجر الزراعي" + "_" + opt + "_" + opt1 + "_" + opt2 + "_" + opt3 + "_" + opt4 + "_" + opt5 + "_" + opt6 + "_" + opt7 + "_" + opt8 + "_" + opt9 + "_" + opt10 + "_" + opt11;
            dto.User_Type_ID = 127;
            dto.Type_log_ID = 135;
            APIHandeling.Put("Log_CheckRequest_API?itemFees=1", dto);

            //Session["_CheckReq_Number"] = ImCheckRequest_Number;
            ViewBag.opt = opt;
            ViewBag.opt1 = opt1;
            ViewBag.opt2 = opt2;
            ViewBag.opt3 = opt3;
            ViewBag.opt4 = opt4;
            ViewBag.opt5 = opt5;
            ViewBag.opt6 = opt6;
            ViewBag.opt7 = opt7;
            ViewBag.opt8 = opt8;
            ViewBag.opt9 = opt9;
            ViewBag.opt10 = opt10;
            ViewBag.opt11 = opt11;
            ViewBag.FullName = Session["FullName"];
            var res = APIHandeling.getData(api + "?ImCheckRequest_Number=" + ImCheckRequest_Number);
            var Lst = res.Content.ReadAsAsync<Check_Requst_Accepted_All_DTO>().Result;
            return View(Lst);
        }



        public ActionResult Accepted_Header()
        {
        ViewBag.FullName=Session["FullName"];
            var ImCheckRequest_Number = Session["_CheckReq_Number"];
            var res = APIHandeling.getData(api + "?ImCheckRequest_Number=" + ImCheckRequest_Number);
            var Lst = res.Content.ReadAsAsync<Check_Requst_Accepted_All_DTO>().Result;
            return View(Lst);
        }
    }
}