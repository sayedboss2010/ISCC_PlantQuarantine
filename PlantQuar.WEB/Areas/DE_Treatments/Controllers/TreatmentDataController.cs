using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
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

namespace PlantQuar.WEB.Areas.DE_Treatments.Controllers
{
    public class TreatmentDataController : Controller
    {
        // GET: DE_Treatments/TreatmentData

       // string apiName = "TreatmentMethod_Api";
        public ActionResult Index(  TreatmentMethodDTO dto)
        {
           
              

            if (dto != null)
            {
                // get id  , value of type&Method

                
                    
                var res2 = APIHandeling.getData
                    ("TreatmentType_API?Id=" + dto.TreatmentType_ID);
                var lst2 = res2.Content.ReadAsAsync<TreatmentTypeDTO>().Result;
                //lst1.RemoveAt(0);
                dto.User_Creation_Id = lst2.ID;
                //var res3 = APIHandeling.getData
                //    ("TreatMentMaterials?Id=" + lst2.MainType_ID);
                //var lst3 = res3.Content.ReadAsAsync<List<TreatmentMaterial>>().Result;
                //lst1.RemoveAt(0);
                dto.User_Updation_Id= lst2.MainType_ID;




                

               // ViewBag.TreatmentType = lst1;


            }
            var res = APIHandeling.getData("TreatmentMainType_API?AddEdit=-1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            ViewBag.TreatmentMain = lst;

            int TreatmentMain_Id = 1;
            var res1 = APIHandeling.getData("TreatmentType_API?TreatmentMain_Id=" + TreatmentMain_Id);
            var lst1 = res1.Content.ReadAsAsync<List<CustomOption>>().Result;
            //lst1.RemoveAt(0);
            ViewBag.TreatmentType = lst1;

            return View(dto);
            
        }
        public ActionResult getTreatmentMain(long TreatmentMain_Id)
        {
           //  int TreatmentMain_Id = 1;
        var res1 = APIHandeling.getData("TreatmentType_API?TreatmentMain_Id=" + TreatmentMain_Id);
        var lst1 = res1.Content.ReadAsAsync<List<CustomOption>>().Result;
        //lst1.RemoveAt(0);
        //ViewBag.TreatmentType = lst1;

            return Json(lst1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateTreatmentMethod(TreatmentMethodDTO Dto,List<TreatmentMaterialDTO> Dto1)
        {
            try
            {
                //var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    Dto.User_Creation_Id = (short)Session["UserId"];
                    Dto.User_Creation_Date = DateTime.Now;

                    //check Repeated Data
                    TransferData data=new TransferData();
                    data.Dto = Dto;
                    data.Dto1 = Dto1;
                    var res = APIHandeling.Post("ItemType", data);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = Dto })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTreatmentMethod");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        [HttpPost]
        public JsonResult getTreatmentMethodTypes()
        {
            int ItemTypeID = 5;
            var res1 = APIHandeling.getData("Item_API?ItemTypeID="+ItemTypeID);
            var lst1 = res1.Content.ReadAsAsync<List<CustomOption>>().Result;
            //lst1.RemoveAt(0);
            //ViewBag.TreatmentType = lst1;

            return Json(lst1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateMathod(TreatmentMethodDTO Dto, List<TreatmentMaterialDTO> Dto1)
        {



            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                   
                    var res = APIHandeling.Put("TreatmentMethod_API", Dto);
                    TransferData data = new TransferData();
                    data.Dto = Dto;
                    data.Dto1 = Dto1;
                    var res1 = APIHandeling.Post("TreatMentMaterials_API", data);
                  
                    
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = Dto })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateTreatmentMethod");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
          //  return Json(lst1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getSelectedTreatmentMethodTypes( int TreatmentMethodId  )
        {
       
            var res1 = APIHandeling.getData
                ("TreatMentMaterials_API?TreatmentMethodID=" + TreatmentMethodId);
            var lst1 = res1.Content.ReadAsAsync<List<TreatmentMaterialDTO>>().Result;
          
            return Json(lst1, JsonRequestBehavior.AllowGet);
        }



    }
}