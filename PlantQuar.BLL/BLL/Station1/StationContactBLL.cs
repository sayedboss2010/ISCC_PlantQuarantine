using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
    public class StationContactBLL : IGenericBLL<StationContactDTO>
    {
        private UnitOfWork uow;
        public StationContactBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                StationContact entity = uow.Repository<StationContact>().Findobject(Id);
                var StatContactDTO = Mapper.Map<StationContact, StationContactDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, StatContactDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<StationContact>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Station.User_Deletion_Id == null
                && p.ContactType.User_Deletion_Id == null
                ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<StationContact>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Station.User_Deletion_Id == null
                && p.ContactType.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<StationContact, StationContactDTO>);
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
            return null;

        }
        public bool GetAny(StationContactDTO entity)
        {
            // var obj = entity as StationContactDTO;
            return uow.Repository<StationContact>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.Value == entity.Value && p.ContactType_ID == entity.ContactType_ID && p.StationID == entity.StationID);

        }
        //******************************************//
        public Dictionary<string, object> Insert(StationContactDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<StationContact>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("StationContacts_seq");

                    uow.Repository<StationContact>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(StationContactDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    StationContact CModel = uow.Repository<StationContact>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<StationContact>().Update(Co);
                    uow.SaveChanges();

                    var StationContactDTO = Mapper.Map<StationContact, StationContactDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, StationContactDTO);
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
        public Dictionary<string, object> Update(StationContactDTO entity, int delete, List<string> Device_Info)
        {
            try
            {

                StationContact CModel = uow.Repository<StationContact>().Findobject(entity.ID);

                entity.User_Creation_Date = CModel.User_Creation_Date;
                entity.User_Creation_Id = CModel.User_Creation_Id;

                if (CModel.User_Updation_Id != null)
                {
                    entity.User_Updation_Date = CModel.User_Updation_Date;
                    entity.User_Updation_Id = CModel.User_Updation_Id;
                }

                var Co = Mapper.Map(entity, CModel);
                uow.Repository<StationContact>().Update(Co);
                uow.SaveChanges();

                var StationContactDTO = Mapper.Map<StationContact, StationContactDTO>(Co);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, StationContactDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, long StaionID, List<StationContactDTO> objRecords, List<string> Device_Info)
        {
            try
            {
                foreach (var item in objRecords)
                {
                    var CModel = Mapper.Map<StationContact>(item);
                    CModel.User_Creation_Date = Date_Now;
                    CModel.User_Creation_Id = user_id;
                    CModel.StationID = StaionID;
                    Insert(Mapper.Map<StationContactDTO>(CModel), Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, long StaionID, List<StationContactDTO> objRecords, List<string> Device_Info)
        {
            try
            {
                var lstAddHagrContacts = objRecords.Where(a => (a.ID == 0)).ToList();
                //// Add///
                if (lstAddHagrContacts.Count > 0)
                {
                    InsertRecords(user_id, Date_Now, StaionID, lstAddHagrContacts, Device_Info);

                }
                // Edit
                var lstEditHagrContacts = objRecords.Where(a => (a.DeleteCheck != 1 && a.ID > 0)).ToList();

                if (lstEditHagrContacts.Count() > 0)
                {
                    foreach (var item in lstEditHagrContacts)
                    {
                        item.User_Updation_Date = Date_Now;
                        item.User_Updation_Id = user_id;
                        item.StationID = StaionID;
                        Update(item, Device_Info);
                    }
                }
                // Delete
                var lstDeleteHagrContacts = objRecords.Where(a => (a.DeleteCheck == 1 && a.ID > 0)).ToList();

                if (lstDeleteHagrContacts.Count() > 0)
                {
                    foreach (var item in lstDeleteHagrContacts)
                    {
                        item.User_Deletion_Date = Date_Now;
                        item.User_Deletion_Id = user_id;
                        item.StationID = StaionID;
                        Update(item, 1, Device_Info);
                    }
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }

        public Dictionary<string, object> GetStationContactByStatID(int StationID, List<string> Device_Info)
        {
            var lang = Device_Info[2];
            //map on the client
            var data = uow.Repository<StationContact>().GetData().Where(a => a.StationID == StationID && a.User_Deletion_Id == null);

            //    .
            //    Select(c => new CustomOption
            //{ //change display lang
            //    DisplayText = (lang == "1" ? c.ContactType.Name_Ar: c.ContactType.Name_En),
            //    Value = c.ID
            //}).OrderBy(a => a.DisplayText).ToList();
            var _data = Mapper.Map<List<StationContactDTO>>(data);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _data);
        }
    }
}
