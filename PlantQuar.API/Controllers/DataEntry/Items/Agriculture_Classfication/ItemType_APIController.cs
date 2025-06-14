using PlantQuar.BLL.BLL.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.DTO.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.DataEntry.Items.Agriculture_Classfication
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemType_APIController : ApiController
    {
        Item_TypeBLL<Item_TypeDTO> cBLL = new Item_TypeBLL<Item_TypeDTO>();

        public HttpResponseMessage GetItemType_List(int ItemTypeList)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillItem_TypeDrop_Add(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetItemType_AddEdit(int ItemTypeAddEdit)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillItem_TypeDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Eslam fill group withou classifaction
        public HttpResponseMessage GetItemTypeGroup_AddEdit(int ItemTypeGroupAddEdit, int ItemType_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillItem_TypeGroupDrop_Edit(ItemType_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
