using PlantQuar.DTO.DTO;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.Web;
using PlantQuar.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PlantQuar.Web.Controllers;
using System.Web.Script.Serialization;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class Im_CheckRequestController : BaseController
    {
        string apiName = "Im_Check_Request";
        string actionName = "GetList_Permission_Requests";

        // GET: ImportLookups/ExaminationRequest
        public ActionResult Index()
        {
            return View();
        }



        //LOAD SEARCH
        public JsonResult ListImPermission_Number(int ImPermission_Number = 0 , int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var res = APIHandeling.getDataRpc(apiName , actionName ,ImPermission_Number ,jtPageSize ,jtStartIndex );
                
                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                //var count = myObj.ElementAt(0).Value;
                //var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = lst, TotalRecordCount = lst.Count() });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listAnalysisType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


    }
}