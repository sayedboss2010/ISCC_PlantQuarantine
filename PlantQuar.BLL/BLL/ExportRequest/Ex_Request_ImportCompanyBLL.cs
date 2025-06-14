using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class Ex_Request_ImportCompanyBLL : IGenericBLL<Ex_Request_ImportCompanyDTO>
    {
        private UnitOfWork uow;
        public Ex_Request_ImportCompanyBLL()
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

        public bool GetAny(Ex_Request_ImportCompanyDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> InsertList(List<Ex_Request_ImportCompanyDTO> entity, List<string> Device_Info)
        {
            try
            {
                foreach(var item in entity)
                {
                    var CModel = Mapper.Map<Ex_Request_ImportCompany>(item);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_Request_ImportCompany_Seq");

                    uow.Repository<Ex_Request_ImportCompany>().InsertRecord(CModel);
                    uow.SaveChanges();
                }
                
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        public Dictionary<string, object> Insert(Ex_Request_ImportCompanyDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(Ex_Request_ImportCompanyDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}