using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
    public class StationCompanyBLL : IGenericBLL<StationCompanyDTO>
    {
        private UnitOfWork uow;
        public StationCompanyBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                StationCompany entity = uow.Repository<StationCompany>().Findobject(Id);
                var DTO = Mapper.Map<StationCompany, StationCompanyDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)PlantQuar.DTO.HelperClasses.Enums.Success.Insert, DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<StationCompany>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<StationCompany>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null
                // get undeleted parent
               && p.Company_National.User_Deletion_Id == null
                && p.StationActivity.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<StationCompany, StationCompanyDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<StationCompany>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<StationCompany>().Update(Cmodel);
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

        public bool GetAny(StationCompanyDTO obj)
        {
            return uow.Repository<StationCompany>().GetAny(a => a.Company_ID == obj.Company_ID && a.StationActivityID == obj.StationActivityID && a.Start_Date == obj.Start_Date && a.End_Date == obj.End_Date && a.ID != obj.ID);
            //return false;
        }
        //******************************************/////
        public Dictionary<string, object> Insert(StationCompanyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<StationCompany>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("StationCompany_seq");
                    uow.Repository<StationCompany>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(StationCompanyDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    StationCompany CModel = uow.Repository<StationCompany>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<StationCompany>().Update(Co);
                    uow.SaveChanges();

                    var DTO = Mapper.Map<StationCompany, StationCompanyDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)PlantQuar.DTO.HelperClasses.Enums.Success.Update, DTO);
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

        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, long StaionActivityID, List<StationCompanyDTO> objRecords, List<string> Device_Info)
        {
            try
            {
                foreach (var item in objRecords)
                {
                    var CModel = Mapper.Map<StationCompany>(item);
                    CModel.User_Creation_Date = Date_Now;
                    CModel.User_Creation_Id = user_id;
                    CModel.StationActivityID = StaionActivityID;
                    Insert(Mapper.Map<StationCompanyDTO>(CModel), Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, long StationActivityID, List<StationCompanyDTO> objRecords, List<string> Device_Info)
        {
            try
            {
                var lstAddHagrContacts = objRecords.Where(a => (a.ID == 0)).ToList();
                //// Add///
                if (lstAddHagrContacts.Count > 0)
                {
                    InsertRecords(user_id, Date_Now, StationActivityID, lstAddHagrContacts, Device_Info);

                }
                // Edit
                var lstEditHagrContacts = objRecords.Where(a => (a.DeleteCheck != 1 && a.ID > 0)).ToList();

                if (lstEditHagrContacts.Count() > 0)
                {
                    foreach (var item in lstEditHagrContacts)
                    {
                        item.User_Updation_Date = Date_Now;
                        item.User_Updation_Id = user_id;
                        item.StationActivityID = StationActivityID;
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
                        item.StationActivityID = StationActivityID;
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
        public Dictionary<string, object> Update(StationCompanyDTO obj, int delete, List<string> Device_Info)
        {
            try
            {

                StationCompany CModel = uow.Repository<StationCompany>().Findobject(obj.ID);

                obj.User_Creation_Date = CModel.User_Creation_Date;
                obj.User_Creation_Id = CModel.User_Creation_Id;

                if (CModel.User_Updation_Id != null)
                {
                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;
                }

                var Co = Mapper.Map(obj, CModel);
                uow.Repository<StationCompany>().Update(Co);
                uow.SaveChanges();

                var StationContactDTO = Mapper.Map<StationCompany, StationCompanyDTO>(Co);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, StationContactDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetStationCompanyByStationActivityID(int StationActivityID)
        {
            var data = uow.Repository<StationCompany>().GetData().Where(p => p.User_Deletion_Id == null && p.StationActivityID == StationActivityID
            && p.Company_National.User_Deletion_Id == null && p.StationActivity.User_Deletion_Id == null).ToList();
            var _data = Mapper.Map<List<StationCompanyDTO>>(data);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _data);

        }
    }
}
