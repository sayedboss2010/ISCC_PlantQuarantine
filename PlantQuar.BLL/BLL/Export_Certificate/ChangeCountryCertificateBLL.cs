using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Export_Certificate
{
    public class ChangeCountryCertificateBLL
    {
        private UnitOfWork uow;
        public ChangeCountryCertificateBLL()
        {

            uow = new UnitOfWork();
        }


        public Dictionary<string, object> GetCountries_Name(List<string> Device_Info)
        {
            string lang = Device_Info[2];

            var data = uow.Repository<Country>().GetData()
           .Where(c => c.IsActive == true && c.User_Deletion_Id == null)
           .Select(c => new CustomOption
           {
               DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
               Value = c.ID
           }).OrderBy(a => a.DisplayText).ToList();
            //            data.Insert()
            //data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = 0 });
            data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "---------" : "---------"), Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> GetPortType(List<string> Device_Info)
        {
            string lang = Device_Info[2];

            var data = uow.Repository<Port_Type>().GetData()
           .Where(c => c.IsActive == true && c.User_Deletion_Id == null)
           .Select(c => new CustomOption
           {
               DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
               Value = c.ID
           }).OrderBy(a => a.DisplayText).ToList();
            //            data.Insert()
            //data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = 0 });
            data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "---------" : "---------"), Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> GetCountry(string RequestNumber, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                //
                //var requests = (from ex in entities.Ex_CheckRequest
                //                join exd in entities.Ex_CheckRequest_Data on ex.ID equals exd.Ex_CheckRequest_ID
                //                join exp in entities.Ex_CheckRequest_Port on exd.ID equals exp.Ex_CheckRequest_Data_ID
                //                join p in entities.Port_International on exp.Port_ID equals p.ID into p1
                //                from p in p1.DefaultIfEmpty()
                //                join c in entities.Countries on p.Country_ID equals c.ID into c1
                //                from c in c1.DefaultIfEmpty()

                //                where ex.CheckRequest_Number == RequestNumber
                //                select new ChangeCountryCertificateDTO
                //                {
                //                    Ex_CheckRequest_ID = ex.ID,
                //                    Ex_CheckRequest_Data_ID = exd.ID,
                //                    Ex_CheckRequest_Port_ID = exp.ID,
                //                    Port_ID = exp.Port_ID,
                //                    Port_International_Name_Ar = p.Name_Ar,
                //                    ReqPortType_ID = exp.ReqPortType_ID, // للشحن "9" -------- للعبور "11" بس
                //                    Port_Type_ID = exp.Port_Type_ID,
                //                    Country_Name = c.Ar_Name,
                //                    Country_ID = c.ID,
                //                }).ToList();


                var requests1 = (from ex in entities.Ex_CheckRequest
                                 join exd in entities.Ex_CheckRequest_Data on ex.ID equals exd.Ex_CheckRequest_ID
                                 join c in entities.Countries on exd.ExportCountry_Id equals c.ID
                                 join exp in entities.Ex_CheckRequest_Port on exd.ID equals exp.Ex_CheckRequest_Data_ID //where exp.Port_Type_ID==10
                                 join p in entities.Port_International on exp.Port_ID equals p.ID into p1
                                 from p in p1.DefaultIfEmpty()
                                 where ex.CheckRequest_Number == RequestNumber
                                 select new ChangeCountryCertificateDTO
                                 {
                                     Ex_CheckRequest_ID = ex.ID,
                                     Ex_CheckRequest_Data_ID = exd.ID,
                                     Ex_CheckRequest_Port_ID = exp.ID,
                                     Port_ID = exp.Port_ID,
                                     Port_International_Name_Ar = p.Name_Ar,
                                     ReqPortType_ID = exp.ReqPortType_ID, // للشحن "9" -------- للعبور "11" بس
                                     Port_Type_ID = exp.Port_Type_ID,
                                     Country_Name = c.Ar_Name,
                                     Country_ID = c.ID,
                                 }).ToList();

                var requests2 = (from ex in entities.Ex_CheckRequest
                                 join exd in entities.Ex_CheckRequest_Data on ex.ID equals exd.Ex_CheckRequest_ID
                                 join c in entities.Countries on exd.TransitCountry_Id equals c.ID
                                 join exp in entities.Ex_CheckRequest_Port on exd.ID equals exp.Ex_CheckRequest_Data_ID
                                 //   where exp.Port_Type_ID == 11
                                 join p in entities.Port_International on exp.Port_ID equals p.ID into p1
                                 from p in p1.DefaultIfEmpty()
                                 where ex.CheckRequest_Number == RequestNumber
                                 select new ChangeCountryCertificateDTO
                                 {
                                     Ex_CheckRequest_ID = ex.ID,
                                     Ex_CheckRequest_Data_ID = exd.ID,
                                     Ex_CheckRequest_Port_ID = exp.ID,
                                     Port_ID = exp.Port_ID,
                                     Port_International_Name_Ar = p.Name_Ar,
                                     ReqPortType_ID = exp.ReqPortType_ID, // للشحن "9" -------- للعبور "11" بس
                                     Port_Type_ID = exp.Port_Type_ID,
                                     Country_Name = c.Ar_Name,
                                     Country_ID = c.ID,
                                 }).ToList();
                var requests = requests1.Union(requests2).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> changeImportCountry
         (string CheckRequestNumber, int newImportCountryID, int newImportPortType, int newImportPortID
            , int currentImportCountryID, int currentImportPortID, short User_Id, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                ChangeCountryCertificateDTO ChangeCountryCertificate = new ChangeCountryCertificateDTO();
                var ExChechRequest = (from ex in entities.Ex_CheckRequest
                                      join exd in entities.Ex_CheckRequest_Data on ex.ID equals exd.Ex_CheckRequest_ID
                                      join c in entities.Countries on exd.ExportCountry_Id equals c.ID
                                      join exp in entities.Ex_CheckRequest_Port on exd.ID equals exp.Ex_CheckRequest_Data_ID
                                      where ex.CheckRequest_Number == CheckRequestNumber && exp.ReqPortType_ID == 10

                                      select new ChangeCountryCertificateDTO
                                      {
                                          Ex_CheckRequest_ID = ex.ID,
                                          Ex_CheckRequest_Data_ID = exd.ID,
                                          Ex_CheckRequest_Port_ID = exp.ID,
                                          Port_ID = exp.Port_ID,
                                          ReqPortType_ID = exp.ReqPortType_ID, // للشحن "9" -------- للعبور "11" بس
                                          Port_Type_ID = exp.Port_Type_ID,
                                      }).FirstOrDefault();
                int CertificatesNewCountry_ID = uow.Repository<Object>().GetNextSequenceValue_Int("Ex_CertificatesNewCountry_seq");

                var ExCertificatesNewCountry = new Ex_CertificatesNewCountry
                {
                    ID = CertificatesNewCountry_ID,
                    Ex_CheckRequest_ID = ExChechRequest.Ex_CheckRequest_ID,
                    OldCountryID = currentImportCountryID,
                    NewCountryId = newImportCountryID,
                    Port_International_ID_new = newImportPortID,
                    Port_International_ID_old = currentImportPortID,
                    ReqPortType_ID = 10,
                    Port_Type_ID_Old = ExChechRequest.Port_Type_ID,
                    Port_Type_ID_New = newImportPortType,
                    User_Creation_Date = DateTime.Now,
                    User_Creation_Id = User_Id,
                };

                entities.Ex_CertificatesNewCountry.Add(ExCertificatesNewCountry);
                entities.SaveChanges();


                #region log Action
                var Ex_CheckRequest_Id = ExChechRequest.Ex_CheckRequest_ID;
                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                dto2.ID_Table_Action = 61;
                // dto2.ID_TableActionValue = checkRequestId;
                dto2.Im_CheckRequest_ID = Ex_CheckRequest_Id;
                dto2.User_Creation_Id = User_Id;
                dto2.User_Creation_Date = DateTime.Now;
                dto2.NOTS = " تم تغير دولة الإستيراد للشهادة ";
                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                dto2.Type_log_ID = 134;  //system code for insert
                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                x.save_EX_CheckRequest_Log(dto2, Device_Info);

                #endregion


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, ChangeCountryCertificate);


                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, geshniPortsLst);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> changePassengerCountry
         (string CheckRequestNumber, int newPassengerPortType, int newPassengerCountryID, int newPassengerPortID
            , int currentPassengerCountryID, int currentPassengerPortID, short User_Id, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                ChangeCountryCertificateDTO ChangeCountryCertificate = new ChangeCountryCertificateDTO();

                var ExChechRequest = (from ex in entities.Ex_CheckRequest
                                      join exd in entities.Ex_CheckRequest_Data on ex.ID equals exd.Ex_CheckRequest_ID
                                      join c in entities.Countries on exd.TransitCountry_Id equals c.ID
                                      join exp in entities.Ex_CheckRequest_Port on exd.ID equals exp.Ex_CheckRequest_Data_ID
                                      where ex.CheckRequest_Number == CheckRequestNumber && exp.ReqPortType_ID == 11
                                      select new ChangeCountryCertificateDTO
                                      {
                                          Ex_CheckRequest_ID = ex.ID,
                                          Ex_CheckRequest_Data_ID = exd.ID,
                                          Ex_CheckRequest_Port_ID = exp.ID,
                                          Port_ID = exp.Port_ID,
                                          ReqPortType_ID = exp.ReqPortType_ID, // للشحن "9" -------- للعبور "11" بس
                                          Port_Type_ID = exp.Port_Type_ID,

                                      }).ToList();




                int CertificatesNewCountry_ID = uow.Repository<Object>().GetNextSequenceValue_Int("Ex_CertificatesNewCountry_seq");

                var ExCertificatesNewCountry = new Ex_CertificatesNewCountry
                {
                    ID = CertificatesNewCountry_ID,
                    Ex_CheckRequest_ID = ExChechRequest.FirstOrDefault().Ex_CheckRequest_ID,
                    OldCountryID = currentPassengerCountryID,
                    NewCountryId = newPassengerCountryID,
                    Port_International_ID_new = newPassengerPortID,
                    Port_International_ID_old = currentPassengerPortID,
                    ReqPortType_ID = 11,
                    Port_Type_ID_Old = ExChechRequest.FirstOrDefault().Port_Type_ID,
                    Port_Type_ID_New = newPassengerPortType,
                    User_Creation_Date = DateTime.Now,
                    User_Creation_Id = User_Id,
                };

                entities.Ex_CertificatesNewCountry.Add(ExCertificatesNewCountry);
                entities.SaveChanges();
                #region log Action
                var Ex_CheckRequest_Id = ExChechRequest.FirstOrDefault().Ex_CheckRequest_ID;
                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                dto2.ID_Table_Action = 62;
                // dto2.ID_TableActionValue = checkRequestId;
                dto2.Im_CheckRequest_ID = Ex_CheckRequest_Id;
                dto2.User_Creation_Id = User_Id;
                dto2.User_Creation_Date = DateTime.Now;
                dto2.NOTS = " تم تغير دولة العبور للشهادة ";
                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                dto2.Type_log_ID = 134;  //system code for insert
                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                x.save_EX_CheckRequest_Log(dto2, Device_Info);

                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, ChangeCountryCertificate);


                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, geshniPortsLst);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
