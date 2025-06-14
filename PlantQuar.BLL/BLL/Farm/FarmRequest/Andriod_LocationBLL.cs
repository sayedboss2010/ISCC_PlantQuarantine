using AutoMapper;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class Andriod_LocationBLL
    {
        private UnitOfWork uow;

        public Andriod_LocationBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Insert(Andriod_LocationDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<PlantQuar.DAL.Andriod_Location>(entity);
                CModel.Id = uow.Repository<Object>().GetNextSequenceValue_Long("Andriod_Location_seq");

                uow.Repository<PlantQuar.DAL.Andriod_Location>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CModel);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
