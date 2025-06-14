using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace PlantQuar.BLL.BLL.Import.Committee
{
   public class Im_RequestCommittee_ShiftBLL : IGenericBLL<Im_RequestCommittee_ShiftDTO>
    {
        private UnitOfWork uow;
        public Im_RequestCommittee_ShiftBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetByCommittee(long committeeId, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;
                var data = uow.Repository<Im_RequestCommittee_Shift>().GetData().Where(a => a.User_Deletion_Id == null && a.Im_RequestCommittee_ID == committeeId).
                    Select(a => new Im_RequestCommittee_ShiftDTO
                    {
                        ID = a.ID,
                        Im_RequestCommittee_ID = a.Im_RequestCommittee_ID,
                        ShiftTiming_ID = a.ShiftTiming_ID,
                        Count = a.Count,
                        money = a.ShiftTiming.count,
                    }).ToList();

                if (pageSize > -1 && index > -1)
                {
                    data = data.Skip(index).Take(pageSize).ToList();
                }
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("committeeShift", data);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Im_RequestCommittee_ShiftDTO entity)
        {
            var obj = entity;
            return uow.Repository<Im_RequestCommittee_Shift>().GetAny(p => p.User_Deletion_Id == null
                                        && p.Im_RequestCommittee_ID == obj.Im_RequestCommittee_ID
                                        && p.ShiftTiming_ID == obj.ShiftTiming_ID
                                        && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Insert(Im_RequestCommittee_ShiftDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_Shift_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;

                    var CModel = Mapper.Map<Im_RequestCommittee_Shift>(entity);
                    uow.Repository<Im_RequestCommittee_Shift>().InsertRecord(CModel);
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

        public Dictionary<string, object> Update(Im_RequestCommittee_ShiftDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity;
                    Im_RequestCommittee_Shift CModel = uow.Repository<Im_RequestCommittee_Shift>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Im_RequestCommittee_Shift>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_RequestCommittee_Shift, Im_RequestCommittee_ShiftDTO>(Co);
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

        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Im_RequestCommittee_Shift>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;

                    uow.Repository<Im_RequestCommittee_Shift>().Update(Cmodel);
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

        //*************************//
        public Dictionary<string, object> FillDrop_ShiftTiming(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive)
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> GetTimingMony(byte shiftId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.ID == shiftId).FirstOrDefault().count;

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
    }
}
