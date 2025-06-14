using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PlantQuar.WEB.Controllers;
using PlantQuar.WEB.App_Start;
using PlantQuar.DTO.DTO.DataEntry.Committees;
using PlantQuar.DTO.DTO.Export_Constrains;

namespace PlantQuar.Web.Areas.Export_Constrains.Controllers
{
    public class Ex_ConstrainActivationController : BaseController
    {
        string apiName = "Ex_ConstrainActivation_API";

        // GET: Export_Constrains/CommitteeType
        public ActionResult Index()
        {
            //ViewBag.ConstrainTypeLst
            var res = APIHandeling.getData("A_SystemCode_API?Syscode=1");
            ViewBag.ConstrainTypeLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.CountriesLst
            res = APIHandeling.getData("Country_API?AddEdit=1");
            ViewBag.CountriesLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            //ViewBag.UnionsLst
            res = APIHandeling.getData("Union_API?AddEdit=1");
            ViewBag.UnionsLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //***********************************//


            //ViewBag.PlantLst
            res = APIHandeling.getData("Plant_API?plant=1");
            ViewBag.PlantLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;


            return View();
        }
      
        #region ShortName _Category
        public JsonResult PlantCategoryList(int plantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantCategory_API?plantId=" + plantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategoryList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetPlantShortName(long Item_Id = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?shortNByIt=1&itemId=" + Item_Id);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                //var res = APIHandeling.getData("Item_ShortName_API?shortNByIt=1&itemId=" + Item_Id);
                //var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;

                //Session["PlantId"] = Item_Id;
                ////Session["PlantPurposeId"] = purposeId;
                ////Session["PlantStatusId"] = statusId;

                ////res = APIHandeling.getData("PlantPart_API?plantPartId=" + partType);
                ////Session["PlantPartType"] = res.Content.ReadAsAsync<byte>().Result;

                ////Session["CategoryId"] = catId;

                //return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Get_ShortNameDetails(int ShortName)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?ShortName=" + ShortName);
                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;


                return Json(new { SubPart_Name = data["SubPart_Name"].ToString(), Status_Name = data["Status_Name"].ToString(), Purpose_Name = data["Purpose_Name"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

      
        [HttpPost]
        public JsonResult listCommitteeType
        (long Item_ShortName = 0, long catId = 0, int constrainType = 0, int owner = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Item_ShortName=" + Item_ShortName + "&catId=" + catId + "&constrainType=" + constrainType + "&owner=" + owner + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCommitteeType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

       

        //UPDATE CommitteeType
        [HttpPost]
        public JsonResult UpdateCommitteeType(Ex_ConstrainActivationDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    //remove item 0
                    if (model.ArrivalPortList != null)
                    {
                        if (model.ArrivalPortList.Contains(0))
                        {
                            model.ArrivalPortList.Remove(0);
                        }
                    }

                    //check Repeated Data
                    var res = APIHandeling.Put("Export_Constrains?active=1", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdatePlantsConstrainRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        User_Session Current = User_Session.GetInstance;

            //        model.User_Updation_Id=(short)Session["UserId"];
            //        model.User_Updation_Date = DateTime.Now;

            //        //check Repeated Data
            //        var res = APIHandeling.Put(apiName, model);
            //        return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
            //          : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            //    }
            //    else
            //    {
            //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ex.HResult == -2146233087)
            //    {
            //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
            //    }
            //    else
            //    {
            //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateCommitteeType");
            //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            //    }
            //}
        }

        


      
    }
}