using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.Station_Pages;
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

namespace PlantQuar.BLL.BLL.Station_Pages
{
    public class Station_Final_Result_BLL
    {

        private UnitOfWork uow;
        private UnitOfWork uow2;
        public Station_Final_Result_BLL()
        {
            uow = new UnitOfWork();
            uow2 = new UnitOfWork(1);
        }
        public Dictionary<string, object> GetStationData(long stationId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("Station_ID", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("Station_ID", stationId.ToString());

                var data = uow.Repository<Station_Final_Result_DTO>().CallStored("Station_Final_Result_St", paramters_Type,
                paramters_Data, Device_Info).ToList();

                #region Station_Accreditation_Data_ALL  مسمي الاعتماد
                var Station_Accreditation_Data_ALL = data.GroupBy(d => new
                {
                    Station_Accreditation_Data_Name = d.Station_Accreditation_Data_Name,
                    Station_Accreditation_Data_ID = d.Station_Accreditation_Data_ID,
                }).Select(a => new Station_Accreditation_Data_ALL_DTO
                {
                    Station_Accreditation_Data_Name = a.Key.Station_Accreditation_Data_Name,
                    Station_Accreditation_Data_ID = a.Key.Station_Accreditation_Data_ID,
                }).ToList();

                data.FirstOrDefault().List_Station_Accreditation_Data_ALL = Station_Accreditation_Data_ALL;
                #endregion

                #region Station_Accreditation_Request_Type انواع مسمي الاعتماد اول مره تجديد

                var Station_Accreditation_Request_Type = data
                    // .Where(a => a.Station_Accreditation_Data_ID == item_Data.Station_Accreditation_Data_ID)
                    .GroupBy(d =>
                     new
                     {
                         Station_Accreditation_Request_Type_ID=d.Station_Accreditation_Request_Type_ID,
                         Station_Accreditation_Request_ID = d.Station_Accreditation_Request_ID,
                         Station_Accreditation_Request_Type_Name = d.Station_Accreditation_Request_Type_Name,
                         Station_Accreditation_Data_ID = d.Station_Accreditation_Data_ID,
                     }).Select(a => new Station_Accreditation_Request_TypeDTO
                     {
                         Station_Accreditation_Request_Type_ID = a.Key.Station_Accreditation_Request_Type_ID,
                         Station_Accreditation_Request_ID = a.Key.Station_Accreditation_Request_ID,
                         Station_Accreditation_Request_Type_Name = a.Key.Station_Accreditation_Request_Type_Name,
                         Station_Accreditation_Data_ID = a.Key.Station_Accreditation_Data_ID,
                     }).OrderBy(a => a.Station_Accreditation_Request_ID).ToList();

                data.FirstOrDefault().List_Station_Accreditation_Request_Type = Station_Accreditation_Request_Type;
                #endregion

                #region Station_Check_Quarantine لجان حالات مسمي الاعتماد
                var Station_Check_Quarantine = data.GroupBy(d => new
                {
                    Station_Accreditation_Data_Name = d.Station_Accreditation_Data_Name,
                    Station_Accreditation_Data_ID = d.Station_Accreditation_Data_ID,
                    Station_Accreditation_Committee_ID = d.Station_Accreditation_Committee,
                    IsAccepted_final_Admin = d.IsAccepted_final_Admin,
                    Station_ID = d.Station_ID,
                    Station_Accreditation_Request_ID = d.Station_Accreditation_Request_ID,
                    Station_Accreditation_Request_Type_Name = d.Station_Accreditation_Request_Type_Name,
                    IsAccepted_Quarantine = d.IsAccepted_Quarantine,
                    Notes_Quarantine = d.Notes_Quarantine,
                    StartDate_Quarantine = d.StartDate_Quarantine,
                    EndDate_Quarantine = d.EndDate_Quarantine,
                    Station_Refuse_Reason_Nots = d.Station_Refuse_Reason_Nots,
                    Committee_position = d.Committee_position,
                }).Select(a => new Station_Check_Quarantine_DTO
                {
                    Station_Accreditation_Data_Name = a.Key.Station_Accreditation_Data_Name,
                    Station_Accreditation_Data_ID = a.Key.Station_Accreditation_Data_ID,
                    Station_Accreditation_Committee_ID = a.Key.Station_Accreditation_Committee_ID,
                    IsAccepted_final_Admin = a.Key.IsAccepted_final_Admin,

                    Station_ID = a.Key.Station_ID,
                    Station_Accreditation_Request_ID = a.Key.Station_Accreditation_Request_ID,
                    Station_Accreditation_Request_Type_Name = a.Key.Station_Accreditation_Request_Type_Name,
                    IsAccepted_Quarantine = a.Key.IsAccepted_Quarantine,
                    Notes_Quarantine = a.Key.Notes_Quarantine,
                    StartDate_Quarantine = a.Key.StartDate_Quarantine,
                    EndDate_Quarantine = a.Key.EndDate_Quarantine,
                    Station_Refuse_Reason_Nots = a.Key.Station_Refuse_Reason_Nots,
                    Committee_position = a.Key.Committee_position
                }).ToList();
                data.FirstOrDefault().List_Station_Check_Quarantine = Station_Check_Quarantine;
                #endregion

                #region Station_Check_Admin
                var Station_Check_Admin = data.Where(a => a.ISAdmin == true)
                                                                    .GroupBy(d =>
                                                                    new
                                                                    {
                                                                        Station_Accreditation_Request_ID = d.Station_Accreditation_Request_ID,
                                                                        Station_Accreditation_Request_Type_Name = d.Station_Accreditation_Request_Type_Name,
                                                                        Station_Accreditation_Committee_ID = d.Station_Accreditation_Committee,
                                                                        Station_Committee_Delegation_Date = d.Station_Committee_Delegation_Date,

                                                                        Station_Committee_StartTime = d.Station_Committee_StartTime,
                                                                        Station_Committee_EndTime = d.Station_Committee_EndTime,
                                                                        Station_CheckList_ID = d.Station_CheckList_ID,
                                                                        Station_CheckList_name = d.Station_CheckList_name,
                                                                        IsAccepted_Band_Admin = d.IsAccepted_Band_Admin,
                                                                        Notes_Ar_Band_Admin = d.Notes_Ar_Band_Admin,
                                                                        FullName = d.FullName,
                                                                        IsAccepted_final_Admin = d.IsAccepted_final_Admin,
                                                                        Notes_Ar_final_Admin = d.Notes_Ar_final_Admin,
                                                                        Station_Refuse_Reason_Nots = d.Station_Refuse_Reason_Nots
                                                                    }).Select(a => new Station_Check_AdminDTO
                                                                    {

                                                                        Station_Accreditation_Request_Type_Name = a.Key.Station_Accreditation_Request_Type_Name,
                                                                        Station_Accreditation_Committee_ID = a.Key.Station_Accreditation_Committee_ID,
                                                                        Station_Committee_Delegation_Date = a.Key.Station_Committee_Delegation_Date,
                                                                        Station_Committee_EndTime = a.Key.Station_Committee_EndTime,
                                                                        Station_Committee_StartTime = a.Key.Station_Committee_StartTime,
                                                                        Station_CheckList_ID = a.Key.Station_CheckList_ID,
                                                                        Station_CheckList_name = a.Key.Station_CheckList_name,
                                                                        IsAccepted_Band_Admin = a.Key.IsAccepted_Band_Admin,
                                                                        Notes_Ar_Band_Admin = a.Key.Notes_Ar_Band_Admin,
                                                                        FullName = a.Key.FullName,
                                                                        IsAccepted_final_Admin = a.Key.IsAccepted_final_Admin,
                                                                        Notes_Ar_final_Admin = a.Key.Notes_Ar_final_Admin,
                                                                        Station_Refuse_Reason_Nots = a.Key.Station_Refuse_Reason_Nots
                                                                    }).ToList();
                data.FirstOrDefault().List_Station_Check_Admin = Station_Check_Admin;

                #endregion

                #region Station_Confirm

                var Station_Confirm = data.Where(a => a.ISAdmin == false)
                                            .GroupBy(d => new
                                            {
                                                Station_Accreditation_Request_Type_Name = d.Station_Accreditation_Request_Type_Name,
                                                Station_Accreditation_Committee_ID = d.Station_Accreditation_Committee,
                                                IsAccepted_Confirm = d.IsAccepted_Confirm,
                                                Notes_Confirm = d.Notes_Confirm,
                                                FullName = d.FullName
                                            }).Select(a => new Station_ConfirmDTO
                                            {
                                                Station_Accreditation_Request_Type_Name = a.Key.Station_Accreditation_Request_Type_Name,
                                                Station_Accreditation_Committee_ID = a.Key.Station_Accreditation_Committee_ID,
                                                IsAccepted_Confirm = a.Key.IsAccepted_Confirm,
                                                Notes_Confirm = a.Key.Notes_Confirm,
                                                FullName = a.Key.FullName,
                                            }).ToList();
                data.FirstOrDefault().List_Station_Confirm = Station_Confirm;

                #endregion

                #region Station_Check_Imge

                //--,saci.AttachmentPath_Binary Station_Committee_Imge_Binary, saci.ID Station_Committee_Imge_ID
                //                    left join dbo.Station_Accreditation_Committee_Imge saci ON sac.ID = saci.Station_Accreditation_Committee_id
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var Station_Check_Imge = (from im in entities.Station_Accreditation_Committee_Imge
                                          where im.Station_Accreditation_Committee.Station_Accreditation_Request.Station_ID == stationId
                                          select new Station_Check_ImgeDTO
                                          {
                                              Station_Accreditation_Request_ID = im.Station_Accreditation_Committee.Station_Accreditation_Request_ID,
                                              Station_Committee_Imge_ID = im.ID,
                                              Station_Committee_Imge_Binary = im.AttachmentPath_Binary,
                                              Infection_Comment = im.Infection_Comment,
                                              Station_Accreditation_Committee_ID = im.Station_Accreditation_Committee_id,


                                          }).Take(5).ToList();

                data.FirstOrDefault().List_Station_Check_Imge = Station_Check_Imge;

                #endregion

                #region List_Station_Fees       
                Dictionary<string, SqlDbType> paramters_Type_Fees = new Dictionary<string, SqlDbType>();
                paramters_Type_Fees.Add("StationId", SqlDbType.BigInt);
                Dictionary<string, string> paramters_Data_Fees = new Dictionary<string, string>();
                paramters_Data_Fees.Add("StationId", stationId.ToString());
                var requests = uow.Repository<Station_Fees_DTO>().CallStored("Station_Get_Fees", paramters_Type,
                paramters_Data, Device_Info).ToList();
                data.FirstOrDefault().List_Station_Fees = requests;
                #endregion



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetStationDataNew(long stationIdNew, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                #region new Code
                //المحطة
                var Stations_Data = (from s in entities.Stations
                                     join g in entities.Governates on s.Gov_Id equals g.ID
                                     join c in entities.Centers on s.Center_Id equals c.ID into c1
                                     from c in c1.DefaultIfEmpty()
                                     join v in entities.Villages on s.Village_Id equals v.ID into v1
                                     from v in v1.DefaultIfEmpty()
                                     where s.ID == stationIdNew
                                     select new Station_Final_Result_New_DTO
                                     {
                                         Station_ID = s.ID,
                                         Station_Name = s.Ar_Name,
                                         station_Address = s.Address_Ar,
                                         StationCode = s.StationCode,
                                         Governate_Name = g.Ar_Name,
                                         Center_Name = c.Ar_Name,
                                         Village_name = v.Ar_Name,
                                     }).FirstOrDefault();
                // نوع اللاعتماد
                var Station_Accreditation_Data = (from s in entities.Stations
                                                  join sar in entities.Station_Accreditation_Request on s.ID equals sar.Station_ID
                                                  join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                  where s.ID == stationIdNew
                                                  select new Station_Accreditation_Data_NewDTO
                                                  {
                                                      Station_ID = s.ID,
                                                      Station_Accreditation_Data_ID = sar.Station_Accreditation_Data_ID,
                                                      Station_Accreditation_Data_Name = sad.Name_AR,
                                                  }).Distinct().ToList();
                Stations_Data.List_Station_Accreditation_Data = Station_Accreditation_Data;


        
                foreach (var item_Station_Data in Station_Accreditation_Data)
                {
                    //نوع اللجنة اول مره تقييم تجديد
                    var Station_Accreditation_Request = (from sar in entities.Station_Accreditation_Request
                                                         //join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                         //join sart in entities.Station_Accreditation_Request_Type on sar.Station_Accreditation_Request_Type_ID equals sart.ID
                                                         where sar.Station_Accreditation_Data_ID == item_Station_Data.Station_Accreditation_Data_ID
                                                         && sar.Station_ID == item_Station_Data.Station_ID
                                                         select new Request_Type_DTO
                                                         {
                                                             Station_Accreditation_Request_Type_ID = sar.Station_Accreditation_Request_Type.ID,
                                                             Station_Accreditation_Data_ID = sar.Station_Accreditation_Data_ID,                                                             
                                                             Station_Accreditation_Request_Type_Name = sar.Station_Accreditation_Request_Type.Name_AR,
                                                         }).Distinct().ToList();

                    item_Station_Data.List_Station_Accreditation_Request_Type = Station_Accreditation_Request;

                    #region موقف الحجر
                    var Station_Accreditation_Request_Hager = (from sar in entities.Station_Accreditation_Request
                                                               join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                               join sart in entities.Station_Accreditation_Request_Type on sar.Station_Accreditation_Request_Type_ID equals sart.ID
                                                               where sar.Station_Accreditation_Data_ID == item_Station_Data.Station_Accreditation_Data_ID
                                                               && sar.Station_ID == item_Station_Data.Station_ID
                                                               select new Station_Accreditation_Request_Hagre_DTO
                                                               {
                                                                   Station_Accreditation_Request_Type_ID = sart.ID,
                                                                   Station_Accreditation_Request_ID = sar.ID,
                                                                   Station_Accreditation_Request_Type_Name = sart.Name_AR,

                                                                   IsAccepted_Quarantine = sar.IsAccepted,
                                                                   Notes_Quarantine = sar.Notes_Quarantine,
                                                                   StartDate_Quarantine = sar.StartDate,
                                                                   EndDate_Quarantine = sar.EndDate,
                                                                   User_Creation_Id = sar.User_Updation_Id

                                                               }).ToList();

                    foreach (var fRes in Station_Accreditation_Request_Hager)
                    {
                        if (fRes.User_Creation_Id != null)
                        {
                            try
                            {
                                fRes.Full_Name_Quarantine = priv.PR_User.Where(c => c.Id == fRes.User_Creation_Id).FirstOrDefault().FullName;
                            }
                            catch
                            {
                            }
                        }
                    }

                    item_Station_Data.List_Station_Accreditation_Request_Hagre = Station_Accreditation_Request_Hager;
                    #endregion


                    var max_Requst = Station_Accreditation_Request_Hager.Select(a => a.Station_Accreditation_Request_ID).Max();

                    if (Station_Accreditation_Request_Hager.Where(a => a.Station_Accreditation_Request_ID == max_Requst).FirstOrDefault().IsAccepted_Quarantine != null)
                        max_Requst = -1;
                    foreach (var item_Comm in Station_Accreditation_Request)
                    {                        
                        // اللجان
                        DateTime Date_Now = DateTime.Now;
                        try
                        {
                            string dd = DateTime.Now.Date.ToString("dd/MM/yyyy");
                            Date_Now = Convert.ToDateTime(dd);
                        }
                        catch (Exception)
                        {
                            string dd = DateTime.Now.Date.ToString("MM/dd/yyyy");
                            Date_Now = Convert.ToDateTime(dd);
                        }
                        var _Request_Committee = (from sac in entities.Station_Accreditation_Committee
                                                  where sac.Station_Accreditation_Request.Station_Accreditation_Request_Type_ID == item_Comm.Station_Accreditation_Request_Type_ID
                                                  && sac.Station_Accreditation_Request.Station_ID == stationIdNew
                                                  && sac.Station_Accreditation_Request.Station_Accreditation_Data_ID == item_Station_Data.Station_Accreditation_Data_ID
                                                  //where sac.Station_Accreditation_Request_ID == item_Comm.Station_Accreditation_Request_ID
                                                  select new Station_Request_Committee_DTO
                                                  {
                                                      Station_Accreditation_Request_ID = sac.Station_Accreditation_Request_ID,
                                                      Station_Accreditation_Request_Type_Name = sac.Station_Accreditation_Request.Station_Accreditation_Request_Type.Name_AR,
                                                      Station_Committee_ID = sac.ID,
                                                      Station_Committee_Delegation_Date = sac.Delegation_Date,
                                                      Station_Committee_StartTime = sac.StartTime,
                                                      Station_Committee_EndTime = sac.EndTime,
                                                      Station_Refuse_Reason_Nots = sac.Notes_Refuse_Ar,
                                                      Committee_position = sac.IsApproved == false ? "تم رفض اللجنة من قبل العميل"
                                                      : sac.Status == false
                                                      && sac.Delegation_Date < Date_Now ? "انتهاء الوقت"
                                                      : sac.Is_Cancel == true ? "تعذر الفحص"
                                                      : sac.Status == true ? "تم انتهاء عمل اللجنة"
                                                      : "يتم العمل على اللجنة",
                                                      Is_final_Status = sac.Status == true && sac.Is_Cancel == null ? true : false,
                                                  }).ToList();
                        item_Comm.List_Station__Request_Committee = _Request_Committee;
                        if (_Request_Committee.Count() > 0)
                        {
                            if (max_Requst > 0)
                            {
                                if (max_Requst == _Request_Committee.FirstOrDefault().Station_Accreditation_Request_ID)
                                {
                                    var Station_Committee_Max = _Request_Committee.Where(a => a.Station_Accreditation_Request_ID == max_Requst).Select(a => a.Station_Committee_ID).Max();

                                    var _Is_final_Status = _Request_Committee.Where(a => a.Station_Committee_ID == Station_Committee_Max).FirstOrDefault();
                                    item_Station_Data.Is_final_Status = _Is_final_Status.Is_final_Status;
                                    item_Station_Data.Station_Accreditation_Request_ID = _Is_final_Status.Station_Accreditation_Request_ID;
                                    item_Station_Data.Station_Accreditation_Request_Type_Name = _Is_final_Status.Station_Accreditation_Request_Type_Name;
                                }
                            }
                            else
                            {
                                if (max_Requst == -1)
                                {
                                    item_Station_Data.Station_Accreditation_Request_ID = -1;
                                }
                                item_Station_Data.Is_final_Status = false;
                            }
                        }
                        else
                        {
                            item_Station_Data.Is_final_Status = false;
                        }
                        foreach (var item_Comm_Data in _Request_Committee)
                        {
                            #region Admin
                            var Station_Request_Committee_Admin = (from saccl in entities.Station_Accreditation_Committee_CheckList
                                                                   join sacl in entities.Station_Accreditation_CheckList on saccl.Station_Accreditation_CheckList_ID equals sacl.ID
                                                                   join scl in entities.Station_CheckList on sacl.Station_CheckList_ID equals scl.ID
                                                                   join sacfr_Admin in entities.Station_Accreditation_Committee_Final_Result on new { a = (long)saccl.Committee_ID, b = (bool)true } equals new { a = sacfr_Admin.Station_Accreditation_Committee_ID, b = sacfr_Admin.ISAdmin }
                                                                   into sacfr_Admin1
                                                                   from sacfr_Admin in sacfr_Admin1.DefaultIfEmpty()
                                                                   join Comm_Emp in entities.CommitteeEmployees on saccl.Committee_ID equals Comm_Emp.Committee_ID

                                                                   where saccl.Committee_ID == item_Comm_Data.Station_Committee_ID
                                                                   && Comm_Emp.ISAdmin == true
                                                                   && Comm_Emp.OperationType == 79
                                                                   select new Station_Request_Committee_Admin_DTO
                                                                   {
                                                                       Station_CheckList_ID = saccl.ID,
                                                                       IsAccepted_Band_Admin = saccl.IsAccepted,
                                                                       Notes_Ar_Band_Admin = saccl.Notes_Ar,
                                                                       Notes_En_Band_Admin = saccl.Notes_En,
                                                                       EmployeeId = Comm_Emp.Employee_Id,
                                                                       //FullName = priv.PR_User.Where(c => c.Id == Comm_Emp.Employee_Id).FirstOrDefault().FullName,// uow2. sac.Notes_Refuse_Ar,
                                                                       Station_CheckList_name = scl.ConstrainText_Ar,
                                                                       IsAccepted_final_Admin = sacfr_Admin.IsAccepted,
                                                                       Notes_Ar_final_Admin = sacfr_Admin.Notes_final,
                                                                   }).ToList();

                            foreach (var fRes in Station_Request_Committee_Admin)
                            {
                                if (fRes.EmployeeId != null)
                                {
                                    try
                                    {
                                        fRes.FullName = priv.PR_User.Where(c => c.Id == fRes.EmployeeId).FirstOrDefault().FullName;

                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            item_Comm_Data.List_Station_Request_Committee_Admin = Station_Request_Committee_Admin;
                            #endregion

                            #region Confirm

                            var Station_Request_Committee_confirm = (from sacfr_Confirm in entities.Station_Accreditation_Committee_Final_Result
                                                                     join Comm_Emp in entities.CommitteeEmployees on sacfr_Confirm.Station_Accreditation_Committee_ID equals Comm_Emp.Committee_ID
                                                                     where sacfr_Confirm.Station_Accreditation_Committee_ID == item_Comm_Data.Station_Committee_ID
                                                                     && Comm_Emp.ISAdmin == false
                                                                     && Comm_Emp.OperationType == 79
                                                                     && sacfr_Confirm.ISAdmin == false
                                                                     select new Station_Request_Committee_Confirm_DTO
                                                                     {
                                                                         IsAccepted_Confirm = sacfr_Confirm.IsAccepted,
                                                                         Notes_Confirm = sacfr_Confirm.Notes_final,
                                                                         Date_Confirm = sacfr_Confirm.User_Creation_Date,
                                                                         Employee_Id_Confirm = Comm_Emp.Employee_Id,
                                                                     }).ToList();
                            if (Station_Request_Committee_confirm.Count() > 0)
                            {
                                foreach (var fRes in Station_Request_Committee_confirm)
                                {
                                    if (fRes.Employee_Id_Confirm != null)
                                    {
                                        try
                                        {
                                            fRes.FullName_Confirm = priv.PR_User.Where(c => c.Id == fRes.Employee_Id_Confirm).FirstOrDefault().FullName;

                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                item_Comm_Data.List_Station_Request_Committee_Confirm = Station_Request_Committee_confirm;

                            }
                            else
                            {
                                // لو المساعد موجود ومشتغلش
                                var Station_Request_Committee_confirmEmpty = (from Comm_Emp in entities.CommitteeEmployees
                                                                              where Comm_Emp.Committee_ID == item_Comm_Data.Station_Committee_ID
                                                                         && Comm_Emp.ISAdmin == false
                                                                         && Comm_Emp.OperationType == 79
                                                                       
                                                                         select new Station_Request_Committee_Confirm_DTO
                                                                         {                                                                             
                                                                             Notes_Confirm = "لا توجد نتائج",                                                                       
                                                                             Employee_Id_Confirm = Comm_Emp.Employee_Id,
                                                                         }).ToList();
                                foreach (var fRes in Station_Request_Committee_confirmEmpty)
                                {
                                    if (fRes.Employee_Id_Confirm != null)
                                    {
                                        try
                                        {
                                            fRes.FullName_Confirm = priv.PR_User.Where(c => c.Id == fRes.Employee_Id_Confirm).FirstOrDefault().FullName;

                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                item_Comm_Data.List_Station_Request_Committee_Confirm = Station_Request_Committee_confirmEmpty;

                            }
                            #endregion

                            #region Station_Check_Imge
                            var Station_Check_Imge = (from im in entities.Station_Accreditation_Committee_Imge
                                                      where im.Station_Accreditation_Committee_id == item_Comm_Data.Station_Committee_ID
                                                      select new Station_Request_Committee_Imge_DTO
                                                      {
                                                          Station_Committee_Imge_ID = im.ID,
                                                          Station_Committee_Imge_Binary = im.AttachmentPath_Binary,
                                                          Infection_Comment = im.Infection_Comment,
                                                          Station_Accreditation_Committee_ID = im.Station_Accreditation_Committee_id,
                                                      }).Take(5).ToList();

                            item_Comm_Data.List_Station_Request_Committee_Imge = Station_Check_Imge;

                            #endregion
                        }

                    }
                       



                    //    var max_Requst = Station_Accreditation_Request.Select(a => a.Station_Accreditation_Request_ID).Max();

                    //if (Station_Accreditation_Request.Where(a => a.Station_Accreditation_Request_ID == max_Requst).FirstOrDefault().IsAccepted_Quarantine != null)
                    //    max_Requst = -1;
                    // اللجان لك نوع
                    //foreach (var item_Comm in Station_Accreditation_Request)
                    //{
                    //    try
                    //    {
                    //        DateTime Date_Now = DateTime.Now;
                    //        try
                    //        {
                    //            string dd = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    //            Date_Now = Convert.ToDateTime(dd);
                    //        }
                    //        catch (Exception)
                    //        {
                    //            string dd = DateTime.Now.Date.ToString("MM/dd/yyyy");
                    //            Date_Now = Convert.ToDateTime(dd);
                    //        }
                    //        var _Request_Committee = (from sac in entities.Station_Accreditation_Committee
                    //                                  where sac.Station_Accreditation_Request.Station_Accreditation_Request_Type_ID == item_Comm.Station_Accreditation_Request_Type_ID
                    //                                  &&sac.Station_Accreditation_Request.Station_ID == stationIdNew
                    //                                  //where sac.Station_Accreditation_Request_ID == item_Comm.Station_Accreditation_Request_ID
                    //                                  select new Station_Request_Committee_DTO
                    //                                  {
                    //                                      Station_Accreditation_Request_ID = sac.Station_Accreditation_Request_ID,
                    //                                      Station_Accreditation_Request_Type_Name = sac.Station_Accreditation_Request.Station_Accreditation_Request_Type.Name_AR,
                    //                                      Station_Committee_ID = sac.ID,
                    //                                      Station_Committee_Delegation_Date = sac.Delegation_Date,
                    //                                      Station_Committee_StartTime = sac.StartTime,
                    //                                      Station_Committee_EndTime = sac.EndTime,
                    //                                      Station_Refuse_Reason_Nots = sac.Notes_Refuse_Ar,
                    //                                      Committee_position = sac.IsApproved == false ? "تم رفض اللجنة من قبل العميل"
                    //                                      : sac.Status == false
                    //                                      && sac.Delegation_Date < Date_Now ? "انتهاء الوقت"
                    //                                      : sac.Is_Cancel == true ? "تعذر الفحص"
                    //                                      : sac.Status == true ? "تم انتهاء عمل اللجنة"
                    //                                      : "يتم العمل على اللجنة",
                    //                                      Is_final_Status = sac.Status == true && sac.Is_Cancel == null ? true : false,
                    //                                  }).ToList();
                    //        item_Comm.List_Station__Request_Committee = _Request_Committee;
                    //        if (_Request_Committee.Count() > 0)
                    //        {
                    //            if (max_Requst > 0)
                    //            {
                    //                if (max_Requst == _Request_Committee.FirstOrDefault().Station_Accreditation_Request_ID)
                    //                {
                    //                    var Station_Committee_Max = _Request_Committee.Where(a => a.Station_Accreditation_Request_ID == max_Requst).Select(a => a.Station_Committee_ID).Max();

                    //                    var _Is_final_Status = _Request_Committee.Where(a => a.Station_Committee_ID == Station_Committee_Max).FirstOrDefault();
                    //                    item_Station_Data.Is_final_Status = _Is_final_Status.Is_final_Status;
                    //                    item_Station_Data.Station_Accreditation_Request_ID = _Is_final_Status.Station_Accreditation_Request_ID;
                    //                    item_Station_Data.Station_Accreditation_Request_Type_Name = _Is_final_Status.Station_Accreditation_Request_Type_Name;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (max_Requst == -1)
                    //                {
                    //                    item_Station_Data.Station_Accreditation_Request_ID = -1;
                    //                }
                    //                item_Station_Data.Is_final_Status = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            item_Station_Data.Is_final_Status = false;
                    //        }

                           
                    //    }
                    //    catch (Exception ex)
                    //    {


                    //    }
                    //}
                }


                #region List_Station_Fees       
                Dictionary<string, SqlDbType> paramters_Type_Fees = new Dictionary<string, SqlDbType>();
                paramters_Type_Fees.Add("StationId", SqlDbType.BigInt);
                Dictionary<string, string> paramters_Data_Fees = new Dictionary<string, string>();
                paramters_Data_Fees.Add("StationId", stationIdNew.ToString());
                var Station_Fees = uow.Repository<Station_Fees_DTO>().CallStored("Station_Get_Fees", paramters_Type_Fees,
                paramters_Data_Fees, Device_Info).ToList();
                Stations_Data.List_Station_Fees = Station_Fees;
                #endregion
                #endregion

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Stations_Data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetStationDataNew_Old(long stationIdNew, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                #region new Code
                //المحطة
                var Stations_Data = (from s in entities.Stations
                                     join g in entities.Governates on s.Gov_Id equals g.ID
                                     join c in entities.Centers on s.Center_Id equals c.ID into c1
                                     from c in c1.DefaultIfEmpty()
                                     join v in entities.Villages on s.Village_Id equals v.ID into v1
                                     from v in v1.DefaultIfEmpty()
                                     where s.ID == stationIdNew
                                     select new Station_Final_Result_New_DTO
                                     {
                                         Station_ID = s.ID,
                                         Station_Name = s.Ar_Name,
                                         station_Address = s.Address_Ar,
                                         StationCode = s.StationCode,
                                         Governate_Name = g.Ar_Name,
                                         Center_Name = c.Ar_Name,
                                         Village_name = v.Ar_Name,
                                     }).FirstOrDefault();
                // نوع اللاعتماد
                var Station_Accreditation_Data = (from s in entities.Stations
                                                  join sar in entities.Station_Accreditation_Request on s.ID equals sar.Station_ID
                                                  join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                  where s.ID == stationIdNew
                                                  select new Station_Accreditation_Data_NewDTO
                                                  {
                                                      Station_ID = s.ID,
                                                      Station_Accreditation_Data_ID = sar.Station_Accreditation_Data_ID,
                                                      Station_Accreditation_Data_Name = sad.Name_AR,
                                                  }).Distinct().ToList();
                Stations_Data.List_Station_Accreditation_Data = Station_Accreditation_Data;


                //نوع اللجنة اول مره تقييم تجديد



                foreach (var item_Station_Data in Station_Accreditation_Data)
                {
                   
                    var Station_Accreditation_Request = (from sar in entities.Station_Accreditation_Request
                                                         join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                         join sart in entities.Station_Accreditation_Request_Type on sar.Station_Accreditation_Request_Type_ID equals sart.ID
                                                         where sar.Station_Accreditation_Data_ID == item_Station_Data.Station_Accreditation_Data_ID
                                                         && sar.Station_ID == item_Station_Data.Station_ID
                                                         select new Station_Accreditation_Request_Hagre_DTO
                                                         {
                                                             Station_Accreditation_Request_Type_ID = sart.ID,
                                                             Station_Accreditation_Request_ID = sar.ID,
                                                             Station_Accreditation_Request_Type_Name = sart.Name_AR,

                                                             IsAccepted_Quarantine = sar.IsAccepted,
                                                             Notes_Quarantine = sar.Notes_Quarantine,
                                                             StartDate_Quarantine = sar.StartDate,
                                                             EndDate_Quarantine = sar.EndDate,
                                                             User_Creation_Id = sar.User_Updation_Id

                                                         }).ToList();

                    foreach (var fRes in Station_Accreditation_Request)
                    {
                        if (fRes.User_Creation_Id != null)
                        {
                            try
                            {
                                 fRes.Full_Name_Quarantine = priv.PR_User.Where(c => c.Id == fRes.User_Creation_Id).FirstOrDefault().FullName;
                            }
                            catch
                            {
                            }
                        }
                    }


                    item_Station_Data.List_Station_Accreditation_Request_Hagre = Station_Accreditation_Request;
                    var max_Requst = Station_Accreditation_Request.Select(a => a.Station_Accreditation_Request_ID).Max();

                    if (Station_Accreditation_Request.Where(a => a.Station_Accreditation_Request_ID == max_Requst).FirstOrDefault().IsAccepted_Quarantine != null)
                        max_Requst = -1;
                    // اللجان لك نوع
                    foreach (var item_Comm in Station_Accreditation_Request)
                    {
                        try
                        {
                            DateTime Date_Now = DateTime.Now;
                            try
                            {
                                string dd = DateTime.Now.Date.ToString("dd/MM/yyyy");
                                Date_Now = Convert.ToDateTime(dd);
                            }
                            catch (Exception)
                            {
                                string dd = DateTime.Now.Date.ToString("MM/dd/yyyy");
                                Date_Now = Convert.ToDateTime(dd);
                            }
                            var _Request_Committee = (from sac in entities.Station_Accreditation_Committee
                                                      where sac.Station_Accreditation_Request.Station_Accreditation_Request_Type_ID == item_Comm.Station_Accreditation_Request_Type_ID
                                                      &&sac.Station_Accreditation_Request.Station_ID == stationIdNew
                                                      //where sac.Station_Accreditation_Request_ID == item_Comm.Station_Accreditation_Request_ID
                                                      select new Station_Request_Committee_DTO
                                                      {
                                                          Station_Accreditation_Request_ID = sac.Station_Accreditation_Request_ID,
                                                          Station_Accreditation_Request_Type_Name = sac.Station_Accreditation_Request.Station_Accreditation_Request_Type.Name_AR,
                                                          Station_Committee_ID = sac.ID,
                                                          Station_Committee_Delegation_Date = sac.Delegation_Date,
                                                          Station_Committee_StartTime = sac.StartTime,
                                                          Station_Committee_EndTime = sac.EndTime,
                                                          Station_Refuse_Reason_Nots = sac.Notes_Refuse_Ar,
                                                          Committee_position = sac.IsApproved == false ? "تم رفض اللجنة من قبل العميل"
                                                          : sac.Status == false
                                                          && sac.Delegation_Date < Date_Now ? "انتهاء الوقت"
                                                          : sac.Is_Cancel == true ? "تعذر الفحص"
                                                          : sac.Status == true ? "تم انتهاء عمل اللجنة"
                                                          : "يتم العمل على اللجنة",
                                                          Is_final_Status = sac.Status == true && sac.Is_Cancel == null ? true : false,
                                                      }).ToList();
                            item_Comm.List_Station__Request_Committee = _Request_Committee;
                            if (_Request_Committee.Count() > 0)
                            {
                                if (max_Requst > 0)
                                {
                                    if (max_Requst == _Request_Committee.FirstOrDefault().Station_Accreditation_Request_ID)
                                    {
                                        var Station_Committee_Max = _Request_Committee.Where(a => a.Station_Accreditation_Request_ID == max_Requst).Select(a => a.Station_Committee_ID).Max();

                                        var _Is_final_Status = _Request_Committee.Where(a => a.Station_Committee_ID == Station_Committee_Max).FirstOrDefault();
                                        item_Station_Data.Is_final_Status = _Is_final_Status.Is_final_Status;
                                        item_Station_Data.Station_Accreditation_Request_ID = _Is_final_Status.Station_Accreditation_Request_ID;
                                        item_Station_Data.Station_Accreditation_Request_Type_Name = _Is_final_Status.Station_Accreditation_Request_Type_Name;
                                    }
                                }
                                else
                                {
                                    if (max_Requst == -1)
                                    {
                                        item_Station_Data.Station_Accreditation_Request_ID = -1;
                                    }
                                    item_Station_Data.Is_final_Status = false;
                                }
                            }
                            else
                            {
                                item_Station_Data.Is_final_Status = false;
                            }

                            foreach (var item_Comm_Data in _Request_Committee)
                            {
                                #region Admin
                                var Station_Request_Committee_Admin = (from saccl in entities.Station_Accreditation_Committee_CheckList
                                                                       join sacl in entities.Station_Accreditation_CheckList on saccl.Station_Accreditation_CheckList_ID equals sacl.ID
                                                                       join scl in entities.Station_CheckList on sacl.Station_CheckList_ID equals scl.ID
                                                                       join sacfr_Admin in entities.Station_Accreditation_Committee_Final_Result on new { a = (long)saccl.Committee_ID, b = (bool)true } equals new { a = sacfr_Admin.Station_Accreditation_Committee_ID, b = sacfr_Admin.ISAdmin }
                                                                       into sacfr_Admin1
                                                                       from sacfr_Admin in sacfr_Admin1.DefaultIfEmpty()
                                                                       join Comm_Emp in entities.CommitteeEmployees on saccl.Committee_ID equals Comm_Emp.Committee_ID

                                                                       where saccl.Committee_ID == item_Comm_Data.Station_Committee_ID
                                                                       && Comm_Emp.ISAdmin == true
                                                                       && Comm_Emp.OperationType == 79
                                                                       select new Station_Request_Committee_Admin_DTO
                                                                       {
                                                                           Station_CheckList_ID = saccl.ID,
                                                                           IsAccepted_Band_Admin = saccl.IsAccepted,
                                                                           Notes_Ar_Band_Admin = saccl.Notes_Ar,
                                                                           Notes_En_Band_Admin = saccl.Notes_En,
                                                                           EmployeeId = Comm_Emp.Employee_Id,
                                                                           //FullName = priv.PR_User.Where(c => c.Id == Comm_Emp.Employee_Id).FirstOrDefault().FullName,// uow2. sac.Notes_Refuse_Ar,
                                                                           Station_CheckList_name = scl.ConstrainText_Ar,
                                                                           IsAccepted_final_Admin = sacfr_Admin.IsAccepted,
                                                                           Notes_Ar_final_Admin = sacfr_Admin.Notes_final,
                                                                       }).ToList();

                                foreach (var fRes in Station_Request_Committee_Admin)
                                {
                                    if (fRes.EmployeeId != null)
                                    {
                                        try
                                        {
                                          fRes.FullName = priv.PR_User.Where(c => c.Id == fRes.EmployeeId).FirstOrDefault().FullName;

                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                item_Comm_Data.List_Station_Request_Committee_Admin = Station_Request_Committee_Admin;
                                #endregion

                                #region Confirm

                                var Station_Request_Committee_confirm = (from sacfr_Confirm in entities.Station_Accreditation_Committee_Final_Result



                                                                         join Comm_Emp in entities.CommitteeEmployees on sacfr_Confirm.Station_Accreditation_Committee_ID equals Comm_Emp.Committee_ID
                                                                         where sacfr_Confirm.Station_Accreditation_Committee_ID == item_Comm_Data.Station_Committee_ID
                                                                         && Comm_Emp.ISAdmin == false
                                                                         && Comm_Emp.OperationType == 79
                                                                         && sacfr_Confirm.ISAdmin == false
                                                                         select new Station_Request_Committee_Confirm_DTO
                                                                         {
                                                                             IsAccepted_Confirm = sacfr_Confirm.IsAccepted,
                                                                             Notes_Confirm = sacfr_Confirm.Notes_final,
                                                                             Date_Confirm = sacfr_Confirm.User_Creation_Date,
                                                                             Employee_Id_Confirm = Comm_Emp.Employee_Id,
                                                                         }).ToList();

                                foreach (var fRes in Station_Request_Committee_confirm)
                                {
                                    if (fRes.Employee_Id_Confirm != null)
                                    {
                                        try
                                        {
                                            fRes.FullName_Confirm = priv.PR_User.Where(c => c.Id == fRes.Employee_Id_Confirm).FirstOrDefault().FullName;

                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                item_Comm_Data.List_Station_Request_Committee_Confirm = Station_Request_Committee_confirm;
                                #endregion

                                #region Station_Check_Imge
                                var Station_Check_Imge = (from im in entities.Station_Accreditation_Committee_Imge
                                                          where im.Station_Accreditation_Committee_id == item_Comm_Data.Station_Committee_ID
                                                          select new Station_Request_Committee_Imge_DTO
                                                          {
                                                              Station_Committee_Imge_ID = im.ID,
                                                              Station_Committee_Imge_Binary = im.AttachmentPath_Binary,
                                                              Infection_Comment = im.Infection_Comment,
                                                              Station_Accreditation_Committee_ID = im.Station_Accreditation_Committee_id,
                                                          }).Take(5).ToList();

                                item_Comm_Data.List_Station_Request_Committee_Imge = Station_Check_Imge;

                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {


                        }
                    }
                }


                #region List_Station_Fees       
                Dictionary<string, SqlDbType> paramters_Type_Fees = new Dictionary<string, SqlDbType>();
                paramters_Type_Fees.Add("StationId", SqlDbType.BigInt);
                Dictionary<string, string> paramters_Data_Fees = new Dictionary<string, string>();
                paramters_Data_Fees.Add("StationId", stationIdNew.ToString());
                var Station_Fees = uow.Repository<Station_Fees_DTO>().CallStored("Station_Get_Fees", paramters_Type_Fees,
                paramters_Data_Fees, Device_Info).ToList();
                Stations_Data.List_Station_Fees = Station_Fees;
                #endregion
                #endregion

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Stations_Data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Insert_Station_Requeste_Quarantine(Station_Accreditation_Request_DTO req, List<string> Device_Info)
        {
            try
            {
                #region Station_Request                                
                Station_Accreditation_Request CModel_Station_Request = uow.Repository<Station_Accreditation_Request>().Findobject(req.ID);
                CModel_Station_Request.StartDate = req.StartDate;
                CModel_Station_Request.EndDate = req.EndDate;
                CModel_Station_Request.Notes_Quarantine = req.Notes_Quarantine;
                CModel_Station_Request.IsAccepted = req.IsAccepted;
                CModel_Station_Request.Is_Final_requst = true;
                CModel_Station_Request.User_Updation_Id = req.User_Updation_Id;
                CModel_Station_Request.User_Updation_Date = DateTime.Now;

                uow.Repository<Station_Accreditation_Request>().Update(CModel_Station_Request);
                uow.SaveChanges();
                #endregion


                #region Station_Accreditation
                if (req.IsAccepted == true)
                {
                    PlantQuarantineEntities Db = new PlantQuarantineEntities();
                    // القديم كله False
                    var Check_Reqst_Final_Old = Db.Station_Accreditation.Where(a => a.Station_ID == req.Station_ID
                      && a.Station_Accreditation_Data_ID == req.Station_Accreditation_Data_ID).ToList();
                    for (int i = 0; i < Check_Reqst_Final_Old.Count(); i++)
                    {
                        Station_Accreditation CModel_Check_Reqst_Final_Old = uow.Repository<Station_Accreditation>().Findobject(Check_Reqst_Final_Old[i].ID);
                        CModel_Check_Reqst_Final_Old.IsActive = false;
                        uow.Repository<Station_Accreditation>().Update(CModel_Check_Reqst_Final_Old);
                        uow.SaveChanges();
                    }

                    // تسجيل الجديد
                    var id_Station_Accreditation = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_seq");
                    var _Station_Accreditation = new Station_Accreditation
                    {
                        ID = id_Station_Accreditation,
                        Station_ID = req.Station_ID,
                        Station_Accreditation_Request_ID = req.ID,
                        Station_Accreditation_Data_ID = req.Station_Accreditation_Data_ID,
                        Notes_Quarantine = req.Notes_Quarantine,
                        StartDate = req.StartDate,
                        EndDate = req.EndDate,
                        IsActive = req.IsAccepted,
                        User_Creation_Id = req.User_Creation_Id,
                        User_Creation_Date = DateTime.Now,
                    };
                    Db.Station_Accreditation.Add(_Station_Accreditation);
                    Db.SaveChanges();
                }
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, req);
            }
            catch (Exception ex)
            {

                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}