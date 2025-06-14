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
    public class ExportRequest_ApprovedStationBll : IGenericBLL<ExportRequest_ApprovedStationDTO>
    {
        private UnitOfWork uow;
        public ExportRequest_ApprovedStationBll()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Insert(ExportRequest_ApprovedStationDTO entity, List<string> Device_Info)
        {
            if (!GetAny(entity))
            {
                var CModel = Mapper.Map<Ex_Request_ApprovedStation>(entity);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_Request_ApprovedStation_Seq");
                uow.Repository<Ex_Request_ApprovedStation>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        public bool GetAny(ExportRequest_ApprovedStationDTO entity)
        {
            var obj = entity;
            return uow.Repository<Ex_Request_ApprovedStation>().GetAny(p => p.Station_ID == obj.Station_ID && p.CheckRequest_ID == obj.CheckRequest_ID);
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }               

        public Dictionary<string, object> Update(ExportRequest_ApprovedStationDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}
