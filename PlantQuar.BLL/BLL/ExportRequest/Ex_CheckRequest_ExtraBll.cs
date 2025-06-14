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
    public class Ex_CheckRequest_ExtraBll : IGenericBLL<Ex_CheckRequest_ExtraDTO>
    {
        private UnitOfWork uow;
        public Ex_CheckRequest_ExtraBll()
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

        public bool GetAny(Ex_CheckRequest_ExtraDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(Ex_CheckRequest_ExtraDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Ex_CheckRequest_Extra>(entity);
                
                uow.Repository<Ex_CheckRequest_Extra>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        public Dictionary<string, object> Update(Ex_CheckRequest_ExtraDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}