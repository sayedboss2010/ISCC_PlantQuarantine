using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PlantQuar.DTO.HelperClasses;
using System.Reflection;
using PlantQuar.DTO.DTO.DataEntry.Fees;
namespace PlantQuar.BLL.BLL.DataEntry.Fees
{
    public class ShiftTimingBLL
    {
        private UnitOfWork uow;

        public ShiftTimingBLL()
        {
            uow = new UnitOfWork();
        }

        //Find ShiftTiming by Id 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                ShiftTiming entity = uow.Repository<ShiftTiming>().Findobject(Id);
                var empDTO = Mapper.Map<ShiftTiming, ShiftTimingDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get ShiftTiming Count where User_Deletion_Id is null
        public Dictionary<string, object> GetCount(List<string> Device_Info)
        {


            var count = uow.Repository<ShiftTiming>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {


            try
            {
                var data = new List<ShiftTiming>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<ShiftTiming>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<ShiftTiming>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<ShiftTiming, ShiftTimingDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }



            //try
            //{
            //    var data = new List<ShiftTiming>();
            //    string lang = Device_Info[2];

            //    if (pageSize == -1 && index == -1)
            //    {
            //        data = uow.Repository<ShiftTiming>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
            //    }
            //    else
            //    {
            //        data = uow.Repository<ShiftTiming>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
            //    }

            //    var dataDTO = data.Select(Mapper.Map<ShiftTiming, ShiftTimingDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }

        //Get ShiftTiming List by ARName or ENName  where User_Deletion_Id is null
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ShiftTiming>();
                Int64 data_Count = 0;
                string lang = Device_Info[2];

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<ShiftTiming>().GetData().Where(a =>
                       a.Name_En.StartsWith(enName) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<ShiftTiming>().GetData().Where(a =>
                         a.Name_Ar.StartsWith(arName) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ShiftTiming>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<ShiftTiming>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }

                var dataDto = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<ShiftTiming, ShiftTimingDTO>);

                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.Name_En).ToList();
                        break;
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("ShiftTiming_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any ShiftTiming  where User_Deletion_Id=null
        public bool GetAny(ShiftTimingDTO entity)
        {
            var obj = entity;
            return uow.Repository<ShiftTiming>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//

        //Create  ShiftTiming 
        public Dictionary<string, object> Insert(ShiftTimingDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("ShiftTiming_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;

                    var CModel = Mapper.Map<ShiftTiming>(entity);
                    uow.Repository<ShiftTiming>().InsertRecord(CModel);
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


        //Update  ShiftTiming 
        public Dictionary<string, object> Update(ShiftTimingDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity;
                    ShiftTiming CModel = uow.Repository<ShiftTiming>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<ShiftTiming>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<ShiftTiming, ShiftTimingDTO>(Co);
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

        //Delete ShiftTiming
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<ShiftTiming>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<ShiftTiming>().Update(Cmodel);
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

        //ADD FUNCTIONS TO FILL DROPS   

        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.User_Deletion_Id == null)
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
        //eman
        public Dictionary<string, object> FillDrop_ByDayType(byte? dayType, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Day_Type == dayType)
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

        public Dictionary<string, object> GetShiftToming(byte Id, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.ID == Id)
                .Select(c => new ShiftTimingDTO
                {
                    //change display lang
                    ShiftTiming_From = c.ShiftTiming_From,
                    ShiftTiming_To = c.ShiftTiming_To,
                    ID = c.ID
                }).FirstOrDefault();
            //set default value fz 17-4-2019

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
    }
}
