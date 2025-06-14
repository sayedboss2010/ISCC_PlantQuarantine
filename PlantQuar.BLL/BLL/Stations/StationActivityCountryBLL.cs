using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Stations
{
    public class StationActivityCountryBLL : IGenericBLL<StationActivityCountryDTO>
    {
        private UnitOfWork uow;
        public StationActivityCountryBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                StationActivityCountry entity = uow.Repository<StationActivityCountry>().Findobject(Id);
                var _DTO = Mapper.Map<StationActivityCountry, StationActivityCountryDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<StationActivityCountry>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.StationActivity.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<StationActivityCountry>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.StationActivity.User_Deletion_Id == null
               ).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<StationActivityCountry, StationActivityCountryDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(StationActivityCountryDTO entity)
        {
            return uow.Repository<StationActivityCountry>().GetAny(a => a.StationActivityID == entity.StationActivityID && a.CountryID == entity.CountryID && a.User_Deletion_Id==null &&a.ID != entity.ID);
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<StationActivityCountry>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<StationActivityCountry>().Update(Cmodel);
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
        public Dictionary<string, object> Insert(StationActivityCountryDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    entity.ID = uow.Repository<Object>().GetNextSequenceValue_Long("StationActivityCountry_seq");
                    var CModel = Mapper.Map<StationActivityCountry>(entity);
                    uow.Repository<StationActivityCountry>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(StationActivityCountryDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    StationActivityCountry CModel = uow.Repository<StationActivityCountry>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<StationActivityCountry>().Update(Co);
                    uow.SaveChanges();

                    var _DTO = Mapper.Map<StationActivityCountry, StationActivityCountryDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
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
        public void Deleterecord(StationActivityCountry obj, List<string> Device_Info)
        {
            try { 
            //var model = Mapper.Map<List<StationActivityCountry>>(lst);
            uow.Repository<StationActivityCountry>().Update(obj);
            uow.SaveChanges();
                }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            }
}
        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, long StaionActivityID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                StationActivityCountryDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new StationActivityCountryDTO();
                    dto.CountryID = item;
                    dto.StationActivityID = StaionActivityID;
                    dto.User_Creation_Date = Date_Now;
                    dto.User_Creation_Id = user_id;
                    dto.IsActive = true;
                    Insert((dto), Device_Info);

                    /*var CModel = Mapper.Map<StationCompany>(item);
                    CModel.User_Creation_Date = Date_Now;
                    CModel.User_Creation_Id = user_id;
                    CModel.StationActivityID = StaionActivityID;
                    Insert(Mapper.Map<T>(CModel));*/
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, long StaionActivityID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<StationActivityCountry>().GetData().Where(x => x.StationActivityID == StaionActivityID && x.User_Deletion_Id == null).ToList();
                var addlst = objRecords.Except(data.Select(x=>x.CountryID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.CountryID) == -1).ToList();
                InsertRecords(user_id, Date_Now, StaionActivityID, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    item.User_Deletion_Date = Date_Now;
                    item.User_Deletion_Id = user_id;
                    Deleterecord(item, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }

    }
}
