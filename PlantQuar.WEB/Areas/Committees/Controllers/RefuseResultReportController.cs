using PlantQuar.DTO.DTO.Log;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class RefuseResultReportController :BaseController
    {
        // GET: Committees/RefuseResultReport
        public ActionResult Index(string ImCheckRequest_Number)
        {
            Log_CheckRequest_DTO dto = new Log_CheckRequest_DTO();
            dto.ID_Table_Action = 48;
            dto.ID_TableActionValue = long.Parse(ImCheckRequest_Number);
            dto.Im_CheckRequest_ID = long.Parse(ImCheckRequest_Number);
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            dto.NOTS = " تقديم طلب لطباعه إخطار الرفض للفحص خلال الحجر الزراعي";
            dto.User_Type_ID = 127;
            dto.Type_log_ID = 135;
            APIHandeling.Put("Log_CheckRequest_API?itemFees=1", dto);
            return View();
        }
    }
}