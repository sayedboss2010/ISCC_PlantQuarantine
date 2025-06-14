using PlantQuar.BLL.BLL.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Farm.FarmRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FarmCommittee_APIController : ApiController
    {
        FarmCommitteeBLL cBLL = new FarmCommitteeBLL();
        // GET: FarmCommittee
        //, int newCreate
        public HttpResponseMessage PostCommittee(Farm_CommitteeDTO model)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Insert(model, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutUpdateCommittee(Farm_CommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Update(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetObjectById(long Id)
        {
            Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }


        public HttpResponseMessage GetFramCommitteResultById(long FramCommitte_Id, bool IsResult)
        {
            Dictionary<string, object> dic = cBLL.Find_CommitteeResult(FramCommitte_Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }

        public HttpResponseMessage PostCreateFarmCommitteeResult(Farm_SampleDataDTO data, int C)
        {
            //Create
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();

                Dictionary<string, object> dic = cBLL.InsertFarmCommittee(data, device_data);
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    Dictionary<string, object> dic_res = new Dictionary<string, object>();
                    dic_res.Add("state_Code", dic["state_Code"]);
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic_res);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["state_Code"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PostConfirmFarmCommitteeResult(Farm_Committee_ConfirmDTO ConfirmDto, bool Is_Confirm)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.ConfirmFarmCommittee(ConfirmDto, device_data);
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    Dictionary<string, object> dic_res = new Dictionary<string, object>();
                    dic_res.Add("state_Code", dic["state_Code"]);
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic_res);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["state_Code"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
