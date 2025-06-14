using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PlantQuar.DAL;
using System.Data;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO.Export_Certificate;
using System.Data.Entity;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using Privilages.DAL;

namespace PlantQuar.BLL.BLL.Export_Certificate
{/// <summary>
/// /mai
/// </summary>
    public class CertificateBLL
    {
        private UnitOfWork uow;
        public CertificateBLL()
        {
            uow = new UnitOfWork();
        }
        private List<string> GetLotsByExRequestItemId(long ExRequestItemId)
        {
            return uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().Where(x => x.Ex_CheckRequest_Items_ID == ExRequestItemId).Select(x => x.Lot_Number).ToList();
        }
        public bool GetAny(Ex_Im_CheckRequest_CertificateDTO entity)
        {
            // return uow.Repository<Ex_Im_CheckRequest_Certificate>().GetAny(p => p.Ex_Im_CheckRequest_Id == entity.Ex_Im_CheckRequest_Id && (entity.ID == 0 ? true : p.ID != entity.ID));
            return uow.Repository<Ex_CertificatesRequests>().GetAny(p => p.Ex_CheckRequest_ID == entity.Ex_Im_CheckRequest_Id && (entity.ID == 0 ? true : p.ID != entity.ID));
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="Device_Info"></param>
        /// <returns></returns>
        //public Dictionary<string, object> GetContainerCatagoryLotByExNumberold(long? certificateId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        //الاستورد ده مستخدم
        //        //fn_Importer_Exporter_GetData_Ar_En
        //        //oracle_GetEmployee_ByCommittee
        //        //fn_Item_GetData_Ar_En
        //        //GetLotData_ByLotId
        //        //fn_Getchecks_notification
        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        //        var Language_IsAr = 1;
        //        Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
        //        paramters_Type.Add("certificateId", SqlDbType.BigInt);

        //        paramters_Type.Add("Language_IsAr", SqlDbType.Int);
        //        Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
        //        paramters_Data.Add("certificateId", certificateId == null ? "" : certificateId.ToString());

        //        paramters_Data.Add("Language_IsAr", Language_IsAr == null ? "" : Language_IsAr.ToString());

        //        var request = uow.Repository<CheckRequest_GetData_web_ResultDTO>().CallStored("GetCertificateData", paramters_Type,
        //        paramters_Data, Device_Info).FirstOrDefault();

        //        //var data = (from ecrld in entities.Ex_CertificatesRequestsLotData
        //        //                join ecr in entities.Ex_CertificatesRequests on ecrld.PlantCertificatesRequestsID equals ecr.ID
        //        //                join excrilc in entities.Ex_CheckRequest_Items_Lot_Category on ecrld.LotID equals excrilc.ID
        //        //                join isn in entities.Item_ShortName on ecrld.Item_ShortName_ID equals isn.ID
        //        //                join it in entities.Items on isn.Item_ID equals it.ID
        //        //                join its in entities.Item_Status on isn.Item_Status_ID equals its.ID
        //        //                where ecrld.PlantCertificatesRequestsID == certificateId && ecr.IS_Lot == true
        //        //                select new Ex_CertificatesRequestsLotDataCategory2
        //        //                {
        //        //                    CertificatesRequest_ID = ecrld.ID,
        //        //                    LotID = ecrld.LotID,
        //        //                    Item_ShortName_ID = ecrld.Item_ShortName_ID,
        //        //                    Item_ID = isn.Item_ID,
        //        //                    Item_Status_ID = isn.Item_Status_ID,
        //        //                    Item_Status_Ar_Name = its.Ar_Name,
        //        //                    Item_Name_Ar = it.Name_Ar,
        //        //                    GrossWeight = excrilc.GrossWeight,
        //        //                    Net_Weight = excrilc.Net_Weight

        //        //                }).ToList();
        //        //request.LotData = data;
        //        //hadder 
        //        var _Additional_Declaretion = uow.Repository<Ex_CertificateAddtionUser>().GetData()//.Include(a => a.Ex_CertificateAddtionUser).
        //             .Where(i => i.PlantCertificatesRequestsID == certificateId && i.Ex_CertificatesRequests.IS_Additional_Declaretion == true).
        //             Select(v => new Ex_CertificateAddtionUserDTO
        //             {
        //                 ID = v.ID,
        //                 Certificate_AddtionText = v.Certificate_AddtionText,
        //             }).ToList();

        //        request.dataAddtion = _Additional_Declaretion;


        //        var _LotData = (from ecrld in entities.Ex_CertificatesRequestsLotData
        //                        join ecr in entities.Ex_CertificatesRequests on ecrld.PlantCertificatesRequestsID equals ecr.ID
        //                        join excrilc in entities.Ex_CheckRequest_Items_Lot_Category on ecrld.LotID equals excrilc.ID
        //                        join isn in entities.Item_ShortName on ecrld.Item_ShortName_ID equals isn.ID
        //                        join it in entities.Items on isn.Item_ID equals it.ID
        //                        join its in entities.Item_Status on isn.Item_Status_ID equals its.ID
        //                        where ecrld.PlantCertificatesRequestsID == certificateId && ecr.IS_Lot == true
        //                        select new Ex_CertificatesRequestsLotDataCategory2
        //                        {
        //                            CertificatesRequest_ID = ecrld.ID,
        //                            LotID = ecrld.LotID,
        //                            Item_ShortName_ID = ecrld.Item_ShortName_ID,
        //                            Item_ID = isn.Item_ID,
        //                            Item_Status_ID = isn.Item_Status_ID,
        //                            Item_Status_Ar_Name = its.Ar_Name,
        //                            Item_Name_Ar = it.Name_Ar,
        //                            GrossWeight = excrilc.GrossWeight,
        //                            Net_Weight = excrilc.Net_Weight

        //                        }).ToList();
        //        request.LotData = _LotData;
        //        //end hadeer

        //        //noura 

        //        //Treatment
        //        var Data_Treatment = (from exrc in entities.Ex_RequestCommittee
        //                              join exrt in entities.Ex_Request_TreatmentData on exrc.ID equals exrt.Ex_RequestCommittee_ID
        //                              where exrc.ExCheckRequest_ID == request.Ex_CheckRequest_ID
        //                              select new CheckRequest_GetData_web_ResultDTO
        //                              {
        //                                  User_Delegation_Date = exrc.Delegation_Date,
        //                                  treatments = exrt.TreatmentMaterial.TreatmentMethod.Ar_Name,
        //                                  treatment_Material_Name = exrt.TreatmentMaterial.Item.Name_Ar,
        //                                  TheDose = exrt.TheDose,
        //                                  Temperature = exrt.Temperature,
        //                                  Notes_Ar = exrt.Note,

        //                              }).ToList();
        //        request.dataTreatment = Data_Treatment;


        //        //CertificatesFiles

        //        var Data_CertificatesFiles = (from cfr in entities.Ex_CertificatesRequestsFiles

        //                                      where cfr.Ex_CertificatesRequests.ID == certificateId
        //                                      select new CheckRequest_GetData_web_ResultDTO
        //                              {
        //                                  Id=cfr.ID,
        //                                  PlantCertificatesRequestsID=cfr.PlantCertificatesRequestsID,
        //                                  FilePath=cfr.FilePath




        //                              }).ToList();
        //        request.data_CertificatesFiles = Data_CertificatesFiles;




        //        //end noura


        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public Dictionary<string, object> GetContainerCatagoryLotByExNumber(long? certificateId, List<string> Device_Info)
        {
            try
            {
                //الاستورد ده مستخدم
                //fn_Importer_Exporter_GetData_Ar_En
                //oracle_GetEmployee_ByCommittee
                //fn_Item_GetData_Ar_En
                //GetLotData_ByLotId
                //fn_Getchecks_notification
                var Language_IsAr = 1;
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities entitiesPriv = new dbPrivilageEntities();
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("certificateId", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("certificateId", certificateId == null ? "" : certificateId.ToString());

                var request = uow.Repository<CheckRequest_GetData_web_ResultDTO>().CallStored("GetCertificateData", paramters_Type,
                paramters_Data, Device_Info).FirstOrDefault();
               

                CertificateDTO data = new CertificateDTO();
              
                var _Additional_Declaretion = uow.Repository<Ex_CertificateAddtionUser>().GetData()//.Include(a => a.Ex_CertificateAddtionUser).
                     .Where(i => i.PlantCertificatesRequestsID == certificateId && i.Ex_CertificatesRequests.IS_Additional_Declaretion == true).
                     Select(v => new Ex_CertificateAddtionUserDTO
                     {
                         ID = v.ID,
                         Certificate_AddtionText = v.Certificate_AddtionText,
                     }).ToList();

                data.dataAddtion = _Additional_Declaretion;

                //var _LotData = uow.Repository<Ex_CertificatesRequestsLotData>().GetData().Include(a=>a.Ex_CheckRequest_Items_Lot_Category)
                //   .Where(i => i.PlantCertificatesRequestsID == certificateId && i.Ex_CertificatesRequests.IS_Lot == true).
                //   Select(v => new Ex_CertificatesRequestsLotData
                //   {
                //       ID = v.ID,
                //       LotID = v.LotID,
                //       Item_ShortName_ID = v.Item_ShortName_ID,
                //   }).ToList();
                //var _LotData = (from ecrld in entities.Ex_CertificatesRequestsLotData
                //                join ecr in entities.Ex_CertificatesRequests on ecrld.PlantCertificatesRequestsID equals ecr.ID
                //                join excrilc in entities.Ex_CheckRequest_Items_Lot_Category on ecrld.LotID equals excrilc.ID
                //                join isn in entities.Item_ShortName on ecrld.Item_ShortName_ID equals isn.ID
                //                join it in entities.Items on isn.Item_ID equals it.ID
                //                join its in entities.Item_Status on isn.Item_Status_ID equals its.ID


                //                join icg in entities.ItemCategories_Group on it.ID equals icg.Item_ID into icg1
                //                from icg in icg1.DefaultIfEmpty()
                //                where ecrld.PlantCertificatesRequestsID == certificateId && ecr.IS_Lot == true
                //                group excrilc by new
                //                {
                //                    CertificatesRequest_ID = ecrld.ID,
                //                    LotID = ecrld.LotID,
                //                    Item_ShortName_ID = ecrld.Item_ShortName_ID,
                //                    Item_ID = isn.Item_ID,
                //                    Item_Status_ID = isn.Item_Status_ID,
                //                    Item_Status_Ar_Name = its.Ar_Name,
                //                    Item_Name_Ar = it.Name_Ar,
                //                    Item_Status_En_Name = its.En_Name,
                //                    Item_Name_En = it.Name_En,

                //                    Scientific_Name = it.Scientific_Name,

                //                    ItemCategories_GroupNameAR = icg.Name_Ar,
                //                    ItemCategories_GroupNameEN = icg.Name_En


                //                } into grp
                //                select new Ex_CertificatesRequestsLotDataCategory2
                //                {
                //                    CertificatesRequest_ID = grp.Key.CertificatesRequest_ID,
                //                    LotID = grp.Key.LotID,
                //                    Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                //                    Item_ID = grp.Key.Item_ID,
                //                    Item_Status_ID = grp.Key.Item_Status_ID,
                //                    Item_Status_Ar_Name = grp.Key.Item_Status_Ar_Name,
                //                    Item_Name_Ar = grp.Key.Item_Name_Ar,
                //                    Item_Status_En_Name = grp.Key.Item_Status_En_Name,
                //                    Item_Name_En = grp.Key.Item_Name_En,
                //                    GrossWeight = grp.Sum(q => q.GrossWeight),
                //                    Net_Weight = grp.Sum(q => q.Net_Weight),
                //                    Package_Count = grp.Sum(q => q.Package_Count),
                //                    Scientific_Name = grp.Key.Scientific_Name,

                //                    ItemCategories_GroupNameAR = grp.Key.ItemCategories_GroupNameAR,
                //                    ItemCategories_GroupNameEN = grp.Key.ItemCategories_GroupNameEN

                //                }).Distinct().ToList();

                var _LotData = (from ecrld in entities.Ex_CertificatesRequestsLotData
                                join ecr in entities.Ex_CertificatesRequests on ecrld.PlantCertificatesRequestsID equals ecr.ID
                                join excrilc in entities.Ex_CheckRequest_Items_Lot_Category on ecrld.LotID equals excrilc.ID
                                join isn in entities.Item_ShortName on ecrld.Item_ShortName_ID equals isn.ID
                                join it in entities.Items on isn.Item_ID equals it.ID
                                join its in entities.Item_Status on isn.Item_Status_ID equals its.ID
                                
                               
                                //join icg in entities.ItemCategories_Group on it.ID equals icg.Item_ID into icg1
                                //from icg in icg1.DefaultIfEmpty()
                                join pm in entities.Package_Material on excrilc.Package_Material_ID equals pm.ID into pm1
                                from pm in pm1.DefaultIfEmpty()
                                where ecrld.PlantCertificatesRequestsID == certificateId && ecr.IS_Lot == true

                                group it by new
                                {
                                    CertificatesRequest_ID = ecrld.ID,
                                    LotID = ecrld.LotID,
                                    LotNumber = excrilc.Lot_Number,
                                    Item_ShortName_ID = ecrld.Item_ShortName_ID,
                                    Item_ID = isn.Item_ID,
                                    Item_Status_ID = isn.Item_Status_ID,
                                    Item_Status_Ar_Name = its.Ar_Name,
                                    Item_Name_Ar = it.Name_Ar,
                                    Item_Status_En_Name = its.En_Name,
                                    Item_Name_En = it.Name_En,
                                    Scientific_Name = it.Scientific_Name,
                                    GrossWeight= excrilc.GrossWeight,
                                    Net_Weight = excrilc.Net_Weight,
                                    Package_Count = excrilc.Package_Count,
                                    //ItemCategories_GroupNameAR = icg.Name_Ar,
                                    //ItemCategories_GroupNameEN = icg.Name_En
                                    Package_Material = pm.Ar_Name

                                } into grp
                                select new Ex_CertificatesRequestsLotDataCategory2
                                {
                                    LotNumber = grp.Key.LotNumber,
                                    CertificatesRequest_ID = grp.Key.CertificatesRequest_ID,
                                    LotID = grp.Key.LotID,
                                    Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                    Item_ID = grp.Key.Item_ID,
                                    Item_Status_ID = grp.Key.Item_Status_ID,
                                    Item_Status_Ar_Name = grp.Key.Item_Status_Ar_Name,
                                    Item_Name_Ar = grp.Key.Item_Name_Ar,
                                    Item_Status_En_Name = grp.Key.Item_Status_En_Name,
                                    Item_Name_En = grp.Key.Item_Name_En,
                                    GrossWeight = grp.Key.GrossWeight,
                                    Net_Weight = grp.Key.Net_Weight,
                                    Package_Count = grp.Key.Package_Count,
                                    Scientific_Name = grp.Key.Scientific_Name,
                                    Package_Material = grp.Key.Package_Material
                                    //ItemCategories_GroupNameAR = grp.Key.ItemCategories_GroupNameAR,
                                    //ItemCategories_GroupNameEN = grp.Key.ItemCategories_GroupNameEN

                                }).Distinct().ToList();
                //group b
                //select new  Ex_CertificatesRequestsLotDataCategory2
                //{
                //    CertificatesRequest_ID = ecrld.ID,
                //    LotID = ecrld.LotID,
                //    LotNumber= excrilc.Lot_Number,
                //    Item_ShortName_ID = ecrld.Item_ShortName_ID,
                //    Item_ID = isn.Item_ID,
                //    Item_Status_ID = isn.Item_Status_ID,
                //    Item_Status_Ar_Name = its.Ar_Name,
                //    Item_Name_Ar = it.Name_Ar,
                //    Item_Status_En_Name = its.En_Name,
                //    Item_Name_En = it.Name_En,
                //    GrossWeight = excrilc.GrossWeight,
                //    Net_Weight = excrilc.Net_Weight,
                //    Scientific_Name = it.Scientific_Name,
                //    Package_Count = excrilc.Package_Count,
                //    //ItemCategories_GroupNameAR= icg.Name_Ar,
                //    //ItemCategories_GroupNameEN= icg.Name_En,
                //    Package_Material= pm.Ar_Name

                //}).GroupBy(a=>a.Item_Name_Ar).Distinct().ToList();
                data.LotData = _LotData;
     



                //Treatment
                var Data_Treatment = (from exrc in entities.Ex_RequestCommittee
                                      join exrt in entities.Ex_Request_TreatmentData on exrc.ID equals exrt.Ex_RequestCommittee_ID
                                      where exrc.ExCheckRequest_ID == request.Ex_CheckRequest_ID
                                      select new CheckRequest_GetData_web_ResultDTO
                                      {
                                          User_Delegation_Date = exrc.Delegation_Date,
                                          treatments = exrt.TreatmentMaterial.TreatmentMethod.Ar_Name,
                                          treatment_Material_Name = exrt.TreatmentMaterial.Item.Name_Ar,
                                          treatmentsEN = exrt.TreatmentMaterial.TreatmentMethod.En_Name,
                                          treatment_Material_NameEN = exrt.TreatmentMaterial.Item.Name_En,
                                          TheDose = exrt.TheDose,
                                          Temperature = exrt.Temperature,
                                          Exposure_Day =    exrt.Exposure_Day,
                                          Exposure_Minute =    exrt.Exposure_Minute,
                                          Exposure_Hour =    exrt.Exposure_Hour,
                                          Notes_Ar = exrt.Note,
                                          //Notes_Ar = exrt.Note,

                                      }).ToList();
                data.dataTreatment = Data_Treatment;

                request.Shipping_PortName = uow.Repository<PortNational>().GetData().Where(s => s.ID == request.Shipping_Port).Select(a => a.Name_Ar).FirstOrDefault();

                //CertificatesFiles

                var Data_CertificatesFiles = (from cfr in entities.Ex_CertificatesRequestsFiles

                                              where cfr.Ex_CertificatesRequests.ID == certificateId
                                              select new CheckRequest_GetData_web_ResultDTO
                                              {
                                                  Id = cfr.ID,
                                                  PlantCertificatesRequestsID = cfr.PlantCertificatesRequestsID,
                                                  FilePath = cfr.FilePath




                                              }).ToList();

                data.data_CertificatesFiles = Data_CertificatesFiles;
                var CommitteeEmployeeID = (from ce in entities.CommitteeEmployees 
                                             join rc in entities.Ex_RequestCommittee on ce.Committee_ID equals rc.ID
                                        
                                              where rc.ExCheckRequest_ID== request.Ex_CheckRequest_ID && ce.ISAdmin == false
                                             select new CommitteeEmployeeName
                                             {
                                                 Employee_Id = ce.Employee_Id,
                                                 Committee_ID=ce.Committee_ID,
                                                


                                             }).ToList();
                var CommitteeEmployeeName = (from ce in CommitteeEmployeeID
                                          
                                           join un in entitiesPriv.PR_User on ce.Employee_Id equals un.Id
                                          // where rc.ExCheckRequest_ID == request.Ex_CheckRequest_ID && ce.ISAdmin == false
                                           select new CommitteeEmployeeName
                                           {
                                             
                                               FullName = un.FullName,
                                               FullNameEn = un.FullNameEn,


                                           }).ToList();

                data.committeeEmployeeName = CommitteeEmployeeName;




                var Data_CertificatesAddition = (from ca in entities.Ex_CertificateAddtion
                                                 join cert in entities.Ex_CertificatesRequests on ca.PlantCertificatesRequestsID equals cert.ID

                                                 where ca.ISAccepted == true && cert.ID == certificateId
                                                 select new CheckRequest_GetData_web_ResultDTO
                                                 {
                                                     Id = ca.ID,
                                                     Certificate_AddtionOriginal = ca.Certificate_AddtionOriginal,
                                                     Certificate_AddtionOriginalUpdate = ca.Certificate_AddtionOriginalUpdate,
                                                     Certificate_AddtionUpdateAdmin = ca.Certificate_AddtionUpdateAdmin,
                                                 }).ToList();
                data.data_CertificatesAddition = Data_CertificatesAddition;


                if (request.Transit_Country != null)
                {
                    request.Transit_CountryName = uow.Repository<Country>().GetData().Where(s => s.ID == request.Transit_Country).Select(a => a.Ar_Name).FirstOrDefault();
                }
                if (request.Shipping_Port != null)
                {
                    request.Shipping_PortName = uow.Repository<PortNational>().GetData().Where(s => s.ID == request.Shipping_Port).Select(a => a.Name_Ar).FirstOrDefault();
                    request.Shipping_PortNameEN = uow.Repository<PortNational>().GetData().Where(s => s.ID == request.Shipping_Port).Select(a => a.Name_En).FirstOrDefault();
                }
                data.data = request;
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //public Dictionary<string, object> CreateCertificate()
        //{
        //    //try
        //    //{

        //    //if (!GetAny(model))
        //    //{
        //    var Renderer = new IronPdf.HtmlToPdf();
        //    var PDF = Renderer.RenderHtmlAsPdf("<html><h1>نهلاخ</h1></html>");
        //    var OutputPath = "D:\\work\\tfs_5-2019\\PlantQuar.BLL\\BLL\\NewFolder1\\mai";
        //    PDF.SaveAs(OutputPath);
        //    //var CModel = Mapper.Map<Ex_Im_CheckRequest_Certificate>(model);
        //    //CModel.FilePath = OutputPath;
        //    //uow.Repository<Ex_Im_CheckRequest_Certificate>().InsertRecord(CModel);
        //    //uow.SaveChanges();

        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
        //    //    }
        //    //    else
        //    //    {
        //    //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //    //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    //}
        //}

        public Dictionary<string, object> printCertificates(AcceptCertificate accept, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("certificateId", SqlDbType.BigInt);
                paramters_Type.Add("User_Updation_Date", SqlDbType.SmallDateTime);
                paramters_Type.Add("User_Updation_Id", SqlDbType.BigInt);
                paramters_Type.Add("ISPrint", SqlDbType.Int);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("certificateId", accept.certificateId.ToString());
                paramters_Data.Add("User_Updation_Date", accept.User_Updation_Date.ToString("yyyy-MM-dd"));
                paramters_Data.Add("User_Updation_Id", accept.User_Updation_Id.ToString());
                paramters_Data.Add("ISPrint", accept.ISPrint.ToString());

                var data = uow.Repository<object>().CallStored("Certificate_Printed", paramters_Type,
                paramters_Data, Device_Info).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        private string GetNotAliveName(long prodPlant_ID)
        {
            return uow.Repository<LiableItem>().GetData().Where(x => x.ID == prodPlant_ID && x.IsAlive == 0).Select(x => x.Name_Ar).SingleOrDefault();
        }

        private string GetLiablePurpose(byte? purpose_ID)
        {
            return uow.Repository<Item_Purpose>().GetData().Where(x => x.ID == purpose_ID.Value).Select(x => x.Ar_Name).SingleOrDefault();
        }

        private string GetLiableStatus(long liableStatus_ID)
        {
            return uow.Repository<LiableItems_Status>().GetData().Where(x => x.ID == liableStatus_ID).Select(x => x.Name_Ar).SingleOrDefault();
        }

        private string GetAlivePhaseName(int? biologicalPhase_ID)
        {
            return uow.Repository<Biological_Phase>().GetData().Where(x => x.ID == biologicalPhase_ID.Value).Select(x => x.Name_Ar).SingleOrDefault();
        }

        private string GetAliveName(long prodPlant_ID)
        {
            return uow.Repository<LiableItem>().GetData().Where(x => x.ID == prodPlant_ID && x.IsAlive == 1).Select(x => x.Name_Ar).SingleOrDefault();
        }

        private string GetProductName(long prodPlant_ID)
        {
            return null;
            //return uow.Repository<Product>().GetData().Where(x => x.ID == prodPlant_ID).Select(x => x.Name_Ar).SingleOrDefault();
        }
        private string GetPlantName(long prodPlant_ID)
        {
            return uow.Repository<Item>().GetData().Where(x => x.ID == prodPlant_ID).Select(x => x.Name_Ar).SingleOrDefault();
        }
    }
}