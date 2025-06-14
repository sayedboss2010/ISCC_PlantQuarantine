
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
    public class Ex_ItemsLotsBLL
    {
        private UnitOfWork uow;
        public Ex_ItemsLotsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetItemsLots(long ItemId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("Item_ID", SqlDbType.BigInt);
                
                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("Item_ID", ItemId.ToString());
                
                var request = uow.Repository<Item_GetLotData_ResultDTO>().CallStored("Item_GetLotData", paramters_Type, paramters_Data, Device_Info).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
