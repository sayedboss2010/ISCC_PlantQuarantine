using AutoMapper;
using Newtonsoft.Json;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;




namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
    public class EX_CheckRequestCertificatesBLL
    {
        private UnitOfWork uow;
        private dbPrivilageEntities priv = new dbPrivilageEntities();
        public EX_CheckRequestCertificatesBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetExCheckRequestDetails
         (string EXCheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];


                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();

                paramters_Type.Add("lang", SqlDbType.Int);
                paramters_Type.Add("CheckRequest_Number", SqlDbType.NVarChar);


                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("lang", lang);
                paramters_Data.Add("CheckRequest_Number", EXCheckRequest_Number);
                var CheckRequestDetails = uow.Repository<EX_CheckRequestCertificatesDTO>().CallStored("Ex_CheckRequestStatementDetails", paramters_Type,
                    paramters_Data, Device_Info).ToList();
                var _EXCheckRequest_DataId = (Int64)CheckRequestDetails.FirstOrDefault().Ex_CheckRequestData_ID;
                //CheckRequestDetails.FirstOrDefault().TransportCountryList = (from exrd in entities.Ex_CheckRequest_Data
                //                                                             join exrp in entities.Ex_CheckRequest_Port on new { a = (long?)exrd.ID, b = 10 } equals new { a = exrp.Ex_CheckRequest_Data_ID, b = exrp.ReqPortType_ID } into exrp1
                //                                                             from exrp in exrp1.DefaultIfEmpty()
                //                                                             join pi in entities.Port_International on exrp.Port_ID equals pi.ID into pi1
                //                                                             from pi in pi1.DefaultIfEmpty()

                //                                                             where exrd.ID == _EXCheckRequest_DataId
                //                                                           select new TransportCountryListCertificate
                //                                                             {
                //                                                                 TransportCountryID = exrd.ExportCountry_Id,
                //                                                                 TransportPortType = pi.Port_Type.Name_Ar,
                //                                                                 TransportPortName = pi.Name_Ar

                //                                                             }).ToList();
                CheckRequestDetails.FirstOrDefault().TransiteCountryList = (from exrd in entities.Ex_CheckRequest_Data
                                                                            join exrp in entities.Ex_CheckRequest_Port on new { a = (long?)exrd.ID, b = 11 } equals new { a = exrp.Ex_CheckRequest_Data_ID, b = exrp.ReqPortType_ID } into exrp1
                                                                            from exrp in exrp1.DefaultIfEmpty()
                                                                            join pi in entities.Port_International on exrp.Port_ID equals pi.ID into pi1
                                                                            from pi in pi1.DefaultIfEmpty()
                                                                            join c in entities.Countries on exrd.TransitCountry_Id equals c.ID into c1
                                                                            from c in c1.DefaultIfEmpty()
                                                                            where exrd.ID == _EXCheckRequest_DataId
                                                                            select new TransiteCountryListCertificate
                                                                            {
                                                                                TransiteCountryID = exrd.TransitCountry_Id,
                                                                                TransitPortType = pi.Port_Type.Name_Ar,
                                                                                TransitPortName = pi.Name_Ar,
                                                                                TransitCountry = c.Ar_Name

                                                                            }).ToList();



                //                 }
                foreach (var check in CheckRequestDetails)
                {
                    //Add Company Activity
                    var CompanyActivities_Details = (from ca in entities.CompanyActivities
                                                         //join cat in entities.Enrollment_type on ca.Enrollment_type_ID equals cat.ID
                                                     where ca.Company_ID == check.Importer_ID
                                                     && ca.IsActive == true
                                                     select new CompanyActivitysCertificateDTO
                                                     {

                                                         CompActivityType__Name = lang == "1" ? ca.A_SystemCode.ValueName : ca.A_SystemCode.ValueNameEN,
                                                         Enrollment_Name = ca.Enrollment_Name,
                                                         Enrollment_Number = ca.Enrollment_Number,
                                                         Enrollment_Start = ca.Enrollment_Start,
                                                         Enrollment_End = ca.Enrollment_End,
                                                         Enrollment_type_Name = lang == "1" ? ca.Enrollment_type.Ar_Name : ca.Enrollment_type.En_Name,

                                                     }).ToList();
                    check._CompanyActivitys = CompanyActivities_Details;
                    check.ExportsContacts = uow.Repository<Ex_ContactData>()
                      .GetData().Include(f => f.ContactType).
                 Where(a => a.Exporter_ID == check.Importer_ID && a.ExporterType_Id == 6).
                 Select(a => new ContactTypeCertificateDTO
                 {
                     Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
                     Value = a.Value
                 }).ToList();
                    /////////////////////////////




                    //Items
                    var itemss = uow.Repository<Ex_CheckRequest_Items>().GetData().Where(i => i.Ex_CheckRequest_ID == check.Ex_CheckRequest_ID).
                        Select(v => new Items_checkReqCertificate
                        {
                            //eslam 
                            Ex_Items_checkReqID = v.ID,
                            Size = v.Size,
                            Package_Count = v.Package_Count,
                            Package_Weight = v.Package_Weight,
                            Units_Number = v.Units_Number,
                            packageTypeID = v.Package_Type_ID,
                            GrossWeight = v.GrossWeight,
                            Net_Weight = v.Net_Weight,
                            Fees = v.Fees,
                            Item_ShortName_ID = v.Item_ShortName_ID,
                            Order_TextItem = v.Order_Text,
                            SubPart_id = v.SubPart_id,
                            Package_Material_ID = v.Package_Material_ID,
                            Is_LotDivision = v.Is_LotDivision,
                            Accept_Date = v.Accept_Date,
                            IsAccepted = v.IsAccepted,
                            FarmsData = v.FarmsData.Name_Ar,
                            ItemCategoryName = v.ItemCategory.Name_Ar,
                            Agriculture_Hand = v.Agriculture_Hand,
                            FarmCode_14 = v.FarmsData.FarmCode_14
                        }).Distinct().ToList();
                    //end Items AND GET DATE FOR ALL LOTS AND ITEMS




                    foreach (var itt in itemss)
                    {

                        if (itt.Item_ShortName_ID != null)
                        {
                            var ism = uow.Repository<Item_ShortName>().GetData().Where(i => i.ID == itt.Item_ShortName_ID && i.User_Deletion_Id == null).FirstOrDefault();
                            itt.ItemShortNameAr = ism.ShortName_Ar;
                            itt.ItemShortNameEn = ism.ShortName_En;



                            if (itt.Purpose != null)
                            {
                                itt.Purpose = (lang == "1" ? ism.Item_Purpose.Ar_Name : ism.Item_Purpose.En_Name);
                            }
                            if (itt.Status != null)
                            {
                                itt.Status = (lang == "1" ? ism.Item_Status.Ar_Name : ism.Item_Status.En_Name);
                            }
                            if (itt.ItemName != null)
                            {
                                itt.ItemName = (lang == "1" ? ism.Item.Name_Ar : ism.Item.Name_En);

                            }
                            if (itt.SubPart_Name != null)
                            {
                                itt.SubPart_Name = (lang == "1" ? ism.SubPart.Name_Ar : ism.SubPart.Name_En);

                            }
                        }


                        var itemShortNameId = itt.Item_ShortName_ID;//13;//
                        var itemCategories_ID = itt.ItemCategory_ID;

                        //Eslam Get Constrain   
                        #region getConstrain

                        var ExportCountryId = CheckRequestDetails.FirstOrDefault().ExportCountryID;//.Select(a => a.TransportCountryID).FirstOrDefault();
                        var TransiteCountryId = CheckRequestDetails.FirstOrDefault().TransiteCountryList.Select(a => a.TransiteCountryID).FirstOrDefault();
                        var dataConstrains = (from Ecc in entities.Ex_CountryConstrain
                                              where Ecc.User_Deletion_Id == null
                                              && Ecc.IsActive == true
                                              && Ecc.User_Deletion_Date == null
                                              && Ecc.Import_Country_ID == ExportCountryId
                                              && Ecc.Item_ShortName_id == itemShortNameId

                                              && Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)

                                              && Ecc.User_Updation_Date == null
                                              && Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)
                                              select new Ex_CountryConstrainCertificateDTO
                                              {
                                                  ID = Ecc.ID,
                                                  IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                                                  IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                                                  IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                                                  ItemCategories_ID = Ecc.ItemCategories_ID
                                              }).FirstOrDefault();

                        if (dataConstrains == null && TransiteCountryId == 0)
                        {
                            dataConstrains = (from Ecc in entities.Ex_CountryConstrain
                                              where Ecc.User_Deletion_Id == null
                                              && Ecc.IsActive == true
                                              && Ecc.User_Deletion_Date == null
                                              && Ecc.Import_Country_ID == 0
                                              && Ecc.Item_ShortName_id == itemShortNameId

                                              && Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)

                                              && Ecc.User_Updation_Date == null
                                              && Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)
                                              select new Ex_CountryConstrainCertificateDTO
                                              {
                                                  ID = Ecc.ID,
                                                  IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                                                  IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                                                  IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                                                  ItemCategories_ID = Ecc.ItemCategories_ID
                                              }).FirstOrDefault();
                        }


                        if (dataConstrains != null)
                        {
                            var CountryConstrain_ID = dataConstrains.ID;
                            var Constrain_Text = (from CCt in entities.Ex_CountryConstrain_Text
                                                  join Txt in entities.EX_Constrain_Text on CCt.EX_Constrain_Text_ID equals Txt.ID
                                                  join Typ in entities.EX_Constrain_Country_Item on Txt.EX_Constrain_Country_Item_ID equals Typ.ID
                                                  where CCt.User_Deletion_Id == null
                                                   && CCt.User_Deletion_Date == null
                                                && CCt.CountryConstrain_ID == CountryConstrain_ID
                                                  select new Ex_CountryConstrain_TextCertificateDTO
                                                  {
                                                      EX_Constrain_Text_ID = Txt.ID,
                                                      Ar_Name_Constrain_Type = Typ.Ar_Name,
                                                      En_Name_Constrain_Type = Typ.En_Name,
                                                      ConstrainText_Ar = Txt.ConstrainText_Ar,
                                                      ConstrainText_En = Txt.ConstrainText_En,
                                                      IsCertificate_Addtion = (bool)Txt.IsCertificate_Addtion,
                                                      InSide_Certificate_Ar = Txt.InSide_Certificate_Ar,
                                                      InSide_Certificate_En = Txt.InSide_Certificate_En,
                                                  }).ToList();

                            var Constrain_Analysis = (
                                     from CCA in entities.Ex_CountryConstrain_AnalysisLabType
                                     join At in entities.AnalysisTypes on CCA.AnalysisTypeID equals At.ID
                                     where CCA.User_Deletion_Id == null
                                     //  && CCA.IsActive == true
                                     && CCA.User_Deletion_Date == null
                                     && CCA.CountryConstrain_ID == CountryConstrain_ID
                                   && At.User_Deletion_Id == null
                                   && At.User_Deletion_Date == null

                                     select new Ex_CountryConstrain_AnalysisLabTypeCertificateDTO
                                     {
                                         AnalysisTypeID = CCA.AnalysisTypeID,
                                         TypeName_Ar = At.Name_Ar,
                                         TypeName_En = At.Name_En,
                                         ExConstrainsLabsAndTypID = CCA.ID
                                     }).ToList();

                            var Constrain_ArrivalPort = (from CCA in entities.Ex_CountryConstrain_ArrivalPort
                                                         join pil in entities.Port_International on CCA.Port_International_Id equals pil.ID
                                                         join Ci in entities.Countries on pil.Country_ID equals Ci.ID
                                                         join v in entities.Port_Type on pil.PortTypeID equals v.ID
                                                         where CCA.User_Deletion_Id == null
                                                         && CCA.User_Deletion_Date == null
                                                         && CCA.Ex_CountryConstrain_Id == CountryConstrain_ID
                                                       && pil.User_Deletion_Date == null
                                                       && pil.User_Deletion_Id == null
                                                       && Ci.User_Deletion_Id == null
                                                       && Ci.User_Deletion_Date == null
                                                       && v.User_Deletion_Id == null
                                                       && v.User_Deletion_Date == null
                                                         select new Ex_CountryConstrain_ArrivalPortCertificateDTO
                                                         {
                                                             AirPortName_Ar = v.Name_Ar,
                                                             AirPortName_En = v.Name_En,
                                                             CountryName_Ar = pil.Name_Ar,
                                                             CountryLabName_En = pil.Name_En,
                                                             ExConstrainsAirPortAndCountryID = pil.ID
                                                         }).ToList();


                            var Constrain_Treatment = (from ect in entities.Ex_CountryConstrain_Treatment
                                                       where ect.User_Deletion_Id == null
                                                       && ect.User_Deletion_Date == null
                                                       && ect.CountryConstrain_ID == CountryConstrain_ID
                                                     && ect.User_Deletion_Date == null
                                                     && ect.User_Deletion_Id == null
                                                       select new Ex_CountryConstrain_TreatmentCertificateDTO
                                                       {
                                                           TreatmentMethod_Ar_Name = ect.TreatmentMethod.Ar_Name,
                                                           TreatmentMethod_En_Name = ect.TreatmentMethod.En_Name,

                                                           TreatmentType_Ar_Name = ect.TreatmentMethod.TreatmentType.Ar_Name,
                                                           TreatmentType_En_Name = ect.TreatmentMethod.TreatmentType.En_Name,

                                                           TreatmentMainType_Ar_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.Ar_Name,
                                                           TreatmentMainType_En_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.En_Name,
                                                           IS_Optional = ect.IS_Optional,
                                                           Parent_ID = ect.ID,
                                                           TreatmentMethods_ID = ect.TreatmentMethods_ID
                                                       }).ToList();
                            //int c = 10;
                            dataConstrains.AnalysisLabType = Constrain_Analysis;
                            dataConstrains.CountryConstrain_TextDTO = Constrain_Text;
                            dataConstrains.ConstraintAirPortInternational = Constrain_ArrivalPort;
                            dataConstrains.Constraint_Treatment = Constrain_Treatment;

                        }
                        CheckRequestDetails.FirstOrDefault().Ex_CountryConstrain = dataConstrains;
                        #endregion

                        if (itemShortNameId != null)
                        {
                            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId && a.User_Deletion_Id == null).FirstOrDefault();
                            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                            itt.ItemShortNameEn = itemShortName.ShortName_En;

                            //الجزء النباتي
                            //itt.SubPart_Name = (lang == "1" ? itemShortName.SubPart.Name_Ar : itemShortName.SubPart.Name_En);
                            if (itt.ItemName != null)
                            {
                                itt.ItemName = (lang == "1" ? itemShortName.Item.Name_Ar : itemShortName.Item.Name_En);

                            }
                            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                            if (stat != null)
                            {
                                itt.Status = stat.Ar_Name;
                            }
                            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                            if (prp != null)
                            {
                                itt.Purpose = prp.Ar_Name;
                            }
                            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                            if (subp != null)
                            {
                                itt.subPartName = subp.Name_Ar;
                            }

                            itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
                            itt.Scientific_Name = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Scientific_Name;
                        }
                        //List categories And lots

                        var catAndLots = uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Ex_CheckRequest_Items_ID == itt.Ex_Items_checkReqID)
                            .Select(v => new categories_lotsCertificate
                            {
                                //eslam
                                ID = v.ID,
                                Ex_CheckRequest_Items_ID = v.Ex_CheckRequest_Items_ID,

                                Size = v.Size,
                                Package_Count = v.Package_Count,
                                Package_Weight = v.Package_Weight,
                                Units_Number = v.Units_Number,
                                packageTypeID = v.Package_Type_ID,
                                Order_TextLot = v.Order_Text,
                                packageMaterialID = v.Package_Material_ID,
                                Lot_Number = v.Lot_Number,
                                Grower_Number = v.Grower_Number,
                                Waybill = v.Waybill,
                                Number_Wooden_Package = v.Number_Wooden_Package,
                                GrossWeight = v.GrossWeight,
                                Net_Weight = v.Net_Weight,
                                Package_Based_Weight = v.Package_Based_Weight,
                                Package_Net_Weight = v.Package_Net_Weight,
                                Reason_Entry = v.Reason_Entry,
                                Based_Weight = v.Based_Weight,

                            })
                          .ToList();


                        foreach (var ctt in catAndLots)
                        {
                            var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
                            if (ptypec != null)
                            {
                                ctt.packageType = (lang == "1" ? ptypec.Ar_Name : ptypec.En_Name);//ptypec.Ar_Name;
                            }


                            var categ = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == ctt.ItemCategory_ID).FirstOrDefault();
                            if (categ != null)
                            {
                                ctt.categoryName = (lang == "1" ? categ.Name_Ar : categ.Name_En);//categ.Name_Ar;
                                ctt.RecordedOrNot = ((bool)categ.IsRegister ? "مسجل" : "غير مسجل");
                                if (categ.ItemCategories_Group_ID == null)
                                {
                                    ctt.ItemCategoryGroup = "لا يوجد";
                                }
                                else
                                {
                                    var ccc = uow.Repository<ItemCategories_Group>().GetData().Where(d => d.ID == categ.ItemCategories_Group_ID).FirstOrDefault();
                                    ctt.ItemCategoryGroup = (lang == "1" ? ccc.Name_Ar : ccc.Name_En);
                                }

                            }
                            var pckmtr = uow.Repository<Package_Material>().GetData().Where(g => g.ID == ctt.packageMaterialID).FirstOrDefault();
                            if (pckmtr != null)
                            {
                                ctt.packageMaterialName = (lang == "1" ? pckmtr.Ar_Name : pckmtr.En_Name);
                            }


                        }
                        itt.ItemCategories_lots = catAndLots;

                    }
                    check.Items_checkReqs = itemss;
                    // Attachments
                    //Attachments


                }
                var _Ex_CheckRequest_ID = CheckRequestDetails.FirstOrDefault().Ex_CheckRequest_ID;
                var _EXCheckRequest_Number = CheckRequestDetails.FirstOrDefault().EXCheckRequest_Number;


                //Attachments
                var attach = uow.Repository<A_AttachmentData_Ex_CheckRequest>().GetData()
                                          .Where(v => v.Ex_CheckRequest_ID == _Ex_CheckRequest_ID && v.User_Deletion_Id == null)
                                           .Select(x => new AttachmentsCertificate
                                           {
                                               Attachment_Number = x.Attachment_Number,
                                               AttachmentPath = x.AttachmentPath,
                                               Attachment_TypeName = x.Attachment_TypeName,
                                               StartDate = x.StartDate,
                                               EndDate = x.EndDate,
                                               Attachment_Name = (lang == "1" ? x.A_AttachmentTableType.Ar_Name : x.A_AttachmentTableType.En_Name)


                                           }).ToList();
                CheckRequestDetails.FirstOrDefault().Attachments = attach;
                #region لجنة الفحص  

                // 
                var committee = (from rc in entities.Ex_RequestCommittee
                                 join cr in entities.Ex_CommitteeResult on rc.ID equals cr.Committee_ID into c1
                                 from cr in c1.DefaultIfEmpty()
                                 join Lot_R in entities.Ex_CheckRequest_Items_Lot_Category on cr.LotData_ID equals Lot_R.ID into c2
                                 from Lot_R in c2.DefaultIfEmpty()
                                     // join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID
                                 where rc.Ex_CheckRequest.ID == _Ex_CheckRequest_ID
                                    && rc.CommitteeType_ID == 1
                                      && rc.User_Deletion_Id == null
                                 // && CE.OperationType == 73

                                 select new Committee_Lot_AcceptCertificate
                                 {
                                     Lot_Number = Lot_R.Lot_Number,
                                     ResultTypes_Name = (lang == "1" ? cr.CommitteeResultType.Name_Ar : cr.CommitteeResultType.Name_En),
                                     //EmployeeId = CE.Employee_Id,
                                     Delegation_Date = rc.Delegation_Date,
                                     StartTime = rc.StartTime,
                                     EndTime = rc.EndTime,
                                     IsApproved = rc.IsApproved,
                                     IsFinishedAll = rc.IsFinishedAll,
                                     Status = rc.Status,
                                     Date = cr.Date,
                                     Notes = cr.Notes,
                                     //ISAdmin = CE.ISAdmin,
                                     Committee_ID = rc.ID,
                                     Is_Result_Finch = cr.CommitteeResultType_ID == null ? false : true,
                                     // عرض  فحص اللوطات
                                     IS_Total = cr.IS_Total,
                                     IS_TotalAndroid = cr.IS_Total_Android,
                                 }).ToList();

                try
                {
                    foreach (var iteme in committee)
                    {

                        var committeeEmployee_NameADMIN = (from rc in entities.Ex_RequestCommittee
                                                           join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID
                                                           where rc.CommitteeType_ID == 1
                                                            && rc.User_Deletion_Id == null
                                               && CE.OperationType == 73
                                               && rc.ID == iteme.Committee_ID && CE.ISAdmin == true
                                                           select new committeeEmployee_Name
                                                           {


                                                               Employee_Id = CE.Employee_Id,
                                                               ISAdmin = CE.ISAdmin,
                                                               //Employee_Name = priv.PR_User.Where(c => c.Id == CE.Employee_Id).FirstOrDefault().FullName
                                                           }).ToList();
                        var committeeEmployee_NameConfirm = (from rc in entities.Ex_RequestCommittee
                                                             join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID
                                                             where rc.CommitteeType_ID == 1
                                                              && rc.User_Deletion_Id == null
                                                 && CE.OperationType == 73
                                                 && rc.ID == iteme.Committee_ID && CE.ISAdmin == false
                                                             select new committeeEmployee_Name
                                                             {


                                                                 Employee_Id = CE.Employee_Id,
                                                                 ISAdmin = CE.ISAdmin,

                                                                 //Employee_Name = priv.PR_User.Where(c => c.Id == CE.Employee_Id).FirstOrDefault().FullName
                                                             }).ToList();
                        string admin = ""; string confirm = "";

                        foreach (var item in committeeEmployee_NameADMIN)
                        {
                            admin = priv.PR_User.Where(c => c.Id == item.Employee_Id).FirstOrDefault().FullName;
                        }
                        foreach (var item in committeeEmployee_NameConfirm)
                        {
                            try
                            {
                                confirm = priv.PR_User.Where(c => c.Id == item.Employee_Id).FirstOrDefault().FullName;

                            }
                            catch (Exception)
                            {
                                throw new Exception();


                            }


                        }

                        var fullname = "الأدمن :-" + admin +
                            " المساعد :-" + confirm + ".";
                        //EmployeeId = short.Parse(iteme.EmployeeId.ToString());


                        //foreach (var iteme in committee)
                        //{

                        //    if (iteme.EmployeeId != null)
                        //    {
                        //        short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                        //        iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                        //    }
                        //}



                        committee.FirstOrDefault().committeeFullEmployee_Name = fullname;



                    }

                }
                catch (Exception ex)
                {
                }
                CheckRequestDetails.FirstOrDefault().List_Committee_Lot_Accept = committee;
                #endregion
                #region الســــــــــــــــــحب
                //join c in entities.Countries on exrd.TransitCountry_Id equals c.ID into c1
                //    from c in c1.DefaultIfEmpty()
                var committee_Sample = (from rc in entities.Ex_RequestCommittee
                                        join cr in entities.Ex_CheckRequest_SampleData on rc.ID equals cr.Ex_RequestCommittee_ID
                                        join isn in entities.Item_ShortName on cr.Item_ShortName_ID equals isn.ID
                                        join Lot_R in entities.Ex_CheckRequest_Items_Lot_Result on cr.LotData_ID equals Lot_R.Ex_CheckRequest_Items_Lot_Category_ID into c1
                                        from Lot_R in c1.DefaultIfEmpty()
                                        where rc.ExCheckRequest_ID == _Ex_CheckRequest_ID
                                        && rc.CommitteeType_ID == 3
                                          && rc.User_Deletion_Id == null
                                        select new Committee_Sample_LotCertificate
                                        {
                                            IsPaidCommitte = rc.IsPaid,
                                            Analysis_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisType.Name_Ar : cr.AnalysisLabType.AnalysisType.Name_En),
                                            Lab_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisLab.Name_Ar : cr.AnalysisLabType.AnalysisLab.Name_En),
                                            itemName = isn.ShortName_Ar,

                                            //نتائج المعمل
                                            IsAccepted = cr.IsAccepted,
                                        }).Distinct().ToList();
                //var data = (from exc in entities.Ex_RequestCommittee
                //            join sd in entities.Ex_CheckRequest_SampleData on exc.ID equals sd.Ex_RequestCommittee_ID
                //            join isn in entities.Item_ShortName on sd.Item_ShortName_ID equals isn.ID

                //            where exc.ExCheckRequest_ID == Ex_CheckRequest_ID
                //            select new EX_Committee_Sample_Lot
                //            {
                //                IS_Total_Name = sd.IS_Total == true ? "كلى" : "جزئ",
                //                ItemName = isn.Item.Name_Ar,
                //                ItemShortName = isn.ShortName_Ar,
                //                Sample_BarCode = sd.Sample_BarCode,
                //                Analysis_Name = sd.AnalysisLabType.AnalysisType.Name_Ar,
                //                Lab_Name = sd.AnalysisLabType.AnalysisLab.Name_Ar,
                //                Delegation_Date = exc.Delegation_Date,
                //                SampleRatio = sd.SampleRatio,
                //                SampleSize = sd.SampleSize,
                //            }).Distinct().ToList();
                CheckRequestDetails.FirstOrDefault().List_Lot_Committee_Sample = committee_Sample;//.OrderByDescending(a=>a.itemName);
                #endregion
                #region المعــــالجة
                //join c in entities.Countries on exrd.TransitCountry_Id equals c.ID into c1
                //    from c in c1.DefaultIfEmpty()
                //var Fees_Treatment = (from rc in entities.Ex_RequestCommittee
                //                           join rtd in entities.Ex_Request_TreatmentData on rc.ID equals rtd.Ex_RequestCommittee_ID
                //                           join tt in entities.TreatmentTypes on rtd.TreatmentType_ID equals tt.ID

                //                           join tm in entities.TreatmentMethods on rtd.TreatmentMethod_ID equals tm.ID
                //                           join tmm in entities.TreatmentMaterials on rtd.TreatmentMat_ID equals tmm.ID into c1
                //                           from tmm in c1.DefaultIfEmpty()
                //                           where rc.ExCheckRequest_ID == _Ex_CheckRequest_ID
                //                           && rc.CommitteeType_ID == 6
                //                             && rc.User_Deletion_Id == null
                //                           select new Committee_TreatmentCertificate
                //                           {
                //                               ID_Request_TreatmentData = rtd.ID,

                //                               TreatmentMethod_Name = tm.Ar_Name,
                //                               TreatmentType_Name = tt.Ar_Name,
                //                               TreatmentMat_Name = tmm.ChemicalComposition,
                //                               Company_Id = rtd.Company_ID,
                //                               CompanyTreatmentPlace = lang == "1" ? rtd.Company_National.Name_Ar : rtd.Company_National.Name_En,
                //                               Station_Id = rtd.Station_ID,
                //                               Station_Place = rtd.Station_Place,
                //                               ThermalSealNumber = rtd.ThermalSealNumber
                //                               //TreatmentStatmentNumber =rtd.ThermalSealNumber

                //                           }).Distinct().ToList();

                //var Committe_Treatment = (from rq in entities.Ex_RequestCommittee
                //                          join td in entities.Ex_Request_TreatmentData on rq.ID equals td.Ex_RequestCommittee_ID
                //                          where td.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                //                         && rq.CommitteeType_ID == 6 && rq.Status == true
                //                          group td by new
                //                          {
                //                              ID = td.ID,
                //                              TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                //                                         entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                //                              TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                //                                         entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                //                              TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                //                                         entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                //                              Treatment_Amount = td.Amount,
                //                              CompanyName = entities.Company_National.Where(c => c.ID == td.Company_ID).FirstOrDefault().Name_Ar,// td.Company_National.Name_Ar,
                //                              Is_Paid_Treatment2 = rq.IsPaid
                //                          } into grp
                //                          select new List_TreatmentCertificate
                //                          {
                //                              ID = grp.Key.ID,
                //                              TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                //                              TreatmentType_Name = grp.Key.TreatmentType_Name,
                //                              TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                //                              Treatment_Amount = grp.Key.Treatment_Amount,
                //                              CompanyName = grp.Key.CompanyName,
                //                              Is_Paid_Treatment2 = grp.Key.Is_Paid_Treatment2
                //                          }).Distinct().ToList();

                //CheckRequestDetails.FirstOrDefault().List_Treatment = Committe_Treatment;
                #endregion
                #region الجشنـــــــي
                //join c in entities.Countries on exrd.TransitCountry_Id equals c.ID into c1
                //    from c in c1.DefaultIfEmpty()
                var committee_Geshny = (from rc in entities.Ex_RequestCommittee


                                        join cr in entities.Ex_CommitteeResult on rc.ID equals cr.Committee_ID
                                        join crr in entities.Ex_CheckRequest_Items_Lot_Category on cr.LotData_ID equals crr.ID
                                        join exsm in entities.Ex_CheckRequset_Shipping_Method on crr.Ex_CheckRequset_Shipping_MethodID equals exsm.ID
                                        join sc in entities.A_SystemCode on exsm.containers_ID equals sc.Id
                                        join ce in entities.CommitteeEmployees on rc.ID equals ce.Committee_ID //into c3
                                        //from ce in c3.DefaultIfEmpty()
                                        where rc.ExCheckRequest_ID == _Ex_CheckRequest_ID
                                        && rc.CommitteeType_ID == 2
                                        && rc.User_Deletion_Id == null
                                        && ce.OperationType == 73
                                        && ce.ISAdmin == true
                                        select new Committee_GeshnyCertificate
                                        {


                                            ID_Committee_Geshny = rc.ID,
                                            IsApproved = rc.IsApproved,
                                            containers_Type_Name = sc.ValueName,
                                            containers_ID = exsm.containers_ID,
                                            containers_type_ID = exsm.containers_type_ID,
                                            ShipholdNumber = exsm.ShipholdNumber,
                                            ContainerNumber = exsm.ContainerNumber,
                                            NavigationalNumber = exsm.NavigationalNumber,
                                            Lot_Number = crr.Lot_Number,
                                            Delegation_Date = rc.Delegation_Date,
                                            EmployeeId = ce.Employee_Id,

                                        }).Distinct().OrderBy(a => a.Lot_Number).ThenBy(n => n.NavigationalNumber).ToList();
                var committee_GeshnyConfirm = (from rc in entities.Ex_RequestCommittee


                                               join cr in entities.Ex_CommitteeResult on rc.ID equals cr.Committee_ID
                                               join crr in entities.Ex_CheckRequest_Items_Lot_Category on cr.LotData_ID equals crr.ID
                                               join exsm in entities.Ex_CheckRequset_Shipping_Method on crr.Ex_CheckRequset_Shipping_MethodID equals exsm.ID
                                               join sc in entities.A_SystemCode on exsm.containers_ID equals sc.Id
                                               join ce in entities.CommitteeEmployees on rc.ID equals ce.Committee_ID //into c3
                                                                                                                      //from ce in c3.DefaultIfEmpty()
                                               where rc.ExCheckRequest_ID == _Ex_CheckRequest_ID
                                               && rc.CommitteeType_ID == 2
                                               && rc.User_Deletion_Id == null
                                               && ce.OperationType == 73
                                               && ce.ISAdmin == false
                                               select new Committee_GeshnyConfirmCertificate
                                               {


                                                   ID_Committee_Geshny = rc.ID,
                                                   containers_Type_Name = sc.ValueName,
                                                   containers_ID = exsm.containers_ID,
                                                   containers_type_ID = exsm.containers_type_ID,
                                                   ShipholdNumber = exsm.ShipholdNumber,
                                                   ContainerNumber = exsm.ContainerNumber,
                                                   NavigationalNumber = exsm.NavigationalNumber,
                                                   Lot_Number = crr.Lot_Number,
                                                   Delegation_Date = rc.Delegation_Date,
                                                   EmployeeId = ce.Employee_Id,

                                               }).Distinct().OrderBy(a => a.Lot_Number).ThenBy(n => n.NavigationalNumber).ToList();
                try
                {

                    foreach (var item in committee_Geshny)
                    {

                        if (item.EmployeeId != null)
                        {
                            short Employee_Comm_Id = short.Parse(item.EmployeeId.ToString());
                            item.Employee_Name = "الأدمن : " + priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;

                        }
                    }
                }
                catch (Exception ex)
                {
                }



                try
                {

                    foreach (var item in committee_GeshnyConfirm)
                    {

                        if (item.EmployeeId != null)
                        {
                            short Employee_Comm_Id = short.Parse(item.EmployeeId.ToString());
                            item.Employee_NameConfirm = "المساعد : " + priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;

                        }
                    }
                }
                catch (Exception ex)
                {
                }
                //{

                //    foreach (var item in committee_Geshny)
                //    {
                //        var committee_Geshny2 = ( from exsm in entities.Ex_CheckRequset_Shipping_Method 

                //                                    join sc in entities.A_SystemCode on exsm.containers_ID equals sc.Id into cc1
                //                                    from sc in cc1.DefaultIfEmpty()
                //                                    join cr in entities.Ex_CommitteeResult on item.ID_Committee_Geshny equals cr.Committee_ID into c2
                //                                    from cr in c2.DefaultIfEmpty()
                //                                    join ce in entities.CommitteeEmployees on item.ID_Committee_Geshny equals ce.Committee_ID into c3
                //                                    from ce in c3.DefaultIfEmpty()
                //                                where exsm.Ex_CheckRequest_ID == _Ex_CheckRequest_ID

                //                                && ce.OperationType == 73


                //                                select new Committee_GeshnyDataCertificate
                //                                {



                //                                    containers_Type_Name = sc.ValueName,
                //                                    containers_ID = exsm.containers_ID,
                //                                    containers_type_ID = exsm.containers_type_ID,
                //                                    ShipholdNumber = exsm.ShipholdNumber,
                //                                    ContainerNumber = exsm.ContainerNumber,
                //                                    NavigationalNumber = exsm.NavigationalNumber,
                //                                    LotData_ID = cr.LotData_ID,

                //                                    EmployeeId = ce.Employee_Id,

                //                                }).Distinct().ToList();

                //        //if (item.EmployeeId != null)
                //        //{

                //        //}

                //        foreach (var item2 in committee_Geshny2)
                //        {
                //            short Employee_Comm_Id = short.Parse(item2.EmployeeId.ToString());
                //            item2.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                //        }
                //        item.Committee_GeshnyDataCertificate = committee_Geshny2;
                //    }

                //}
                //catch (Exception ex)
                //{
                //}

                CheckRequestDetails.FirstOrDefault().List_GeshnyCommittee = committee_Geshny;
                CheckRequestDetails.FirstOrDefault().List_GeshnyCommitteeConfirm = committee_GeshnyConfirm;
                #endregion
                #region ALLLLLLLLLLLLLLFees الرســــــــــــــوم
                //الرسوم Fees Eslam

                var item_Fees = (from im_i in entities.Ex_CheckRequest_Items
                                 join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                 where im_i.Ex_CheckRequest_ID == _Ex_CheckRequest_ID
                                 group im_i by new
                                 {
                                     im_i.Item_ShortName_ID,
                                     isn.ShortName_Ar,
                                     isn.ShortName_En,
                                     Item_ID = isn.Item.ID,
                                     ItemName_Ar = isn.Item.Name_Ar,
                                     ItemName_En = isn.Item.Name_En,
                                     qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
                                     qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
                                 } into grp
                                 select new Fees_ItemCertificate
                                 {
                                     ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                     ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                     //  Fees = grp.Sum(q => q.Fees),
                                     GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                     Net_Weight = grp.Sum(q => q.Net_Weight),
                                 }).Distinct().ToList();
                CheckRequestDetails.FirstOrDefault().Fees_Item_All = item_Fees;
                //CheckRequestDetails.FirstOrDefault().Is_Paid_Committee = item_Fees;
                item_Fees.FirstOrDefault().Fees = uow.Repository<Ex_CheckRequest_Fees>().GetData().Where(d => d.Ex_CheckRequest_ID == _Ex_CheckRequest_ID).FirstOrDefault().Total_Amount;
                if (CheckRequestDetails.FirstOrDefault().IsPaidCommittee != null)
                {


                    CheckRequestDetails.FirstOrDefault().IsPaidCommittee = uow.Repository<Ex_RequestCommittee>().GetData().Where(d => d.ExCheckRequest_ID == _Ex_CheckRequest_ID).FirstOrDefault().IsPaid;
                }
                // item_Fees.FirstOrDefault().Is_Paid_Committee = uow.Repository<Ex_RequestCommittee>().GetData().Where(d => d.ExCheckRequest_ID == _Ex_CheckRequest_ID).().IsPaid;
                CheckRequestDetails.FirstOrDefault().Fees_Item_Shift_All = (from cms in entities.Ex_RequestCommittee_Shift
                                                                            where cms.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                                                            && cms.Ex_RequestCommittee.IsApproved == true && cms.Ex_RequestCommittee.IsFinishedAll == true
                                              && (DbFunctions.TruncateTime(cms.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(cms.Ex_RequestCommittee.User_Creation_Date)))
                                              && (DbFunctions.TruncateTime(cms.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(cms.Ex_RequestCommittee.Delegation_Date), 1))


                                                                            select new Fees_Item_ShiftCertificate
                                                                            {
                                                                                Amount_Per_Shift = cms.Amount,
                                                                                Count_Per_Shift = cms.Count,
                                                                                total_Per_Shift = (cms.Count * cms.Amount)
                                                                            }).ToList();

                var _total_Per_Shift = CheckRequestDetails.FirstOrDefault().Fees_Item_Shift_All.Select(z => z.total_Per_Shift).Sum();
                CheckRequestDetails.FirstOrDefault().Shift_Item_All = _total_Per_Shift;

                #region رسم  النوبتجية
                // 
                var today = DateTime.Now.Date;
                var Fees_Item_Shift = (from sh in entities.Ex_RequestCommittee_Shift

                                       where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                       && sh.Ex_RequestCommittee.IsApproved == true
                                        && (sh.Ex_RequestCommittee.IsPaid == true || sh.Ex_RequestCommittee.Delegation_Date >= today)
                                       // && sh.Ex_RequestCommittee.IsFinishedAll == true
                                       // && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date)))
                                       //  && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))


                                       group sh by new
                                       {
                                           ID = sh.ID,
                                           Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
                                           IsPaidCommittee = sh.Ex_RequestCommittee.IsPaid,
                                           Delegation_Date = sh.Ex_RequestCommittee.Delegation_Date,
                                           User_Creation_Date = sh.Ex_RequestCommittee.User_Creation_Date,
                                           IsApproved = sh.Ex_RequestCommittee.IsApproved,
                                           IsFinishedAll = sh.Ex_RequestCommittee.IsFinishedAll,
                                           StartTime = sh.Ex_RequestCommittee.StartTime,
                                           EndTime = sh.Ex_RequestCommittee.EndTime,
                                           CommitteTypeName = sh.Ex_RequestCommittee.CommitteeType.Name_Ar,
                                           CommitteeType_ID = sh.Ex_RequestCommittee.CommitteeType_ID,

                                       } into grp
                                       select new List_ShiftCertificate
                                       {
                                           ID = grp.Key.ID,
                                           Shift_Name = grp.Key.Shift_Name,
                                           IsPaidCommittee = grp.Key.IsPaidCommittee,
                                           Shift_Count = grp.Sum(q => q.Count),
                                           Shift_Amount = grp.Sum(q => q.Amount),
                                           CommitteeType_ID = grp.Key.CommitteeType_ID,
                                           Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                                       }).Distinct().ToList();
                //foreach (var item in Fees_Item_Shift)
                //{
                //    long ID_Item = long.Parse(item.ID.ToString());
                //    var _Shift = (from ftd in entities.Fees_Transactions_Detiles

                //                  where ftd.Shift_ID == ID_Item
                //                  && ftd.Fees_Transactions.TableName_ID == 4
                //                  && ftd.Fees_Transactions.Table_ID == _Ex_CheckRequest_ID
                //                  select new List_ShiftCertificate
                //                  {
                //                      Is_Paid_Shift = ftd.Shift_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                //                  }
                //               ).ToList();
                //    if (_Shift.Count > 0)
                //        item.Is_Paid_Shift = _Shift.FirstOrDefault().Is_Paid_Shift;
                //    else
                //        item.Is_Paid_Shift = null;
                //}

                CheckRequestDetails.FirstOrDefault().List_Shift = Fees_Item_Shift;
                #endregion
                #region رسم  المهندسين
                // 
                var Fees_Engineers = (from sh in entities.Ex_RequestCommittee_Fees_ENG


                                      where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                     && sh.Ex_RequestCommittee.IsApproved == true
                                      && (sh.Ex_RequestCommittee.IsPaid == true || sh.Ex_RequestCommittee.Delegation_Date >= today)
                                      //&& sh.Ex_RequestCommittee.IsFinishedAll == true
                                      //&& (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date)))
                                      // && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))

                                      group sh by new
                                      {
                                          CommitteTypeName = sh.Ex_RequestCommittee.CommitteeType.Name_Ar,

                                          ID = sh.ID,
                                          Shift_Name = lang == "1" ? sh.EX_Fees_Type.Name : sh.EX_Fees_Type.Name,
                                          IsPaidEngineers = sh.Ex_RequestCommittee.IsPaid,
                                          Num_Eng = sh.Num_Eng,
                                          Shift_Amount = sh.Value,
                                          CommitteeType_ID = sh.Ex_RequestCommittee.CommitteeType_ID,
                                          Delegation_Date = sh.Ex_RequestCommittee.Delegation_Date,
                                          User_Creation_Date = sh.Ex_RequestCommittee.User_Creation_Date,
                                          IsApproved = sh.Ex_RequestCommittee.IsApproved,
                                          IsFinishedAll = sh.Ex_RequestCommittee.IsFinishedAll,
                                          StartTime = sh.Ex_RequestCommittee.StartTime,
                                          EndTime = sh.Ex_RequestCommittee.EndTime,

                                      } into grp
                                      select new List_ShiftEngineersCertificate
                                      {

                                          CommitteTypeName = grp.Key.CommitteTypeName,
                                          StartTime = grp.Key.StartTime,
                                          EndTime = grp.Key.EndTime,
                                          Delegation_Date = grp.Key.Delegation_Date,
                                          User_Creation_Date = grp.Key.User_Creation_Date,
                                          IsApproved = grp.Key.IsApproved,
                                          IsFinishedAll = grp.Key.IsFinishedAll,
                                          ID = grp.Key.ID,
                                          Shift_Name = grp.Key.Shift_Name,
                                          IsPaidEngineers = grp.Key.IsPaidEngineers,
                                          Num_Eng = grp.Key.Num_Eng,
                                          Shift_Amount = grp.Key.Shift_Amount,
                                          Shift_Sum_All = grp.Key.Num_Eng * grp.Key.Shift_Amount,
                                          CommitteeType_ID = grp.Key.CommitteeType_ID,

                                      }).Distinct().ToList();



                CheckRequestDetails.FirstOrDefault().List_ShiftEngineersCertificate = Fees_Engineers;
                #endregion

                #region   رسوم السحب




                var Fees_Sample = (from sm in entities.Ex_CheckRequest_SampleData

                                   where sm.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                   && sm.Ex_RequestCommittee.CommitteeType_ID == 3 && sm.Count_Sample != null && sm.Amount != null
                                   && sm.Ex_RequestCommittee.IsApproved == true && sm.Ex_RequestCommittee.IsFinishedAll == true
                                    && (sm.Ex_RequestCommittee.IsPaid == true || sm.Ex_RequestCommittee.Delegation_Date >= today)

                                   group sm by new
                                   {
                                       ID = sm.ID,
                                       Sample_BarCode = sm.Sample_BarCode,
                                       Is_Total = sm.IS_Total,
                                       Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                       Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,
                                       Is_Paid_Sample2 = sm.Ex_RequestCommittee.IsPaid,
                                       Sample_Count = sm.Count_Sample,
                                       Sample_Amount = sm.Amount

                                   } into grp
                                   select new List_SampleCertificate
                                   {
                                       ID = grp.Key.ID,
                                       Sample_BarCode = grp.Key.Sample_BarCode,
                                       Laboratory_Name = grp.Key.Laboratory_Name,
                                       Sample_Name = grp.Key.Sample_Name,
                                       Is_Paid_Sample2 = grp.Key.Is_Paid_Sample2,
                                       Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
                                       Sample_Count = grp.Key.Sample_Count,
                                       Sample_Amount = grp.Key.Sample_Amount,
                                       Sample_Sum_All = (grp.Key.Sample_Count) * (grp.Key.Sample_Amount),
                                   }).Distinct().ToList();

                var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();

                //foreach (var item in Fees_Sample44)
                //{
                //    long ID_Item = long.Parse(item.ID.ToString());
                //    var _Sample = (from ftd in entities.Fees_Transactions_Detiles

                //                   where ftd.SampleData_ID == ID_Item
                //                   && ftd.Fees_Transactions.TableName_ID == 4
                //                   && ftd.Fees_Transactions.Table_ID == _Ex_CheckRequest_ID
                //                   select new List_SampleCertificate
                //                   {
                //                       Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                //                   }
                //               ).ToList();
                //    if (_Sample.Count > 0)
                //        item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
                //    else
                //        item.Is_Paid_Sample = null;
                //}
                CheckRequestDetails.FirstOrDefault().List_Sample = Fees_Sample44;




                #endregion


                #region رسوم المعالجة



                var Fees_Treatment2 = (from rq in entities.Ex_RequestCommittee
                                       join td in entities.Ex_Request_TreatmentData on rq.ID equals td.Ex_RequestCommittee_ID
                                       where td.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                     && rq.CommitteeType_ID == 6 && rq.Status == true
                                      && rq.IsApproved == true
                                          && (rq.IsPaid == true || rq.Delegation_Date >= today)

                                       // && rq.IsFinishedAll == true
                                       //&& (rq.Delegation_Date >= DateTime.Now )
                                       group td by new
                                       {
                                           ID = td.ID,
                                           TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                         entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                                           TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                         entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                                           TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                                                         entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                                           Treatment_Amount = td.Amount,
                                           Station_Place = td.Station_Place != null ? td.Station_Place : entities.Stations.Where(c => c.ID == td.Station_ID).FirstOrDefault().Ar_Name,
                                           CompanyName = entities.Company_National.Where(c => c.ID == td.Company_ID).FirstOrDefault().Name_Ar,// td.Company_National.Name_Ar,
                                           Is_Paid_Treatment2 = rq.IsPaid,
                                           Is_Paid_TreatmenCommitte = rq.IsPaid,
                                           ThermalSealNumber = td.ThermalSealNumber,
                                       } into grp
                                       select new List_TreatmentCertificate
                                       {
                                           //ID = grp.Key.ID,
                                           //TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                           //TreatmentType_Name = grp.Key.TreatmentType_Name,
                                           //TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                           //Treatment_Amount = grp.Key.Treatment_Amount,
                                           ID = grp.Key.ID,
                                           TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                           TreatmentType_Name = grp.Key.TreatmentType_Name,
                                           TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                           Treatment_Amount = grp.Key.Treatment_Amount,
                                           CompanyName = grp.Key.CompanyName,
                                           Station_Place = grp.Key.Station_Place,
                                           Is_Paid_Treatment2 = grp.Key.Is_Paid_Treatment2,
                                           Is_Paid_TreatmenCommitte = grp.Key.Is_Paid_TreatmenCommitte,
                                           ThermalSealNumber = grp.Key.ThermalSealNumber
                                       }).Distinct().ToList();
                //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();
                //if (Fees_Treatment.Count() > 0)
                //{
                //    foreach (var item in Fees_Treatment)
                //    {
                //        long ID_Item = long.Parse(item.ID.ToString());
                //        var _Treatment = (from ftd in entities.Fees_Transactions_Detiles
                //                          where ftd.TreatmentData_ID == ID_Item
                //                          select new List_TreatmentCertificate
                //                          {
                //                              //Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
                //                          }
                //                   ).ToList();
                //        if (_Treatment.Count > 0)
                //        {
                //            item.Is_Paid_Treatment = _Treatment.FirstOrDefault().Is_Paid_Treatment;
                //        }
                //        else
                //        {
                //            item.Is_Paid_Treatment = null;
                //        }
                //    }

                //}
                CheckRequestDetails.FirstOrDefault().List_Treatment = Fees_Treatment2;


                #endregion


                //List_Fees_MartyrsCertificate


                #region رسم  الشهداء
                //           //                select fa.Name_Ar,fa.Amount from
                //
                //join   on ftd.Fees_Transactions_ID = ft.ID
                //join Fees_Action fa on ftd.Fees_Transactions_ID = fa.ID
                //where fa.ID = 21

                //                select sum(ftd.Amount)Amount
                //from  Fees_Transactions ft
                //left join Fees_Transactions_Detiles ftd on ft.ID = ftd.Fees_Transactions_ID
                //left join Ex_CheckRequest cr on ft.Table_ID = cr.ID
                //left join Ex_RequestCommittee rc on ft.Table_ID = rc.ID
                //where(cr.id = 650 or rc.ExCheckRequest_ID = 650) and ft.TableName_ID in(8, 9)and ftd.Fees_Action_ID = 21
                Dictionary<string, SqlDbType> paramters_Type2 = new Dictionary<string, SqlDbType>();

                paramters_Type2.Add("lang", SqlDbType.Int);
                paramters_Type2.Add("ExCheckRequest_ID", SqlDbType.BigInt);


                Dictionary<string, string> paramters_Data2 = new Dictionary<string, string>();
                paramters_Data2.Add("lang", lang);
                paramters_Data2.Add("ExCheckRequest_ID", EXCheckRequest_Number);
                //            var Fees_MartyrsSP = uow.Repository<EXRequestDetailsDTO>().CallStored("Fees_Martyrs", paramters_Type2,
                //paramters_Data2, Device_Info).ToList();
                var Fees_Martyrs1 = (from ft in entities.Fees_Transactions
                                     join ftd in entities.Fees_Transactions_Detiles on ft.ID equals ftd.Fees_Transactions_ID
                                     join fa in entities.Fees_Action on ftd.Fees_Action_ID equals fa.ID
                                     join cr in entities.Ex_CheckRequest on ft.Table_ID equals cr.ID into cr1
                                     from cr in cr1.DefaultIfEmpty()

                                     join rc in entities.Ex_RequestCommittee on ft.Table_ID equals rc.ID into rc1
                                     from rc in rc1.DefaultIfEmpty()
                                     where ftd.Fees_Action_ID == 21 && cr.ID == 424

                                     //رسوم الشهيد للجان
                                     group ftd by new
                                     {
                                         Fees_Transactions_DetilesID = ftd.ID,
                                         Name_Ar = fa.Name_Ar,
                                         table_name_id = ft.TableName_ID,
                                         Total_Amount = ftd.Amount
                                     } into grp
                                     select new List_Fees_MartyrsCertificate
                                     {
                                         Name_Ar = grp.Key.Name_Ar,
                                         Total_Amount = grp.Sum(q => q.Amount),
                                         table_name_id = grp.Key.table_name_id


                                     }).Distinct().ToList();


                var Fees_Martyrs2 = (from ft in entities.Fees_Transactions
                                     join ftd in entities.Fees_Transactions_Detiles on ft.ID equals ftd.Fees_Transactions_ID
                                     join fa in entities.Fees_Action on ftd.Fees_Action_ID equals fa.ID
                                     join rc in entities.Ex_RequestCommittee on ft.Table_ID equals rc.ID
                                     where ftd.Fees_Action_ID == 21 && rc.ExCheckRequest_ID == 424

                                     //رسوم الشهيد للجان
                                     group ftd by new
                                     {
                                         Fees_Transactions_DetilesID = ftd.ID,
                                         Name_Ar = fa.Name_Ar,
                                         table_name_id = ft.TableName_ID,
                                         Total_Amount = ftd.Amount
                                     } into grp
                                     select new List_Fees_MartyrsCertificate
                                     {
                                         Name_Ar = grp.Key.Name_Ar,
                                         Total_Amount = grp.Sum(q => q.Amount),
                                         table_name_id = grp.Key.table_name_id
                                     }).Distinct().ToList();
                var Fees_Martyrs = (Fees_Martyrs1.Union(Fees_Martyrs2)).ToList();
                var list1 = Fees_Martyrs.Where(a => a.table_name_id == 8);
                var list2 = Fees_Martyrs.Where(a => a.table_name_id == 9);

                var Fees_Martyrslist = (list1.Union(list2)).ToList();
                CheckRequestDetails.FirstOrDefault().List_Fees_MartyrsCertificate = Fees_Martyrslist;
                #endregion

                decimal item_Fees_Total = 0;


                if (item_Fees != null)
                {
                    item_Fees_Total = item_Fees.Select(a => a.Fees).Sum().Value;
                }

                var Sum_List_Sample = CheckRequestDetails.FirstOrDefault().List_Sample.Select(c => c.Sample_Sum_All).Sum();



                CheckRequestDetails.FirstOrDefault().SUM_Shift_Fees_Item = 10 + _total_Per_Shift + item_Fees_Total + Sum_List_Sample;
                #endregion
                //                    ///////////////ESLAM///////////////
                //                }
                //visa
                var CheckRequest_Visa = (from exv in entities.Ex_CheckRequest_Visa
                                         where exv.Ex_CheckRequest_ID == _Ex_CheckRequest_ID
                                         select new EX_CheckRequest_VisaCertificate
                                         {
                                             Im_CheckRequest_Visa_ID = exv.Ex_CheckRequest_ID,
                                             Date_Visa = exv.Date,
                                             Visa_Result_EmployeeId = exv.User_Creation_Id,
                                             Visa_Name = (lang == "1" ? exv.Ex_Visa.Ar_Name : exv.Ex_Visa.En_Name),
                                             Visa_Result_Name = (lang == "1" ? exv.Ex_Visa.Description_Ar : exv.Ex_Visa.Description_En),
                                         }).ToList();

                foreach (var Item_Visa in CheckRequest_Visa)
                {
                    if (Item_Visa.Visa_Result_EmployeeId != null)
                    {

                        Item_Visa.Visa_Result_Employee_Name = priv.PR_User.Where(c => c.Id == Item_Visa.Visa_Result_EmployeeId).FirstOrDefault().FullName;
                    }
                }
                CheckRequestDetails.FirstOrDefault().EX_CheckRequest_VisaCertificate = CheckRequest_Visa;

                ///////////////////////////////////////////////////////////////////////////////////



                var finalResult = (from crfr in entities.Ex_CheckRequest_Final_Result
                                   join imfr in entities.Ex_Final_Result on crfr.Ex_Final_Result_ID equals imfr.ID
                                   where crfr.Ex_CheckRequest_ID == _Ex_CheckRequest_ID
                                   select new EX_FinalResultCertificate
                                   {
                                       EX_CheckRequest_FinalResult_ID = crfr.Ex_CheckRequest_ID,
                                       DateFinalResult = crfr.Date,
                                       Final_Result_EmployeeId = crfr.User_Creation_Id,
                                       Final_Result_Status = imfr.Status,
                                       Final_Result_Name = (lang == "1" ? imfr.Ar_Name : imfr.En_Name),
                                   }).ToList();

                foreach (var fRes in finalResult)
                {
                    if (fRes.Final_Result_EmployeeId != null)
                    {

                        fRes.Final_Result_Employee_Name = priv.PR_User.Where(c => c.Id == fRes.Final_Result_EmployeeId).FirstOrDefault().FullName;
                    }
                }
                CheckRequestDetails.FirstOrDefault().FinalResultCertificate = finalResult;
                //            var objResponse1 =
                //JsonConvert.DeserializeObject<List<object>>(CheckRequestDetails.ToString());
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}
