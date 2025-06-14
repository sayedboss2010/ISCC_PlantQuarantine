using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
   public class Refuse_ReasonsBLL : IGenericBLL<EX_Refuse_ReasonsDTO>
    {
        private UnitOfWork uow;

        public Refuse_ReasonsBLL()
        {
            uow = new UnitOfWork();
        }

        //Find Refuse_Reason by Id 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Refuse_Reason entity = uow.Repository<Refuse_Reason>().Findobject(Id);
                var empDTO = Mapper.Map<Refuse_Reason, EX_Refuse_ReasonsDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Refuse_Reason Count where User_Deletion_Id is null
        public Dictionary<string, object> GetCount(List<string> Device_Info)
        {


            var count = uow.Repository<Refuse_Reason>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        //public Dictionary<string, object> GetAll(List<string> Device_Info)
        //{
        //    try
        //    {
        //        var data = uow.Repository<Refuse_Reason>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.Name_Ar).ToList();
        //        var dataDTO = data.Select(Mapper.Map<Refuse_Reason, EX_Refuse_ReasonsDTO>);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        //Get Refuse_Reason List where User_Deletion_Id is null
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<Refuse_Reason>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Refuse_Reason>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Refuse_Reason>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Refuse_Reason, EX_Refuse_ReasonsDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Refuse_Reason List by ARName or ENName  where User_Deletion_Id is null
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Refuse_Reason>();
                Int64 data_Count = 0;
                string lang = Device_Info[2];

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<Refuse_Reason>().GetData().Where(a =>
                       a.Name_En.StartsWith(enName) &&
                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<Refuse_Reason>().GetData().Where(a =>
                         a.Name_Ar.StartsWith(arName) &&
                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Refuse_Reason>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<Refuse_Reason>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName)) &&
                  a.User_Deletion_Id == null).ToList();
                }

                var dataDto = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Refuse_Reason, EX_Refuse_ReasonsDTO>);

                data_Count = data.Count();
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
                dic.Add("Count_Data", data_Count);
                dic.Add("Refuse_Reason_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any Refuse_Reason  where User_Deletion_Id=null
        public bool GetAny(EX_Refuse_ReasonsDTO entity)
        {
            var obj = entity;
            return uow.Repository<Refuse_Reason>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//

        //Create  Refuse_Reason 
        public Dictionary<string, object> Insert(EX_Refuse_ReasonsDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Short("Refuse_Reason_SEQ");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;

                    var CModel = Mapper.Map<Refuse_Reason>(entity);
                    uow.Repository<Refuse_Reason>().InsertRecord(CModel);
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


        //Update  Refuse_Reason 
        public Dictionary<string, object> Update(EX_Refuse_ReasonsDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity;
                    Refuse_Reason CModel = uow.Repository<Refuse_Reason>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Refuse_Reason>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Refuse_Reason, EX_Refuse_ReasonsDTO>(Co);
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

        //Delete Refuse_Reason
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Refuse_Reason>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Refuse_Reason>().Update(Cmodel);
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
        //
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Refuse_Reason>().GetData().Where(lab => lab.User_Deletion_Id == null)
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
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Refuse_Reason>().GetData().
                Where(a => a.User_Deletion_Id == null && a.IsActive == true).
                Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
       
        public Dictionary<string, object> Delete(int id, DateTime _DateTime, short Userid, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Outlet>().Findobject(id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = _DateTime;
                    Cmodel.User_Deletion_Id = Userid;
                    uow.Repository<Outlet>().Update(Cmodel);
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
    }
}