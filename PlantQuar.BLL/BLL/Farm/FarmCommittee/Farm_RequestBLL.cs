using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmCommittee
{
    public class Farm_RequestBLL : IGenericBLL<FarmRequestDTO>
    {
        private UnitOfWork uow;

        public Farm_RequestBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(FarmRequestDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(FarmRequestDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(FarmRequestDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Request CModel = uow.Repository<Farm_Request>().Findobject(entity.ID);
                CModel.IsStatus = entity.IsStatus;
                CModel.User_Updation_Date = entity.User_Updation_Date;
                CModel.User_Updation_Id = entity.User_Updation_Id;
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update,"");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
