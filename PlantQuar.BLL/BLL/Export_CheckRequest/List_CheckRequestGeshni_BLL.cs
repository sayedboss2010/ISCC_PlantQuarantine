using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using PlantQuar.DTO.HelperClasses;
using System.Data;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DTO.DTO.Log;

namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
    public class List_CheckRequestGeshni_BLL
    {
        private UnitOfWork uow;
        public List_CheckRequestGeshni_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetGeshniCommitteeList
            (string CheckRequestNumber, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                //
                var requests = (from ch in entities.Ex_CheckRequest

                                join chp in entities.Ex_CheckRequest_Places on ch.ID equals chp.Ex_CheckRequest_ID
                                join outlet in entities.Outlets on chp.Outlet_Genshi_ID equals outlet.ID into outlet1
                                from outlet in outlet1.DefaultIfEmpty()
                                where ch.CheckRequest_Number == CheckRequestNumber
                                select new GeshniCommitteesDTO
                                {
                                    Ex_CheckRequest_ID = ch.ID,
                                    CenterName = chp.Center.Ar_Name,
                                    GovernName = chp.Governate.Ar_Name,
                                    Station_Examination_ID = chp.Station_Examination_ID,
                                    Station_Examination = chp.Station1.Ar_Name,
                                    PortNationalName = chp.PortNational.Name_Ar,
                                    Station_Geshni_Name = chp.Station.Ar_Name,
                                    Examination_location = chp.Examination_location,
                                    OutletNameGashni = outlet.Ar_Name
                                }).FirstOrDefault();




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetEmployeeGeshniChange
     (string requestNumber, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from exc in entities.Ex_CheckRequest
                            join tl in entities.Table_Action_Log_EX on exc.ID equals tl.Ex_CheckRequest_ID

                            where exc.CheckRequest_Number == requestNumber &&tl.ID_Table_Action==52
                            select new EmployeeGeshniChangeDTO
                            {
                                Ex_CheckRequest_ID2 = tl.Ex_CheckRequest_ID,
                                Emp_ID2 = tl.User_Creation_Id,
                                User_Creation_Date2 = tl.User_Creation_Date,
                                Notes2 = tl.NOTS
                                //Ex_CheckRequest_ID = exc.ID,


                            }).OrderBy(a=>a.User_Creation_Date2).Distinct().ToList();
                dbPrivilageEntities prv = new dbPrivilageEntities();
                foreach (var item in data)
                {
                    item.EmpName2 = prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.FullName).FirstOrDefault();
                    item.Emp_ID2= prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.EmpId).FirstOrDefault();

                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetGeshniPortsList
    (int port, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                //
                var requests = (from outlet in entities.Outlets

                                where outlet.IsDisplay == 1
                                select new CustomOptionLongId
                                {
                                    DisplayText = (lang == "1" ? outlet.Ar_Name : outlet.Ar_Name),
                                    Value = outlet.ID
                                }).ToList();
                requests.Insert(0, new CustomOptionLongId() { DisplayText = "أختـــار منفذ", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetGeshniStationList
    (int station, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                //
                string CheckRequestNumber = "73620242177385865";
                var stationsGeshniList = (from s in entities.Stations

                                          join sa in entities.Station_Accreditation on s.ID equals sa.Station_ID
                                          join sad in entities.Station_Accreditation_Data on sa.Station_Accreditation_Data_ID equals sad.ID
                                          join sadi in entities.Station_Accreditation_Data_Item_ShortName on sad.ID equals sadi.Station_Accreditation_Data_ID


                                          //where ch.CheckRequest_Number == CheckRequestNumber
                                          select new stationsGeashni
                                          {
                                              IDStation = s.ID,

                                              Ar_Name = s.Ar_Name,

                                              Item_ShortName_ID = sadi.Item_ShortName_ID
                                          }).ToList();
                var EXReqStationsGeshniList = (from ex in entities.Ex_CheckRequest.Where(ex => ex.User_Deletion_Id == null)

                                               join exi in entities.Ex_CheckRequest_Items on ex.ID equals exi.Ex_CheckRequest_ID


                                               where ex.CheckRequest_Number == CheckRequestNumber
                                               select new ExdChechRequestItemGeshni
                                               {
                                                   IDCheckrequest = ex.ID,

                                                   Item_ShortName_ID = exi.Item_ShortName_ID,


                                               }).ToList();
                var requests = (from s in entities.Stations

                                join sa in entities.Station_Accreditation on s.ID equals sa.Station_ID
                                //  join sad in entities.Station_Accreditation_Data on sa.Station_Accreditation_Data_ID equals sad.ID
                                // join sadi in entities.Station_Accreditation_Data_Item_ShortName on sad.ID equals sadi.Station_Accreditation_Data_ID


                                where s.User_Deletion_Id == null//&&sa.IsActive==true//&&sa.User_Deletion_Id==null&&sa.StationCompanies.Count()==1
                                select new CustomOptionLongId
                                {
                                    DisplayText = (lang == "1" ? s.Ar_Name : s.En_Name),
                                    Value = s.ID
                                }).Distinct().ToList();
                requests.Insert(0, new CustomOptionLongId() { DisplayText = "أختـــار محطة", Value = null });



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> PutGeshniPort
(GeshniPortsDTO geshniPortsLst, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];


                //update

                // var checkRequestNumber = geshniPortsLst.Select(a => a.Ex_CheckRequest_Number).FirstOrDefault();
                var checkRequestId = entities.Ex_CheckRequest.Where(a => a.CheckRequest_Number == geshniPortsLst.Ex_CheckRequest_Number).Select(a => a.ID).FirstOrDefault();
                var checkRequestplaceId = entities.Ex_CheckRequest_Places.Where(a => a.Ex_CheckRequest_ID == checkRequestId).Select(a => a.ID).FirstOrDefault();
                // var portNational_ID = geshniPortsLst.Select(a => a.NewPortGeshni_Id).FirstOrDefault();
                var Cmodel = //entities.Ex_CheckRequest_Places.Where(a => a.Ex_CheckRequest_ID == checkRequestId).FirstOrDefault();
               uow.Repository<Ex_CheckRequest_Places>().Findobject(checkRequestplaceId);




                if (Cmodel != null)
                {
                    Cmodel.Outlet_Genshi_ID = geshniPortsLst.NewPortGeshni_Id;
                    Cmodel.Station_Genshi_ID = null;
                    uow.Repository<Ex_CheckRequest_Places>().UpdateReturn(Cmodel);
                    uow.SaveChanges();


                    #region log Action

                    Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                    dto2.ID_Table_Action = 52;
                    // dto2.ID_TableActionValue = checkRequestId;
                    dto2.Im_CheckRequest_ID = checkRequestId;
                    dto2.User_Creation_Id = geshniPortsLst.User_Creation_Id;
                    dto2.User_Creation_Date = geshniPortsLst.User_Creation_Date;
                    dto2.NOTS = " تم تغير مكان الجشني لمنفذ في طلب الفحص الصادر ";
                    dto2.User_Type_ID = 127;// System Code For موظف الحجر
                    dto2.Type_log_ID = 135;  //system code for Update
                    Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                    x.save_EX_CheckRequest_Log(dto2, Device_Info);

                    #endregion


                    // return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, Cmodel);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, geshniPortsLst);
                // return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, Cmodel);

                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Cmodel);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



        public Dictionary<string, object> PutGeshniStation
(GeshniStationDTO geshniStationLst, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];


                //update

                var checkRequestNumber = geshniStationLst.Ex_CheckRequest_Number;//geshniStationLst.Select(a => a.Ex_CheckRequest_Number).FirstOrDefault();
                var checkRequestId = entities.Ex_CheckRequest.Where(a => a.CheckRequest_Number == checkRequestNumber).Select(a => a.ID).FirstOrDefault();
                var checkRequestplaceId = entities.Ex_CheckRequest_Places.Where(a => a.Ex_CheckRequest_ID == checkRequestId).Select(a => a.ID).FirstOrDefault();
                var stationNational_ID = geshniStationLst.NewStationGeshni_Id;
                var Cmodel = //entities.Ex_CheckRequest_Places.Where(a => a.Ex_CheckRequest_ID == checkRequestId).FirstOrDefault();
               uow.Repository<Ex_CheckRequest_Places>().Findobject(checkRequestplaceId);
                if (Cmodel != null)
                {
                    Cmodel.PortNational_ID = null;
                    Cmodel.Outlet_Genshi_ID = null;
                    Cmodel.Station_Genshi_ID = stationNational_ID;
                    uow.Repository<Ex_CheckRequest_Places>().Update(Cmodel);
                    uow.SaveChanges();
                    // return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, geshniStationLst);
                    #region log Action

                    Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                    dto2.ID_Table_Action = 52;
                    // dto2.ID_TableActionValue = checkRequestId;
                    dto2.Im_CheckRequest_ID = checkRequestId;
                    dto2.User_Creation_Id = geshniStationLst.User_Creation_Id;
                    dto2.User_Creation_Date = geshniStationLst.User_Creation_Date;
                    dto2.NOTS = " تم تغير مكان الجشني لمحطة في طلب الفحص الصادر ";
                    dto2.User_Type_ID = 127;// System Code For موظف الحجر
                    dto2.Type_log_ID = 135;  //system code for Update
                    Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                    x.save_EX_CheckRequest_Log(dto2, Device_Info);

                    #endregion

                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, geshniStationLst);

                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Cmodel);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}
