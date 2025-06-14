using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using PlantQuar.DTO.HelperClasses;
using System.Data;
using System.Reflection;
using PlantQuar.DTO.DTO.Admin;

namespace PlantQuar.BLL.BLL.Admin
{

    public class ErrorDisplayBLL : IGenericBLL<A__plant_Error_SaveDTO>
    {
        private UnitOfWork uow;

        public ErrorDisplayBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                var data = uow.Repository<A__plant_Error_Save>().GetData().ToList().Skip(index).Take(pageSize).OrderByDescending(a => a.Id);
                var dataDTO = data.Select(Mapper.Map<A__plant_Error_Save, A__plant_Error_SaveDTO>);
                Int64 data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Error_Save_Data", dataDTO);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<A__plant_Error_Save>().Findobject(obj.id);

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("RowId", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("RowId", obj.id.ToString());

                var request = uow.Repository<object>().CallStored("ErrorSave_Delete", paramters_Type,
                    paramters_Data, Device_Info).FirstOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(A__plant_Error_SaveDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(A__plant_Error_SaveDTO entity, List<string> Device_Info)
        {
            entity.User_Ip = Device_Info[0];
            entity.IsWeb = bool.Parse(Device_Info[1]);

            var CModel = Mapper.Map<A__plant_Error_Save>(entity);
            CModel.Date = DateTime.Now;
            CModel.Id = uow.Repository<Object>().GetNextSequenceValue_Long("A__plant_Error_Save_seq");

            var CreatedModel = uow.Repository<A__plant_Error_Save>().InsertReturn(CModel);
            uow.SaveChanges();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CModel);
        }

        public Dictionary<string, object> Update(A__plant_Error_SaveDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}