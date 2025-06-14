using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
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
    public class Transation_LogsBLL
    {


        private UnitOfWork uow;
        private UnitOfWork uow2;
      
        PlantQuarantineEntities entities1 = new PlantQuarantineEntities();
        dbPrivilageEntities db = new dbPrivilageEntities();


        public Transation_LogsBLL()
        {
            uow = new UnitOfWork();

            uow2 = new UnitOfWork(1);
        }

        public Dictionary<string, object> Get_Employee_Data_List(int Operation_Type_ID, decimal Order_Permission_Number, List<string> Device_Info)
        {
            try
            {//SELECT ta.Name_Ar,u.FullName,l.User_Creation_Date
             //FROM Im_PermissionRequest PR
             //INNER JOIN Table_Action_Log l ON pr.ID = l.ID_TableActionValue
             //INNER JOIN dbo.Table_Action ta ON l.ID_Table_Action = ta.ID
             //INNER JOIN[dbPrivilage].dbo.PR_User u ON l.User_Creation_Id = u.Id
             //WHERE pr.ImPermission_Number = '16532022207102722'
                var lang = "1";
              //اذن استيراد
                if (Operation_Type_ID == 1)
                {
                    var Data_Employee = (from PR in entities1.Im_PermissionRequest
                                         join TAL in entities1.Table_Action_Log on PR.ID equals TAL.ID_TableActionValue
                                         join TA in entities1.Table_Action on TAL.ID_Table_Action equals TA.ID
                                         //join U in db.PR_User on TAL.User_Creation_Id equals U.Id
                                         join a_sc in entities1.A_SystemCode on TAL.User_Type_ID equals a_sc.Id into a_sc1
                                         from a_sc in a_sc1.DefaultIfEmpty()

                                         join cn in entities1.Company_National on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)6, b = (long?)cn.ID } into cn1
                                         from cn in cn1.DefaultIfEmpty()
                                         join po in entities1.Public_Organization on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)7, b = (long?)po.ID } into po1
                                         from po in po1.DefaultIfEmpty()
                                         join pr in entities1.People on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                                         from pr in pr1.DefaultIfEmpty()
                                         where PR.ImPermission_Number == Order_Permission_Number
                                         //&& TAL.User_Type_ID!=null
                                         select new Transation_LogsDTO
                                         {
                                             User_Id = TAL.User_Creation_Id,
                                             Action_Date = TAL.User_Creation_Date,
                                             Staus_Name = TA.Name_Ar,
                                             User_Type_ID = TAL.User_Type_ID,
                                             Notes = TAL.NOTS,
                                             User_Type_Name = lang == "1" ? a_sc.ValueName : a_sc.ValueName,
                                             User_Name = TAL.User_Type_ID == 6 ? (lang == "1" ? cn.Name_Ar : cn.Name_En)
                                                                                    : TAL.User_Type_ID == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                                                    : TAL.User_Type_ID == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                                                    : "127"

                                         }).OrderByDescending(a=>a.Action_Date).Distinct().ToList();

                    foreach (var item in Data_Employee)
                    {
                        if (item.User_Type_ID == 127)
                        {


                            var User = (from U in db.PR_User
                                        where U.Id == item.User_Id 
                                        select new Transation_LogsDTO
                                        {
                                            User_Name = U.FullName
                                        }).FirstOrDefault();
                            item.User_Name = User.User_Name;
                        }
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Data_Employee);
                }
                //وارد
                else if (Operation_Type_ID == 2)
                {
                    string _CheckRequest_Number = Order_Permission_Number.ToString();
                    var Data_Employee = (from PR in entities1.Im_CheckRequest
                                         join TAL in entities1.Table_Action_Log_CheckRequest on PR.ID equals TAL.ID_TableActionValue
                                         join TA in entities1.Table_Action on TAL.ID_Table_Action equals TA.ID
                                         //join U in db.PR_User on TAL.User_Creation_Id equals U.Id
                                         join a_sc in entities1.A_SystemCode on TAL.User_Type_ID equals a_sc.Id

                                         join cn in entities1.Company_National on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)6, b = (long?)cn.ID } into cn1
                                         from cn in cn1.DefaultIfEmpty()
                                         join po in entities1.Public_Organization on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)7, b = (long?)po.ID } into po1
                                         from po in po1.DefaultIfEmpty()
                                         join pr in entities1.People on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                                         from pr in pr1.DefaultIfEmpty()
                                         where PR.CheckRequest_Number == _CheckRequest_Number
                                         select new Transation_LogsDTO
                                         {
                                             User_Id_CheckRequest = TAL.User_Creation_Id,
                                             Action_Date = TAL.User_Creation_Date,
                                             Staus_Name = TA.Name_Ar,
                                             User_Type_ID = TAL.User_Type_ID,
                                             Notes = TAL.NOTS,
                                             User_Type_Name = lang == "1" ? a_sc.ValueName : a_sc.ValueName,
                                             User_Name = TAL.User_Type_ID == 6 ? (lang == "1" ? cn.Name_Ar : cn.Name_En)
                                                                                    : TAL.User_Type_ID == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                                                    : TAL.User_Type_ID == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                                                    : "127"
                                         }).OrderByDescending(a => a.Action_Date).Distinct().ToList();

                    foreach (var item in Data_Employee)
                    {
                        if (item.User_Name == "127")
                        {
                            var User = (from U in db.PR_User
                                        where U.Id == item.User_Id_CheckRequest
                                        select new Transation_LogsDTO
                                        {
                                            User_Name = U.FullName
                                        }).FirstOrDefault();
                            item.User_Name = User.User_Name;
                        }
                        else { item.User_Name = item.User_Name; }
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Data_Employee);
                }
                //محطات
                else if (Operation_Type_ID == 3)
                {
                    string _StationCode = Order_Permission_Number.ToString();
                    var Data_Employee = (from PR in entities1.Stations
                                         join TAL in entities1.Table_Action_Log_Station on PR.ID equals TAL.Station_ID
                                         join TA in entities1.Table_Action on TAL.ID_Table_Action equals TA.ID
                                         //join U in db.PR_User on TAL.User_Creation_Id equals U.Id
                                         join a_sc in entities1.A_SystemCode on TAL.User_Type_ID equals a_sc.Id

                                         join cn in entities1.Company_National on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)6, b = (long?)cn.ID } into cn1
                                         from cn in cn1.DefaultIfEmpty()
                                         join po in entities1.Public_Organization on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)7, b = (long?)po.ID } into po1
                                         from po in po1.DefaultIfEmpty()
                                         join pr in entities1.People on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                                         from pr in pr1.DefaultIfEmpty()
                                         where PR.StationCode == _StationCode
                                         select new Transation_LogsDTO
                                         {
                                             User_Id = TAL.User_Creation_Id,
                                             Action_Date = TAL.User_Creation_Date,
                                             Staus_Name = TA.Name_Ar,
                                             User_Type_ID = TAL.User_Type_ID,
                                             Notes = TAL.NOTS,
                                             User_Type_Name = lang == "1" ? a_sc.ValueName : a_sc.ValueName,
                                             User_Name = TAL.User_Type_ID == 6 ? (lang == "1" ? cn.Name_Ar : cn.Name_En)
                                                                                    : TAL.User_Type_ID == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                                                    : TAL.User_Type_ID == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                                                    : "127"

                                         }).OrderByDescending(a => a.Action_Date).Distinct().ToList();

                    foreach (var item in Data_Employee)
                    {
                        var User = (from U in db.PR_User
                                    where U.Id == item.User_Id
                                    select new Transation_LogsDTO
                                    {
                                        User_Name = U.FullName
                                    }).FirstOrDefault();
                        item.User_Name = User.User_Name;
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Data_Employee);
                }

                //طلبات فحص صادر
                else if (Operation_Type_ID == 4)
                {
                    string _CheckRequest_Number = Order_Permission_Number.ToString();
                    var Data_Employee = (from PR in entities1.Ex_CheckRequest
                                         join TAL in entities1.Table_Action_Log_EX on PR.ID equals TAL.Ex_CheckRequest_ID
                                         join TA in entities1.Table_Action on TAL.ID_Table_Action equals TA.ID
                                         //join U in db.PR_User on TAL.User_Creation_Id equals U.Id
                                         join a_sc in entities1.A_SystemCode on TAL.User_Type_ID equals a_sc.Id

                                         join cn in entities1.Company_National on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)6, b = (long?)cn.ID } into cn1
                                         from cn in cn1.DefaultIfEmpty()
                                         join po in entities1.Public_Organization on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)7, b = (long?)po.ID } into po1
                                         from po in po1.DefaultIfEmpty()
                                         join pr in entities1.People on new { a = (long?)TAL.User_Type_ID, b = (long?)TAL.User_Creation_Id } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                                         from pr in pr1.DefaultIfEmpty()
                                         where PR.CheckRequest_Number == _CheckRequest_Number
                                         select new Transation_LogsDTO
                                         {
                                             User_Id_CheckRequest = TAL.User_Creation_Id,
                                             Notes = TAL.NOTS,
                                             Action_Date = TAL.User_Creation_Date,
                                            Staus_Name = TA.Name_Ar,
                                             User_Type_ID = TAL.User_Type_ID,

                                             User_Type_Name = lang == "1" ? a_sc.ValueName : a_sc.ValueName,
                                             User_Name = TAL.User_Type_ID == 6 ? (lang == "1" ? cn.Name_Ar : cn.Name_En)
                                                                                    : TAL.User_Type_ID == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                                                    : TAL.User_Type_ID == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                                                    : "127"
                                         }).OrderByDescending(a => a.Action_Date).Distinct().ToList();

                    foreach (var item in Data_Employee)
                    {
                        if (item.User_Name == "127")
                        {
                            var User = (from U in db.PR_User
                                        where U.Id == item.User_Id_CheckRequest
                                        select new Transation_LogsDTO
                                        {
                                            User_Name = U.FullName
                                        }).FirstOrDefault();
                            item.User_Name = User.User_Name;
                        }
                        else { item.User_Name = item.User_Name; }
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Data_Employee);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "Not Found");
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
