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

    public class Im_WarehousesBLL<T> : IGenericBLL<T>
    {
        private UnitOfWork uow;

        public Im_WarehousesBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Im_Warehouses entity = uow.Repository<Im_Warehouses>().Findobject(Id);
                var empDTO = Mapper.Map<Im_Warehouses, Im_WarehousesDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Im_Warehouses>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Im_Warehouses>().GetData(pageSize, index, A => 1 == 1).Where(a => a.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<Im_Warehouses, Im_WarehousesDTO>);
                return uow.Repository<Object>().DataReturn((int)Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {

            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<Im_Warehouses>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Warehouses>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Name_En.StartsWith(enName)).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Warehouses>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Name_Ar.StartsWith(arName)).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Warehouses>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Im_Warehouses>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))).ToList();
                }
                string lang = Device_Info[2];
                var dataDTO = data.OrderBy(A => (lang=="1"?A.Name_Ar:A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Im_Warehouses, Im_WarehousesDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Im_Warehouses", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(T entity)
        {
            var obj = entity as Im_WarehousesDTO;
            return uow.Repository<Im_Warehouses>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_Ar == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Im_Warehouses>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Im_Warehouses>().Update(Cmodel);
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

        //******************************************//
        public Dictionary<string, object> Insert(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as Im_WarehousesDTO;
                    var id = uow.Repository<Object>().GetNextSequenceValue_Int("Im_Warehouses_seq");
                    obj.ID = id;
                    var CModel = Mapper.Map<Im_Warehouses>(entity);
                    uow.Repository<Im_Warehouses>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as Im_WarehousesDTO;
                    Im_Warehouses CModel = uow.Repository<Im_Warehouses>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Im_Warehouses>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_Warehouses, Im_WarehousesDTO>(Co);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(int List , List<string> Device_Info)
        {
            string lang =Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(WHouse => WHouse.SystemCodeTypeId == 9).Select(c => new CustomOption {
                //DisplayText = c.ValueName,
                //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueName),
                Value = c.Id }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang =Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(WHouse => WHouse.SystemCodeTypeId == 9).Select(c => new CustomOption {
                //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueName),
               // DisplayText = c.ValueName,
                Value = c.Id }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
