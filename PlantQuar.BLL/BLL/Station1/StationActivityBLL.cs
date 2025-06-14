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
    public class StationActivityBLL : IGenericBLL<StationActivityDTO>
    {
        private UnitOfWork uow;

        public StationActivityBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                StationActivity entity = uow.Repository<StationActivity>().Findobject(Id);
                var empDTO = Mapper.Map<StationActivity, StationActivityDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<StationActivity>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Station.User_Deletion_Id == null
                && p.StationActivityType.User_Deletion_Id == null
                ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {

                var data = uow.Repository<StationActivity>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Station.User_Deletion_Id == null
                && p.StationActivityType.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<StationActivity, StationActivityDTO>);

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
                var Cmodel = uow.Repository<StationActivity>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<StationActivity>().Update(Cmodel);
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

        public bool GetAny(StationActivityDTO obj)
        {
            return uow.Repository<StationActivity>().GetAny(p => p.User_Deletion_Id == null &&
                                       p.Station_ID == obj.Station_ID &&
                                       p.StationActivityType_ID == obj.StationActivityType_ID &&
                                      ((p.Enrollment_Start == obj.Enrollment_Start &&
                                       p.Enrollment_End == obj.Enrollment_End) || (p.Enrollment_End > obj.Enrollment_Start) || p.IsActive == true) && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //******************************************//
        public Dictionary<string, object> Insert(StationActivityDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<StationActivity>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("StationActivity_seq");
                    var data = uow.Repository<StationActivity>().InsertReturn(CModel);
                    uow.SaveChanges();
                    entity.ID = data.ID;
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
        public Dictionary<string, object> Update(StationActivityDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    StationActivity CModel = uow.Repository<StationActivity>().Findobject(obj.ID);
                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }
                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<StationActivity>().Update(Co);
                    uow.SaveChanges();

                    var _DTO = Mapper.Map<StationActivity, StationActivityDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
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
        public Dictionary<string, object> GetStationActivityByStationID(int StationID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                Int64 data_Count = 0;
                var data = uow.Repository<StationActivity>().GetData().Where(p => p.User_Deletion_Id == null && p.Station_ID == StationID
             && p.Station.User_Deletion_Id == null && p.StationActivityType.User_Deletion_Id == null
             //&& p.StationActivityCountries.Select(cc=>cc.User_Deletion_Id)==null
             //&& p.StationCompanies.Select(com=>com.User_Deletion_Id)==null
             ).Select(x => new StationActivityDTO
             {
                 Station_ID = x.Station_ID,
                 ID = x.ID,
                 Enrollment_End = x.Enrollment_End,
                 Enrollment_Start = x.Enrollment_Start,
                 IsActive = x.IsActive,
                 StationActivityType_ID = x.StationActivityType_ID,
                 User_Creation_Date = x.User_Creation_Date,
                 User_Creation_Id = x.User_Creation_Id,
                 User_Deletion_Date = x.User_Deletion_Date,
                 User_Deletion_Id = x.User_Deletion_Id,
                 User_Updation_Date = x.User_Updation_Date,
                 User_Updation_Id = x.User_Updation_Id,
                 Company = x.StationCompanies.Where(c => c.User_Deletion_Id == null).Select(c => new StationCompanyDTO()
                 {
                     Company_ID = c.Company_ID,
                     User_Deletion_Id = c.User_Deletion_Id,
                     End_Date = c.End_Date,
                     ID = c.ID,
                     IsActive = c.IsActive,
                     Start_Date = c.Start_Date,
                     StationActivityID = c.StationActivityID,
                     User_Creation_Date = c.User_Creation_Date,
                     User_Creation_Id = c.User_Creation_Id,
                     User_Deletion_Date = c.User_Deletion_Date,
                     User_Updation_Date = c.User_Updation_Date,
                     User_Updation_Id = c.User_Updation_Id

                 }).ToList(),
                 CountryID = x.StationActivityCountries.Where(c => c.User_Deletion_Id == null).Select(c => c.CountryID).ToList(),

             });
                data_Count = data.Count();
                var _data = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                dic.Add("Count_Data", data_Count);
                dic.Add("Station_Data", _data);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetStationActivityWithTypeName(int Station_ID)
        {
            var data = uow.Repository<StationActivity>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.Station_ID == Station_ID)
                .Select(c => new CustomOption { DisplayText = c.StationActivityType.Ar_Name, Value = (int)c.ID, startDate = c.Enrollment_Start, endDate = c.Enrollment_End }).ToList();
            data.Insert(0, new CustomOption { DisplayText = "-", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> GetStationAllActivity(int Station_ID)
        {
            var data = uow.Repository<StationActivity>().GetData().Where(a => a.User_Deletion_Id == null && a.Station_ID == Station_ID)
                .Select(c => new CustomOption { DisplayText = c.StationActivityType.Ar_Name, Value = (int)c.ID }).ToList();
            data.Insert(0, new CustomOption { DisplayText = "-", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(A => A.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
