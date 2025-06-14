using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Stations
{
    public class StationAccrediationCommitteeBLL
    {
        private UnitOfWork uow;
        public StationAccrediationCommitteeBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(string stationCode, int? Status,short? stationActivityType, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("stationCode", SqlDbType.NVarChar);
                paramters_Type.Add("Status", SqlDbType.Int);
                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);
                paramters_Type.Add("stationActivityType", SqlDbType.SmallInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("stationCode", (stationCode != null ? stationCode : ""));
                paramters_Data.Add("Status", Status.ToString());
                paramters_Data.Add("Language_IsAr", "1");
                paramters_Data.Add("stationActivityType", stationActivityType.ToString());

                var request = uow.Repository<Station_Accrediation_Committee_GetData_DTO>().CallStored("Station_Accrediation_Committee_GetData", paramters_Type,
                    paramters_Data, Device_Info).ToList();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
