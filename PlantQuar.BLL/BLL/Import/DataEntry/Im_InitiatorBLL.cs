using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.DataEntry;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Import.DataEntry
{
    public class Im_InitiatorBLL : IGenericBLL<Im_InitiatorDTO>
    {
        private UnitOfWork uow;

        public Im_InitiatorBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Im_Initiator entity = uow.Repository<Im_Initiator>().Findobject(Id);
                var empDTO = Mapper.Map<Im_Initiator, Im_InitiatorDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount(List<string> Device_Info)
        {
            var count = uow.Repository<Im_Initiator>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //.Where(a => a.User_Deletion_Id != null).OrderBy(A => A.ID).Skip(index).Take(pageSize)
                var data = uow.Repository<Im_Initiator>().GetData().ToList();
                var dataDTO = data.Select(Mapper.Map<Im_Initiator, Im_InitiatorDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Im_Initiator>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Im_Initiator>().GetData().Where(a =>
                       a.Item_ShortName.ShortName_En.StartsWith(enName.Trim()) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<Im_Initiator>().GetData().Where(a =>
                         a.Item_ShortName.ShortName_Ar.StartsWith(arName.Trim()) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_Initiator>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Im_Initiator>().GetData().Where(a =>
                    (a.Item_ShortName.ShortName_Ar.StartsWith(arName) && a.Item_ShortName.ShortName_En.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }

                var dataDto = data.OrderBy(A => A.Item_ShortName).Skip(index).Take(pageSize).Select(Mapper.Map<Im_Initiator, Im_InitiatorDTO>);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Im_Initiator_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Im_InitiatorDTO entity)
        {
            var obj = entity;
            return uow.Repository<Im_Initiator>().
                GetAny(p =>
                p.User_Deletion_Id == null
                && p.Item_ShortName_ID == obj.Item_ShortName_ID
                && p.QualitativeGroup_Id == obj.QualitativeGroup_Id
                && p.Country_Id == obj.Country_Id
                && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        //Create  Im_Initiator 
        //public Dictionary<string, object> Insert(Im_InitiatorDTO entity, List<string> Device_Info)
        //{
        //    try
        //    {
        //        if (!GetAny(entity))
        //        {
        //            //entity.ID =int.Parse( id.ToString());
        //            entity.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Initiator_seq");
        //            var CModel = Mapper.Map<Im_Initiator>(entity);
        //            uow.Repository<Im_Initiator>().InsertRecord(CModel);
        //            uow.SaveChanges();
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
        //        }
        //        else
        //        {
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        //Update  Im_Initiator 
        public Dictionary<string, object> Update(Im_InitiatorDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    Im_Initiator CModel = uow.Repository<Im_Initiator>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;
                    //entity.AttachmentPath = CModel.AttachmentPath.ToList();
                    entity.Country_Id = CModel.Country_Id;

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<Im_Initiator>().UpdateReturn(Co);

                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_Initiator, Im_InitiatorDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //Delete Im_Initiator
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Im_Initiator>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;

                    uow.Repository<Im_Initiator>().Update(Cmodel);

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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //**********************************************//
        //SARA//
        public Dictionary<string, object> GetInitiatorsByShortName(long shortNameId, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var dataDTO = new List<Im_InitiatorDTO>();
                Int64 data_Count = 0;

                var data = uow.Repository<Im_Initiator>().GetData()
                    .Where(i => i.User_Deletion_Id == null && i.Item_ShortName_ID == shortNameId)
                    .Select(c => new Im_InitiatorDTO
                    {
                        ID = c.ID,
                        Country_Id = c.Country_Id,
                        Initiator_Status = c.Initiator_Status,
                        ForbiddenReason = c.ForbiddenReason,
                        IsActive = c.IsActive,
                        User_Creation_Date = c.User_Creation_Date,
                        User_Creation_Id = c.User_Creation_Id,
                        User_Updation_Id = c.User_Updation_Id,
                        User_Updation_Date = c.User_Updation_Date,
                        Item_ShortName_ID = c.Item_ShortName_ID,
                        QualitativeGroup_Id = c.QualitativeGroup_Id,
                        AttachmentPath = c.AttachmentPath
                    }).ToList();
                if (data.Count>0)
                {
                    dataDTO= data.Skip(index).Take(pageSize).ToList();
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Im_Initiator_Data", dataDTO);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetInitiatorsByQualGrp(int qualGrp, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var dataDTO = new List<Im_InitiatorDTO>();
                Int64 data_Count = 0;

                var data = uow.Repository<Im_Initiator>().GetData()
                    .Where(i => i.User_Deletion_Id == null && i.QualitativeGroup_Id == qualGrp)
                    .Select(c => new Im_InitiatorDTO
                    {
                        ID = c.ID,
                        Country_Id = c.Country_Id,
                        Initiator_Status = c.Initiator_Status,
                        ForbiddenReason = c.ForbiddenReason,
                        IsActive = c.IsActive,
                        User_Creation_Date = c.User_Creation_Date,
                        User_Creation_Id = c.User_Creation_Id,
                        User_Updation_Id = c.User_Updation_Id,
                        User_Updation_Date = c.User_Updation_Date,
                        Item_ShortName_ID = c.Item_ShortName_ID,
                        QualitativeGroup_Id = c.QualitativeGroup_Id,
                         AttachmentPath = c.AttachmentPath
                    }).ToList();
                if (data.Count > 0)
                {
                    dataDTO = data.Skip(index).Take(pageSize).ToList();
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Im_Initiator_Data", dataDTO);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Get_InitiatorStatus(int syscode, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(x => x.SystemCodeTypeId == syscode).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueNameEN),
                Value = c.Id
            }).OrderBy(a => a.DisplayText).ToList(); 
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //sara
        public Dictionary<string, object> Insert(Im_InitiatorDTO entity, List<string> Device_Info)
        {
            try
            {
                if (entity.Country_Id > 0)
                {
                    if (!GetAny(entity))
                    {
                        //entity.ID =int.Parse( id.ToString());
                        entity.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Initiator_seq");
                        var CModel = Mapper.Map<Im_Initiator>(entity);
                        uow.Repository<Im_Initiator>().InsertRecord(CModel);
                        uow.SaveChanges();
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                    }
                }
                else
                {
                    foreach (var item in uow.Repository<Country>().GetData().
                            Where(u => u.User_Deletion_Id == null && u.IsActive == true).Select(u => u.ID).ToList())
                    {
                        entity.Country_Id = item;

                        if (!GetAny(entity))
                        {
                            entity.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Initiator_seq");
                            var CModel = Mapper.Map<Im_Initiator>(entity);
                            uow.Repository<Im_Initiator>().InsertRecord(CModel);
                            uow.SaveChanges();
                        }
                        else
                        {
                            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                        }
                    }

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}