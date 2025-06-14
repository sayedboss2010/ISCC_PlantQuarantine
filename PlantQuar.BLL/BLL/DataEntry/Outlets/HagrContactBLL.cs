using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Outlets
{

    public class HagrContactBLL : IGenericBLL<HagrContactDTO>

    {
        private UnitOfWork uow;

        public HagrContactBLL()
        {

            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<HagrContact>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.ContactType.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

               public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                HagrContact entity = uow.Repository<HagrContact>().Findobject(Id);
                var empDTO = Mapper.Map<HagrContact, HagrContactDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<HagrContact>().GetData().Where(a => a.User_Deletion_Id == null
             // get undeleted parent
             && a.ContactType.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<HagrContact, HagrContactDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
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
                //Complete Code

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(HagrContactDTO entity)
        {
           // return true;
           var obj = entity as HagrContactDTO;
            return uow.Repository<HagrContact>().GetAny(a => a.ContactOwnerID == obj.ContactOwnerID && a.ContactType_ID==obj.ContactType_ID&&a.OutlitAdmin==obj.OutlitAdmin&&a.Value==obj.Value&&a.User_Deletion_Id==null&& a.ID != obj.ID);
        }
        //******************************************//
        public Dictionary<string, object> Insert(HagrContactDTO entity, List<string> Device_Info)
        {
            try
            {
                
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<HagrContact>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("HagrContact_seq");
                    uow.Repository<HagrContact>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(HagrContactDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as HagrContactDTO;
                    HagrContact CModel = uow.Repository<HagrContact>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<HagrContact>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<HagrContact, HagrContactDTO>(Co);
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
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<HagrContact>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<HagrContact>().Update(Cmodel);
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
        public Dictionary<string, object> InsertRecords(short user_id,long ContactOwnerID, DateTime Date_Now, int OutlitAdmin, List<HagrContactDTO> objRecords, List<string> Device_Info)
        {
            try
            {
                foreach (var item in objRecords)
                {
                    var CModel = Mapper.Map<HagrContact>(item);
                    CModel.User_Creation_Date = Date_Now;
                    CModel.User_Creation_Id = user_id;
                    CModel.OutlitAdmin = OutlitAdmin;
                    CModel.ContactOwnerID = ContactOwnerID;
                    Insert(Mapper.Map<HagrContactDTO>(CModel),  Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> UpdateRecords(short user_id, long ContactOwnerID, DateTime Date_Now, int OutlitAdmin, List<HagrContactDTO> objRecords,
            List<string> Device_Info)
        {
            try
            {
                var CDTO = Mapper.Map<List<HagrContactDTO>>(objRecords);
                var lstAddHagrContacts = CDTO.Where(a => (a.ID == 0)).ToList();
                //// Add///
                if (lstAddHagrContacts.Count > 0)
                {
                    InsertRecords(user_id, ContactOwnerID, Date_Now, OutlitAdmin, Mapper.Map<List<HagrContactDTO>>(lstAddHagrContacts),   Device_Info);

                }
                // Edit
                var lstEditHagrContacts = CDTO.Where(a => (a.DeleteCheck != 1 && a.ID > 0)).ToList();

                if (lstEditHagrContacts.Count() > 0)
                {
                    foreach (var item in lstEditHagrContacts)
                    {
                        item.User_Updation_Date = Date_Now;
                        item.User_Updation_Id = user_id;
                        item.OutlitAdmin = OutlitAdmin;
                        item.ContactOwnerID = ContactOwnerID;
                        Update(Mapper.Map<HagrContactDTO>(item), Device_Info);
                    }
                }
                // Delete
                var lstDeleteHagrContacts = CDTO.Where(a => (a.DeleteCheck == 1 && a.ID > 0)).ToList();

                if (lstDeleteHagrContacts.Count() > 0)
                {
                    foreach (var item in lstDeleteHagrContacts)
                    {
                        item.User_Deletion_Date = Date_Now;
                        item.User_Deletion_Id = user_id;
                        item.OutlitAdmin = OutlitAdmin;
                        item.ContactOwnerID = ContactOwnerID;
                        Update(Mapper.Map<HagrContactDTO>(item), Device_Info);
                    }
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }
        public Dictionary<string, object> GetHagrContactByOutlitAdminID(long ContactOwnerID,int outletadmin)
        {
            var data = uow.Repository<HagrContact>().GetData().Where(a => a.ContactOwnerID == ContactOwnerID && a.User_Deletion_Id == null&&a.OutlitAdmin==outletadmin).ToList();
            var _data = Mapper.Map<List<HagrContactDTO>>(data);

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _data);
        }
    }
}
