using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.DataEntry.Analysis;
using PlantQuar.DTO.DTO.Employee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Employee
{

    public class MissionBLL
    {

        private UnitOfWork uow;
        dbPrivilageEntities db = new dbPrivilageEntities();

        public MissionBLL()
        {
            uow = new UnitOfWork();

            // uow2 = new UnitOfWork(1);
        }
        public Dictionary<string, object> GetAll(long GrID,List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();
                if (GrID == -1)
                {
                    data = uow.Repository<Outlet>().GetData().Where(c =>
                                  c.User_Deletion_Id == null && c.IsActive == true )
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


        public Dictionary<string, object> GetPR_User_Id_List(long outletId, List<string> Device_Info)
        {

            string lang = Device_Info[2];


            var d = new List<PR_User>();


            var data = db.PR_User.Select(c => new User
            { //change display lang
                DisplayText = (lang == "1" ? c.FullName : c.FullName),
                Value = c.Id,
                FullName = (lang == "1" ? c.FullName : c.FullName),
                Outlet_ID = c.Outlet_ID,
                Id = c.Id,
                EmpId=c.EmpId
                ,
               Adress=c.Adress_Ar

            }).Where(a => a.Outlet_ID == outletId).OrderBy(a => a.FullName).ToList();



            data.Insert(0, new User() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
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
        
      (  x.c.EndDate < _StartDate && x.c.EndDate <= _End_Date)
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
                           && dc_d.DealerContact.Outlet_ID== outletId
                           &&dc_d.Dealer.Outlet_ID==outletId
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
                           (dc_d.DealerContact.EndDate >= _StartDate  && dc_d.DealerContact.StartDate <= _End_Date)

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
                              IsActive =dc_d.DealerContact.IsActive
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

    }



}
