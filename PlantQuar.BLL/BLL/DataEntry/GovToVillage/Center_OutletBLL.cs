using AutoMapper;
using PlantQuar.BLL.BLL.DataEntry.Outlets;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.API.Controllers.DataEntry.GovToVillage
{
   public class Center_OutletBLL 
    {
        private UnitOfWork uow;
        public Center_OutletBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Center_Outlet entity = uow.Repository<Center_Outlet>().Findobject(Id);
                var _DTO = Mapper.Map<Center_Outlet, Center_OutletDTO>(entity);
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
            var count = uow.Repository<Center_Outlet>().GetData().Where(p => p.User_Deletion_Id == null
            // get undeleted parent
            && p.Outlet.User_Deletion_Id == null
             && p.Center.User_Deletion_Id == null);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        //public Dictionary<string, object> GetAll(long outlet_ID,int pageSize, int index, List<string> Device_Info)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        Int64 data_Count = 0;
        //        var data = uow.Repository<Center_Outlet>().GetData().Where(p => p.User_Deletion_Id == null
        //       // get undeleted parent
        //       && p.Outlet.User_Deletion_Id == null
        //        && p.Center.User_Deletion_Id == null && p.Outlet_ID == outlet_ID).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
        //        var dataDTO = data.Select(Mapper.Map<Center_Outlet, Center_OutletDTO>);
        //        data_Count = data.Count();
        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("centers_Data", dataDTO);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public Dictionary<string, object> GetAll(long outlet_ID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;
                var data = uow.Repository<Center_Outlet>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Outlet.User_Deletion_Id == null
                && p.Center.User_Deletion_Id == null && p.Outlet_ID == outlet_ID).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                OutletBLL outletbll = new OutletBLL();
                var dataDto = data.Select(x => new Center_OutletDTO
                {

                    ID = x.ID,
                    Center_ID = x.Center_ID,
                    Outlet_ID = x.Outlet_ID,
                    IsActive = x.IsActive,
                    User_Creation_Date = x.User_Creation_Date,
                    User_Creation_Id = x.User_Creation_Id,
                    User_Deletion_Date = x.User_Deletion_Date,
                    User_Deletion_Id = x.User_Deletion_Id,
                    User_Updation_Date = x.User_Updation_Date,
                    User_Updation_Id = x.User_Updation_Id,
                    GovID = outletbll.GetGovIDByCenterID(x.Center_ID, Device_Info),

                }).ToList();

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("centers_Data", dataDto);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public bool GetAny(Center_OutletDTO obj)
        {
            return uow.Repository <Center_Outlet> ().GetAny(p => p.User_Deletion_Id == null &&
                                       p.Outlet_ID == obj.Outlet_ID && p.Center_ID == obj.Center_ID && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //******************************************//
        public Dictionary<string, object> Insert(Center_OutletDTO entity, List<string> Device_Info)
        {
            try
            {
                
                if (!GetAny(entity))
                {

                    var CModel = Mapper.Map<Center_Outlet>(entity);

                    CModel.ID= uow.Repository<Object>().GetNextSequenceValue_Long("Center_Outlet_seq");
                    CModel.IsActive = true;
                    uow.Repository<Center_Outlet>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Center_OutletDTO obj, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(obj))
                {
                    Center_Outlet CModel = uow.Repository<Center_Outlet>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Center_Outlet>().Update(Co);
                    uow.SaveChanges();

                    var _DTO = Mapper.Map<Center_Outlet, Center_OutletDTO>(Co);
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
        public void Delete(Center_Outlet obj, List<string> Device_Info)
        {
            try
            {
                uow.Repository<Center_Outlet>().Update(obj);
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            }
        }

        public Dictionary<string, object> Delete2(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Center_Outlet>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Center_Outlet>().Update(Cmodel);
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

        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, long Outlet_ID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                Center_OutletDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new Center_OutletDTO();
                    dto.Center_ID = item;
                    dto.Outlet_ID = Outlet_ID;
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
        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, long Outlet_ID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Center_Outlet>().GetData().Where(x => x.Outlet_ID == Outlet_ID && x.User_Deletion_Id == null).ToList();
                var addlst = objRecords.Except(data.Select(x => x.Center_ID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.Center_ID) == -1).ToList();
                InsertRecords(user_id, Date_Now, Outlet_ID, addlst, Device_Info);
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

        public Dictionary<string, object> GetOutlet_GovList(short Gov_ID,  List<string> Device_Info)
        {

            string lang = Device_Info[2];
            var data = uow.Repository<Center_Outlet>().GetData()
                .Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Outlet.User_Deletion_Id == null
                && p.Center.User_Deletion_Id == null && p.Center.Govern_ID == Gov_ID)
                .Select(c => new CustomOptionLongId
                { //change display lang
                DisplayText = lang == "1" ? c.Outlet.Ar_Name : c.Outlet.En_Name,
                    Value = c.Outlet_ID
                }).Distinct().OrderBy(a => a.DisplayText).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());


        }

    }
}
