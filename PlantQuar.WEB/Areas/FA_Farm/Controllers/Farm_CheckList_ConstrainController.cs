using PlantQuar.DTO.DTO.Farm.FarmConstrain;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class Farm_CheckList_ConstrainController : BaseController
    {
        // GET: FA_Farm/Farm_CheckList_Constrain

        string apiName = "Farm_CheckList_Constrain_API";

        public ActionResult Index(string message)
        {
            ViewBag.message = message;
            return View();
        }


        [HttpPost]
        public JsonResult Farm_Constrain_List()
        {
            try
            {
                var Farm_Constrain_Text = APIHandeling.getData("Farm_Constrain_API?Text1=1");
                var lst = Farm_Constrain_Text.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Farm_Item_List()
        {
            try
            {
                var Farm_Constrain_Item = APIHandeling.getData("Farm_Constrain_API?Item=1");
                var lst = Farm_Constrain_Item.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //new 
        public JsonResult listFarm_CheckList_Constrain(short? Country_ID, long Item_ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Country_ID=" + Country_ID + "&Item_ID=" + Item_ID);
                var lst = res.Content.ReadAsAsync<List<Farm_CheckList_Constrain_DTO>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult Country_List()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?Farm=2");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public ActionResult SaveFarm_CheckList_Constrain(short? Country_Id, long? Item_ID, long Farm_Constrain_Text_ID)
        //  string ConstrainText_Ar, string ConstrainText_En, string Description_Ar, string Description_En

        {
            User_Session Current = User_Session.GetInstance;
            var msg = "";
            var CheckList_Constrain_List = new Farm_CheckList_Constrain_DTO();
            //if (CheckList_Constrain_List.ID > 0)
            //{
            //    //edit
            //    CheckList_Constrain_List.User_Updation_Id = (short)Session["UserId"];
            //    CheckList_Constrain_List.User_Updation_Date = DateTime.Now;
            //    CheckList_Constrain_List.Country_Id = Country_Id;
            //    CheckList_Constrain_List.ConstrainText_Ar = ConstrainText_Ar;
            //    CheckList_Constrain_List.ConstrainText_En = ConstrainText_En;
            //    CheckList_Constrain_List.Description_Ar = Description_Ar;
            //    CheckList_Constrain_List.Description_En = Description_En;
            //    var mynewobj = APIHandeling.Put(apiName, CheckList_Constrain_List);
            //    if ((int)mynewobj.StatusCode != 409)
            //    {
            //        msg = "تم التعديل ";
            //    }
            //    else
            //    {
            //        msg = "هذا السجل موجود من قبل ";
            //    }
            //    return RedirectToAction("EX_ConstrainAddEdit", "Farm_CheckList_Constrain", new { area = "FA_Farm", id = CheckList_Constrain_List.ID, message = msg });
            //}
            //else
            //{
            //add
            CheckList_Constrain_List.User_Creation_Id = (short)Session["UserId"];
            CheckList_Constrain_List.User_Creation_Date = DateTime.Now;
            CheckList_Constrain_List.Country_Id = Country_Id;
            CheckList_Constrain_List.Item_ID = Item_ID;
            CheckList_Constrain_List.Item_ID = Item_ID;
            CheckList_Constrain_List.Farm_Constrain_Text_ID = Farm_Constrain_Text_ID;
            //CheckList_Constrain_List.ConstrainText_Ar = ConstrainText_Ar;
            //CheckList_Constrain_List.ConstrainText_En = ConstrainText_En;
            //CheckList_Constrain_List.Description_Ar = Description_Ar;
            //CheckList_Constrain_List.Description_En = Description_En;
            var res = APIHandeling.Post(apiName, CheckList_Constrain_List);
            var countryLst = res.Content.ReadAsAsync<Farm_CheckList_Constrain_DTO>().Result;//object
                                                                                            //model.ID = countryLst.ID;
            if ((int)res.StatusCode != 409)
            {
                msg = "تمت الاضافه";
            }
            else
            {
                msg = "هذا السجل موجود من قبل ";
            }
            return Json(new { Result = "OK", Options = msg });
            //return RedirectToAction("Index", "Farm_CheckList_Constrain", new { area = "FA_Farm", message = msg });
            //}
        }


        public ActionResult DeleteFarm_CheckList_Constrain(string id)
        {
            try
            {
                long _ID = 0;
                DeleteParameters obj = new DeleteParameters();
                obj.id = _ID;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                var msg = "";
                APIHandeling.Delete(apiName, obj);
                msg = "تم الحذف";
                return RedirectToAction("Index", "Farm_CheckList_Constrain", new { area = "FA_Farm", message = msg });

            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFarm_CheckList_Constrain");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


        public JsonResult Update_Delete_Farm_CheckList_Constrain(string id)
        {

            try
            {
                var msg = "";
                long _ID_Farm_CheckList = long.Parse(id.Split('_')[0]);
                long _ID_Farm_Country_CheckList = long.Parse(id.Split('_')[1]);
                int Update_Delete = int.Parse(id.Split('_')[2]);
                short Userid = (short)Session["UserId"];

                var Dto = new Farm_CheckList_Constrain_DTO();
                Dto.ID_Farm_CheckList = _ID_Farm_CheckList;
                Dto.ID_Farm_Country_CheckList = _ID_Farm_Country_CheckList;
                Dto.User_Updation_Id = Userid;
                Dto.User_Deletion_Id = Userid;


                var res2 = APIHandeling.Put(apiName + "?Update_Delete=" + Update_Delete, Dto);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;


                if (list != null)
                {

                    msg = "تمت الاضافه";
                }

                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }
                return Json(new { Result = "OK", Options = list }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFarm_CheckList_Constrain");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }




        private Farm_CheckList_Constrain_DTO getFarm_CheckList_ConstrainByID(long id)
        {

            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            var list = res.Content.ReadAsAsync<Farm_CheckList_Constrain_DTO>().Result;
            return list;
        }


        public JsonResult Get_Farm_CheckList_Constrain_Detiles(int ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?details=0&&Id=" + ID);
                var lst = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                return Json(new { Description_Ar = lst["Description_Ar"].ToString(), Description_En = lst["Description_En"].ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



    }
}