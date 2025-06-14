using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Admin
{
    public class AddUserBLL
    {
        private UnitOfWork uow;
        private UnitOfWork uow2;
        dbPrivilageEntities db = new dbPrivilageEntities();

        public AddUserBLL()
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
                EmpId = c.EmpId
                ,
                Adress = c.Adress_Ar

            }).Where(a => a.Outlet_ID == outletId).OrderBy(a => a.FullName).ToList();



            data.Insert(0, new User() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public bool GetAny(string user_name, string email, short Id)
        {
            //|| p.Email == email
            var bbb = db.PR_User.FirstOrDefault(p => (p.LoginName == user_name) && (p.Id != Id) );
            if (bbb == null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }



        public int Save_PR_User(short Id, string user_name, string pass, string email,bool IS_Change_Password, List<string> Device_Info)
         {

            try
            {
                if (GetAny(user_name, email, Id))
                {
                    PR_User pu = db.PR_User.Find(Id);

                    pu.LoginName = user_name;
                    pu.Password = pass;                  
                    pu.IS_Mail_Send = false;
                    pu.IS_Change_Password = IS_Change_Password;
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return 0;
            }
        }

        public Dictionary<string, object> GetPR_User_Email_Id(short Id, List< string> Device_Info)
        {

            string lang = Device_Info[2];


            var d = new List<PR_User>();


            var data = db.PR_User.Where(a=> a.Id==Id).Select(c => new User
            {
                Email=c.Email

            }).FirstOrDefault();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }






    }
}
