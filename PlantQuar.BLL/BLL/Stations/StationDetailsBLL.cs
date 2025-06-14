using AutoMapper;
using PlantQuar.DAL;
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
   public class StationDetailsBLL
    {
        private UnitOfWork uow;
        public StationDetailsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetStationData(long StationAccrediationRequestId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("StationAccrediationRequestId", SqlDbType.BigInt);
                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);

                Dictionary <string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("StationAccrediationRequestId", StationAccrediationRequestId.ToString());
                paramters_Data.Add("Language_IsAr", Device_Info[2]);  //"2018-12-26"

                var data = uow.Repository<Station_Get_Data_ResultDTO>().CallStored("Station_Get_Data", paramters_Type,
                paramters_Data, Device_Info).FirstOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> ApproveStation(Station_Get_Data_ResultDTO dto, List<string> Device_Info)
        {
            try
            {
                Station_Accreditation_Committee CModel = uow.Repository<Station_Accreditation_Committee>().Findobject(dto.requestId);
                CModel.IsApproved = dto.IsApproved;

                uow.SaveChanges();

                var empDTO = Mapper.Map<Station_Accreditation_Committee, Station_Accrediation_Committee_GetData_DTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
