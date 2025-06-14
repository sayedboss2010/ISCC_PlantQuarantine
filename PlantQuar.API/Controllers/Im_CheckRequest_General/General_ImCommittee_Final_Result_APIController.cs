using PlantQuar.BLL.BLL.Im_CheckRequest_General;
using PlantQuar.DTO.DTO.Im_CheckRequest_General;
//using PlantQuar.BLL.BLL.Import.Committee;
//using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Im_CheckRequest_General
{
    public class General_ImCommittee_Final_Result_APIController : ApiController
    {
        General_ImCommittee_Final_ResultBLL cBLL = new General_ImCommittee_Final_ResultBLL();


        public HttpResponseMessage Get_CheckRequestList
           (long ImCheckRequest_Number, long item_ShortName_ID, long Lots_itemShortName_ID, long CommitteeTypeLst_ID)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImCheckRequestList_filter(ImCheckRequest_Number, item_ShortName_ID, Lots_itemShortName_ID, CommitteeTypeLst_ID, API_HelperFunctions.Get_DeviceInfo());
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
        //public HttpResponseMessage Get_CheckRequestDetails
        // (long ImCheckRequest_Number, long item_ShortName_ID = 0)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetImCheckRequestDetails(ImCheckRequest_Number, item_ShortName_ID, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        public HttpResponseMessage Insert_Lot_Result(int Lot_Result, General_Im_CheckRequest_Items_Lot_ResultDTO Lot_ResultList)
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

        public HttpResponseMessage Insert_Im_CheckRequest_Visa(int Visa_ID, General_Im_CheckRequest_VisaDTO CheckRequest_VisaDList)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Im_CheckRequest_Visa(CheckRequest_VisaDList, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Insert_Im_CheckRequest_Final_Result(int Im_CheckRequest_Final_Result, General_Im_CheckRequest_Final_ResultDTO Im_CheckRequest_Final_ResultList)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Im_CheckRequest_Final_Result(Im_CheckRequest_Final_Result, Im_CheckRequest_Final_ResultList, API_HelperFunctions.Get_DeviceInfo());
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
        public HttpResponseMessage GetLotStatusLst_AddEDIT()
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

        public HttpResponseMessage Get_Fees(string ImCheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Get_Fees(ImCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
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
    }
}
