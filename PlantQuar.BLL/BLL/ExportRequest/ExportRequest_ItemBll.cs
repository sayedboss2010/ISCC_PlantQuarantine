using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class ExportRequest_ItemBll : IGenericBLL<ExportRequest_ItemDTO>
    {
        private UnitOfWork uow;
        public ExportRequest_ItemBll()
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

        public bool GetAny(ExportRequest_ItemDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(ExportRequest_ItemDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Ex_Request_Item>(entity);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_Request_Item_seq");
                CModel = uow.Repository<Ex_Request_Item>().InsertReturn(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CModel.ID);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.ErrorHappened, null);
            }         
        }

        public Dictionary<string, object> Update(ExportRequest_ItemDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}
