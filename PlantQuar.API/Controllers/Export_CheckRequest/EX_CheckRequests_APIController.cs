using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Search;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.UI;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EX_CheckRequests_APIController : ApiController
    {
        // EX_CheckRequestDetailsBLL cBLL = new EX_CheckRequestDetailsBLL();
        List_EXCheckRequest_BLL cBLLList = new List_EXCheckRequest_BLL();
        //public HttpResponseMessage Get_CheckRequestDetails
        //  (string ImCheckRequest_Number)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLLList.GetImCheckRequestDetails(ImCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        public HttpResponseMessage Get_CheckRequestList(short IsApproved, short userId)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetImCheckRequestList_filter(IsApproved, userId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CheckRequestList2(long Outlet_User_ID, long Station_User_ID,
        string CanView_Outlet_Examination, string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetExCheckRequestList_filter(Outlet_User_ID, Station_User_ID, CanView_Outlet_Examination, CanAdd_Station_Examination, CanEdit_Outlet_Genshi, CanDelete_Station_Genshi, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_User_Station(short IS_Station, short userId)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.Get_User_Station(userId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //public HttpResponseMessage Get_CheckRequestList
        //  (string ImCheckRequest_Number, short IsApproved, short userId)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLLList.GetImCheckRequestList_filter(IsApproved, ImCheckRequest_Number, userId, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        //approve request
        public HttpResponseMessage Put_ApproveCheckReq(int approve, EX_CheckRequestDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLLList.ApproveCheckReq(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //refuse reason
        //Im_CheckRequests_API
        public HttpResponseMessage Put_saveItemfees(int itemFees, Items_checkReq dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLLList.saveItemFees(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetRefuseReasons(int List, int refuse)
        {
            try
            {
                //if rfuse = 1 ......refuse request .....if refuse = 2  ..stopprequest
                Dictionary<string, object> dic = cBLLList.FillDrop_RefuseReason(refuse, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostReasons(ReasonsListReqIdDTO Dto, int listt)
        {
            //Create
            try
            {

                Dictionary<string, object> dic = cBLLList.InsertReasons(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_CheckRequestList2(string SearchALL , short userId, long Outlet_User_ID, long Station_User_ID,
        string CanView_Outlet_Examination, string CanAdd_Station_Examination,
        string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi,int CurrentPage, int pageSize)
        {
            try
            {
                //Dictionary<string, object> dic =                    cBLLList.GetExCheckRequestList_filter(userId, Outlet_User_ID, Station_User_ID, CanView_Outlet_Examination, CanAdd_Station_Examination, CanEdit_Outlet_Genshi, CanDelete_Station_Genshi, API_HelperFunctions.Get_DeviceInfo());
                Dictionary<string, object> dic =
                    cBLLList.GetListWithPage_List_filter(SearchALL, userId, Outlet_User_ID, Station_User_ID
                    , CanView_Outlet_Examination, CanAdd_Station_Examination, CanEdit_Outlet_Genshi
                    , CanDelete_Station_Genshi, CurrentPage, pageSize, API_HelperFunctions.Get_DeviceInfo());

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //"&radio_ID=" + radio_ID + "&Company_ID=" + Company_ID +
        //            "&Country_ID=" + Country_ID + "&ExChechRequest_Num=" + ExChechRequest_Num);
        public HttpResponseMessage Get_CheckRequestList3(short userId, long Outlet_User_ID, long Station_User_ID,
string CanView_Outlet_Examination, string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi,
           int? radio_ID, long? Company_ID, long? Country_ID,long? StationId, string ExChechRequest_Num, string dateFrom, string dateEnd)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetExCheckRequestList_filterAll(userId, Outlet_User_ID, Station_User_ID
                    , CanView_Outlet_Examination, CanAdd_Station_Examination, 
                    CanEdit_Outlet_Genshi,
                    CanDelete_Station_Genshi,
                    radio_ID, Company_ID, Country_ID, StationId, ExChechRequest_Num, dateFrom, dateEnd, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage Get_ListExportersService_filter(string ListExportersService)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.Get_ListExportersService_filter(ListExportersService, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_ListAll_filter(string ListAll, int page , string Search )
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.Get_ListAll_filter(ListAll, page,  Search, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetListWithPage(int CurrentPage, int pageSize, string Search)
        {
            Dictionary<string, object> dic =cBLLList.GetListWithPage(CurrentPage, pageSize,  Search);
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }

        //public PaginatedList<SearchVM> GetListWithPage(string term, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        using var dbContext = new AEDBContext();

        //        var data = dbContext.Products.Where(a => a.Name_Ar.Contains(term) || a.Name_En.Contains(term) || a.DescreptionAr.Contains(term) || a.DescreptionEn.Contains(term) && a.IsActive == true).Select
        //            (a => new SearchVM
        //            {
        //                NameArabic = a.Name_Ar,
        //                NameEnglish = a.Name_En,
        //                NameArabicDesc = a.DescreptionAr,
        //                NameEnglishDesc = a.DescreptionMoreEn,
        //                path = a.Path,
        //                ProductID = a.ID

        //            }).ToList();

        //        var datapaging = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //        var TotalResults = data.Count();
        //        var TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);

        //        return new PaginatedList<SearchVM>(datapaging, pageIndex, TotalPages, TotalResults);


        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}

        public HttpResponseMessage Get_CheckRequestListQuick(string SearchALL, short userId, long Outlet_User_ID, long Station_User_ID,
         string CanView_Outlet_Examination, string CanAdd_Station_Examination,
         string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi, int CurrentPage, int pageSize, int Quick)
        {
            try
            {
                //Dictionary<string, object> dic =                    cBLLList.GetExCheckRequestList_filter(userId, Outlet_User_ID, Station_User_ID, CanView_Outlet_Examination, CanAdd_Station_Examination, CanEdit_Outlet_Genshi, CanDelete_Station_Genshi, API_HelperFunctions.Get_DeviceInfo());
                Dictionary<string, object> dic =
                    cBLLList.GetListWithPage_List_filterQuick(SearchALL, userId, Outlet_User_ID, Station_User_ID
                    , CanView_Outlet_Examination, CanAdd_Station_Examination, CanEdit_Outlet_Genshi
                    , CanDelete_Station_Genshi, CurrentPage, pageSize, API_HelperFunctions.Get_DeviceInfo());

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}