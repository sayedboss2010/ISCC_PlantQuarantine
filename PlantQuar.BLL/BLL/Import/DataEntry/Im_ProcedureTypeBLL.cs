using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using PlantQuar.DTO.DTO.Import.DataEntry;
namespace PlantQuar.BLL.BLL.Import.DataEntry
{

    public class Im_ProcedureTypeBLL : IGenericBLL<Im_ProcedureTypeDTO>
    {
        private UnitOfWork uow;

        public Im_ProcedureTypeBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {            

            try
            {
                Im_ProcedureType entity = uow.Repository<Im_ProcedureType>().Findobject(Id);
                var empDTO = Mapper.Map<Im_ProcedureType, Im_ProcedureTypeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                var data = new List<Im_ProcedureType>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_ProcedureType>().GetData().Where(a => a.Name_En.StartsWith(enName)).ToList();
                    data_Count = data.Count();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_ProcedureType>().GetData().Where(a => a.Name_Ar.StartsWith(arName)).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_ProcedureType>().GetData().Where(a=>a.User_Deletion_Date == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Im_ProcedureType>().GetData().Where(a => (a.Name_Ar.StartsWith(arName)
                    && a.Name_En.StartsWith(enName))).ToList();
                    data_Count = data.Count();
                }
                string lang = Device_Info[2];
                var dataDTO = data.OrderBy(A =>(lang=="1"? A.Name_Ar:A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Im_ProcedureType, Im_ProcedureTypeDTO>);

                dic.Add("Count_Data", data_Count);
                dic.Add("Im_ProcedureType_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Im_ProcedureTypeDTO entity)
        {
            var obj = entity;
            return uow.Repository<Im_ProcedureType>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Insert(Im_ProcedureTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {

                    var CModel = Mapper.Map<Im_ProcedureType>(entity);
                    uow.Repository<Im_ProcedureType>().InsertRecord(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }

        public Dictionary<string, object> Update(Im_ProcedureTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity;
                    Im_ProcedureType CModel = uow.Repository<Im_ProcedureType>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Im_ProcedureType>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_ProcedureType, Im_ProcedureTypeDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }

        ////Find Im_ProcedureType Obj
        //public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        //{
        //    try
        //    {
        //        Im_ProcedureType entity = uow.Repository<Im_ProcedureType>().Findobject(Id);
        //        var empDTO = Mapper.Map<Im_ProcedureType, Im_ProcedureTypeDTO>(entity);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        ////Get Count Im_ProcedureType
        //public Dictionary<string, object> GetCount()
        //{
        //    var count = uow.Repository<Im_ProcedureType>().GetData().Count();
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        //}

        ////Get Im_ProcedureType List 
        //public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        //{
        //    try
        //    {
        //        var data = uow.Repository<Im_ProcedureType>().GetData().OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
        //        var dataDTO = data.Select(Mapper.Map<Im_ProcedureType, Im_ProcedureTypeDTO>);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        ////Get Im_ProcedureType List Search by ArName & EnName
        //public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        //{
        //    return null;

        //}

        ////Get Any Im_ProcedureType
        //public bool GetAny(Im_ProcedureTypeDTO entity)
        //{
        //    return false;
        //}

        ////Create Im_ProcedureType
        //public Dictionary<string, object> Insert(Im_ProcedureTypeDTO entity, List<string> Device_Info)
        //{
        //    return null;

        //}

        ////Update Im_ProcedureType
        //public Dictionary<string, object> Update(Im_ProcedureTypeDTO entity, List<string> Device_Info)
        //{
        //    return null;
        //}

        ////Delete Im_ProcedureType
        //public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        //{
        //    return null;

        //}

        ////DROPS
        //Get Im_ProcedureType List DDL
        public Dictionary<string, object> FillIm_ProcedureType_List(List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Im_ProcedureType>().GetData()
                    .Select(c => new CustomOptionShortId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
                data.Insert(0, new CustomOptionShortId() { Value = null, DisplayText = "----------" });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        ////Get Im_ProcedureType Create & Update DDL
        //public Dictionary<string, object> FillIm_ProcedureType_AddEdit()
        //{
        //    var data = uow.Repository<Im_ProcedureType>().GetData().Select(c => new CustomOptionShortId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
        //    CustomOptionShortId empty = new CustomOptionShortId();
        //    empty.Value = null;
        //    empty.DisplayText = "-";
        //    data.Insert(0, empty);
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        //}
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Im_ProcedureType>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Im_ProcedureType>().Update(Cmodel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
