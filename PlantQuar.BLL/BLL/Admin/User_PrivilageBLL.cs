using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace PlantQuar.BLL.BLL.Admin
{
    public class User_PrivilageBLL
    {
        private UnitOfWork uow;
        private UnitOfWork uow2;
        dbPrivilageEntities db = new dbPrivilageEntities();

        public User_PrivilageBLL()
        {
            uow = new UnitOfWork();

            uow2 = new UnitOfWork(1);
        }

        public Dictionary<string, object> GetAll(long GrID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();
                if (GrID == -1)
                {
                    data = uow.Repository<Outlet>().GetData().Where(c =>
                                  c.User_Deletion_Id == null && c.IsActive == true)
                                      .Select(c => new CustomOptionLongId
                                      { //change display lang
                                          DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                                          Value = c.ID_HR
                                      }).OrderBy(a => a.DisplayText).ToList();
                }
                else if (GrID == 5) // خاص بالصادر تشكيل لجنة
                {
                    data = uow.Repository<Outlet>().GetData().Where(c =>
                                  c.User_Deletion_Id == null && c.IsActive == true)
                                      .Select(c => new CustomOptionLongId
                                      { //change display lang
                                          DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                                          Value = c.ID
                                      }).OrderBy(a => a.DisplayText).ToList();
                }
                else
                {
                    data = uow.Repository<Outlet>().GetData().Where(c =>
                                  c.User_Deletion_Id == null && c.IsActive == true && c.GrAdmin_ID == GrID)
                                      .Select(c => new CustomOptionLongId
                                      { //change display lang
                                          DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                                          Value = c.ID_HR
                                      }).OrderBy(a => a.DisplayText).ToList();
                }

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string, object> GetAll_Group(long Group, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();
                if (Group == -1)
                {
                    data = (from g in db.PR_Group
                            join ga in db.PR_GroupModuleMenu on g.Id equals ga.PR_GroupId
                            where g.Active == true
                            select new CustomOptionLongId
                            { //change display lang
                                DisplayText = (lang == "1" ? g.GroupName : g.GroupName_En),
                                Value = g.Id
                            }).Distinct().OrderBy(a => a.DisplayText).ToList();
                }


                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Dictionary<string, object> GetAll_Module(long PR_GroupId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from m in db.PR_Module
                        join ga in db.PR_GroupModuleMenu on m.Id equals ga.PR_ModuleId
                        where m.Active == true
                        && ga.PR_GroupId == PR_GroupId
                        select new CustomOptionLongId
                        { //change display lang
                            DisplayText = (lang == "1" ? m.ModuleName : m.ModuleName_En),
                            Value = m.Id
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

                //data = db.PR_Module.Where(p => p.Active == true &&p.PR_GroupModuleMenu.)
                //    .Select(c => new CustomOptionLongId
                //    { //change display lang
                //        DisplayText = (lang == "1" ? c.ModuleName : c.ModuleName_En),
                //        Value = c.Id
                //    }).OrderBy(a => a.DisplayText).ToList();



                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string, object> GetAll_Menu(long PR_ModuleId, long PR_GroupId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from m in db.PR_Menu
                        join ga in db.PR_GroupModuleMenu on m.Id equals ga.PR_MenuId
                        where m.Active == true
                        && ga.PR_GroupId == PR_GroupId
                        && ga.PR_ModuleId == PR_ModuleId
                        select new CustomOptionLongId
                        { //change display lang
                            DisplayText = (lang == "1" ? m.MenuTitle : m.MenuTitle_En),
                            Value = m.Id
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();



                //data = db.PR_Menu.Where(p => p.Active == true)
                //    .Select(c => new CustomOptionLongId
                //    { //change display lang
                //        DisplayText = (lang == "1" ? c.MenuTitle : c.MenuTitle_En),
                //        Value = c.Id
                //    }).OrderBy(a => a.DisplayText).ToList();




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }
        public Dictionary<string, object> GetPR_User_Id_List(long outletId, List<string> Device_Info)
        {

            string lang = Device_Info[2];


            var d = new List<PR_User>();


            var data = db.PR_User.Where(p => p.LoginName != null && p.Password != null)
                .Select(c => new User
                {
                    DisplayText = (lang == "1" ? c.FullName : c.FullName),
                    Value = c.Id,
                    FullName = (lang == "1" ? c.FullName : c.FullName),
                    Outlet_ID = c.Outlet_ID,
                    Id = c.Id,
                    EmpId = c.EmpId
                ,
                    Adress = c.Adress_Ar

                }).Where(a => a.Outlet_ID == outletId).OrderBy(a => a.FullName).ToList();



            data.Insert(0, new User() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }



        public Dictionary<string, object> Insert_EmpPrivilage(User_PrivilageDTO menus, List<string> Device_Info)
        {



            try
            {



                //foreach (var item in menus)
                //{
                //    if (item.Check_Delete == true)
                //    {


                //        try
                //        {
                //            PR_GroupModuleMenuPrivilage Cmodel = new PR_GroupModuleMenuPrivilage();
                //            var data = db.PR_GroupModuleMenuPrivilage.Find(item.Id);



                //            var query = from ord in db.PR_GroupModuleMenuPrivilage where ord.Id == item.Id select ord;
                //            db.PR_GroupModuleMenuPrivilage.Remove(data);
                //            db.SaveChanges();

                //            uow.SaveChanges();

                //        }

                //        catch (Exception ex)
                //        {
                //            uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                //        }

                //    }
                //}

                foreach (var item in menus.List_Emp_New)
                {
                    if (item.CanDelete == null) item.CanDelete = false;
                    if (item.CanView == null) item.CanView = false;
                    if (item.CanEdit == null) item.CanEdit = false;
                    if (item.CanAdd == null) item.CanAdd = false;


                    if (menus.List_Emp_Old != null)
                    {
                        if (menus.List_Emp_Old.Where(a => a.Old_PR_GroupId == item.New_PR_GroupId
                                          && a.Old_PR_ModuleId == item.New_PR_ModuleId
                                          && a.Old_PR_MenuId == item.New_PR_MenuId).Any() != true)
                        {
                            int seq = db.Database.SqlQuery<int>("SELECT NEXT VALUE FOR dbo.PR_GroupModuleMenuPrivilage_seq").Single();
                            //  PR_GroupModuleMenuPrivilage_seq

                            // int seq = uow.Repository<Object>().GetNextSequenceValue_Int("PR_GroupModuleMenuPrivilage_seq");

                            PR_GroupModuleMenuPrivilage prm = new PR_GroupModuleMenuPrivilage();
                            prm.Id = seq;
                            prm.PR_User_id = menus.EmpId;
                            if (item.New_PR_MenuId == 100|| item.New_PR_MenuId == 227)
                            {
                                prm.CanView = (bool)item.CanView;
                                prm.CanAdd = (bool)item.CanAdd;
                                prm.CanDelete = (bool)item.CanDelete;
                                prm.CanEdit = (bool)item.CanEdit;
                            }
                            else
                            {
                                prm.CanAdd = true;
                                prm.CanDelete = true;
                                prm.CanEdit = true;
                                prm.CanView = true;
                            }
                            //prm.CanAdd = true;
                            //prm.CanDelete = true;
                            //prm.CanEdit = true;
                            prm.CanPrint = true;
                            //prm.CanView = true;
                            prm.IS_Active = true;
                            prm.PR_GroupId = item.New_PR_GroupId;
                            prm.PR_ModuleId = item.New_PR_ModuleId;
                            prm.PR_MenuId = item.New_PR_MenuId;

                            db.PR_GroupModuleMenuPrivilage.Add(prm);



                            db.SaveChanges();
                            //return uow.Repository<PR_GroupModuleMenuPrivilage>().DataReturn((int)Enums.Success.GetData, item);
                        }
                    }
                    else
                    {

                        int seq = db.Database.SqlQuery<int>("SELECT NEXT VALUE FOR dbo.PR_GroupModuleMenuPrivilage_seq").Single();
                        // int seq = uow.Repository<Object>().GetNextSequenceValue_Int("PR_GroupModuleMenuPrivilage_seq");

                        PR_GroupModuleMenuPrivilage prm = new PR_GroupModuleMenuPrivilage();
                        prm.Id = seq;
                        prm.PR_User_id = menus.EmpId;
                        if (item.New_PR_MenuId == 100)
                        {
                            prm.CanView = (bool)item.CanView;
                            prm.CanAdd = (bool)item.CanAdd;
                            prm.CanDelete = (bool)item.CanDelete;
                            prm.CanEdit = (bool)item.CanEdit;
                        }
                        else
                        {
                            prm.CanAdd = true;
                            prm.CanDelete = true;
                            prm.CanEdit = true;
                            prm.CanView = true;
                        }
                        prm.IS_Active = true;
                        prm.CanPrint = true;
                        prm.PR_GroupId = item.New_PR_GroupId;
                        prm.PR_ModuleId = item.New_PR_ModuleId;
                        prm.PR_MenuId = item.New_PR_MenuId;

                        db.PR_GroupModuleMenuPrivilage.Add(prm);



                        db.SaveChanges();
                        //return uow.Repository<PR_GroupModuleMenuPrivilage>().DataReturn((int)Enums.Success.GetData, item);
                    }




                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, menus);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetPR_User_Old_List(short checkedEmpId, List<string> Device_Info)
        {

            string lang = Device_Info[2];


            var d = new List<PR_GroupModuleMenuPrivilage>();


            var data = db.PR_GroupModuleMenuPrivilage.Where(p => p.PR_User_id == checkedEmpId)
                .Select(c => new Emp_Old_DTO
                {
                    Id = c.Id,

                    Old_PR_GroupId = c.PR_GroupId,
                    Old_PR_ModuleId = c.PR_ModuleId,
                    Old_PR_MenuId = c.PR_MenuId,
                    Old_PR_Group_Name = c.PR_Group.GroupName,
                    Old_PR_Module_Name = c.PR_Module.ModuleName,
                    Old_PR_Menu_Name = c.PR_Menu.MenuTitle

                }).ToList();



            //data.Insert(0, new User() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> Get_Priv_Emp_Old2(long Check_Delete_id, List<string> Device_Info)
        {

            string lang = Device_Info[2];

            PR_GroupModuleMenuPrivilage Cmodel = new PR_GroupModuleMenuPrivilage();
            var data = db.PR_GroupModuleMenuPrivilage.Find(Check_Delete_id);

            //var query = from ord in db.PR_GroupModuleMenuPrivilage where ord.Id == Check_Delete_id select ord;
            db.PR_GroupModuleMenuPrivilage.Remove(data);

            db.SaveChanges();

            //uow.SaveChanges();

            //data.Insert(0, new User() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
        }






    }
}
