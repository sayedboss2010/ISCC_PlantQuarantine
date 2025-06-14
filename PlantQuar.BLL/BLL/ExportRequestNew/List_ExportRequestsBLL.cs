using PlantQuar.DAL;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequestNew
{
   public class List_ExportRequestsBLL
    {
        private UnitOfWork uow;
        public List_ExportRequestsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Get_ExportRequestFor_Admin(short CommitteeType_ID, short IsApproved, string requestnumber, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("IsApproved", SqlDbType.SmallInt);
                paramters_Type.Add("CommitteeType_ID", SqlDbType.SmallInt);
                paramters_Type.Add("requestNumber", SqlDbType.VarChar);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("IsApproved", IsApproved.ToString());
                paramters_Data.Add("CommitteeType_ID", CommitteeType_ID.ToString());
                paramters_Data.Add("requestNumber", requestnumber == null ? "" : requestnumber);
                var request = uow.Repository<CheckRequest_AdminGetData_Result>().CallStored("EX_List_ExportRequests", paramters_Type, paramters_Data, Device_Info).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
