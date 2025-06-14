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

    //public class GeneralAdminBLL<T> : IGenericBLL<T>
    public class GeneralAdminBLL : IGenericBLL<GeneralAdminDTO>

    {
        private UnitOfWork uow;

        public GeneralAdminBLL()
        {
            
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                General_Admin entity = uow.Repository<General_Admin>().Findobject(Id);
                var empDTO = Mapper.Map<General_Admin, GeneralAdminDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<General_Admin>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<General_Admin>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<General_Admin>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<General_Admin>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<General_Admin, GeneralAdminDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }





            //try
            //{
            // string lang = Device_Info[2];
            //    var data = uow.Repository<General_Admin>().GetData().Where(a => a.User_Deletion_Id == null)
            //        .OrderBy(A => (lang=="1"?A.Ar_Name:A.En_Name)).Skip(index).Take(pageSize).ToList();
            //    var dataDTO = data.Select(Mapper.Map<General_Admin, GeneralAdminDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<General_Admin>();
                Int64 data_Count = 0;
                  data = uow.Repository<General_Admin>().GetData().Where(a => a.User_Deletion_Id == null).ToList();

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data.Where(a => a.En_Name.StartsWith(enName)).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data.Where(a => a.Ar_Name.StartsWith(arName)).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))

                {
                    data = data.Where(a => (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).ToList();
                }

                var dataDto = data.OrderBy(A => (lang=="1"?A.Ar_Name:A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<General_Admin, GeneralAdminDTO>);

                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.En_Name).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.En_Name).ToList();
                        break;
                }
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("General_Admin_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(GeneralAdminDTO obj)
        {
            //return uow.Repository<General_Admin>().GetAny(p => p.User_Deletion_Id == null &&
            //                            p.Ar_Name == obj.Ar_Name && p.En_Name == obj.En_Name &&
            //                  p.Address_Ar == obj.Address_Ar && p.Address_En == obj.Address_En &&
            //                  p.Admin_ID==obj.Admin_ID&&
            //                            (obj.ID == 0 ? true : p.ID != obj.ID));

            return uow.Repository<General_Admin>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.Ar_Name == obj.Ar_Name && p.En_Name == obj.En_Name &&
                                        //p.Address_Ar == obj.Address_Ar && p.Address_En == obj.Address_En &&
                                        //p.Admin_ID == obj.Admin_ID &&
                                        (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(GeneralAdminDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<General_Admin>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Byte("General_Admin_seq");
                    var dto= uow.Repository<General_Admin>().InsertReturn(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dto);
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

        public Dictionary<string, object> Update(GeneralAdminDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as GeneralAdminDTO;
                    General_Admin CModel = uow.Repository<General_Admin>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<General_Admin>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<General_Admin, GeneralAdminDTO>(Co);
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
                var Cmodel = uow.Repository<General_Admin>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<General_Admin>().Update(Cmodel);
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

        public Dictionary<string, object> FillDrop_Add( List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<General_Admin>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true)
                .Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit( List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<General_Admin>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).OrderBy(a=>a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> GetByCenter(int centerID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entity = new PlantQuarantineEntities();

                var data = (
                        from outlet in entity.Outlets
                        join centerOutlet in entity.Centers on outlet.ID equals centerOutlet.Outlet_ID
                        join general in entity.General_Admin on outlet.GrAdmin_ID equals general.ID
                        where outlet.IsActive == true && outlet.User_Deletion_Id == null
                        && centerOutlet.IsActive == true && centerOutlet.User_Deletion_Id == null
                        && centerOutlet.ID == centerID && general.IsActive == true &&
                        general.User_Deletion_Id == null

                        select new CustomOption { DisplayText = general.Ar_Name, Value = general.ID }).Distinct().ToList();

                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null); ;
            }
        }
    }
}