using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class ExportRequest_UnapprovedPlacesBll : IGenericBLL<ExportRequest_UnapprovedPlacesDTO>
    {
        private UnitOfWork uow;
        public ExportRequest_UnapprovedPlacesBll()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Insert(ExportRequest_UnapprovedPlacesDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Ex_Request_UnapprovedPlaces>(entity);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_Request_UnapprovedPlaces_Seq");

                uow.Repository<Ex_Request_UnapprovedPlaces>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        public bool GetAny(ExportRequest_UnapprovedPlacesDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(ExportRequest_UnapprovedPlacesDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}
