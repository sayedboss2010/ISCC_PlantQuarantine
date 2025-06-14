using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Linq;
using System.Collections.Generic;
using PlantQuar.DTO.DTO.ExportRequest;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class ExportRequest_PortBll : IGenericBLL<ExportRequest_PortDTO>
    {
        private UnitOfWork uow;
        public ExportRequest_PortBll()
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

        public bool GetAny(ExportRequest_PortDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> InsertList(List<ExportRequest_PortDTO> ports, List<string> Device_Info)
        {
            try
            {
                foreach (var entity in ports)
                {
                    var CModel = Mapper.Map<Ex_Request_Port>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("Ex_Request_Port_Seq");
                    uow.Repository<Ex_Request_Port>().InsertRecord(CModel);
                    uow.SaveChanges();
                }
               
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, ports.FirstOrDefault());
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        public Dictionary<string, object> Insert(ExportRequest_PortDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
        public Dictionary<string, object> Update(ExportRequest_PortDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}