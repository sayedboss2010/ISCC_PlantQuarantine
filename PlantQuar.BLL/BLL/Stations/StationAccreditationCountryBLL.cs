using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Stations
{
    public class StationAccreditationCountryBLL : IGenericBLL<StationAccrediationCountryDTO>
    {
        private UnitOfWork uow;
        public StationAccreditationCountryBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                StationAccrediationCountry entity = uow.Repository<StationAccrediationCountry>().Findobject(Id);
                var _DTO = Mapper.Map<StationAccrediationCountry, StationAccrediationCountryDTO>(entity);
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
            var count = uow.Repository<StationAccrediationCountry>().GetData().Where(p => p.User_Deletion_Id == null
            // get undeleted parent
               && p.Station_Accreditation.User_Deletion_Id == null
                && p.Country.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<StationAccrediationCountry>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Station_Accreditation.User_Deletion_Id == null
                && p.Country.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<StationAccrediationCountry, StationAccrediationCountryDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(StationAccrediationCountryDTO obj)
        {
            return uow.Repository<StationAccrediationCountry>().GetAny(p => p.User_Deletion_Id == null &&
                                       p.StationAccrediationID == obj.StationAccrediationID && p.CountryID == obj.CountryID && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //******************************************//
        public Dictionary<string, object> Insert(StationAccrediationCountryDTO entity, List<string> Device_Info)
        {
            try
            {
                
                if (!GetAny(entity))
                {
                    entity.ID = uow.Repository<Object>().GetNextSequenceValue_Long("StationAccrediationCountry_seq");
                    var CModel = Mapper.Map<StationAccrediationCountry>(entity);
                    uow.Repository<StationAccrediationCountry>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(StationAccrediationCountryDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    StationAccrediationCountry CModel = uow.Repository<StationAccrediationCountry>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<StationAccrediationCountry>().Update(Co);
                    uow.SaveChanges();

                    var _DTO = Mapper.Map<StationAccrediationCountry, StationAccrediationCountryDTO>(Co);
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
        public void Delete(StationAccrediationCountry obj, List<string> Device_Info)
        {
                try {
                uow.Repository<StationAccrediationCountry>().Update(obj);
            uow.SaveChanges();
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            }
        }
        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, long StationAccrediationID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                StationAccrediationCountryDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new StationAccrediationCountryDTO();
                    dto.CountryID = item;
                    dto.StationAccrediationID = StationAccrediationID;
                    dto.User_Creation_Date = Date_Now;
                    dto.User_Creation_Id = user_id;
                    dto.IsActive = true;
                    Insert(dto, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, long StationAccrediationID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<StationAccrediationCountry>().GetData().Where(x => x.StationAccrediationID == StationAccrediationID && x.User_Deletion_Id == null).ToList();
                var addlst = objRecords.Except(data.Select(x => x.CountryID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.CountryID) == -1).ToList();
                InsertRecords(user_id, Date_Now, StationAccrediationID, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    item.User_Deletion_Date = Date_Now;
                    item.User_Deletion_Id = user_id;
                    Delete(item, Device_Info);
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
