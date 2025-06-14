using AutoMapper;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Export_Certificate
{
    public class CertificateDetailsBLL
    {
        private UnitOfWork uow;
        public CertificateDetailsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetExCheckRequestDetails
         (long? certificate_ID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>
                {
                    { "certificateId", SqlDbType.NVarChar }
                };

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>
                {
                    { "certificateId", certificate_ID == null ? "" : certificate_ID.ToString() }
                };

                var request = uow.Repository<CheckRequest_GetData_web_ResultDTO>().CallStored("GetCertificateData", paramters_Type,
                paramters_Data, Device_Info).FirstOrDefault();

                CertificateDTO data = new CertificateDTO();
                
                //Addiation For User
                var _Additional_Declaretion = uow.Repository<Ex_CertificateAddtionUser>().GetData()//.Include(a => a.Ex_CertificateAddtionUser).
                     .Where(i => i.PlantCertificatesRequestsID == certificate_ID && i.Ex_CertificatesRequests.IS_Additional_Declaretion == true).
                     Select(v => new Ex_CertificateAddtionUserDTO
                     {
                         ID = v.ID,
                         Certificate_AddtionText = v.Certificate_AddtionText,
                     }).ToList();

                data.dataAddtion = _Additional_Declaretion;


                //Lots Data 
                var _LotData = (from ecrld in entities.Ex_CertificatesRequestsLotData
                                join ecr in entities.Ex_CertificatesRequests on ecrld.PlantCertificatesRequestsID equals ecr.ID
                                join excrilc in entities.Ex_CheckRequest_Items_Lot_Category on ecrld.LotID equals excrilc.ID
                                join exci in entities.Ex_CheckRequest_Items on excrilc.Ex_CheckRequest_Items_ID equals exci.ID
                                join isn in entities.Item_ShortName on ecrld.Item_ShortName_ID equals isn.ID
                               join it in entities.Items on isn.Item_ID equals it.ID into it1
                                from it in it1.DefaultIfEmpty()
                                join its in entities.Item_Status on isn.Item_Status_ID equals its.ID into its1
                                from its in its1.DefaultIfEmpty()
                                join exsm in entities.Ex_CheckRequset_Shipping_Method on ecrld.Ex_CheckRequset_Shipping_MethodID equals exsm.ID into exsm1
                                from exsm in exsm1.DefaultIfEmpty()

                                join pm in entities.Package_Material on excrilc.Package_Material_ID equals pm.ID into pm1
                                from pm in pm1.DefaultIfEmpty()
                                join pt in entities.Package_Type on excrilc.Package_Type_ID equals pt.ID into pt1
                                from pt in pt1.DefaultIfEmpty()
                                join sc in entities.A_SystemCode on exsm.containers_type_ID equals sc.Id into sc1
                                from sc in sc1.DefaultIfEmpty()
                         
                                
                                where ecrld.PlantCertificatesRequestsID == certificate_ID && ecr.IS_Lot == true
                                select new Ex_CertificatesRequestsLotDataCategory2
                                {
                                    Item_Name_Ar = it.Name_Ar,
                                    Item_ShortName = isn.ShortName_Ar,
                                    //items

                                    GrossWeight_Items = exci.GrossWeight,
                                    LotNumber = excrilc.Lot_Number,
                                    Net_Weight_Items = exci.Net_Weight,
                                    //items
                                    containers_type =sc.ValueName,

                                    ShipholdNumber = exsm.ShipholdNumber,
                                    ContainerNumber = exsm.ContainerNumber,
                                    NavigationalNumber = exsm.NavigationalNumber,

                                    GrossWeight_Lots = excrilc.GrossWeight,
                                    Net_Weight_Lots = excrilc.Net_Weight,


                                    Package_Count = excrilc.Package_Count,

                                    Package_Weight = excrilc.Package_Weight,
                                    Units_Number = excrilc.Units_Number,
                                    Package_Type = pt.Ar_Name,
                                    Package_Material = pm.Ar_Name,
                                    Package_Based_Weight = excrilc.Package_Based_Weight,
                                    Package_Net_Weight = excrilc.Package_Net_Weight,






                                }).ToList();

                
                data.LotData = _LotData;
                var Data_Treatment = (from exrc in entities.Ex_RequestCommittee
                                      join exrt in entities.Ex_Request_TreatmentData on exrc.ID equals exrt.Ex_RequestCommittee_ID
                                      where exrc.ExCheckRequest_ID == request.Ex_CheckRequest_ID
                                      select new CheckRequest_GetData_web_ResultDTO
                                      {
                                          User_Delegation_Date = exrc.Delegation_Date,
                                          treatments = exrt.TreatmentMaterial.TreatmentMethod.Ar_Name,
                                          treatment_Material_Name = exrt.TreatmentMaterial.Item.Name_Ar,
                                          TheDose = exrt.TheDose,
                                          Temperature = exrt.Temperature,
                                          Notes_Ar = exrt.Note,

                                      }).ToList();
                data.dataTreatment = Data_Treatment;
           

                //CertificatesFiles

                var Data_CertificatesFiles = (from cfr in entities.Ex_CertificatesRequestsFiles

                                              where cfr.Ex_CertificatesRequests.ID == certificate_ID
                                              select new CheckRequest_GetData_web_ResultDTO
                                              {
                                                  Id = cfr.ID,
                                                  PlantCertificatesRequestsID = cfr.PlantCertificatesRequestsID,
                                                  FilePath = cfr.FilePath,
                                                  AttachmentTableType = cfr.A_AttachmentTableType.Ar_Name,
                                                  CertifactionDate = cfr.User_Creation_Date,


                                              }).ToList();
                data.data_CertificatesFiles = Data_CertificatesFiles;




                var Data_CertificatesAddition = (from ca in entities.Ex_CertificateAddtion
                                                 join cert in entities.Ex_CertificatesRequests on ca.PlantCertificatesRequestsID equals cert.ID

                                                 where cert.ID == certificate_ID //&& ca.ISAccepted == true
                                                 select new CheckRequest_GetData_web_ResultDTO
                                                 {
                                                     Id = ca.ID,
                                                     Certificate_AddtionOriginal = ca.Certificate_AddtionOriginal,
                                                     Certificate_AddtionOriginalUpdate = ca.Certificate_AddtionOriginalUpdate,
                                                     Certificate_AddtionUpdateAdmin = ca.Certificate_AddtionUpdateAdmin,
                                                     AdminID = ca.AdminID,
                                                     //
                                                 }).ToList();
                data.data_CertificatesAddition = Data_CertificatesAddition;
                //CustomMessages
                var Data_CustomMessages = (from ca in entities.Ex_CheckRequest_Customs_Message
                                           join cert in entities.Ex_CertificatesRequests on ca.Ex_CertificatesRequests_ID equals cert.ID

                                           where  cert.ID == certificate_ID
                                           select new CheckRequest_Getdata_CustomMessagesDTO
                                           {
                                               CustomMessages_ID = ca.ID,
                                               Customs_Certificate_Number = ca.Customs_Certificate_Number,
                                               Certification_Date = ca.Certification_Date,
                                               Shipment_Date = ca.Shipment_Date,
                                               Manifest_Number = ca.Manifest_Number,
                                               Shipping_Agency = ca.ShippingAgency.Name_Ar,
                                               OperationType = ca.Im_OperationType == 17 ? "صادر" : "أخري",
                                               Certificate_Number_Each_Product = ca.Certificate_Number_Each_Product,
                                           }).FirstOrDefault();
                data.customMessages = Data_CustomMessages;


                // CertifacteFees
                var CertifacteFees = (from crp in entities.Ex_CertificatesRequestsPayments
                                      join crpd in entities.Ex_CertificatesRequestsPaymentsType on crp.Ex_CertificatesRequestsPaymentsType equals crpd.ID
                                      join cert in entities.Ex_CertificatesRequests on crp.PlantCertificatesRequestsID equals cert.ID

                                      where cert.ID == certificate_ID
                                      group crp by new
                                      {
                                          Ex_CertificatesRequestsPaymentsID = crp.ID,
                                          Ex_CertificatesRequestsPaymentsTypeValue = crp.Ex_CertificatesRequestsPaymentsType1.Name,
                                          Ex_CertificatesRequestsPaymentsTypeID = crp.Ex_CertificatesRequestsPaymentsType,
                                          CertificatesRequestsPaymentsValue = crp.Value,
                                          IsPayment = crp.IsPayment,
                                          CertificatesRequestsPaymentsUser_Creation_Date = crp.User_Creation_Date,

                                      } into grp
                                      select new CheckRequest_Getdata_CertifacteFeesDTO
                                      {

                                          Ex_CertificatesRequestsPaymentsID = grp.Key.Ex_CertificatesRequestsPaymentsID,
                                          Ex_CertificatesRequestsPaymentsTypeValue = grp.Key.Ex_CertificatesRequestsPaymentsTypeValue,
                                          Ex_CertificatesRequestsPaymentsTypeID = grp.Key.Ex_CertificatesRequestsPaymentsTypeID,
                                          CertificatesRequestsPaymentsValue = grp.Key.CertificatesRequestsPaymentsValue,
                                          IsPayment = grp.Key.IsPayment,
                                          CertificatesRequestsPaymentsUser_Creation_Date = grp.Key.CertificatesRequestsPaymentsUser_Creation_Date,
                                          AllCertificatesRequestsPaymentsValue = grp.Sum(q => q.Value),
                                      }).ToList();
                data.CertifacteFees = CertifacteFees;


                request .Shipping_PortName= uow.Repository<PortNational>().GetData().Where(s => s.ID == request.Shipping_Port).Select(a=>a.Name_Ar).FirstOrDefault();
                data.data = request;
                return uow.Repository<Object>().DataReturn((int)Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<object>().DataReturn((int)Enums.Error.Exception, null);
            }
        }
        //approve request
        public Dictionary<string, object> ApproveCheckReq(EX_CheckRequestDTO dto, List<string> Device_Info)
        {

            try
            {

                Ex_CheckRequest CModel = uow.Repository<Ex_CheckRequest>().Findobject(dto.ID);
                CModel.IsAccepted = dto.IsAccepted;
                CModel.IsActive = dto.IsAccepted;
                uow.SaveChanges();

                var empDTO = Mapper.Map<Ex_CheckRequest, EX_CheckRequestDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //refuse reason 
        public Dictionary<string, object> FillDrop_RefuseReason(int refuse, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Refuse_Reason>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true && (lab.IsExport == 81 || lab.IsExport == 82));
            if (refuse == 1)
            {
                data = data.Where(res => res.Refused_stopped == 84);
            }
            else
            {
                data = data.Where(res => res.Refused_stopped == 83);
            }
            var data2 = data.Select(c => new CustomOption
            {
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data2.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data2);
        }
        //public Dictionary<string, object> InsertReasons(ReasonsListReqIdDTO dto, List<string> Device_Info)
        //{
        //    try
        //    {
        //        EX_CheckRequest_RefuseReasonDTO rr = new EX_CheckRequest_RefuseReasonDTO();
        //        foreach (var id in dto.refuseReasonsIds)
        //        {

        //            rr.Ex_CheckRequest_Id = dto.checkReqId;
        //            rr.Refuse_Reason_Id = id;
        //            rr.User_Creation_Id = dto.User_Creation_Id;
        //            rr.User_Creation_Date = dto.User_Creation_Date;
        //            InsertReason(rr, Device_Info);
        //        }




        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.checkReqId);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        //public Dictionary<string, object> InsertReason(EX_CheckRequest_RefuseReasonDTO entity, List<string> Device_Info)
        //{
        //    try
        //    {

        //        var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_seq");
        //        entity.ID = idd;
        //        var CModel = Mapper.Map<Ex_CheckRequest_RefuseReason>(entity);

        //        uow.Repository<Ex_CheckRequest_RefuseReason>().InsertRecord(CModel);
        //        uow.SaveChanges();
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public Dictionary<string, object> saveItemFees(Items_checkReq item, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest_Items CModel = uow.Repository<Ex_CheckRequest_Items>().Findobject((long)item.Ex_Items_checkReqID);
                CModel.Fees = item.Fees;

                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, item.Ex_Items_checkReqID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> InsertReasons(ReasonsListReqIdDTO dto, List<string> Device_Info)
        {
            try
            {

                EX_CheckRequest_RefuseReasonDTO rr = new EX_CheckRequest_RefuseReasonDTO();
                foreach (var id in dto.refuseReasonsIds)
                {

                    rr.Ex_CheckRequest_Id = dto.checkReqId;
                    rr.Refuse_Reason_Id = id;

                    rr.User_Creation_Id = dto.User_Creation_Id;
                    rr.User_Creation_Date = dto.User_Creation_Date;
                    InsertReason(rr, dto, Device_Info);
                }
                #region log Action reject for Ex_CheckRequest

                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                dto2.ID_Table_Action = 54;
                // dto2.ID_TableActionValue = checkRequestId;
                dto2.Im_CheckRequest_ID = dto.checkReqId; ;
                dto2.User_Creation_Id = dto.User_Id;
                dto2.User_Creation_Date = DateTime.Now;
                dto2.NOTS = " تم الرفض علي طلب الفحص الصادر ";
                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                dto2.Type_log_ID = 135;  //system code for Update
                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                x.save_EX_CheckRequest_Log(dto2, Device_Info);

                #endregion



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.checkReqId);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertReason(EX_CheckRequest_RefuseReasonDTO entity, ReasonsListReqIdDTO dto, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_seq");
                entity.ID = idd;

                // var CModel = Mapper.Map<Ex_CheckRequest_RefuseReason>(entity);
                Ex_CheckRequest_RefuseReason CModel = new Ex_CheckRequest_RefuseReason();
                CModel.ID = idd;
                CModel.Refuse_Reason_Id = entity.Refuse_Reason_Id;
                CModel.Ex_CheckRequest_Id = entity.Ex_CheckRequest_Id;
                CModel.User_Creation_Id = entity.User_Creation_Id;
                CModel.User_Creation_Date = entity.User_Creation_Date;
                uow.Repository<Ex_CheckRequest_RefuseReason>().InsertRecord(CModel);
                Ex_CheckRequest x = uow.Repository<Ex_CheckRequest>().Findobject(dto.checkReqId);
                x.Notes_Reject = dto.Notes_Reject;
                uow.SaveChanges();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> AddationDeclartionِAdmin(CheckRequest_Getdata_AdditionDec_AdminDTO item, List<string> Device_Info)
        {
            try
            {
                //Ex_CertificateAddtion CModel = uow.Repository<Ex_CertificateAddtion>().Findobject((long)item.Ex_Items_checkReqID);
                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CertificateAddtion_SEQ");



                Ex_CertificateAddtion CModel = new Ex_CertificateAddtion();
                CModel.ID = idd;
                CModel.Certificate_AddtionUpdateAdmin = item.Certificate_AddtionUpdateAdmin;
                CModel.PlantCertificatesRequestsID = item.PlantCertificatesRequestsID;
                CModel.Date_Accepted = item.Date_Accepted;
                CModel.AdminID = item.AdminID;
                CModel.ISAccepted = item.ISAccepted;

                uow.Repository<Ex_CertificateAddtion>().InsertRecord(CModel);
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, item.PlantCertificatesRequestsID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> AcceptOrNotAcceptCertificates(AcceptCertificate accept, List<string> Device_Info)
        {

            try
            {

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("certificateId", SqlDbType.BigInt);
                paramters_Type.Add("ISAccepted", SqlDbType.Bit);
                paramters_Type.Add("User_Updation_Date", SqlDbType.SmallDateTime);
                paramters_Type.Add("User_Updation_Id", SqlDbType.BigInt);
                paramters_Type.Add("RejectReason", SqlDbType.NVarChar);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("certificateId", accept.certificateId.ToString());
                paramters_Data.Add("ISAccepted", accept.IsAccepted.ToString());
                paramters_Data.Add("User_Updation_Date", accept.User_Updation_Date.ToString("yyyy-MM-dd"));
                paramters_Data.Add("User_Updation_Id", accept.User_Updation_Id.ToString());
                if (accept.rejreasons_text!=null)
                {
                    paramters_Data.Add("RejectReason", accept.rejreasons_text.ToString());

                }
                else { paramters_Data.Add("RejectReason","" ); }
                

                var data = uow.Repository<object>().CallStored("Certificate_Accept", paramters_Type,
                paramters_Data, Device_Info).ToList();



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
