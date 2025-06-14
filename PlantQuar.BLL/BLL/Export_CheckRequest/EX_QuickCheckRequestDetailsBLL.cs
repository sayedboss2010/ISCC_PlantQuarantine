using AutoMapper;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
    public class EX_QuickCheckRequestDetailsBLL
    {

        private UnitOfWork uow;

        public EX_QuickCheckRequestDetailsBLL()
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
                //var CheckRequestDetails = entities.Ex_CheckRequestDetails(1, EXCheckRequest_Number).ToList();

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();

                paramters_Type.Add("lang", SqlDbType.Int);
                paramters_Type.Add("CheckRequest_Number", SqlDbType.NVarChar);


                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("lang", lang);
                paramters_Data.Add("CheckRequest_Number", EXCheckRequest_Number);
                var CheckRequestDetails = uow.Repository<EXRequestDetailsDTO>().CallStored("Ex_QuickCheckRequestDetails", paramters_Type,
                    paramters_Data, Device_Info).ToList();


                var _EXCheckRequest_DataId = (Int64)CheckRequestDetails.FirstOrDefault().Ex_CheckRequestData_ID;
                var _EXCheckRequest_Id = (Int64)CheckRequestDetails.FirstOrDefault().Ex_CheckRequest_ID;
                CheckRequestDetails.FirstOrDefault().TransportCountryList = (from exrd in entities.Ex_CheckRequest_Data
                                                                             join exrp in entities.Ex_CheckRequest_Port on new { a = (long?)exrd.ID, b = 10 } equals new { a = exrp.Ex_CheckRequest_Data_ID, b = exrp.ReqPortType_ID } into exrp1
                                                                             from exrp in exrp1.DefaultIfEmpty()
                                                                             join pi in entities.Port_International on exrp.Port_ID equals pi.ID into pi1
                                                                             from pi in pi1.DefaultIfEmpty()
                                                                             join pt in entities.Port_Type on exrp.Port_Type_ID equals pt.ID into pt1
                                                                             from pt in pt1.DefaultIfEmpty()

                                                                             where exrd.ID == _EXCheckRequest_DataId
                                                                             select new TransportCountryList
                                                                             {
                                                                                 TransportCountryID = exrd.ExportCountry_Id,
                                                                                 TransportPortType = pt.Name_Ar,
                                                                                 TransportPortName = pi.Name_Ar

                                                                             }).ToList();
                CheckRequestDetails.FirstOrDefault().TransiteCountryList = (from exrd in entities.Ex_CheckRequest_Data
                                                                            join exrp in entities.Ex_CheckRequest_Port on new { a = (long?)exrd.ID, b = 11 } equals new { a = exrp.Ex_CheckRequest_Data_ID, b = exrp.ReqPortType_ID } into exrp1
                                                                            from exrp in exrp1.DefaultIfEmpty()
                                                                            join pi in entities.Port_International on exrp.Port_ID equals pi.ID into pi1
                                                                            from pi in pi1.DefaultIfEmpty()
                                                                            join c in entities.Countries on exrd.TransitCountry_Id equals c.ID into c1
                                                                            from c in c1.DefaultIfEmpty()
                                                                            where exrd.ID == _EXCheckRequest_DataId
                                                                            select new TransiteCountryList
                                                                            {
                                                                                TransiteCountryID = exrd.TransitCountry_Id,
                                                                                TransitPortType = pi.Port_Type.Name_Ar,
                                                                                TransitPortName = pi.Name_Ar,
                                                                                TransitCountry = c.Ar_Name

                                                                            }).ToList();

                //اسباب الرفض
                if (CheckRequestDetails.FirstOrDefault().IsAccepted == false)
                {


                    CheckRequestDetails.FirstOrDefault().RefuseNotes = entities.Ex_CheckRequest.Where(a => a.ID == _EXCheckRequest_Id).Select(a => a.Notes_Reject).FirstOrDefault();
          
                    var _CheckRequest_RefuseReason = (from rr in entities.Ex_CheckRequest_RefuseReason
                                                      join ss in entities.Refuse_Reason on rr.Refuse_Reason_Id equals ss.ID
                                                      where rr.Ex_CheckRequest_Id == _EXCheckRequest_Id
                                                      select new CheckRequest_RefuseReasonDTO
                                                      {
                                                          Refuse_Reason_Name = ss.Name_Ar,
                                                      }).ToList();
                    CheckRequestDetails.FirstOrDefault().List__CheckRequest_RefuseReasonDTO = _CheckRequest_RefuseReason;
                }
                //                 }
                foreach (var check in CheckRequestDetails)
                {
                    //Add Company Activity
                    var CompanyActivities_Details = (from ca in entities.CompanyActivities
                                                         //join cat in entities.Enrollment_type on ca.Enrollment_type_ID equals cat.ID
                                                     where ca.Company_ID == check.Importer_ID
                                                     && ca.IsActive == true
                                                     select new CompanyActivitysDTO
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
                 Select(a => new ContactTypeDTO
                 {
                     Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
                     Value = a.Value
                 }).ToList();
                    /////////////////////////////

                    //Items
                    var itemss = uow.Repository<Ex_CheckRequest_Items>().GetData().Include(a => a.Item_ShortName).
                       Where(i => i.Ex_CheckRequest_ID == check.Ex_CheckRequest_ID).
                       Select(v => new Items_checkReq
                       {
                           //eslam 
                           Item_Type_Name = v.Item_ShortName.Item.Group.SecondaryClassification.MainCalssification.Item_Type.Name_Ar,
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
                           Scientific_Name = v.Item_ShortName.Item.Scientific_Name,

                           Order_TextItem = v.Order_Text,
                           SubPart_id = v.SubPart_id,
                           Package_Material_ID = v.Package_Material_ID,
                           Is_LotDivision = v.Is_LotDivision,
                           Accept_Date = v.Accept_Date,
                           IsAccepted = v.IsAccepted,
                           FarmsData = v.FarmsData.Name_Ar,
                           ItemCategoryName = v.ItemCategory.Name_Ar,
                           Agriculture_Hand = v.Agriculture_Hand,
                           ItemCategories_Name = v.ItemCategory.Name_Ar,
                           CurrentStatus = v.ItemCategory.CurrentStatus,
                           IsRegister = v.ItemCategory.IsRegister,
                           ItemCategories_Group_Name = v.ItemCategories_Group.Name_Ar,
                           ItemCategories_Group_ID = v.ItemCategories_Group_ID,
                           FarmCode = v.FarmsData.FarmCode_14,
                           Farm_Name_Ar = v.FarmsData.Name_Ar,
                           //Farm_Govern_Name = v.FarmsData.Governate.Ar_Name,
                           //Farm_Center_Name = v.FarmsData.Center.Ar_Name,
                           //Farm_Village_Name = v.FarmsData.Village.Ar_Name,
                           Farm_Village_Name = v.Village.Ar_Name == null ? v.FarmsData.Village.Ar_Name : v.Village.Ar_Name,
                           Farm_Govern_Name = v.Governate.Ar_Name == null ? v.FarmsData.Governate.Ar_Name : v.Governate.Ar_Name,
                           Farm_Center_Name = v.Center.Ar_Name == null ? v.FarmsData.Center.Ar_Name : v.Center.Ar_Name,
                           Farm_Agriculture_Hand = v.Agriculture_Hand,
                           QualitativeGroup_Name_Ar = v.Item_ShortName.QualitativeGroup.Name_Ar,
                       }).Distinct().ToList();
                    //foreach (var item in itemss)
                    //{                    var dddd =item.ItemCategories_Group_ID;
                    //var ItemCategories_Group_Name2= entities.ItemCategories_Group.Where(a => a.ID == dddd).Select(a => a.Name_Ar ).FirstOrDefault();

                    //}

                    //end Items AND GET DATE FOR ALL LOTS AND ITEMS

                    //constrainsEslamMaher
                    var ExportCountryId = CheckRequestDetails.FirstOrDefault().TransportCountryList.Select(a => a.TransportCountryID).FirstOrDefault();

                    var TransiteCountryId = CheckRequestDetails.FirstOrDefault().TransiteCountryList.Select(a => a.TransiteCountryID).FirstOrDefault();
                    if (TransiteCountryId != null)
                    {
                        var TransiteCountry = CheckRequestDetails.FirstOrDefault().TransiteCountryList.Select(a => a.TransiteCountryID).FirstOrDefault();

                    }
                    var Item_ShortName_List = itemss.Select(a => a.Item_ShortName_ID).ToList();
                    var itemCategories_List = itemss.Select(a => a.ItemCategory_ID).ToList();



                    //var dataConstrains_Country = (from Ecc in entities.Ex_CountryConstrain
                    //                              join c in entities.Countries on Ecc.Import_Country_ID equals c.ID
                    //                              //join c2 in entities.Countries on Ecc.TransportCountry_ID equals c2.ID
                    //                              where Ecc.User_Deletion_Id == null
                    //                              && Ecc.IsActive == true
                    //                              && Ecc.User_Deletion_Date == null
                    //                              && Ecc.Import_Country_ID == ExportCountryId


                    //                              //&& Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)
                    //                              && Item_ShortName_List.Contains(Ecc.Item_ShortName_id) //Ecc.Item_ShortName_id == itemShortNameId
                    //                                                                                     //&& Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)
                    //                              && Ecc.User_Updation_Date == null

                    //                              select new Ex_CountryConstrainDTO
                    //                              {
                    //                                  ID = Ecc.ID,
                    //                                  IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                    //                                  IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                    //                                  IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                    //                                  ItemCategories_ID = Ecc.ItemCategories_ID,
                    //                                  Import_Country_Name = c.Ar_Name,

                    //                                  Item_Short_Name = Ecc.Item_ShortName.ShortName_Ar,
                    //                                  Item_ShortName_id = Ecc.Item_ShortName.ID
                    //                              }).ToList();

                    //string x = "";
                    ////if (dataConstrains_Country.Count()>0)
                    ////{
                    //x = entities.Countries.Where(a => a.ID == ExportCountryId).Select(a => a.Ar_Name).FirstOrDefault();
                    ////}
                    ////else
                    ////{

                    ////}

                    //if (dataConstrains_Country.Count() != Item_ShortName_List.Count())
                    //{
                    //    //var filteredList = dataConstrains_Country.Select(a => a.Item_ShortName_id).ToList();//.Except( Item_ShortName_List);
                    //    var Item_ShortName_ListOld = Item_ShortName_List.Distinct().ToList();///.Where(a=>a.Value != );
                    //    var Item_ShortName_ListNew = Item_ShortName_ListOld.Where(p => dataConstrains_Country.All(p2 => p2.Item_ShortName_id != p.Value)).ToList();
                    //    var dataConstrains_Country0 = (from Ecc in entities.Ex_CountryConstrain
                    //                                       //           join c in entities.Countries on Ecc.Import_Country_ID equals c.ID
                    //                                       //join c2 in entities.Countries on Ecc.TransportCountry_ID equals c2.ID
                    //                                   where Ecc.User_Deletion_Id == null
                    //                                   && Ecc.IsActive == true
                    //                                   && Ecc.User_Deletion_Date == null
                    //                                   && Ecc.Import_Country_ID == 0

                    //                                   //&& Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)
                    //                                    && Item_ShortName_ListNew.Contains(Ecc.Item_ShortName_id)
                    //                                   && Ecc.User_Updation_Date == null

                    //                                   select new Ex_CountryConstrainDTO
                    //                                   {
                    //                                       ID = Ecc.ID,
                    //                                       IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                    //                                       IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                    //                                       IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                    //                                       ItemCategories_ID = Ecc.ItemCategories_ID,
                    //                                       Import_Country_Name = x,

                    //                                       Item_Short_Name = Ecc.Item_ShortName.ShortName_Ar
                    //                                   }).ToList();
                    //    dataConstrains_Country = dataConstrains_Country.Union(dataConstrains_Country0).ToList();
                    //}


                    ////var dataConstrains_NowCountry = (from Ecc in entities.Ex_CountryConstrain

                    ////                              where Ecc.User_Deletion_Id == null
                    ////                              && Ecc.IsActive == true
                    ////                              && Ecc.User_Deletion_Date == null
                    ////                              && Ecc.Import_Country_ID == 0
                    ////                              && Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)
                    ////                              && Item_ShortName_List.Contains(Ecc.Item_ShortName_id) //Ecc.Item_ShortName_id == itemShortNameId
                    ////                                                                                     //&& Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)
                    ////                              && Ecc.User_Updation_Date == null

                    ////                              select new Ex_CountryConstrainDTO
                    ////                              {
                    ////                                  ID = Ecc.ID,
                    ////                                  IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                    ////                                  IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                    ////                                  IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                    ////                                  ItemCategories_ID = Ecc.ItemCategories_ID,
                    ////                                  Import_Country_Name = "كل دول العالم",

                    ////                                  Item_Short_Name = Ecc.Item_ShortName.ShortName_Ar
                    ////                              }).ToList();

                    ////var dataConstrains555 = dataConstrains_Country.Union(dataConstrains_NowCountry).ToList();  
                    ////var y = dataConstrains_Country.Union(dataConstrains_Country0).ToList();
                    //var y = dataConstrains_Country.ToList();
                    //var dataConstrains555 = y;

                    //foreach (var item in dataConstrains555)
                    //{
                    //    if (dataConstrains555 != null)
                    //    {
                    //        var CountryConstrain_ID = item.ID;
                    //        //var Item_ShortName_Constrain_ID = dataConstrains.Item_ShortName_id;
                    //        var Constrain_Text = (from CCt in entities.Ex_CountryConstrain_Text
                    //                              join Txt in entities.EX_Constrain_Text on CCt.EX_Constrain_Text_ID equals Txt.ID
                    //                              join Typ in entities.EX_Constrain_Country_Item on Txt.EX_Constrain_Country_Item_ID equals Typ.ID
                    //                              // join ECC in entities.Ex_CountryConstrain on CCt.CountryConstrain_ID equals ECC.ID
                    //                              where CCt.User_Deletion_Id == null
                    //                               && CCt.User_Deletion_Date == null
                    //                            && CCt.CountryConstrain_ID == CountryConstrain_ID
                    //                              // && ECC.Item_ShortName_id == Item_ShortName_Constrain_ID
                    //                              select new Ex_CountryConstrain_TextDTO
                    //                              {
                    //                                  EX_Constrain_Text_ID = Txt.ID,
                    //                                  Ar_Name_Constrain_Type = Typ.Ar_Name,
                    //                                  En_Name_Constrain_Type = Typ.En_Name,
                    //                                  ConstrainText_Ar = Txt.ConstrainText_Ar,
                    //                                  ConstrainText_En = Txt.ConstrainText_En,
                    //                                  IsCertificate_Addtion = (bool)Txt.IsCertificate_Addtion,
                    //                                  InSide_Certificate_Ar = Txt.InSide_Certificate_Ar,
                    //                                  InSide_Certificate_En = Txt.InSide_Certificate_En,
                    //                              }).ToList();

                    //        var Constrain_Analysis = (
                    //                 from CCA in entities.Ex_CountryConstrain_AnalysisLabType
                    //                 join At in entities.AnalysisTypes on CCA.AnalysisTypeID equals At.ID
                    //                 where CCA.User_Deletion_Id == null
                    //                 //  && CCA.IsActive == true
                    //                 && CCA.User_Deletion_Date == null
                    //                 && CCA.CountryConstrain_ID == CountryConstrain_ID
                    //               && At.User_Deletion_Id == null
                    //               && At.User_Deletion_Date == null

                    //                 select new Ex_CountryConstrain_AnalysisLabTypeDTO
                    //                 {
                    //                     AnalysisTypeID = CCA.AnalysisTypeID,
                    //                     TypeName_Ar = At.Name_Ar,
                    //                     TypeName_En = At.Name_En,
                    //                     ExConstrainsLabsAndTypID = CCA.ID
                    //                 }).ToList();

                    //        var Constrain_ArrivalPort = (from CCA in entities.Ex_CountryConstrain_ArrivalPort
                    //                                     join pil in entities.Port_International on CCA.Port_International_Id equals pil.ID
                    //                                     join Ci in entities.Countries on pil.Country_ID equals Ci.ID
                    //                                     join v in entities.Port_Type on pil.PortTypeID equals v.ID
                    //                                     where CCA.User_Deletion_Id == null
                    //                                     && CCA.User_Deletion_Date == null
                    //                                     && CCA.Ex_CountryConstrain_Id == CountryConstrain_ID
                    //                                   && pil.User_Deletion_Date == null
                    //                                   && pil.User_Deletion_Id == null
                    //                                   && Ci.User_Deletion_Id == null
                    //                                   && Ci.User_Deletion_Date == null
                    //                                   && v.User_Deletion_Id == null
                    //                                   && v.User_Deletion_Date == null
                    //                                     select new Ex_CountryConstrain_ArrivalPortDTO
                    //                                     {
                    //                                         AirPortName_Ar = v.Name_Ar,
                    //                                         AirPortName_En = v.Name_En,
                    //                                         CountryName_Ar = pil.Name_Ar,
                    //                                         CountryLabName_En = pil.Name_En,
                    //                                         ExConstrainsAirPortAndCountryID = pil.ID
                    //                                     }).ToList();

                    //        var Constrain_Treatment = (from ect in entities.Ex_CountryConstrain_Treatment
                    //                                       // join excht in entities.EX_Choose_Treatment on ect.ID equals excht.Ex_CountryConstrain_Treatment_id into excht1
                    //                                       //from excht in excht1.DefaultIfEmpty()
                    //                                       //join excht in entities.EX_Choose_Treatment on ect.ID equals excht.Ex_CountryConstrain_Treatment_id into excht1
                    //                                       //from excht in excht1.DefaultIfEmpty()
                    //                                       //join exx in entities.Ex_CheckRequest on excht.Ex_CheckRequest_ID equals exx.ID into exx1
                    //                                       //from exx in exx1.DefaultIfEmpty()
                    //                                   where ect.User_Deletion_Id == null
                    //                                   && ect.User_Deletion_Date == null
                    //                                   && ect.CountryConstrain_ID == CountryConstrain_ID
                    //                                 && ect.User_Deletion_Date == null
                    //                                && ect.User_Deletion_Id == null
                    //                                   //&& check.Ex_CheckRequest_ID
                    //                                   //&& exx.CheckRequest_Number == EXCheckRequest_Number
                    //                                   select new Ex_CountryConstrain_TreatmentDTO
                    //                                   {
                    //                                       TreatmentMethod_Ar_Name = ect.TreatmentMethod.Ar_Name,
                    //                                       TreatmentMethod_En_Name = ect.TreatmentMethod.En_Name,

                    //                                       TreatmentType_Ar_Name = ect.TreatmentMethod.TreatmentType.Ar_Name,
                    //                                       TreatmentType_En_Name = ect.TreatmentMethod.TreatmentType.En_Name,

                    //                                       TreatmentMainType_Ar_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.Ar_Name,
                    //                                       TreatmentMainType_En_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.En_Name,
                    //                                       IS_Optional = ect.IS_Optional,
                    //                                       Parent_ID = ect.ID,
                    //                                       TreatmentMethods_ID = ect.TreatmentMethods_ID,
                    //                                       //Constrain_Treatment_Already_Choosen = excht.IS_Optional == false ? "تم الاختيار" : "",
                    //                                       ////EX_Choose_Treatment_ID = excht.ID,
                    //                                       //EX_Choose_Treatment_IS_Optional = excht.IS_Optional

                    //                                   }).Distinct().ToList();

                    //        foreach (var item_Constrain_Treatment in Constrain_Treatment)
                    //        {
                    //            var Constrain_Treatment33 = (from excht in entities.EX_Choose_Treatment

                    //                                         where excht.Ex_CountryConstrain_Treatment_id == item_Constrain_Treatment.Parent_ID
                    //                                         && excht.Ex_CheckRequest_ID == check.Ex_CheckRequest_ID
                    //                                         select new Ex_CountryConstrain_TreatmentDTO
                    //                                         {
                    //                                             Constrain_Treatment_Already_Choosen = excht.IS_Optional == false ? "تم الاختيار" : "",
                    //                                             EX_Choose_Treatment_IS_Optional = excht.IS_Optional
                    //                                         }).FirstOrDefault();
                    //            if (Constrain_Treatment33 != null)
                    //            {
                    //                item_Constrain_Treatment.Constrain_Treatment_Already_Choosen = Constrain_Treatment33.Constrain_Treatment_Already_Choosen;
                    //                item_Constrain_Treatment.EX_Choose_Treatment_IS_Optional = Constrain_Treatment33.EX_Choose_Treatment_IS_Optional;
                    //            }
                    //        }


                    //        //var Constrain_Treatment = (from ect in entities.Ex_CountryConstrain_Treatment
                    //        //                           //join excht in entities.EX_Choose_Treatment on ect.ID equals excht.Ex_CountryConstrain_Treatment_id into excht1
                    //        //                           //from excht in excht1.DefaultIfEmpty()
                    //        //                               //join excht in entities.EX_Choose_Treatment on ect.ID equals excht.Ex_CountryConstrain_Treatment_id into excht1
                    //        //                               //from excht in excht1.DefaultIfEmpty()
                    //        //                               //join exx in entities.Ex_CheckRequest on excht.Ex_CheckRequest_ID equals exx.ID into exx1
                    //        //                               //from exx in exx1.DefaultIfEmpty()
                    //        //                           where ect.User_Deletion_Id == null
                    //        //                           && ect.User_Deletion_Date == null
                    //        //                           && ect.CountryConstrain_ID == CountryConstrain_ID
                    //        //                         && ect.User_Deletion_Date == null
                    //        //                        && ect.User_Deletion_Id == null
                    //        //                           //&& exx.CheckRequest_Number == EXCheckRequest_Number
                    //        //                           select new Ex_CountryConstrain_TreatmentDTO
                    //        //                           {
                    //        //                               TreatmentMethod_Ar_Name = ect.TreatmentMethod.Ar_Name,
                    //        //                               TreatmentMethod_En_Name = ect.TreatmentMethod.En_Name,

                    //        //                               TreatmentType_Ar_Name = ect.TreatmentMethod.TreatmentType.Ar_Name,
                    //        //                               TreatmentType_En_Name = ect.TreatmentMethod.TreatmentType.En_Name,

                    //        //                               TreatmentMainType_Ar_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.Ar_Name,
                    //        //                               TreatmentMainType_En_Name = ect.TreatmentMethod.TreatmentType.TreatmentMainType.En_Name,
                    //        //                               IS_Optional = ect.IS_Optional,
                    //        //                               Parent_ID = ect.ID,
                    //        //                               TreatmentMethods_ID = ect.TreatmentMethods_ID,
                    //        //                              // Constrain_Treatment_Already_Choosen = excht.IS_Optional == false ? "تم الاختيار" : ""
                    //        //                               //EX_Choose_Treatment_ID = excht.ID,
                    //        //                               //EX_Choose_Treatment_IS_Optional = excht.IS_Optional

                    //        //                           }).Distinct().ToList();

                    //        //var Constrain_Treatment_Choosen = (from excht in entities.EX_Choose_Treatment
                    //        //                                       // join excht in entities.EX_Choose_Treatment on ect.ID equals excht.Ex_CountryConstrain_Treatment_id
                    //        //                                       //join excht in entities.EX_Choose_Treatment on ect.ID equals excht.Ex_CountryConstrain_Treatment_id into excht1
                    //        //                                       //from excht in excht1.DefaultIfEmpty()
                    //        //                                   join exx in entities.Ex_CheckRequest on excht.Ex_CheckRequest_ID equals exx.ID into exx1
                    //        //                                   from exx in exx1.DefaultIfEmpty()
                    //        //                                   where
                    //        //                                    exx.CheckRequest_Number == EXCheckRequest_Number
                    //        //                                   select new Ex_CountryConstrain_TreatmentDTO
                    //        //                                   {

                    //        //                                       EX_Choose_Treatment_ID = excht.ID,
                    //        //                                       EX_Choose_Treatment_IS_Optional = excht.IS_Optional,
                    //        //                                       Constrain_Treatment_Already_Choosen = excht.IS_Optional == false ? "تم الاختيار" : ""
                    //        //                                   }).Distinct().ToList();

                    //        //var hkh = (from ct in Constrain_Treatment
                    //        //           join ctc in Constrain_Treatment_Choosen on ct.ID equals ctc.EX_Choose_Treatment_ID into ctc1
                    //        //           from ctc in ctc1.DefaultIfEmpty()
                    //        //           select new Ex_CountryConstrain_TreatmentDTO
                    //        //           {
                    //        //               TreatmentMethod_Ar_Name = ct.TreatmentMethod_Ar_Name,
                    //        //               TreatmentMethod_En_Name = ct.TreatmentMethod_En_Name,

                    //        //               TreatmentType_Ar_Name = ct.TreatmentType_Ar_Name,
                    //        //               TreatmentType_En_Name = ct.TreatmentType_En_Name,

                    //        //               TreatmentMainType_Ar_Name = ct.TreatmentMainType_Ar_Name,
                    //        //               TreatmentMainType_En_Name = ct.TreatmentMainType_En_Name,
                    //        //               IS_Optional = ct.IS_Optional,
                    //        //               Parent_ID = ct.Parent_ID,
                    //        //               TreatmentMethods_ID = ct.TreatmentMethods_ID,
                    //        //               dddd= ctc.dddd
                    //        //               //EX_Choose_Treatment_ID = excht.ID,
                    //        //               //EX_Choose_Treatment_IS_Optional = excht.IS_Optional

                    //        //           }).Distinct().ToList();
                    //        //int c = 10;
                    //        // List<object> myInts = new List<object>();
                    //        item.AnalysisLabType = Constrain_Analysis;
                    //        item.CountryConstrain_TextDTO = Constrain_Text;
                    //        // myInts.Add(dataConstrains.CountryConstrain_TextDTO);
                    //        item.ConstraintAirPortInternational = Constrain_ArrivalPort;
                    //        item.Constraint_Treatment = Constrain_Treatment;
                    //    }
                    //}


                    //CheckRequestDetails.FirstOrDefault().Ex_CountryConstrain = dataConstrains555;
                    foreach (var itt in itemss)
                    {

                        if (itt.Item_ShortName_ID != null)
                        {
                            var ism = uow.Repository<Item_ShortName>().GetData().
                        Where(i => i.ID == itt.Item_ShortName_ID && i.User_Deletion_Id == null).FirstOrDefault();
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


                        //var dataConstrains = (from Ecc in entities.Ex_CountryConstrain
                        //                      where Ecc.User_Deletion_Id == null
                        //                      && Ecc.IsActive == true
                        //                      && Ecc.User_Deletion_Date == null
                        //                      && Ecc.Import_Country_ID == ExportCountryId
                        //                      && Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)


                        //                      && Ecc.Item_ShortName_id == itemShortNameId

                        //                      && Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)

                        //                      && Ecc.User_Updation_Date == null

                        //                      select new Ex_CountryConstrainDTO
                        //                      {
                        //                          ID = Ecc.ID,
                        //                          IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                        //                          IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                        //                          IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                        //                          ItemCategories_ID = Ecc.ItemCategories_ID
                        //                      }).FirstOrDefault();

                        //if (dataConstrains == null && TransiteCountryId == 0)
                        //{
                        //    dataConstrains = (from Ecc in entities.Ex_CountryConstrain
                        //                      where Ecc.User_Deletion_Id == null
                        //                      && Ecc.IsActive == true
                        //                      && Ecc.User_Deletion_Date == null
                        //                      && Ecc.Import_Country_ID == 0
                        //                      && Ecc.Item_ShortName_id == itemShortNameId

                        //                      && Ecc.ItemCategories_ID == (itemCategories_ID > 0 ? itemCategories_ID : null)

                        //                      && Ecc.User_Updation_Date == null
                        //                      && Ecc.TransportCountry_ID == (TransiteCountryId > 0 ? TransiteCountryId : null)
                        //                      select new Ex_CountryConstrainDTO
                        //                      {
                        //                          ID = Ecc.ID,
                        //                          IsStationAccreditation = (bool)Ecc.IsStationAccreditation,
                        //                          IsCompanyAccreditation = (bool)Ecc.IsCompanyAccreditation,
                        //                          IsFarmAccreditation = (bool)Ecc.IsFarmAccreditation,
                        //                          ItemCategories_ID = Ecc.ItemCategories_ID
                        //                      }).FirstOrDefault();
                        //}



                        //CheckRequestDetails.FirstOrDefault().Ex_CountryConstrain = dataConstrains;
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
                        }
                        //List categories And lots

                        var catAndLots = uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Ex_CheckRequest_Items_ID == itt.Ex_Items_checkReqID)
                            .Select(v => new categories_lots
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
                                ItemcategoryName = v.Ex_CheckRequest_Items.ItemCategory.Name_Ar,

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
                                           .Select(x => new Attachments
                                           {
                                               Attachment_Number = x.Attachment_Number,
                                               AttachmentPath = x.AttachmentPath,
                                               Attachment_TypeName = x.Attachment_TypeName,
                                               StartDate = x.StartDate,
                                               EndDate = x.EndDate,
                                               Attachment_Name = (lang == "1" ? x.A_AttachmentTableType.Ar_Name : x.A_AttachmentTableType.En_Name)


                                           }).ToList();
                CheckRequestDetails.FirstOrDefault().Attachments = attach;

                //Attachments
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
                                 select new Fees_Item
                                 {

                                     ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                     ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                     Fees = grp.Sum(q => q.Fees),
                                     GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                     Net_Weight = grp.Sum(q => q.Net_Weight),
                                 }).Distinct().ToList();
                if (item_Fees.Count > 0)
                {
                    var fees = uow.Repository<Ex_CheckRequest_Fees>().GetData().Where(d => d.Ex_CheckRequest_ID == _Ex_CheckRequest_ID).FirstOrDefault().Total_Amount;
                    if (fees != null || fees > 0)
                    {


                        item_Fees.FirstOrDefault().Fees = uow.Repository<Ex_CheckRequest_Fees>().GetData().Where(d => d.Ex_CheckRequest_ID == _Ex_CheckRequest_ID).FirstOrDefault().Total_Amount;
                    }
                    else
                    {
                        item_Fees.FirstOrDefault().Fees = 0;
                    }

                }


                CheckRequestDetails.FirstOrDefault().Fees_Item_All = item_Fees;
                CheckRequestDetails.FirstOrDefault().Fees_Item_Shift_All = (from cms in entities.Ex_RequestCommittee_Shift
                                                                            where cms.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                                                            && cms.Ex_RequestCommittee.IsApproved == true
                                                                            select new Fees_Item_Shift
                                                                            {
                                                                                Amount_Per_Shift = cms.Amount,
                                                                                Count_Per_Shift = cms.Count,
                                                                                total_Per_Shift = (cms.Count * cms.Amount)
                                                                            }).ToList();

                var _total_Per_Shift = CheckRequestDetails.FirstOrDefault().Fees_Item_Shift_All.Select(z => z.total_Per_Shift).Sum();

                CheckRequestDetails.FirstOrDefault().Shift_Item_All = _total_Per_Shift;

                #region رسم  النوبتجية
                // 
                //var day = DateTime.UtcNow.AddDays(1);
                var Fees_Shift = (from sh in entities.Ex_RequestCommittee_Shift

                                  where sh.Ex_RequestCommittee.Ex_CheckRequest.ID == _Ex_CheckRequest_ID
                                               && sh.Ex_RequestCommittee.IsApproved == true
                                  //&&sh.Ex_RequestCommittee.IsFinishedAll== true
                                  //&& (DbFunctions.TruncateTime( sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date )))
                                  // && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date),1))

                                  //.Where(p => EntityFunctions.AddDays(p.StartDate, p.Period)
                                  group sh by new
                                  {

                                      ID = sh.ID,
                                      Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
                                      IsPaidCommittee = sh.Ex_RequestCommittee.IsPaid,
                                      Delegation_Date = sh.Ex_RequestCommittee.Delegation_Date,
                                      User_Creation_Date = sh.Ex_RequestCommittee.User_Creation_Date,
                                      IsApproved = sh.Ex_RequestCommittee.IsApproved,
                                      IsFinishedAll = sh.Ex_RequestCommittee.IsFinishedAll,
                                      Is_Cancel = sh.Ex_RequestCommittee.Is_Cancel,
                                      StartTime = sh.Ex_RequestCommittee.StartTime,
                                      EndTime = sh.Ex_RequestCommittee.EndTime,
                                      CommitteTypeName = sh.Ex_RequestCommittee.CommitteeType.Name_Ar,
                                      CommitteeType_ID = sh.Ex_RequestCommittee.CommitteeType_ID,

                                  } into grp
                                  select new List_Shift
                                  {
                                      CommitteTypeName = grp.Key.CommitteTypeName,
                                      StartTime = grp.Key.StartTime,
                                      EndTime = grp.Key.EndTime,
                                      Delegation_Date = grp.Key.Delegation_Date,
                                      User_Creation_Date = grp.Key.User_Creation_Date,
                                      ID = grp.Key.ID,
                                      Shift_Name = grp.Key.Shift_Name,
                                      IsPaidCommittee = grp.Key.IsPaidCommittee,
                                      Shift_Count = grp.Sum(q => q.Count),
                                      Shift_Amount = grp.Sum(q => q.Amount),
                                      IsApproved = grp.Key.IsApproved,
                                      IsFinishedAll = grp.Key.IsFinishedAll,
                                      Is_Cancel = grp.Key.Is_Cancel,
                                      Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                                      CommitteeType_ID = grp.Key.CommitteeType_ID,
                                  }).Distinct().ToList();
                //.Where(p => p.User_Creation_Date.AddDays(1) >= p.Delegation_Date)
                //if (Fees_Shift.Count==0 || Fees_Shift !=null)
                //{
                //     Fees_Shift = (from sh in entities.Ex_RequestCommittee_Shift

                //                      where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                //                                    //&& sh.Ex_RequestCommittee.IsApproved == true// && sh.Ex_RequestCommittee.IsPaid == true
                //                                  //&& (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date)))
                //                                //  && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))

                //                      //.Where(p => EntityFunctions.AddDays(p.StartDate, p.Period)
                //                      group sh by new
                //                      {

                //                          ID = sh.ID,
                //                          Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
                //                          IsPaidCommittee = sh.Ex_RequestCommittee.IsPaid,
                //                          Delegation_Date=sh.Ex_RequestCommittee.Delegation_Date,
                //                          User_Creation_Date=sh.Ex_RequestCommittee.User_Creation_Date,
                //                          CommitteeType_ID =sh.Ex_RequestCommittee.CommitteeType_ID,

                //                      } into grp
                //                      select new List_Shift
                //                      {
                //                          Delegation_Date=grp.Key.Delegation_Date,
                //                          User_Creation_Date = grp.Key.User_Creation_Date,
                //                          ID = grp.Key.ID,
                //                          Shift_Name = grp.Key.Shift_Name,
                //                          IsPaidCommittee = grp.Key.IsPaidCommittee,
                //                          Shift_Count = grp.Sum(q => q.Count),
                //                          Shift_Amount = grp.Sum(q => q.Amount),
                //                          CommitteeType_ID = grp.Key.CommitteeType_ID,
                //                          Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                //                      }).Distinct().ToList(); //.Where(p => p.User_Creation_Date.AddDays(1) >= p.Delegation_Date)
                //}                //if (Fees_Shift.Count==0 || Fees_Shift !=null)
                //{
                //     Fees_Shift = (from sh in entities.Ex_RequestCommittee_Shift

                //                      where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                //                                    //&& sh.Ex_RequestCommittee.IsApproved == true// && sh.Ex_RequestCommittee.IsPaid == true
                //                                  //&& (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date)))
                //                                //  && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))

                //                      //.Where(p => EntityFunctions.AddDays(p.StartDate, p.Period)
                //                      group sh by new
                //                      {

                //                          ID = sh.ID,
                //                          Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
                //                          IsPaidCommittee = sh.Ex_RequestCommittee.IsPaid,
                //                          Delegation_Date=sh.Ex_RequestCommittee.Delegation_Date,
                //                          User_Creation_Date=sh.Ex_RequestCommittee.User_Creation_Date,
                //                          CommitteeType_ID =sh.Ex_RequestCommittee.CommitteeType_ID,

                //                      } into grp
                //                      select new List_Shift
                //                      {
                //                          Delegation_Date=grp.Key.Delegation_Date,
                //                          User_Creation_Date = grp.Key.User_Creation_Date,
                //                          ID = grp.Key.ID,
                //                          Shift_Name = grp.Key.Shift_Name,
                //                          IsPaidCommittee = grp.Key.IsPaidCommittee,
                //                          Shift_Count = grp.Sum(q => q.Count),
                //                          Shift_Amount = grp.Sum(q => q.Amount),
                //                          CommitteeType_ID = grp.Key.CommitteeType_ID,
                //                          Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                //                      }).Distinct().ToList(); //.Where(p => p.User_Creation_Date.AddDays(1) >= p.Delegation_Date)
                //}

                CheckRequestDetails.FirstOrDefault().List_Shift = Fees_Shift;



                #endregion
                #region رسم  المهندسين
                // 
                var Fees_Engineers = (from sh in entities.Ex_RequestCommittee_Fees_ENG


                                      where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                     && sh.Ex_RequestCommittee.IsApproved == true //&& sh.Ex_RequestCommittee.IsFinishedAll == true
                                      // && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date)))
                                      //        && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))
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
                                      select new List_ShiftEngineers
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

                //if (Fees_Engineers.Count == 0 || Fees_Engineers != null)
                //{
                //     Fees_Engineers = (from sh in entities.Ex_RequestCommittee_Fees_ENG


                //                          where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                //                          && sh.Ex_RequestCommittee.IsApproved == true //&& sh.Ex_RequestCommittee.IsPaid == true
                //                         //  && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.User_Creation_Date)))
                //                                  //&& (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))
                //                          group sh by new
                //                          {
                //                              ID = sh.ID,
                //                              Shift_Name = lang == "1" ? sh.EX_Fees_Type.Name : sh.EX_Fees_Type.Name,
                //                              IsPaidEngineers = sh.Ex_RequestCommittee.IsPaid,
                //                              Num_Eng = sh.Num_Eng,
                //                              Shift_Amount = sh.Value,
                //                              CommitteeType_ID=sh.Ex_RequestCommittee.CommitteeType_ID

                //                          } into grp
                //                          select new List_ShiftEngineers
                //                          {
                //                              ID = grp.Key.ID,
                //                              CommitteeType_ID=grp.Key.CommitteeType_ID,
                //                              Shift_Name = grp.Key.Shift_Name,
                //                              IsPaidEngineers = grp.Key.IsPaidEngineers,
                //                              Num_Eng = grp.Key.Num_Eng,
                //                              Shift_Amount = grp.Key.Shift_Amount,
                //                              Shift_Sum_All = grp.Key.Num_Eng * grp.Key.Shift_Amount,

                //                          }).Distinct().ToList();


                //}

                CheckRequestDetails.FirstOrDefault().List_ShiftEngineers = Fees_Engineers;
                #endregion

                #region   رسوم السحب




                var Fees_Sample = (from sm in entities.Ex_CheckRequest_SampleData

                                   where sm.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                   && sm.Ex_RequestCommittee.CommitteeType_ID == 3 && sm.Count_Sample != null && sm.Amount != null
                                   && sm.Ex_RequestCommittee.IsApproved == true //&& sm.Ex_RequestCommittee.IsFinishedAll == true
                                    && (DbFunctions.TruncateTime(sm.Ex_RequestCommittee.Delegation_Date) >= (DbFunctions.TruncateTime(sm.Ex_RequestCommittee.User_Creation_Date)))
                                   //  && (DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date) <= DbFunctions.AddDays(DbFunctions.TruncateTime(sh.Ex_RequestCommittee.Delegation_Date), 1))
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
                                   select new List_Sample
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


                CheckRequestDetails.FirstOrDefault().List_Sample = Fees_Sample44;




                #endregion

                #region رسوم المعالجة
                //جزئى


                var Fees_Treatment = (from rq in entities.Ex_RequestCommittee
                                      join td in entities.Ex_Request_TreatmentData on rq.ID equals td.Ex_RequestCommittee_ID
                                      where td.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                     && rq.CommitteeType_ID == 6 && rq.Status == true
                                           && rq.IsApproved == true && rq.IsFinishedAll == true
                                    && (DbFunctions.TruncateTime(rq.Delegation_Date) >= (DbFunctions.TruncateTime(rq.User_Creation_Date)))
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
                                          Is_Paid_Treatment2 = rq.IsPaid
                                      } into grp
                                      select new List_Treatment
                                      {
                                          ID = grp.Key.ID,
                                          TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                          TreatmentType_Name = grp.Key.TreatmentType_Name,
                                          TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                          Treatment_Amount = grp.Key.Treatment_Amount,
                                          Is_Paid_Treatment2 = grp.Key.Is_Paid_Treatment2
                                      }).Distinct().ToList();
                // //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();
                //if (Fees_Treatment.Count() > 0)
                //{
                //    foreach (var item in Fees_Treatment)
                //    {
                //        long ID_Item = long.Parse(item.ID.ToString());
                //        //var _Treatment = (from ftd in entities.Fees_Transactions_Detiles
                //        //                  where ftd.TreatmentData_ID == ID_Item
                //        //                  select new List_Treatment
                //        //                  {
                //        //                      //Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
                //        //                  }
                //        //           ).ToList();
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
                CheckRequestDetails.FirstOrDefault().List_Treatment = Fees_Treatment;


                #endregion

                ///

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
                                     select new List_Fees_Martyrs
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
                                     select new List_Fees_Martyrs
                                     {
                                         Name_Ar = grp.Key.Name_Ar,
                                         Total_Amount = grp.Sum(q => q.Amount),
                                         table_name_id = grp.Key.table_name_id
                                     }).Distinct().ToList();
                var Fees_Martyrs = (Fees_Martyrs1.Union(Fees_Martyrs2)).ToList();
                var list1 = Fees_Martyrs.Where(a => a.table_name_id == 8);
                var list2 = Fees_Martyrs.Where(a => a.table_name_id == 9);

                var Fees_Martyrslist = (list1.Union(list2)).ToList();
                CheckRequestDetails.FirstOrDefault().List_Fees_Martyrs = Fees_Martyrslist;
                #endregion


                decimal item_Fees_Total = 0;


                if (item_Fees != null)
                {
                    item_Fees_Total = item_Fees.Select(a => a.Fees).Sum().Value;
                }

                var Sum_List_Sample = CheckRequestDetails.FirstOrDefault().List_Sample.Select(c => c.Sample_Sum_All).Sum();



                CheckRequestDetails.FirstOrDefault().SUM_Shift_Fees_Item = 10 + _total_Per_Shift + item_Fees_Total + Sum_List_Sample;
                //                    ///////////////ESLAM///////////////
                //                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
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

    }
}