using PlantQuar.BLL.BLL.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Farm.FarmCommittee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Farm_Committee_Examination_APIController : ApiController
    {
        Farm_Committee_ExaminationBLL cBLL = new Farm_Committee_ExaminationBLL();

        public HttpResponseMessage GetFarm_Committee_ExaminationName(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(FarmCommittee_ID, arName, enName, pageSize, index, jtSorting, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PutUpdateFarm_Committee_Examination(Farm_Committee_ExaminationDTO Dto)
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
        public HttpResponseMessage PostCreateSaveFinalNotesCheckList(short user_Id, long farmCommitteeId, string notes, List<Farm_Committee_CheckList_DTO> CheckListStatus)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.SaveFinalNotesCheckList(user_Id, farmCommitteeId, notes, CheckListStatus, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostCreateSaveAreaAndWightFarmRequestQaurList(short user_id, List<Farm_Request_ItemCategoriesDTO> FinalItemCategoryAreaforAll)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.SaveAreaAndWightFarmRequestQaurList(user_id, FinalItemCategoryAreaforAll, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
