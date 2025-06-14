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
using PlantQuar.DTO.DTO.Admin;
using Privilages.DAL;

namespace PlantQuar.BLL.BLL.Admin
{

    public class PR_MissionBLL
    {
        dbPrivilageEntities db = new dbPrivilageEntities();
        private UnitOfWork uow; private UnitOfWork uow2;
        public PR_MissionBLL()
        {
            uow = new UnitOfWork();
            uow2 = new UnitOfWork(1);
        }
        //Find PR_Mission by Id 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                PR_Mission entity = uow.Repository<PR_Mission>().Findobject(Id);
                var empDTO = Mapper.Map<PR_Mission, PR_MissionDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get PR_Mission Count where User_Deletion_Id is null
        public Dictionary<string, object> GetCount(List<string> Device_Info)
        {


            var count = uow.Repository<PR_Mission>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<PR_Mission>().GetData().Where(p => p.User_Deletion_Id == null).
                    OrderBy(A => (lang == "1" ? A.ID : A.ID)).Skip(index).Take(pageSize).ToList();
                //var data = uow.Repository<PR_Mission>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<PR_Mission, PR_MissionDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                dbPrivilageEntities db = new dbPrivilageEntities();
                var data = new List<PR_MissionDTO>();
                Int64 data_Count = 0;
                //     db.Configuration.ProxyCreationEnabled = false;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    // db.PR_Mission.SingleOrDefault(x => x.LoginName == user.LoginName && x.Password == user.Password);

                    data = db.PR_Mission.Select(a => new PR_MissionDTO
                    {
                        ID = a.ID,
                        EndDate = a.EndDate,
                        StartDate = a.StartDate,
                        PR_User_Id = a.PR_User_Id,
                        Outlet_ID = a.Outlet_ID,
                        IsActive = a.IsActive,
                        
                    }).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = db.PR_Mission.Select(a => new PR_MissionDTO
                    {
                        ID = a.ID,
                        EndDate = a.EndDate,
                        StartDate = a.StartDate,
                        PR_User_Id = a.PR_User_Id,
                        Outlet_ID = a.Outlet_ID,
                        IsActive = a.IsActive,
                    }).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = db.PR_Mission.Select(a => new PR_MissionDTO
                    {
                        ID = a.ID,
                        EndDate = a.EndDate,
                        StartDate = a.StartDate,
                        PR_User_Id = a.PR_User_Id,
                        Outlet_ID = a.Outlet_ID,
                        IsActive = a.IsActive,
                    }).ToList();
                }
                else
                {
                    //data = uow.Repository<PR_Mission>().GetData().Select(a => new PR_MissionDTO
                    data = db.PR_Mission.Select(a => new PR_MissionDTO
                    {
                        ID = a.ID,
                        EndDate = a.EndDate,
                        StartDate = a.StartDate,
                        PR_User_Id = a.PR_User_Id,
                        Outlet_ID = a.Outlet_ID,
                        IsActive = a.IsActive,
                    }).ToList();
                }
                //switch (jtSorting)
                //{
                //    case "Ar_Name ASC":
                //        data = data.OrderBy(t => t.Ar_Name).ToList();
                //        break;
                //    case "Ar_Name DESC":
                //        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                //        break;
                //    case "En_Name ASC":
                //        data = data.OrderBy(t => t.En_Name).ToList();
                //        break;
                //    case "En_Name DESC":
                //        data = data.OrderByDescending(t => t.En_Name).ToList();
                //        break;


                //}
                string lang = Device_Info[2];

                var dataDTO = data.Skip(index).Take(pageSize);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("PR_Mission_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);

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
                PR_Mission Cmodel =new PR_Mission();
                var data=db.PR_Mission.Find(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                   
                    var query =from ord in db.PR_Mission where ord.ID == dto.id select ord;
                    db.PR_Mission.Remove(data);
                    db.SaveChanges();
                   
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)Enums.Success.DeletedScussfuly, Cmodel);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)Enums.Error.RepeatedData, null);
                }
            }

            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public bool GetAny(PR_MissionDTO entity)
        {
            dbPrivilageEntities db = new dbPrivilageEntities();
            var obj = entity as PR_MissionDTO;
            var ff = db.PR_Mission.Where(p => p.User_Deletion_Id == null);
            if (ff.Count() > 0)
            {
                return true;
            }
            else
                return false;

        }

        public Dictionary<string, object> Insert(PR_MissionDTO entity, List<string> Device_Info)
        {


            try
            {

                if (GetAny(entity))
                {
                    long seq = db.Database.SqlQuery<long>("SELECT NEXT VALUE FOR dbo.PR_Mission_seq").Single();

                    // var dd = "22/8/1989";
                    PR_Mission prm = new PR_Mission();

                    prm.IsActive = entity.IsActive;
                    prm.Outlet_ID = entity.Outlet_ID;
                    prm.PR_User_Id = entity.PR_User_Id;

                    prm.ID = seq;
                    db.PR_Mission.Add(prm); 
                
                        prm.EndDate=  entity.EndDate;
                        prm.StartDate = entity.StartDate ;
                

                    db.PR_Mission.Add(prm);
                    db.SaveChanges();
                    return uow.Repository<PR_Mission>().DataReturn((int)Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(PR_MissionDTO entity, List<string> Device_Info)
        {
            dbPrivilageEntities db = new dbPrivilageEntities();

            try
            {

                if (GetAny(entity))
                {



                    PR_Mission prm = new PR_Mission();
                    prm.EndDate = entity.EndDate;
                    prm.StartDate = entity.StartDate;
                    prm.IsActive = entity.IsActive;
                    prm.Outlet_ID = entity.Outlet_ID;
                    prm.PR_User_Id = entity.PR_User_Id;
                    db.PR_Mission.Add(prm);

                    //db.PR_Mission.Find(prm);
                    db.SaveChanges();
                    //var obj = entity as PR_MissionDTO;
                    //PR_Mission CModel = uow.Repository<PR_Mission>().Findobject(obj.Id);



                    //var Co = Mapper.Map(obj, CModel);

                    //uow.Repository<PR_Mission>().Update(Co);
                    //uow.SaveChanges();

                    //var empDTO = Mapper.Map<PR_Mission, PR_MissionDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, prm);
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

        //Delete PR_Mission


        //ADD FUNCTIONS TO FILL DROPS   

        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<PR_Mission>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {
                    // Value =c.ID
                }).OrderBy(a => a.Value).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> GetPR_User_Id_List(List<string> Device_Info)
        {

            string lang = Device_Info[2];
            var data = db.PR_User.Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.FullName : c.FullName),
                Value = (int)c.EmpId
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> GetOutlet_ID_List(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();
                data = uow.Repository<Outlet>().GetData().Where(c=>c.User_Deletion_Id==null&&c.IsActive==true) .Select(c => new CustomOptionLongId
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();

                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
