using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Admin
{
    public class AcceptAggreementBackBLL
    {
        private UnitOfWork uow;
        public AcceptAggreementBackBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetRequestAggreement
           (string RequestNumber, int Request_type,short User_Id, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                AcceptAggreementBackDTO acceptaggreementback = new AcceptAggreementBackDTO();
                // List<AcceptAggreementBackDTO> Request= new List<AcceptAggreementBackDTO> ;
                // VetDBContext obj = new VetDBContext();
                if (Request_type == 79) //محطات
                {
                    var Check_Station = entities.Stations.Where(s => s.StationCode == RequestNumber).Count();
                    if (Check_Station > 0)
                    {
                        var StationRequests = (from s in entities.Stations
                                               join sar in entities.Station_Accreditation_Request on s.ID equals sar.Station_ID
                                               join sac in entities.Station_Accreditation_Committee on sar.ID equals sac.Station_Accreditation_Request_ID
                                               where s.StationCode == RequestNumber
                                               select new AcceptAggreementBackDTO
                                               {
                                                   StationID = s.ID,
                                                   StationCode = s.StationCode,
                                                   StationIsApproved = s.IsApproved,
                                                   StationIsActive = s.IsActive,
                                                   StationAccreditationRequestIsPaid = sar.IsPaid,
                                                   StationAccreditationCommitteeIsPaid = sac.IsPaid,
                                               }).ToList();
                        if (StationRequests.Count() > 0)
                        {
                            //ارسال رساله ان الرقم لا يمكن التعديل عليه
                            acceptaggreementback = null;
                        }
                        else
                        {
                            //var obj = entities.Stations.Find(StationRequests.FirstOrDefault().StationID);
                            var obj = entities.Stations.Where(a => a.StationCode == RequestNumber).FirstOrDefault();

                            if (obj != null)
                            {
                                //var Station_Id= StationRequests.FirstOrDefault().StationID;
                                obj.IsActive = null;
                                obj.IsApproved = null;
                                entities.SaveChanges();
                                #region log Action
                                //var Station_Id = Station_Id;
                                Table_Action_Log_Station_DTO dto2 = new Table_Action_Log_Station_DTO();
                                dto2.ID_Table_Action = 63;
                                // dto2.ID_TableActionValue = checkRequestId;
                                dto2.Station_ID = obj.ID;
                                dto2.User_Creation_Id = User_Id;
                                dto2.User_Creation_Date = DateTime.Now;
                                dto2.NOTS = " تم تغير الموافقه في طلب المحطات ";
                                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                                dto2.Type_log_ID = 135;  //system code for Update
                                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                                x.save_Station_Log(dto2, Device_Info);

                                #endregion
                            }

                        }
                    }
                    else
                    {
                        acceptaggreementback = null;
                    }

                }
                else if (Request_type == 80) // صادر
                {

                    var obj = entities.Ex_CheckRequest.Where(s => s.CheckRequest_Number == RequestNumber).FirstOrDefault();
                    if (obj.IsPaid == null)
                    {
                        // var Ex_CheckRequest = entities.Ex_CheckRequest.Where(s => s.CheckRequest_Number == RequestNumber && s.IsPaid == null);
                        //var obj = entities.Ex_CheckRequest.Find(Ex_CheckRequest.ID);

                        if (obj != null)
                        {
                            if (obj.IsActive != null && obj.IsAccepted != null)
                            {
                                obj.IsActive = null;
                                obj.IsAccepted = null;
                                entities.SaveChanges();

                                #region log Action
                                var Ex_CheckRequest_Id = obj.ID;
                                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                                dto2.ID_Table_Action = 60;
                                // dto2.ID_TableActionValue = checkRequestId;
                                dto2.Im_CheckRequest_ID = Ex_CheckRequest_Id;
                                dto2.User_Creation_Id = User_Id;
                                dto2.User_Creation_Date = DateTime.Now;
                                dto2.NOTS = " تم تغير الموافقه في طلب الفحص الصادر ";
                                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                                dto2.Type_log_ID = 135;  //system code for Update
                                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                                x.save_EX_CheckRequest_Log(dto2, Device_Info);

                                #endregion



                            }

                            else
                            {   //ارسال رساله ان الرقم لا يمكن التعديل عليه
                                acceptaggreementback = null;
                            }
                        }
                        //
                    }
                    else 
                    
                    {
                        acceptaggreementback = null;
                    }
                    }
                    else if (Request_type == 81) // وارد
                {
                    var Im_CheckRequest =(from ch in entities.Im_CheckRequest
                                          join com in entities.Im_RequestCommittee on ch.ID equals com.ImCheckRequest_ID
                                          
                                          where ch.CheckRequest_Number == RequestNumber 
                                          select ch).FirstOrDefault();
                    if (Im_CheckRequest != null)
                    {
                        var obj = entities.Im_CheckRequest.Find(Im_CheckRequest.ID);

                        if (obj != null)
                        {
                            obj.IsActive = null;
                            obj.IsAccepted = null;
                            entities.SaveChanges();
                            #region log Action
                            var Im_CheckRequest_Id = Im_CheckRequest.ID;
                            Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                            dto2.ID_Table_Action = 61;
                            // dto2.ID_TableActionValue = checkRequestId;
                            dto2.Im_CheckRequest_ID = Im_CheckRequest_Id;
                            dto2.User_Creation_Id = User_Id;
                            dto2.User_Creation_Date = DateTime.Now;
                            dto2.NOTS = " تم تغير الموافقه في طلب الفحص الوارد ";
                            dto2.User_Type_ID = 127;// System Code For موظف الحجر
                            dto2.Type_log_ID = 135;  //system code for Update
                            Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                            x.save_CheckRequest_Log(dto2, Device_Info);

                            #endregion


                        }
                        else
                        {   //ارسال رساله ان الرقم لا يمكن التعديل عليه
                            acceptaggreementback = null;
                        }

                    }
                    else
                    {   //ارسال رساله ان الرقم لا يمكن التعديل عليه
                        acceptaggreementback = null;
                    }
                    //
                }
                else if (Request_type == 31) //إذن إستيراد
                {
                    decimal RequestNumber_pr = decimal.Parse(RequestNumber);
                    var Im_PermissionRequest = entities.Im_PermissionRequest.Where(s => s.ImPermission_Number == RequestNumber_pr && s.IsPaid == null).FirstOrDefault();
                    if ( Im_PermissionRequest != null ) { 
                    var obj = entities.Im_PermissionRequest.Find(Im_PermissionRequest.ID);

                        if (obj != null)
                        {
                            //  obj.IsActive = null;
                            obj.IsAcceppted = null;
                            entities.SaveChanges();
                            #region log Action
                            var Im_PermissionRequest_Id = Im_PermissionRequest.ID;
                            Table_Action_Log_DTO dto2 = new Table_Action_Log_DTO();
                            dto2.ID_Table_Action = 62;
                            // dto2.ID_TableActionValue = checkRequestId;
                            dto2.ID = Im_PermissionRequest_Id;
                            dto2.User_Creation_Id = User_Id;
                            dto2.User_Creation_Date = DateTime.Now;
                            dto2.NOTS = " تم تغير الموافقه في طلب الاذن ";
                            dto2.User_Type_ID = 127;// System Code For موظف الحجر
                            dto2.Type_log_ID = 135;  //system code for Update
                            Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                            x.save_Permission_Log(dto2, Device_Info);
                        }
                        else

                        {
                            acceptaggreementback = null;
                        }

                        #endregion

                    }
                    else
                    {   //ارسال رساله ان الرقم لا يمكن التعديل عليه
                        acceptaggreementback = null;
                    }
                    //
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, acceptaggreementback);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, geshniPortsLst);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetEmployeeGeshniChange(string requestNumber, int requesttype,List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data= new List<EmployeeGeshniChangeDTO>();
             // export
                if (requesttype == 80)
                {
                    data = (from exc in entities.Ex_CheckRequest
                            join tl in entities.Table_Action_Log_EX on exc.ID equals tl.Ex_CheckRequest_ID

                            where exc.CheckRequest_Number == requestNumber && tl.ID_Table_Action == 60
                            select new EmployeeGeshniChangeDTO
                            {
                                Ex_CheckRequest_ID2 = tl.Ex_CheckRequest_ID,
                                Emp_ID2 = tl.User_Creation_Id,
                                User_Creation_Date2 = tl.User_Creation_Date,
                                Notes2 = tl.NOTS
                                //Ex_CheckRequest_ID = exc.ID,


                            }).Distinct().ToList();
                    dbPrivilageEntities prv = new dbPrivilageEntities();
                    foreach (var item in data)
                    {
                        item.EmpName2 = prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.FullName).FirstOrDefault();

                    }
                }
              //import
                else if (requesttype == 81)
                {
                    data = (from exc in entities.Im_CheckRequest
                            join tl in entities.Table_Action_Log_CheckRequest on exc.ID equals tl.Im_CheckRequest_ID

                            where exc.CheckRequest_Number == requestNumber && tl.ID_Table_Action == 61
                            select new EmployeeGeshniChangeDTO
                            {
                                Ex_CheckRequest_ID2 = tl.Im_CheckRequest_ID,
                                Emp_ID2 = tl.User_Creation_Id,
                                User_Creation_Date2 = tl.User_Creation_Date,
                                Notes2 = tl.NOTS
                                //Ex_CheckRequest_ID = exc.ID,


                            }).Distinct().ToList();
                    dbPrivilageEntities prv = new dbPrivilageEntities();
                    foreach (var item in data)
                    {
                        item.EmpName2 = prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.FullName).FirstOrDefault();

                    }
                }
                //station
                else if (requesttype == 79)
                {
                    data = (from exc in entities.Stations
                            join tl in entities.Table_Action_Log_Station on exc.ID equals tl.Station_ID

                            where exc.StationCode == requestNumber && tl.ID_Table_Action == 63
                            select new EmployeeGeshniChangeDTO
                            {
                                Ex_CheckRequest_ID2 = tl.Station_ID,
                                Emp_ID2 = tl.User_Creation_Id,
                                User_Creation_Date2 = tl.User_Creation_Date,
                                Notes2 = tl.NOTS
                                //Ex_CheckRequest_ID = exc.ID,


                            }).Distinct().ToList();
                    dbPrivilageEntities prv = new dbPrivilageEntities();
                    foreach (var item in data)
                    {
                        item.EmpName2 = prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.FullName).FirstOrDefault();

                    }
                }
                //permission
                else if (requesttype == 31)
                {
                    decimal permNumber =decimal.Parse( requestNumber);
                    data = (from exc in entities.Im_PermissionRequest
                            join tl in entities.Table_Action_Log on exc.ID equals tl.Im_PermissionRequest_ID

                            where exc.ImPermission_Number == permNumber && tl.ID_Table_Action == 62
                            select new EmployeeGeshniChangeDTO
                            {
                                Ex_CheckRequest_ID2 = tl.Im_PermissionRequest_ID,
                                Emp_ID2 = tl.User_Creation_Id,
                                User_Creation_Date2 = tl.User_Creation_Date,
                                Notes2 = tl.NOTS
                                //Ex_CheckRequest_ID = exc.ID,


                            }).Distinct().ToList();
                    dbPrivilageEntities prv = new dbPrivilageEntities();
                    foreach (var item in data)
                    {
                        item.EmpName2 = prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.FullName).FirstOrDefault();

                    }
                }
              

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
