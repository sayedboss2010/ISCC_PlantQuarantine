using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class Ex_Change_OutlitController : Controller
    {
        // GET: Export_CheckRequest/Ex_Change_Outlit

        public ActionResult Index()
        {
            var IsApproved = 1;
            var userId = (short)Session["UserId"];
            var res = APIHandeling.getData("Ex_Change_Outlit_API?IsApproved=" + IsApproved + "&OutlitUserID=" + userId + "");
            var modelLst = res.Content.ReadAsAsync<List<Ex_Change_OutlitDTO>>().Result;
            if (modelLst != null)
            {
                if (modelLst.Count > 0)
                {
                    var res_Fees_Money = APIHandeling.getData("Outlet_API?Port_National_ID=" + modelLst.FirstOrDefault().Port_ID);
                    var Fees_Money_lst = res_Fees_Money.Content.ReadAsAsync<List<CustomOption>>().Result;  
                    ViewBag.Fees_Money_List = Fees_Money_lst;
                }
            }
            return View(modelLst);
        }


        [HttpPost]
        public JsonResult Ex_CheckRequest_List(string IsApproved = "1", string requestnumber = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                User_Session Current = User_Session.GetInstance;
                string User_ID = Session["UserId"].ToString();
                var res = APIHandeling.getData("Ex_Change_Outlit_API?ImCheckRequest_Number=" + requestnumber +
                   "&IsApproved=" + IsApproved + "&OutlitUserID=" + User_ID + "&pageSize=" + jtPageSize.ToString()
                   + "&index=" + jtStartIndex.ToString()
                   + "&jtSorting=" + jtSorting.ToString());
                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object


                if (lst != null)
                {
                    var StatusCode = lst.ElementAt(0).Value;

                    var obj = lst.ElementAt(1).Value;

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                    var count = myObj.ElementAt(0).Value;
                    var Lobj = myObj.ElementAt(1).Value;

                    return Json(new { Result = "No_Center_Outlit", Records = lst });
                }
                else
                {
                    return Json(new { Result = "OK", Records = 0, TotalRecordCount = 0 });
                }

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ImRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult listGovernates
       (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData("Ex_Change_Outlit_API?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listGovernate");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


        //UPDATE
        [HttpPost]
        public JsonResult Update_Check_Outlet(List<Ex_Change_OutlitDTO> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = APIHandeling.Put("Ex_Change_Outlit_API", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

                }






                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Update_Outlet");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
    }
}