using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class Transation_LogsController : Controller
    {
        // GET: Admin/Transation_Logs

        string apiName = "Transation_Logs_API";
        public ActionResult Index()
        {
            return View();


            //SELECT ta.Name_Ar,u.FullName,l.User_Creation_Date
            //FROM Im_PermissionRequest PR
            //INNER JOIN Table_Action_Log l ON pr.ID = l.ID_TableActionValue
            //INNER JOIN dbo.Table_Action ta ON l.ID_Table_Action = ta.ID
            //INNER JOIN[dbPrivilage].dbo.PR_User u ON l.User_Creation_Id = u.Id
            //WHERE pr.ImPermission_Number = '16532022207102722'

        }

        [HttpPost]

        public JsonResult Get_Employee_Data_List(int Operation_Type_ID, decimal Order_Permission_Number)
        {
            try
            {

                var res = APIHandeling.getData(apiName + "?Operation_Type_ID=" + Operation_Type_ID + "&Order_Permission_Number=" + Order_Permission_Number);

                var lst = res.Content.ReadAsAsync<List<Transation_LogsDTO>>().Result;


                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CenterList_ByGov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }
}