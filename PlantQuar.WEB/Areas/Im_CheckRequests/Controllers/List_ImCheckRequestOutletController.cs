
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
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Im_CheckRequests.Controllers
{
    public class List_ImCheckRequestOutletController : BaseController
    {
        // GET: Im_CheckRequests/List_ImCheckRequestOutlet
        public ActionResult Index()
        {
            var IsApproved = 1;
            var userId = (short)Session["UserId"];
            //var res = APIHandeling.getData("Im_CheckRequests_API?IsApproved=" + IsApproved + "&userId=" + userId);
            var res = APIHandeling.getData("List_Im_checkRequestsOutlet_API?IsApproved=" + IsApproved + "&OutlitUserID=" + userId + "");
            var modelLst = res.Content.ReadAsAsync<List<ImCheckRequestListOutlitDTO>>().Result;
            if (modelLst != null)
            {
                if (modelLst.Count > 0)
                {
                    var res_Fees_Money = APIHandeling.getData("Outlet_API?Port_National_ID=" + modelLst.FirstOrDefault().Port_ID);
                    var Fees_Money_lst = res_Fees_Money.Content.ReadAsAsync<List<CustomOption>>().Result;   //is CustomOption change with Dto i will created it & wih files related
                    ViewBag.Fees_Money_List = Fees_Money_lst;
                }
            }
            return View(modelLst);
        }


        [HttpPost]
        public JsonResult Im_CheckRequest_List(string IsApproved = "1", string requestnumber = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                User_Session Current = User_Session.GetInstance;
                // string User_ID =   Current.UserId;
                string User_ID = Session["UserId"].ToString();
                //var res = APIHandeling.getData("List_Im_checkRequestsOutlet_API?ImCheckRequest_Number=" + requestnumber 
                //    + "&IsApproved=" + IsApproved + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() 
                //    + "&jtSorting=" + jtSorting.ToString()+);

                var res = APIHandeling.getData("List_Im_checkRequestsOutlet_API?ImCheckRequest_Number=" + requestnumber +
                   "&IsApproved=" + IsApproved + "&OutlitUserID=" + User_ID+ "&pageSize=" + jtPageSize.ToString()
                   + "&index=" + jtStartIndex.ToString() 
                    + "&jtSorting=" + jtSorting.ToString());
               // var modelLst = res.Content.ReadAsAsync<List<ImCheckRequestListOutlitDTO>>().Result;
                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object


                if (lst != null)
                {
                    var StatusCode = lst.ElementAt(0).Value;

                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

               // return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
                //var StatusCode = lst.ElementAt(0).Value;
                //var obj = lst.ElementAt(1).Value;
                ////if (obj != null)
                ////{



                //    JavaScriptSerializer ser = new JavaScriptSerializer();

                //    var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());
                //    var count = myObj.ElementAt(0).Value;
                //    var Lobj = myObj.ElementAt(1).Value;


                
                    return Json(new { Result = "No_Center_Outlit", Records = lst });
                }
                else
                {
                    return Json(new { Result = "OK", Records = 0, TotalRecordCount = 0 });
                }
                //}
                //else
                //{
                //    return Json(new { Result = "NUll", Records = count });
                //}
                //List<Student> students = _repository.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);

                // var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object





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
                var res = APIHandeling.getData("List_Im_checkRequestsOutlet_API?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
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
        public JsonResult Update_Check_Outlet(List<ImCheckRequestListOutlitDTO> model)
                 {
            try
            {
                //var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    //User_Session Current = User_Session.GetInstance;

                    //model.User_Updation_Id = Current.UserId;
                    //model.User_Updation_Date = DateTime.Now;
                    foreach (var item in model)
                    {
                        Log_CheckRequest_DTO dto = new Log_CheckRequest_DTO();
                        dto.ID_Table_Action = 11;
                        dto.ID_TableActionValue = item.ID;
                        dto.Im_CheckRequest_ID = item.ID;
                        dto.User_Creation_Id = (short)Session["UserId"];
                        dto.User_Creation_Date = DateTime.Now;
                        dto.NOTS = "تحويل لمنفذ";
                        dto.User_Type_ID = 127;
                        dto.Type_log_ID = 135;
                        APIHandeling.Put("Log_CheckRequest_API?itemFees=1", dto);
                    }
                   


                    var res = APIHandeling.Put("List_Im_checkRequestsOutlet_API", model);
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