using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class Ex_ItemsBLL
    {
        private UnitOfWork uow;
        public Ex_ItemsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetPlants(long requestId,int isplant,List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("CheckRequest_Id", SqlDbType.BigInt);
                paramters_Type.Add("IsPlant", SqlDbType.Int);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("CheckRequest_Id", requestId.ToString());
                paramters_Data.Add("IsPlant", isplant.ToString());

                var request = uow.Repository<Ex_plants>().CallStored("CheckRequest_GetItems", paramters_Type, paramters_Data, Device_Info).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
