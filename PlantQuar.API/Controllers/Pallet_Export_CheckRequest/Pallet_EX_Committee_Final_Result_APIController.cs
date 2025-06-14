
using PlantQuar.BLL.BLL.Pallet_Export_CheckRequest;
using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Pallet_Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Pallet_EX_Committee_Final_Result_APIController : ApiController
    {
        Pallet_EX_Committee_Final_ResultBLL cBLL = new Pallet_EX_Committee_Final_ResultBLL();


        public HttpResponseMessage Get_CheckRequestList
           (long EX_CheckRequest_Number, long item_ShortName_ID, long Lots_itemShortName_ID, long CommitteeTypeLst_ID)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetEX_CheckRequestList_filter(EX_CheckRequest_Number, item_ShortName_ID, Lots_itemShortName_ID, CommitteeTypeLst_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetItemType_AddEdit(long ItemShortNameAddEdit)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillItem_TypeDrop_Add(ItemShortNameAddEdit, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetLot_AddEdit(long Req, long ItemShortNameID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillLot_TypeDrop_Add(Req, ItemShortNameID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage Insert_Lot_Result(int Lot_Result, Pallet_EX_CheckRequest_Items_Lot_ResultDTO Lot_ResultList)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Lot_Result(Lot_ResultList, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Insert_EX_CheckRequest_Visa(int Visa_ID, Pallet_EX_CheckRequest_VisaDTO CheckRequest_VisaDList)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_EX_CheckRequest_Visa(CheckRequest_VisaDList, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Insert_EX_CheckRequest_Final_Result(int EX_CheckRequest_Final_Result, Pallet_EX_CheckRequest_Final_ResultDTO EX_CheckRequest_Final_ResultList)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_EX_CheckRequest_Final_Result(EX_CheckRequest_Final_ResultList, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //noura  
        public HttpResponseMessage GetVisaLabResult_AddEdit(int VisaResult)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillVisaLabResultDrop_Edit(VisaResult, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetLotStatusLst_AddEDIT(int Lot_Result_Status)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillLotStatusLst_AddEDIT(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_Fees(long EXCheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Get_Fees(EXCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage Get_Update_Status_Confirm(long Status_Id, bool Status)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Update_Status_Confirm(Status_Id, Status, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetItemType_AddEdit(int VisaAddEdit = 1)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillIVisaDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage GetFinalResult_AddEdit(int FinalResult)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillFinalResultDrop_Edit(FinalResult, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetEmployee_byOutlet_List(string FullName, long EmplyeeNo, long OutLet_ID, int Type_ID_HR)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetEmployee_byOutlet(FullName, EmplyeeNo, OutLet_ID, Type_ID_HR, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_Delete_Emp_Confirm(long Committee_ID, long Employee_ID, short UserId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Delete_Emp_Confirm(Committee_ID, Employee_ID, UserId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





    }
}
