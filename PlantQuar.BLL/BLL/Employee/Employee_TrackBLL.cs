using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Employee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Employee
{
    public class Employee_TrackBLL
    {
        private UnitOfWork uow;
        private UnitOfWork uow2;
        dbPrivilageEntities db = new dbPrivilageEntities();

        public Employee_TrackBLL()
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

                else if (GrID == 1)
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
        public Dictionary<string, object> GetAllGeneralAdmin(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();
                data = uow.Repository<General_Admin>().GetData().Where(c =>
                c.User_Deletion_Id == null && c.IsActive == true)
                    .Select(c => new CustomOptionLongId
                    { //change display lang
                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string, object> GetPR_User_Id_List(long outletId, int Operation_Type, string Start_Date, string End_Date, long Emp_ID, long Company_ID, byte Committee_TypeId, int PageNo, string request_number, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            //var ddddd = (from u in db.PR_User
            //             where u.Outlet_ID== outletId
            //             select new User
            //             {
            //                 FullName = (lang == "1" ? u.FullName : u.FullNameEn),
            //                 Id = u.Id,
            //                 Outlet_ID = u.Outlet_ID,
            //                 EmpId = u.EmpId,
            //             }).ToList();
            string _Operation_Type = "";
            if (Operation_Type == 96)
            {
                _Operation_Type = "74";
            }
            if (Operation_Type == 93)
            {
                _Operation_Type = "73";
            }
            Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
            paramters_Type.Add("Outlet_ID", SqlDbType.BigInt);
            paramters_Type.Add("Operation_Type", SqlDbType.Int);

            Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
            paramters_Data.Add("Outlet_ID", outletId.ToString());
            paramters_Data.Add("Operation_Type", _Operation_Type.ToString());

            var Data_User = uow.Repository<Employee_TrackDTO>().CallStored("Get_User_RequestCommittee_List", paramters_Type,
            paramters_Data, Device_Info).ToList();

            //var Data_User = (from u in ddddd
            //                 join ce in entities.CommitteeEmployees on u.Id equals ce.Employee_Id
            //                 join im_r in entities.Im_RequestCommittee on ce.Committee_ID equals im_r.ID
            //                 join ct in entities.CommitteeTypes on im_r.CommitteeType_ID equals ct.ID
            //                 join im_cr in entities.Im_CheckRequest on im_r.ImCheckRequest_ID equals im_cr.ID
            //                 join cr_d in entities.Im_CheckRequest_Data on im_cr.ID equals cr_d.Im_CheckRequest_ID


            //                 join cm in entities.Company_National on cr_d.Importer_ID equals cm.ID into comp
            //                 from cm in comp.DefaultIfEmpty()

            //                 join or in entities.Public_Organization on cr_d.Importer_ID equals or.ID into hgh
            //                 from or in hgh.DefaultIfEmpty()

            //                 join pe in entities.People on cr_d.Importer_ID equals pe.ID into fffff
            //                 from pe in fffff.DefaultIfEmpty()

            //                 where ce.User_Deletion_Date == null && ce.User_Deletion_Id == null

            //                 && ce.OperationType == null
            //                 select new Employee_TrackDTO
            //                 {
            //                     Name_Ar_Company = cr_d.ImporterType_Id == 6 ? cm.Name_Ar : cr_d.ImporterType_Id == 7 ? or.Name_Ar : cr_d.ImporterType_Id == 9 ? pe.Name : "",
            //                     Name_En_Company = cr_d.ImporterType_Id == 6 ? cm.Name_En : cr_d.ImporterType_Id == 7 ? or.Name_En : cr_d.ImporterType_Id == 9 ? pe.Name_EN : "",

            //                     Importer_ID = cr_d.Importer_ID,
            //                     ImporterType_Id = cr_d.ImporterType_Id,
            //                     CheckRequest_Number = im_cr.CheckRequest_Number,
            //                     FullName = u.FullName,
            //                     Id_User = u.Id,
            //                     Outlet_ID_user = u.Outlet_ID,
            //                     EmpId_user = u.EmpId,
            //                     Committee_ID = ce.Committee_ID,
            //                     Employee_Id = u.EmpId,
            //                     ISAdmin = (ce.ISAdmin == true ? "أدمن" : "مساعد"),
            //                     OperationType = ce.OperationType,
            //                     StartTime = im_r.StartTime,
            //                     EndTime = im_r.EndTime,
            //                     Delegation_Date = im_r.Delegation_Date,
            //                     Name_Committee = (lang == "1" ? ct.Name_Ar : ct.Name_En),
            //                     CommitteeType_ID = im_r.CommitteeType_ID,
            //                     IsFinishedAll = im_r.IsFinishedAll,
            //                     IsApproved = im_r.IsApproved,
            //                     Status = im_r.Status,
            //                     Company_National_Id = cm.ID,
            //                     CommitteeTypes_Id = ct.ID,
            //                     User_Deletion_Id = ce.User_Deletion_Id,
            //                     User_Deletion_Date = ce.User_Deletion_Date,
            //                     // Committee_ID = im_r.ID

            //                 }).ToList();

            if (Start_Date != null && End_Date == null && request_number == null)
            {
                Data_User = Data_User.Where(a => a.Delegation_Date == Convert.ToDateTime(Start_Date)).ToList();
            }
            if (End_Date != null && Start_Date == null)
            {
                Data_User = Data_User.Where(a => a.Delegation_Date == Convert.ToDateTime(End_Date)).ToList();
            }
            if (Start_Date != null && End_Date != null)
            {
                Data_User = Data_User.Where(a => a.Delegation_Date >= Convert.ToDateTime(Start_Date) && a.Delegation_Date <= Convert.ToDateTime(End_Date)).ToList();
            }
            //موظف 
            if (Emp_ID != 0)
            {
                Data_User = Data_User.Where(a => a.Id_User == Emp_ID).ToList();
            }
            ////منفذ بس
            //if (outletId != 0)
            //{
            //    Data_User = Data_User.Where(a => a.Outlet_ID_user == outletId).ToList();
            //}
            //شركة بس
            if (Company_ID > 0)
            {
                Data_User = Data_User.Where(a => a.Company_National_Id == Company_ID).ToList();

            }
            //نوع اللجنة بس     where ct.ID == Committee_TypeId 
            if (Committee_TypeId > 0)
            {
                Data_User = Data_User.Where(a => a.CommitteeTypes_Id == Committee_TypeId).ToList();
            }

            if (PageNo == 2)
            {
                //var No_Conf =
                //            (from c in entities.Im_CommitteeResult
                //            where !(from o in entities.Im_CommitteeResult_Confirm
                //                    select o.Im_CommitteeResult_ID)
                //                   .Contains(c.Committee_ID)
                //            select c).ToList();
                Data_User = Data_User.Where(a => a.ISAdmin == "مساعد" && a.User_Deletion_Date == null && a.User_Deletion_Id == null).ToList();
            }

            if (PageNo == 3)
            {
                Data_User = Data_User.Where(a => a.IsFinishedAll == null).ToList();
            }


            if (request_number != null)
            {
                Data_User = Data_User.Where(a => a.CheckRequest_Number == request_number).ToList();
            }
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Data_User);



            //}
            //else
            //{
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
            //}
        }

        public Dictionary<string, object> GetPR_User_Id_List(long outletId, List<string> Device_Info)
        {

            string lang = Device_Info[2];
            PlantQuarantineEntities entities = new PlantQuarantineEntities();


            var ddddd = (from u in db.PR_User
                         select new User
                         {
                             FullName = (lang == "1" ? u.FullName : u.FullNameEn),
                             Id = u.Id,
                             Outlet_ID = u.Outlet_ID,
                             EmpId = u.EmpId,

                         }).ToList();

            //if (Operation_Type == 74)
            //{

            var Data_User = (from u in ddddd
                             join ce in entities.CommitteeEmployees on u.Id equals ce.Employee_Id
                             join im_r in entities.Im_RequestCommittee on ce.Committee_ID equals im_r.ID
                             join ct in entities.CommitteeTypes on im_r.CommitteeType_ID equals ct.ID

                             where u.Outlet_ID == outletId
                             select new Employee_TrackDTO
                             {
                                 FullName = u.FullName,
                                 Id_User = u.Id,
                                 Outlet_ID_user = u.Outlet_ID,
                                 EmpId_user = u.EmpId,
                                 Committee_ID = ce.Committee_ID,
                                 Employee_Id = u.EmpId,
                                 ISAdmin = (ce.ISAdmin == true ? "أدمن" : "مساعد"),
                                 OperationType = ce.OperationType,
                                 StartTime = im_r.StartTime,
                                 EndTime = im_r.EndTime,
                                 Name_Committee = (lang == "1" ? ct.Name_Ar : ct.Name_En),
                             }).ToList();

            //}





            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Data_User);
        }
        public Dictionary<string, object> GetPR_User_Id_List1(string Start_Date, string End_Date, long outletId, List<string> Device_Info)
        {

            string lang = Device_Info[2];
            DateTime _StartDate = Convert.ToDateTime(Start_Date);
            DateTime _End_Date = Convert.ToDateTime(End_Date);

            var data1 = (
                             from user in db.PR_User

                             join mission in db.PR_Mission
                             on user.Id equals mission.PR_User_Id

                             into comp
                             from cm in comp.DefaultIfEmpty()

                             select new
                             {

                                 usr = user,
                                 c = cm == null ? null : cm,

                             }
                             )
                           .Where(
                x => x.usr.Outlet_ID == outletId
                &&
        (
        x.c.PR_User_Id == null ||
        (
        x.c.IsActive == true &&

      (x.c.EndDate < _StartDate && x.c.EndDate <= _End_Date)
        ||
        (x.c.StartDate >= _StartDate && x.c.StartDate >= _End_Date)
        )
        )
       ).


       Select(x => new User
       {
           Value = x.usr.Id,
           DisplayText = x.usr.FullName
       }

                            )
                             .ToList();



            // var target = data1.ConvertAll(x => new  User  {
            //         Value  =x.usr.Id,         
            //        DisplayText =x.usr.FullName
            //});
            data1.Insert(0, new User() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data1);
        }

        public Dictionary<string, object> Insert(Classsss entity, List<string> Device_Info)
        {
            try
            {
                //var obj = entity.pR_MissionDTO;
                //var ff = db.PR_Mission.Where(p => p.User_Deletion_Id == null && p.Outlet_ID == obj.Outlet_ID
                //&& p.StartDate >= obj.StartDate && p.EndDate <= obj.EndDate);

                for (int i = 0; i < entity.Objs.Count; i++)
                {
                    long seq = db.Database.SqlQuery<long>("SELECT NEXT VALUE FOR dbo.PR_Mission_seq").Single();
                    // var dd = "22/8/1989";
                    PR_Mission prm = new PR_Mission();
                    prm.IsActive = entity.pR_MissionDTO.IsActive;
                    prm.Outlet_ID = entity.pR_MissionDTO.Outlet_ID;
                    prm.PR_User_Id = entity.Objs[i].value_Id;
                    prm.ID = seq;
                    prm.EndDate = entity.EndDate;
                    prm.StartDate = entity.StartDate;
                    db.PR_Mission.Add(prm);
                    db.SaveChanges();
                }


                return uow.Repository<PR_Mission>().DataReturn((int)Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)Enums.Error.Exception, null);
            }
        }
        public bool GetAny(PR_MissionDTO entity)
        {
            dbPrivilageEntities db = new dbPrivilageEntities();
            var obj = entity as PR_MissionDTO;
            var ff = db.PR_Mission.Where(p => p.User_Deletion_Id == null && p.Outlet_ID == entity.Outlet_ID
            && p.StartDate >= entity.StartDate && p.EndDate <= entity.EndDate);
            if (ff.Count() > 0)
            {
                return true;
            }
            else
                return false;

        }

        // get user in specific peroid.
        public Dictionary<string, object> GetAll(string Start_Date, string End_Date, long outletId, long outletId1, List<string> Device_Info)
        {
            // Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                dbPrivilageEntities db = new dbPrivilageEntities();
                //   var data = new List<PR_MissionDTO>();
                DateTime _StartDate = Convert.ToDateTime(Start_Date);
                DateTime _End_Date = Convert.ToDateTime(End_Date);
                //   Int64 data_Count = 0;
                string lang = Device_Info[2];


                //                (
                //                --StartDate between '2021-02-01' and '2021-02-12'
                //StartDate >= '2021-02-01'
                //and
                //StartDate <= '2021-02-12'
                //and
                //--EndDate between '2021-02-01' and '2021-02-12'
                //EndDate >= '2021-02-01'
                //and
                //EndDate <= '2021-02-12'

                //)


                //OR
                // (
                //EndDate >= '2021-02-01'
                //AND StartDate <= '2021-02-12'
                //)


                var data = db.PR_Mission.Join(db.PR_User,
                              dc => dc.PR_User_Id,
                              d => d.Id,
                              (dc, d) => new { DealerContact = dc, Dealer = d })

                           .Where(dc_d => dc_d.DealerContact.User_Deletion_Id == null
                           && dc_d.DealerContact.Outlet_ID == outletId
                           && dc_d.Dealer.Outlet_ID == outletId
                              &&
                            (
                          (dc_d.DealerContact.StartDate >= _StartDate
                           &&
                           dc_d.DealerContact.StartDate <= _End_Date
                           &&
                           dc_d.DealerContact.EndDate >= _StartDate
                           &&
                           dc_d.DealerContact.EndDate <= _End_Date

                           ) ||
                           (dc_d.DealerContact.EndDate >= _StartDate && dc_d.DealerContact.StartDate <= _End_Date)

                            )

                            ||
                            (
                            dc_d.DealerContact.EndDate >= _StartDate
&&
dc_d.DealerContact.EndDate <= _End_Date

                            )


                              && dc_d.DealerContact.IsActive == true)
                          .Select(dc_d => new Class1
                          {
                              PR_User_Name = dc_d.Dealer.FullName,
                              EndDate = dc_d.DealerContact.EndDate,
                              Outlet_ID = dc_d.DealerContact.Outlet_ID,
                              StartDate = dc_d.DealerContact.StartDate,
                              ID = dc_d.DealerContact.ID,
                              IsActive = dc_d.DealerContact.IsActive
                          }).ToList();

                var data1 = data.Where(a => a.Outlet_ID == outletId);



                return uow.Repository<PR_MissionDTO>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data1);



            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllEmployeesForOutlet(long Outlet_ID, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities prv = new dbPrivilageEntities();
                string lang = Device_Info[2];
                var data = prv.PR_User.Where(a => a.Outlet_ID == Outlet_ID)
                    .Select(a => new CustomOptionShortId { DisplayText = (lang == "1" ? a.FullName : a.FullNameEn), Value = a.Id }).ToList();
                data.Insert(0, new CustomOptionShortId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllEmployees_Outlet(long Outlet_ID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities prv = new dbPrivilageEntities();
                string lang = Device_Info[2];
                var Outlets_HR = entities.Outlets.Where(a => a.ID == Outlet_ID)
                 .Select(a => a.ID_HR).FirstOrDefault();

                var data = prv.PR_User.Where(a => a.Outlet_ID == Outlets_HR)
                    .Select(a => new CustomOptionShortId { DisplayText = (lang == "1" ? a.FullName : a.FullNameEn), Value = a.Id }).ToList();
                data.Insert(0, new CustomOptionShortId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllCompany(long Company, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();

                data = uow.Repository<Company_National>().GetData().Where(c =>
                              c.User_Deletion_Id == null && c.IsActive == true)
                                  .Select(c => new CustomOptionLongId
                                  { //change display lang
                                      DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                                      Value = c.ID
                                  }).OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string, object> GetAllCommittee(long Committee, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();

                data = uow.Repository<CommitteeType>().GetData().Where(c =>
                              c.User_Deletion_Id == null && c.type_Id == Committee)
                                  .Select(c => new CustomOptionLongId
                                  { //change display lang
                                      DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                                      Value = c.ID
                                  }).OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string, object> GetAllCommitteTypeForOperation(long Operation_Id, List<string> Device_Info)
        {

            try
            {

                dbPrivilageEntities prv = new dbPrivilageEntities();
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                string lang = Device_Info[2];
                var data = entities.A_SystemCode.Where(a => a.Id == Operation_Id)
                    .Select(a => new CustomOption { DisplayText = (lang == "1" ? a.ValueName : a.ValueNameEN), Value = a.Id }).ToList();
                //CustomOption empty = new CustomOption();
                //empty.Value = null;
                //empty.DisplayText = "-";
                ////data.Insert(0, empty);
                ///
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());



            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> DeleteEmployeeConfirm(long Committee_ID, long Employee_ID, bool Addmin_Confirm, List<string> Device_Info)
        {
            try
            {
             
                PlantQuarantineEntities db = new PlantQuarantineEntities();
               
                var  Op= db.CommitteeEmployees.Where(p => p.User_Deletion_Id == null && p.Committee_ID == Committee_ID
                && p.Employee_Id == Employee_ID && p.ISAdmin == Addmin_Confirm).Select(a=>a.OperationType).FirstOrDefault();

                
                if (Committee_ID != null && Employee_ID != null&&Op==74) // الوارد
                {
                    var datafarm = uow.Repository<CommitteeEmployee>().GetData().
                        Where(a => a.Committee_ID == Committee_ID && a.Employee_Id == Employee_ID
                        && a.OperationType == 74 && a.ISAdmin == Addmin_Confirm).FirstOrDefault();
                    if (datafarm != null)
                    {
                     

                        datafarm.User_Deletion_Date = DateTime.Now;
                        datafarm.User_Deletion_Id = (short?)Employee_ID;
                        uow.Repository<CommitteeEmployee>().Update(datafarm);
                        uow.SaveChanges();
                        if (Addmin_Confirm == true)
                        {
                            var Comm = uow.Repository<Im_RequestCommittee>().Findobject(Committee_ID);

                            if (Comm != null)
                            {
                                var _Im_CheckRequest_SampleData = uow.Repository<Im_CheckRequest_SampleData>().GetData().
                        Where(a => a.Im_RequestCommittee_ID == Committee_ID).ToList();
                                if (_Im_CheckRequest_SampleData != null)
                                {
                                    foreach (var item in _Im_CheckRequest_SampleData)
                                    {
                                        uow.Repository<Im_CheckRequest_SampleData>().Delete_Eslam(item);
                                    }

                                }

                                var _Im_RequestCommittee_Shift = uow.Repository<Im_RequestCommittee_Shift>().GetData().
                                    Where(a => a.Im_RequestCommittee_ID == Committee_ID).ToList();
                                if (_Im_RequestCommittee_Shift != null)
                                {
                                    foreach (var item in _Im_RequestCommittee_Shift)
                                    {
                                        uow.Repository<Im_RequestCommittee_Shift>().Delete_Eslam(item);
                                    }
                                }

                                var _Im_Request_TreatmentData = uow.Repository<Im_Request_TreatmentData>().GetData().
                                    Where(a => a.Im_RequestCommittee_ID == Committee_ID).ToList();
                                if (_Im_Request_TreatmentData != null)
                                {
                                    foreach (var item in _Im_Request_TreatmentData)
                                    {
                                        uow.Repository<Im_Request_TreatmentData>().Delete_Eslam(item);
                                    }
                                }
                                var _Im_CommitteeResult = uow.Repository<Im_CommitteeResult>().GetData().
                                    Where(a => a.Committee_ID == Committee_ID).ToList();
                                if (_Im_CommitteeResult != null)
                                {
                                    foreach (var item in _Im_CommitteeResult)
                                    {
                                        uow.Repository<Im_CommitteeResult>().Delete_Eslam(item);
                                    }
                                }

                            }

                            uow.Repository<Im_RequestCommittee>().Delete_Eslam(Comm);




                            //Comm.User_Deletion_Id = (short?)Employee_ID;
                            // Comm.User_Deletion_Date = DateTime.Now;
                            //uow.Repository<Im_RequestCommittee>().Update(Comm);

                            uow.SaveChanges();

                            //esss

                            //    Im_Request_TreatmentData

                            //    
                            //    Im_CommitteeResult
                        }


                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                    }
        
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid Delete");

                    }

                }

                else if (Committee_ID != null && Employee_ID != null && Op == 73)
                {

                    var datafarm2 = uow.Repository<CommitteeEmployee>().GetData().
                       Where(a => a.Committee_ID == Committee_ID && a.Employee_Id == Employee_ID
                       && a.OperationType == 73 && a.ISAdmin == Addmin_Confirm).FirstOrDefault();
                    if (datafarm2 != null)
                    {
                      

                        datafarm2.User_Deletion_Date = DateTime.Now;
                        datafarm2.User_Deletion_Id = (short?)Employee_ID;
                        uow.Repository<CommitteeEmployee>().Update(datafarm2);
                        uow.SaveChanges();
                        if (Addmin_Confirm == true)
                        {
                            var Comm = uow.Repository<Ex_RequestCommittee>().Findobject(Committee_ID);
                            var _committeeType = db.Ex_RequestCommittee.Where(p => p.User_Deletion_Id == null && p.ID == Committee_ID
        ).Select(a => a.CommitteeType_ID).FirstOrDefault();
                            if (Comm != null)
                            {
                                if (_committeeType == 3)
                                { 
                                var _Ex_CheckRequest_SampleData = uow.Repository<Ex_CheckRequest_SampleData>().GetData().
                        Where(a => a.Ex_RequestCommittee_ID == Committee_ID).ToList();
                                if (_Ex_CheckRequest_SampleData != null)
                                {
                                    foreach (var item in _Ex_CheckRequest_SampleData)
                                    {
                                        uow.Repository<Ex_CheckRequest_SampleData>().Delete_Eslam(item);
                                    }

                                }
}
                                var _Ex_RequestCommittee_Shift = uow.Repository<Ex_RequestCommittee_Shift>().GetData().
                                    Where(a => a.Ex_RequestCommittee_ID == Committee_ID).ToList();
                                if (_Ex_RequestCommittee_Shift != null)
                                {
                                    foreach (var item in _Ex_RequestCommittee_Shift)
                                    {
                                        uow.Repository<Ex_RequestCommittee_Shift>().Delete_Eslam(item);
                                    }
                                }
                                if (_committeeType == 6)
                                {
                                    var _Ex_Request_TreatmentData = uow.Repository<Ex_Request_TreatmentData>().GetData().
                                    Where(a => a.Ex_RequestCommittee_ID == Committee_ID).ToList();
                                    if (_Ex_Request_TreatmentData != null)
                                    {
                                        foreach (var item in _Ex_Request_TreatmentData)
                                        {
                                            uow.Repository<Ex_Request_TreatmentData>().Delete_Eslam(item);
                                        }
                                    }
                                }
                                var _Ex_CommitteeResult = uow.Repository<Ex_CommitteeResult>().GetData().
                                    Where(a => a.Committee_ID == Committee_ID).ToList();
                                if (_Ex_CommitteeResult != null)
                                {
                                    foreach (var item in _Ex_CommitteeResult)
                                    {
                                        uow.Repository<Ex_CommitteeResult>().Delete_Eslam(item);
                                    }
                                }
                                var _Ex_RequestCommittee_Fees_ENG = uow.Repository<Ex_RequestCommittee_Fees_ENG>().GetData().
                                                              Where(a => a.Ex_RequestCommittee_ID == Committee_ID).ToList();
                                if (_Ex_RequestCommittee_Fees_ENG != null)
                                {
                                    foreach (var item in _Ex_RequestCommittee_Fees_ENG)
                                    {
                                        uow.Repository<Ex_RequestCommittee_Fees_ENG>().Delete_Eslam(item);
                                    }
                                }
                            }

                            uow.Repository<Ex_RequestCommittee>().Delete_Eslam(Comm);




                            //Comm.User_Deletion_Id = (short?)Employee_ID;
                            // Comm.User_Deletion_Date = DateTime.Now;
                            //uow.Repository<Im_RequestCommittee>().Update(Comm);

                            uow.SaveChanges();

                            //esss

                            //    Im_Request_TreatmentData

                            //    
                            //    Im_CommitteeResult
                        }


                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");


                    }

                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid Delete");

                    }
                }

                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid Delete");

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
