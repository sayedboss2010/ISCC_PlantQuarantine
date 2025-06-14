using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequests.Controllers
{
    public class Im_checkRequest_Update_PortController : BaseController
    {
        // GET: Im_CheckRequests/Im_checkRequest_Update_Port
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult Port_List(string CheckNumber)
        {
            try
            {
                var res = APIHandeling.getData("Im_checkRequest_Update_PortAPI?CheckNumber=" + CheckNumber);
                var lst = res.Content.ReadAsAsync<List<Im_checkRequest_Update_PortDTO>>().Result;//object
                //return Json(new { Result = "OK", Options = lst });
                return Json(new { Result = lst.FirstOrDefault().Message, Records = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Port_ID(int government_ID)
        {
            try
            {
                var res = APIHandeling.getData("Im_checkRequest_Update_PortAPI?government_ID=" + government_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Port_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult InternationalPort_ID(int country_ID)
        {
            try
            {
                var res = APIHandeling.getData("Im_checkRequest_Update_PortAPI?country_ID=" + country_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "InternationalPort_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult country_ID()
        {
            try
            {
                var res = APIHandeling.getData("Im_checkRequest_Update_PortAPI?id=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "country_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Government_ID()
        {
            try
            {
                var res = APIHandeling.getData("Im_checkRequest_Update_PortAPI?gov=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Government_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
       

        public JsonResult update_port( List<Im_CheckRequest_PortDTO> list_of_values, string CheckNumber)
        {
            try
            {
                var res = APIHandeling.Put("Im_checkRequest_Update_PortAPI?UserId=" + (short)Session["UserId"], list_of_values);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(res, JsonRequestBehavior.AllowGet);
                //Eslam
                //PlantQuarantineEntities entities1 = new PlantQuarantineEntities();
                //var CheckNumber_ID = entities1.Im_CheckRequest.Where(a => a.CheckRequest_Number == CheckNumber).Select(a => a.ID).SingleOrDefault();
                //var CheckNumberData_ID = entities1.Im_CheckRequest_Data.Where(a => a.Im_CheckRequest_ID == CheckNumber_ID).Select(a => a.ID).SingleOrDefault();
                //var CheckNumberPort_ID = entities1.Im_CheckRequest_Port.Where(a => a.Im_CheckRequest_Data_ID == CheckNumberData_ID).Select(a => a.ID).SingleOrDefault();
                //Log_CheckRequest_DTO dto = new Log_CheckRequest_DTO();
                //dto.ID_Table_Action = 49;
                //dto.ID_TableActionValue = CheckNumberPort_ID;
                //dto.Im_CheckRequest_ID = CheckNumber_ID;
                //dto.User_Creation_Id = (short)Session["UserId"];
                //dto.User_Creation_Date = DateTime.Now;
                //dto.NOTS = " تغيير المواني من خلال الحجر الزراعي";
                //dto.User_Type_ID = 127;
                //dto.Type_log_ID = 135;
                //APIHandeling.Put("Log_CheckRequest_API?itemFees=1", dto);
                //List<Im_CheckRequest_PortDTO> model = new List<Im_CheckRequest_PortDTO>();
                //for (int i = 0; i < list_of_values.Count; i++)
                //{
                //    model[i].Im_CheckRequest_Port_ID = list_of_values[i].Im_CheckRequest_Port_ID;
                //    model[i].Port_ID = list_of_values[i].Port_ID;
                //    model[i].status = list_of_values[i].status;
                //    model[i].user_id = (short)Session["UserId"];
                //}

                //var res2 = APIHandeling.Put("Im_checkRequest_Update_PortAPI", model);
                //var lst2 = res2.Content.ReadAsAsync<List<CustomOption>>().Result;

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "update_port");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
         
        }



      
    }
}