using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.checkRequests
{
    public class Im_CheckRequest_ShowDataBLL
    {
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        public Im_CheckRequest_ShowDataBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetAll(List<string> Device_Info)
        {
            try
            {

                string lang = Device_Info[2];
                var data = uow.Repository<Im_CheckRequest_ShowDataDTO>().CallStored("Pro_Im_CheckRequest_ShowData", null,
                    null, Device_Info).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        

    }
}
