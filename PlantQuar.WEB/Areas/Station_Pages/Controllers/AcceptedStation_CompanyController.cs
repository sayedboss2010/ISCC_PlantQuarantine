using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class AcceptedStation_CompanyController : BaseController
    {
        // GET: Station_Pages/AcceptedStation_Company
        string apiName2 = "AcceptedStation_Company_API";
        public ActionResult Index()
        {
            //var Outlet_Name = Session["Outlet_Name"].ToString();
            //var dd1 = Session["Outlet_Type_ID"].ToString();
            //var Outlet_ID = Session["Outlet_ID"].ToString();

    
            return View();
        }


        [HttpPost]
        public ActionResult StationsCompany(long Company_Id, int Company_Type_Id)
        {
            try
            {
                var Outlet_Name = Session["Outlet_Name"].ToString();
                var dd1 = Session["Outlet_Type_ID"].ToString();
                var Outlet_ID = Session["Outlet_ID"];

                var Fees_Process = APIHandeling.getData(apiName2 + "?Company_Id=" + Company_Id + "&Company_Type_Id=" + Company_Type_Id);
                var lst = Fees_Process.Content.ReadAsAsync<List<Station_Company_DTO>>().Result;
                //  ViewBag.sss = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



        [HttpPost]
        public ActionResult Company_Name_Pr( int Company_Type_Id)
        {
            try
            {
                if (Company_Type_Id == 6)
                {
                    var Company = APIHandeling.getData(apiName2 + "?Company=-1");
                    var lst_Company_Name = Company.Content.ReadAsAsync<List<CustomOption>>().Result;                          
                    return Json(new { Result = "OK", Options = lst_Company_Name });
                }
               else if (Company_Type_Id == 7)
                {
                    var Orgniztion = APIHandeling.getData(apiName2 + "?Orgniztion=-1");
                    var lst_Orgniztion_Name = Orgniztion.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst_Orgniztion_Name });
                }
              else  if (Company_Type_Id == 8)
                {
                    var Person = APIHandeling.getData(apiName2 + "?Person=-1");
                    var lst_Person_Name = Person.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst_Person_Name });
                }             
                else
                {
                    return Json(new { Result = "OK", Options = 1 });
                }
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult InsertStationsCompany(List<Station_Company_DTO> menus_Status_new)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    foreach (var item in menus_Status_new)
                    {
                        item.User_Updation_Id = (short)Session["UserId"];
                        item.User_Updation_Date = DateTime.Now;
                        #region Log_Station
                        var dto2 = new DAL.Table_Action_Log_Station();
                        dto2.ID_TableActionValue =item.StationCompany_ID;
                        dto2.Station_ID = item.Station_ID;
                        dto2.User_Creation_Id = (short)Session["UserId"];
                        dto2.User_Creation_Date = DateTime.Now;
                        if (item.Status == 1)
                        {
                            dto2.ID_Table_Action = 43;
                            dto2.NOTS = " تم  قبول استعمال المحطة للشركة ";
                        }
                        else
                        {
                            dto2.ID_Table_Action = 44;
                            dto2.NOTS = " تم  رفض استعمال المحطة للشركة  ";
                        }
                        dto2.User_Type_ID = 127;
                        dto2.Type_log_ID = 135;
                        APIHandeling.Put("Log_CheckRequest_API?Station_Log=1", dto2);
                        #endregion
                    }

                    var res = APIHandeling.Post(apiName2, menus_Status_new);

                 

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = menus_Status_new })
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


    }
}