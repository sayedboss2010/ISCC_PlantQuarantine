using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.DTO.Employee;
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
    public class AddEmployeeBLL
    {
        private UnitOfWork uow;
        private UnitOfWork uow1;
        dbPrivilageEntities db = new dbPrivilageEntities();
        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        public AddEmployeeBLL()
        {
            uow = new UnitOfWork();
            uow1 = new UnitOfWork(1);

            // uow2 = new UnitOfWork(1);
        }

        public Dictionary<string, object> GetPR_User_Id_List(string FullName, int id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var d = new List<PR_User>();
                var data = db.PR_User.Select(c => new User
                { //change display lang
                    DisplayText = (lang == "1" ? c.FullName : c.FullName),
                    Value = c.Id,
                    FullName = (lang == "1" ? c.FullName : c.FullName),
                    Outlet_ID = c.Outlet_ID,
                    Id = c.Id
                    ,Governorate=c.Governorate,
                    Station=c.Station,
                    EmpId = c.EmpId,
                    Adress = (lang == "1" ? c.Adress_Ar : c.Adress_En)

                })
                .Where(a => a.FullName.StartsWith(FullName)||a.EmpId==id).OrderBy(a => a.FullName).ToList();


                // var data1 = uow.Repository<Outlet>().GetData().ToList();
                //  var data2 = uow.Repository<General_Admin>().GetData().ToList();
                var data3 = (
                   from outlet in entities.Outlets
                   join generalAdmin in entities.General_Admin
                   on outlet.GrAdmin_ID equals generalAdmin.ID
                   where outlet.User_Deletion_Id == null && outlet.IsActive == true
                   select new outletGeneralDTO
                   {
                       ID_HR = outlet.ID_HR,
                       outLetAr_Name = outlet.Ar_Name,
                       GrAdminAr_Name = generalAdmin.Ar_Name,
                       GrAdminEn_Name = "",
                       GrAdminAddress_Ar = "" ,                       
                       GrAdminAddress_En="",
                       GrAdmin_ID=1,
                       HR_SECTOR_NO=1,
                       ID_Orcael=1,
                       outLetAddress_Ar="",
                       outLetAddress_En="",
                       outLetEn_Name=""

                   }

                   ).ToList();

                var b = data.Join(data3, x => x.Outlet_ID, y => y.ID_HR,
                        (x, y) => new User
                        {
                            DisplayText = (lang == "1" ?
                            x.FullName : x.FullName),
                            Value = x.Id,
                            FullName = (lang == "1" ?
                            x.FullName : x.FullName),
                            Outlet_ID = x.Outlet_ID,
                            Id = (short)y.GrAdmin_ID,
                            EmpId = x.EmpId
                    ,
                            JobTitleName = y.outLetAr_Name
                            ,Governorate=x.Governorate
                            ,Station=x.Station,
                            Adress = x.Adress
                            ,
                            Email = y.GrAdminAr_Name
                        }).ToList();
                //  b.Insert(0, new User() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, b);
            }

            catch (Exception ex)
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.ErrorHappened, null);
            }
        }

        public Dictionary<string, object> GetEmployee_byOutlet(string FullName, long EmplyeeNo, long OutLet_ID,int Type_ID_HR, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var d = new List<PR_User>();
                long hr_id = 0;
                if (Type_ID_HR == 0)// بدلالة رقم المنفذ
                {
                     hr_id = (long)entities.Outlets.Where(a => a.ID == OutLet_ID).Select(a => a.ID_HR).FirstOrDefault();
                }
                else // بدلالة رقم المنفذ من الموارد البشرية
                {
                     hr_id = (long)entities.Outlets.Where(a => a.ID_HR == OutLet_ID).Select(a => a.ID_HR).FirstOrDefault();
                }
                
              
                var datalist = db.PR_User.Select(c => new User
                { //change display lang
                    DisplayText = (lang == "1" ? c.FullName : c.FullName),
                    Value = c.Id,
                    FullName = (lang == "1" ? c.FullName : c.FullName),
                    Outlet_ID = c.Outlet_ID,
                    Id = c.Id,
                    Governorate = c.Governorate,
                    Station = c.Station,
                    EmpId = c.EmpId,
                    Active= c.Active,
                    Adress = (lang == "1" ? c.Adress_Ar : c.Adress_En)
                })
                    .Where(a => a.Outlet_ID == hr_id &&a.Active == true
                    //&& a.Password !=null
                    //&& a.LoginName !=""
                    
                    
                    ).OrderBy(a => a.FullName).ToList();

                var data= datalist;

                if (FullName != null)
                {
                    data = datalist.Where(a => a.FullName.StartsWith(FullName)).ToList();

                }
                if ( EmplyeeNo!=0)
                {
                    data = datalist.Where(a => a.EmpId == EmplyeeNo && a.EmpId != 0).ToList();
                }
                
                //.Where(a => a.FullName.StartsWith(FullName) ||(a.EmpId == EmplyeeNo&& a.EmpId!=0)||a.Outlet_ID == hr_id).OrderBy(a => a.FullName).ToList();
                //  .Where(a => a.Outlet_ID== OutLet_ID ).OrderBy(a => a.FullName).ToList();


                // var data1 = uow.Repository<Outlet>().GetData().ToList();
                //  var data2 = uow.Repository<General_Admin>().GetData().ToList();
                var data3 = (
                   from outlet in entities.Outlets
                   join generalAdmin in entities.General_Admin
                   on outlet.GrAdmin_ID equals generalAdmin.ID
                   where outlet.User_Deletion_Id == null && outlet.IsActive == true

                   select new outletGeneralDTO
                   {
                       ID_HR = outlet.ID_HR,
                       outLetAr_Name = outlet.Ar_Name,
                       GrAdminAr_Name = generalAdmin.Ar_Name,
                       GrAdminEn_Name = "",
                       GrAdminAddress_Ar = "" ,
                       GrAdminAddress_En = "",
                       GrAdmin_ID = 1,
                       HR_SECTOR_NO = 1,
                       ID_Orcael = 1,
                       outLetAddress_Ar = "",
                       outLetAddress_En = "",
                       outLetEn_Name = ""
                   }

                   ).ToList();

                var b = data.Join(data3, x => x.Outlet_ID, y => y.ID_HR,
                        (x, y) => new User
                        {
                            DisplayText = (lang == "1" ?
                            x.FullName : x.FullName),
                            Value = x.Id,
                            FullName = (lang == "1" ?
                            x.FullName : x.FullName),
                            Outlet_ID = x.Outlet_ID,
                            Id = (short)y.GrAdmin_ID,
                            EmpId = x.EmpId,
                            JobTitleName = y.outLetAr_Name,
                            Governorate = x.Governorate,
                            Station = x.Station,
                            Adress = x.Adress,
                            Email = y.GrAdminAr_Name
                        }).ToList();
               // b.Insert(0, new User() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,b);
            }

            catch (Exception ex)
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.ErrorHappened, ex);
            }
        }

        public Dictionary<string, object> GetEmployee_by_Station_ID_List(string FullName, long EmplyeeNo, long OutLet_ID,  long user_Station_ID, List<string> Device_Info)
        {
            try

            {
                
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("Station_User_ID", SqlDbType.BigInt);
                paramters_Type.Add("outLet__User_ID", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("Station_User_ID", user_Station_ID.ToString());
                paramters_Data.Add("outLet__User_ID", OutLet_ID.ToString());


                var data = uow.Repository<User>().CallStored("Ex_GetUserStations", paramters_Type,
                paramters_Data, Device_Info).ToList();
                //data = data.Max();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetEmployee_by_User_List(string FullName, long EmplyeeNo
            , long OutLet_ID, long OutLet_HR_ID, List<string> Device_Info)
        {
           
            try

            {

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("OutLet_ID", SqlDbType.NVarChar);
                paramters_Type.Add("OutLet_HR_ID", SqlDbType.NVarChar);
                paramters_Type.Add("FullName", SqlDbType.NVarChar);
                paramters_Type.Add("EmplyeeNo", SqlDbType.NVarChar);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("OutLet_ID", (OutLet_ID.ToString() != "0" ? OutLet_ID.ToString() : ""));
                paramters_Data.Add("OutLet_HR_ID", (OutLet_HR_ID.ToString() != "0" ? OutLet_HR_ID.ToString() : ""));
                paramters_Data.Add("FullName", (FullName != null ? FullName : "") );
                paramters_Data.Add("EmplyeeNo", (EmplyeeNo.ToString() != "0" ? EmplyeeNo.ToString() : ""));


                var data = uow.Repository<Ex_GetUserDTO>().CallStored("Ex_GetUser", paramters_Type,
                paramters_Data, Device_Info).ToList();
                //data = data.Max();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
