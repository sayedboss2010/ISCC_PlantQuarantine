
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.BLL.BLL.DataEntry.Treatments;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.BLL.BLL.DataEntry.Items;
using PlantQuar.BLL.BLL.DataEntry.Items.ItemType;

namespace PlantQuar.API.Controllers.DataEntry.Items.ItemTypeData
{
    public class ItemTypeController : ApiController
    {
        ItemType cBLL = new ItemType();
        public HttpResponseMessage PostCreateTreatmentMethod(TransferData Dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
