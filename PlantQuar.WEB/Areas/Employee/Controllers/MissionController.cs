
using Newtonsoft.Json;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Employee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Employee.Controllers
{
    
    public class MissionController : Controller
    {
        string apiName = "Mission_API";
        List<CustomOption> lst1;
        // GET: Employee/Mission
        public ActionResult Index()
        {
            //return RedirectToAction("GetOutlet_ID");



            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Outlet=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                
                
                
                
                var Fees_Process1 = APIHandeling.getData(apiName);
                var lst2 = Fees_Process1.Content.ReadAsAsync<List<CustomOption>>().Result;



                ViewBag.Test = lst2;
                ViewBag.ddd = lst;

                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User=6");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.sss = lst;

                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                //  return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }



            return View();
        }

        [HttpPost]
        public ActionResult GetOutlet_ID1(int markaId,string strt, string end)
        {
            ViewBag.sss = null;
            try
            {
                DateTime StartDate = Convert.ToDateTime(strt);
                DateTime EndDate = Convert.ToDateTime(end);

                var Fees_Process = APIHandeling.getData(apiName + "?Start_Date=" + StartDate.ToString() + "&End_Date=" + EndDate.ToString() + "&User1=" +   markaId + "");
                lst1 = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
              
                ViewBag.sss = markaId;
               
                return Json(new SelectList(lst1, "Value", "DisplayText"));

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]

        public JsonResult CreatePR_Mission(PR_MissionDTO usr, List<object> lstEmployeeId, string str, string end)
       
        {
            


            try
            {
                var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<List<objs>>(lstEmployeeId[0].ToString());

                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                   
                    User_Session Current = User_Session.GetInstance;
                    usr.User_Creation_Id = (short)Session["UserId"];
                    usr.User_Creation_Date = DateTime.Now;
                 
                    Classsss classssses=new Classsss();
                    classssses.pR_MissionDTO = usr;
                    classssses.Objs = oMycustomclassname;
                    classssses.StartDate = Convert.ToDateTime(str); 
                    classssses.EndDate = Convert.ToDateTime(end);  
                    var res = APIHandeling.Post(apiName, classssses);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = usr })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult listPR_Mission(string strt, string end,
            long outletId , long outletId1)
        {
            try
            {
                DateTime StartDate = Convert.ToDateTime(strt);
                DateTime EndDate = Convert.ToDateTime(end);
                //var res = APIHandeling.getData(apiName + "?Start_Date=" + StartDate.ToString()+ "&End_Date=" + EndDate.ToString()+ "&outletId="+outletId+ "&outletId1="+outletId );
                //var lst = res.Content.ReadAsAsync<List<PR_MissionDTO>>().Result;//object
                var Fees_Process = APIHandeling.getData(apiName + "?Start_Date=" + StartDate.ToString() + "&End_Date=" + EndDate.ToString() + "&outletId=" + outletId + "&outletId1=" + outletId);

                var lst = Fees_Process.Content.ReadAsAsync<List<Class1>>().Result;

                //  var StatusCode = lst.ElementAt(0).Value;
                //  var obj = lst.ElementAt(1).Value;

                //  JavaScriptSerializer ser = new JavaScriptSerializer();
                //  var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                //  var count = myObj.ElementAt(0).Value;
                //  var Lobj = myObj.ElementAt(1).Value;
                return Json(lst, JsonRequestBehavior.AllowGet);
              //  return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult GetOutlet_ID()
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Outlet=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
        [HttpPost]

        public JsonResult GetPR_User_Id()
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User=1");
                    var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPR_User_Id");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }   
        [HttpPost]

        public JsonResult GetOutletID(long GRID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Outlet="+GRID+"");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;


                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPR_User_Id");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

    }


  
}
