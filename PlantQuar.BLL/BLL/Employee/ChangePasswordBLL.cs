using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Employee;
using PlantQuar.DTO.DTO.Log;
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
    public class ChangePasswordBLL
    {

        private UnitOfWork uow;

        public ChangePasswordBLL()
        {
            uow = new UnitOfWork(1);
        }


        public int IsUserlogin12(string LoginName, string Password, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities db = new dbPrivilageEntities();
                var _user = db.PR_User.SingleOrDefault(x => x.LoginName
                 == LoginName && x.Password == Password);
                //User_LoginDTO userDTO = new User_LoginDTO()
                //{
                //    FullName = _user.FullName,
                //    UserId = _user.Id,
                //    IS_Change_Password = _user.IS_Change_Password,

                //};
                if (_user != null)
                {
                    if (_user.IS_Change_Password == true)
                    {
                        //  changed
                        return 2;
                    }
                    else
                    {
                        // not changed
                        return 2;
                    }
                }
                else
                {
                    return 0;
                }
              
                //}
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }
        public Dictionary<string, object> Update(CustomUserLogin Dto, List<string> Device_Info)
        {
            dbPrivilageEntities db = new dbPrivilageEntities();

            //   Dto.LoginName = Dto.LoginName.Trim();
            ////   Dto.Password = Dto.Password.Trim();
            //   User user = new User();
            //   user.EmpId = Dto.Emp_ID;
            //   user.LoginName = Dto.LoginName;
            //   user.Password = Dto.Password;
            //   user.IS_Change_Password = true;

            try
            {


             //   var obj = user as User;
                var f = db.PR_User.FirstOrDefault(x => x.LoginName ==
                Dto.LoginName);
                f.Password = Dto.Password;
                f.IS_Change_Password = true;
                db.SaveChanges();
                //PR_User CModel = uow.Repository<PR_User>().
                //    Findobject(1);

                ////obj.User_Creation_Date = CModel.User_Creation_Date;
                ////obj.User_Creation_Id = CModel.User_Creation_Id;

                ////if (CModel.User_Updation_Id != null)
                ////{
                ////    obj.User_Updation_Date = CModel.User_Updation_Date;
                ////    obj.User_Updation_Id = CModel.User_Updation_Id;
               
                //var Co = Mapper.Map(obj, CModel);
                //uow.Repository<PR_User>().Update(Co);
                //uow.SaveChanges();
                //Ex_ContactDataBLL contactBLL = new Ex_ContactDataBLL();
                //int exporter_type = 6;
                //contactBLL.UpdateRecords(entity.User_Updation_Id.Value, entity.ID, entity.User_Updation_Date.Value, exporter_type, entity.Contacts, Device_Info);
              //  var empDTO = Mapper.Map<PR_User, User>(Co);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, Dto);

                //else
                //{
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                //}
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }


















        }

    }
}