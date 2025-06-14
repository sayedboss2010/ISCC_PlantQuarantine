using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Employee;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station_Pages
{
    public class AcceptedStation_Company_BLL
    {
        private UnitOfWork uow;
        private UnitOfWork uow2;
        dbPrivilageEntities db = new dbPrivilageEntities();
        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        public AcceptedStation_Company_BLL()
        {
            uow = new UnitOfWork();

            uow2 = new UnitOfWork(1);
        }

        public Dictionary<string, object> GetStation_Company_List(long Company_Id,int  Company_Type_Id ,List<string> Device_Info)
        {
            try
            {

                string lang = Device_Info[2];
                var data = new List<Station_Company_DTO>();



                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
               // paramters_Type.Add("Outlet_ID", SqlDbType.BigInt);
                paramters_Type.Add("Company_Id", SqlDbType.BigInt);
                paramters_Type.Add("Company_Type_Id", SqlDbType.Int);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
               // paramters_Data.Add("Outlet_ID", Outlet_ID.ToString());
                paramters_Data.Add("Company_Id", Company_Id.ToString());
                paramters_Data.Add("Company_Type_Id", Company_Type_Id.ToString());

                var Data_User = uow.Repository<Station_Company_DTO>().CallStored("Station_Company_List", paramters_Type,
                paramters_Data, Device_Info).ToList();


                
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Data_User);
            }


            catch (Exception ex)
            {

                throw;
            }
        }



        public Dictionary<string, object> Insert_Stations_Company(List<Station_Company_DTO> menus_Status_new, List<string> Device_Info)
        {

            PlantQuarantineEntities entities = new PlantQuarantineEntities();

            try
            {
                foreach (var item in menus_Status_new)
                {
                    StationCompany SC = entities.StationCompanies.Find(item.StationCompany_ID);
                    if (item.Status == 1)
                    {
                        SC.IsActive =true ;
                    }
                    else
                    {
                        SC.IsActive = false;
                    }
                    SC.User_Updation_Id = item.User_Updation_Id;
                    SC.User_Updation_Date = item.User_Updation_Date;
                    SC.Status = item.Status;
                    entities.SaveChanges();

                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, null);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        //public Dictionary<string, object> GetAllCompany(long Company, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];
        //        var data = new List<CustomOptionLongId>();


        //        data = (from cn in entities.Company_National
        //                join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
        //                //where g.Active == true
        //                select new CustomOptionLongId
        //                { //change display lang
        //                    DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
        //                    Value = cn.ID
        //                }).Distinct().OrderBy(a => a.DisplayText).ToList();

        //        data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        //    }

        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public Dictionary<string, object> GetAllOrgniztion(long Orgniztion, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];
        //        var data = new List<CustomOptionLongId>();


        //        data = (from cn in entities.Public_Organization
        //                join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
        //                //where g.Active == true
        //                select new CustomOptionLongId
        //                { //change display lang
        //                    DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
        //                    Value = cn.ID
        //                }).Distinct().OrderBy(a => a.DisplayText).ToList();

        //        data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        //    }

        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public Dictionary<string, object> GetAllPerson(long Person, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];
        //        var data = new List<CustomOptionLongId>();


        //        data = (from cn in entities.People
        //                join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
        //                //where g.Active == true
        //                select new CustomOptionLongId
        //                { //change display lang
        //                    DisplayText = (lang == "1" ? cn.Name : cn.Name_EN),
        //                    Value = cn.ID
        //                }).Distinct().OrderBy(a => a.DisplayText).ToList();

        //        data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        //    }

        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        public Dictionary<string, object> GetAllCompany(long Company, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from cn in entities.Company_National
                        join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
                        where sc.Company_Type_Id == 6
                        select new CustomOptionLongId
                        { //change display lang
                            DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
                            Value = cn.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }
        public Dictionary<string, object> GetAllOrgniztion(long Orgniztion, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from cn in entities.Public_Organization
                        join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
                        where sc.Company_Type_Id == 7
                        select new CustomOptionLongId
                        { //change displaylang
                            DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
                            Value = cn.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }

            catch (Exception)
            {

                throw;
            }
        }
        public Dictionary<string, object> GetAllPerson(long Person, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();


                data = (from cn in entities.People
                        join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
                        where sc.Company_Type_Id == 8
                        select new CustomOptionLongId
                        { //change display lang
                            DisplayText = (lang == "1" ? cn.Name : cn.Name_EN),
                            Value = cn.ID
                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

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



//using PlantQuar.DAL;
//using PlantQuar.DTO.DTO.Admin;
//using PlantQuar.DTO.DTO.Station_Pages;
//using PlantQuar.DTO.HelperClasses;
//using PlantQuar.UOW.UnitOfWork;
//using Privilages.DAL;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlantQuar.BLL.BLL.Station_Pages
//{
//    public class AcceptedStation_Company_BLL
//    {
//        private UnitOfWork uow;
//        private UnitOfWork uow2;
//        dbPrivilageEntities db = new dbPrivilageEntities();
//        PlantQuarantineEntities entities = new PlantQuarantineEntities();
//        dbPrivilageEntities priv = new dbPrivilageEntities();

//        public AcceptedStation_Company_BLL()
//        {
//            uow = new UnitOfWork();

//            uow2 = new UnitOfWork(1);
//        }

//        public Dictionary<string, object> GetStation_Company_List(long Company_Id, List<string> Device_Info)
//        {
//            try
//            {

//                string lang = Device_Info[2];
//                var data = new List<Station_Company_DTO>();
//                //1- معرفة منفذ المستخدم 
//                //2- اجيب DECLARE @IsExport int=0
//                //SET @IsExport = (SELECT o.IsExport FROM dbo.Outlet o where o.ID = @OutLit_ID) 

//                //if (@IsExport = 81 or @IsExport = 82)  // صادر
//                        //s.Accreditation_Type_ID = 80 AND @OutLit_ID = 156 // وارد
//                data = (from m in entities.StationCompanies
//                        join sa in entities.Station_Accreditation on m.Station_Accreditation_ID equals sa.ID
//                        join sad in entities.Station_Accreditation_Data on sa.Station_Accreditation_Data_ID equals sad.ID

//                        where m.Company_ID == Company_Id

//                        select new Station_Company_DTO
//                        { //change display lang
//                            Station_Name = m.Station_Accreditation.Station.Ar_Name,
//                            ID = m.ID,
//                            IsActive = m.IsActive,
//                            Station_Accreditation_Name = sad.Name_AR,
//                            Status = m.Status,
//                            User_Updation_Id = m.User_Updation_Id,
//                            Start_Date = m.Start_Date.ToString(),
//                            End_Date = m.End_Date.ToString(),                                             
//                        }).ToList();

//                foreach (var Item_Emp in data)
//                {
//                    if (Item_Emp.User_Updation_Id != null)
//                    {
//                        if (Item_Emp.User_Updation_Id > 0)
//                        {
//                            Item_Emp.Emp_Name = priv.PR_User.Where(c => c.Id == Item_Emp.User_Updation_Id).FirstOrDefault().FullName;
//                            Item_Emp.EmpId = priv.PR_User.Where(c => c.Id == Item_Emp.User_Updation_Id).FirstOrDefault().Id;
//                        }
//                    }
//                }
//                //data.FirstOrDefault().Emp_Name = data;


//                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
//            }

//            catch (Exception)
//            {

//                throw;
//            }
//        }



//        public Dictionary<string, object> Insert_Stations_Company(List<List_Status_New_DTO> menus_Status_new, List<string> Device_Info)
//        {

//            PlantQuarantineEntities entities = new PlantQuarantineEntities();

//            try
//            {
//                foreach (var item in menus_Status_new)
//                {
//                    StationCompany SC = entities.StationCompanies.Find(item.ID);

//                    SC.IsActive = true;
//                    SC.User_Updation_Id = item.User_Updation_Id;
//                    SC.User_Updation_Date = item.User_Updation_Date;
//                    SC.Status = item.Status;
//                    entities.SaveChanges();

//                }
//                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, null);
//            }
//            catch (Exception ex)
//            {
//                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
//                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
//            }
//        }


//        public Dictionary<string, object> GetAllCompany(long Company, List<string> Device_Info)
//        {
//            try
//            {
//                string lang = Device_Info[2];
//                var data = new List<CustomOptionLongId>();

//                //data = uow.Repository<Company_National>().GetData().Where(c =>
//                //              c.User_Deletion_Id == null && c.IsActive == true)
//                //                  .Select(c => new CustomOptionLongId
//                //                  { //change display lang
//                //                      DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
//                //                      Value = c.ID
//                //                  }).OrderBy(a => a.DisplayText).ToList();


//                //data = (from cn in entities.Company_National
//                //        join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
//                //        //where g.Active == true
//                //        select new Station_Company
//                //        { //change display lang
//                //            ID = sc.ID,
//                //            Station_Name = (lang == "1" ? sc.Company_National.Name_Ar : sc.Company_National.Name_Ar),
//                //            IsActive = sc.IsActive,
//                //        }).OrderBy(a => a.).ToList();

//                data = (from cn in entities.Company_National
//                        join sc in entities.StationCompanies on cn.ID equals sc.Company_ID
//                        //where g.Active == true
//                        select new CustomOptionLongId
//                        { //change display lang
//                            DisplayText = (lang == "1" ? cn.Name_Ar : cn.Name_En),
//                            Value = cn.ID
//                        }).Distinct().OrderBy(a => a.DisplayText).ToList();

//                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });



//                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
//            }

//            catch (Exception)
//            {

//                throw;
//            }
//        }
//    }
//}
