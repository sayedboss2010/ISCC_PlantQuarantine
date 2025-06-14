using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.DTO.Im_CheckRequest_General;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Im_CheckRequest_General
{
    public class RequestDetailsGeneralBLL
    {
        private UnitOfWork uow;


        public RequestDetailsGeneralBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImCheckRequestDetails
           (string Request_Number, int? Outlet_ID, List<string> Device_Info)
        {



            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];

                //if (Operation_Type == 0)
                //{
                var CheckRequestDetails = (from cc in entities.Im_CheckRequest
                                           join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID
                                           join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                           //join Out in entities.Outlets on cc.Outlet_ID equals Out.ID into ps                                                                                     
                                           //from Out in ps.DefaultIfEmpty()
                                           where cc.CheckRequest_Number == Request_Number


                                           select new Im_CheckRequestDetails_General_DTO
                                           {
                                               Im_CheckRequest_ID = cc.ID,
                                               IsAccepted = cc.IsAccepted,
                                               IsActive = cc.IsActive,
                                               IsPaid = cc.IsPaid,
                                               Im_CheckRequestData_ID = rr.ID,
                                               ImCheckRequest_Number = cc.CheckRequest_Number,
                                               Ship_Name = rr.Ship_Name,
                                               ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                               Importer_ID = rr.Importer_ID,
                                               ImporterType_Id = rr.ImporterType_Id,
                                               Transport_Mean_Id = rr.Transport_Mean_Id,
                                               InternationalTransport_Id = rr.InternationalTransportation_ID,
                                               Shipment_Mean_Id = rr.Shipment_Mean_Id,
                                               outletName = Outlet_ID == null ? "" : cc.Outlet.Ar_Name,
                                               TransitCountryId = rr.TransitCountry_Id,
                                               MessageOwner = rr.DelegateName,
                                               MessageOwnerNationalID = rr.DelegateAddress,


                                           }).FirstOrDefault();



                if (CheckRequestDetails != null)
                {

                    #region الرسوم  

                    var item_Fees = (from im_i in entities.Im_CheckRequest_Items
                                     join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                     where im_i.Item_Permission_Number == Request_Number
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
                                         InitiatorCountry = im_i.Im_Initiator.Country.Ar_Name,
                                         InitiatorCountryEn = im_i.Im_Initiator.Country.En_Name,
                                     } into grp
                                     select new Fees_ItemGeneral
                                     {

                                         ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                         ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                         Fees = grp.Sum(q => q.Fees),
                                         GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                         Net_Weight = grp.Sum(q => q.Net_Weight),
                                     }).Distinct().ToList();



                    CheckRequestDetails.Fees_Item_All = item_Fees;

                    CheckRequestDetails.Fees_Item_Shift_All = (from cms in entities.Im_RequestCommittee_Shift
                                                               where cms.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == Request_Number
                                                               select new Fees_Item_ShiftGeneral
                                                               {
                                                                   Amount_Per_Shift = cms.Amount,
                                                                   Count_Per_Shift = cms.Count,
                                                                   total_Per_Shift = (cms.Count * cms.Amount)
                                                               }).ToList();

                    var _total_Per_Shift = CheckRequestDetails.Fees_Item_Shift_All.Select(z => z.total_Per_Shift).Sum();
                    CheckRequestDetails.Shift_Item_All = _total_Per_Shift;



                    #region رسم  النوبتجية
                    // 
                    var Fees_Item_Shift = (from sh in entities.Im_RequestCommittee_Shift

                                           where sh.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == Request_Number
                                           group sh by new
                                           {
                                               ID = sh.ID,
                                               Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,

                                           } into grp
                                           select new List_ShiftGeneral
                                           {
                                               ID = grp.Key.ID,
                                               Shift_Name = grp.Key.Shift_Name,
                                               Shift_Count = grp.Sum(q => q.Count),
                                               Shift_Amount = grp.Sum(q => q.Amount),
                                               Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                                           }).Distinct().ToList();
                    foreach (var item in Fees_Item_Shift)
                    {
                        long ID_Item = long.Parse(item.ID.ToString());
                        var _Shift = (from ftd in entities.Fees_Transactions_Detiles

                                      where ftd.Shift_ID == ID_Item
                                      && ftd.Fees_Transactions.TableName_ID == 4
                                      && ftd.Fees_Transactions.Table_ID == CheckRequestDetails.ID
                                      select new List_ShiftGeneral
                                      {
                                          Is_Paid_Shift = ftd.Shift_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                      }
                                   ).ToList();
                        if (_Shift.Count > 0)
                            item.Is_Paid_Shift = _Shift.FirstOrDefault().Is_Paid_Shift;
                        else
                            item.Is_Paid_Shift = null;
                    }

                    CheckRequestDetails.List_Shift = Fees_Item_Shift;
                    #endregion


                    #region   رسوم السحب




                    var Fees_Sample = (from sm in entities.Im_CheckRequest_SampleData

                                       where sm.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == Request_Number
                                       group sm by new
                                       {
                                           ID = sm.ID,
                                           Sample_BarCode = sm.Sample_BarCode,
                                           Is_Total = sm.IS_Total,
                                           Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                           Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,

                                       } into grp
                                       select new List_SampleGeneral
                                       {
                                           ID = grp.Key.ID,
                                           Sample_BarCode = grp.Key.Sample_BarCode,
                                           Laboratory_Name = grp.Key.Laboratory_Name,
                                           Sample_Name = grp.Key.Sample_Name,
                                           Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
                                           Sample_Count = grp.Count(q => q.AnalysisLabType_ID != null),
                                           Sample_Amount = 200,
                                           Sample_Sum_All = grp.Count(q => q.AnalysisLabType_ID != null) * 200,
                                       }).Distinct().ToList();

                    var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();

                    foreach (var item in Fees_Sample44)
                    {
                        long ID_Item = long.Parse(item.ID.ToString());
                        var _Sample = (from ftd in entities.Fees_Transactions_Detiles

                                       where ftd.SampleData_ID == ID_Item
                                       && ftd.Fees_Transactions.TableName_ID == 4
                                       && ftd.Fees_Transactions.Table_ID == CheckRequestDetails.ID
                                       select new List_SampleGeneral
                                       {
                                           Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                       }
                                   ).ToList();
                        if (_Sample.Count > 0)
                            item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
                        else
                            item.Is_Paid_Sample = null;
                    }
                    CheckRequestDetails.List_Sample = Fees_Sample44;
                    #endregion


                    #region رسوم المعالجة
                    //جزئى

                    //join pn1 in entities.PortNationals on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pn1.ID, b = 10 } into pn1
                    //                from pn in pn1.DefaultIfEmpty()

                    var Fees_Treatment = (from rq in entities.Im_RequestCommittee
                                          join td in entities.Im_Request_TreatmentData on rq.ID equals td.Im_RequestCommittee_ID
                                          where td.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == Request_Number
                                         && rq.CommitteeType_ID == 14 && rq.Status == true
                                          group td by new
                                          {
                                              ID = td.ID,
                                              TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                         entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                                              TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                         entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                                              TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                                                         entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                                              Treatment_Amount = td.Amount
                                          } into grp
                                          select new List_TreatmentGeneral
                                          {
                                              ID = grp.Key.ID,
                                              TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                              TreatmentType_Name = grp.Key.TreatmentType_Name,
                                              TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                              Treatment_Amount = grp.Key.Treatment_Amount
                                          }).Distinct().ToList();
                    //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();
                    if (Fees_Treatment.Count() > 0)
                    {
                        foreach (var item in Fees_Treatment)
                        {
                            long ID_Item = long.Parse(item.ID.ToString());
                            var _Treatment = (from ftd in entities.Fees_Transactions_Detiles


                                              where ftd.TreatmentData_ID == ID_Item
                                              //&& ftd.Fees_Transactions.TableName_ID == 4
                                              //&& ftd.TreatmentData_ID !=null                                     
                                              select new List_TreatmentGeneral
                                              {
                                                  // Treatment_Amount = ftd.Fees_Transactions.Amount_Total,
                                                  Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
                                              }
                                       ).ToList();
                            if (_Treatment.Count > 0)
                            {
                                item.Is_Paid_Treatment = _Treatment.FirstOrDefault().Is_Paid_Treatment;
                                // item.Treatment_Amount = _Treatment.FirstOrDefault().Treatment_Amount;
                            }
                            else
                            {
                                item.Is_Paid_Treatment = null;
                                //item.Treatment_Amount = null;
                            }
                        }

                    }


                    CheckRequestDetails.List_Treatment = Fees_Treatment;


                    #endregion


                    decimal item_Fees_Total = 0;


                    if (item_Fees != null)
                    {
                        item_Fees_Total = item_Fees.Select(a => a.Fees).Sum().Value;
                    }

                    var Sum_List_Sample = CheckRequestDetails.List_Sample.Select(c => c.Sample_Sum_All).Sum();



                    CheckRequestDetails.SUM_Shift_Fees_Item = 10 + _total_Per_Shift + item_Fees_Total + Sum_List_Sample;
                    // [Owner_Ar]
                    #endregion

                    #region ImporterType
                    if (CheckRequestDetails.ImporterType_Id == 6)
                    {
                        CheckRequestDetails.ImporterType = "شركة";
                        CheckRequestDetails.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                        CheckRequestDetails.ImporterAddress = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                        CheckRequestDetails.OwnerName = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Owner_Ar : s.Owner_En).FirstOrDefault();

                        //Add Company Activity
                        var CompanyActivities_Details = (from ca in entities.CompanyActivities
                                                             //join cat in entities.Enrollment_type on ca.Enrollment_type_ID equals cat.ID
                                                         where ca.Company_ID == CheckRequestDetails.Importer_ID
                                                         && ca.IsActive == true

                                                         select new CompanyActivityGeneralDTO
                                                         {

                                                             CompActivityType__Name = lang == "1" ? ca.A_SystemCode.ValueName : ca.A_SystemCode.ValueNameEN,
                                                             Enrollment_Name = ca.Enrollment_Name,
                                                             Enrollment_Number = ca.Enrollment_Number,
                                                             Enrollment_Start = ca.Enrollment_Start,
                                                             Enrollment_End = ca.Enrollment_End,
                                                             Enrollment_type_Name = lang == "1" ? ca.Enrollment_type.Ar_Name : ca.Enrollment_type.En_Name,

                                                         }).ToList();
                        CheckRequestDetails._CompanyActivitys = CompanyActivities_Details;
                        CheckRequestDetails.ImporterContacts = uow.Repository<Ex_ContactData>()
                       .GetData().Include(f => f.ContactType).
                  Where(a => a.Exporter_ID == CheckRequestDetails.Importer_ID && a.ExporterType_Id == 6).
                  Select(a => new ContactTypeDTO
                  {
                      Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
                      Value = a.Value
                  }).ToList();
                        //CheckRequestDetails.CompanyActivity= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_Name : s.Enrollment_Name).FirstOrDefault();
                        //CheckRequestDetails.Enrollment_Number= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_Number : s.Enrollment_Number).FirstOrDefault();
                        //CheckRequestDetails.Enrollment_Start= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_Start : s.Enrollment_Start).FirstOrDefault();
                        //CheckRequestDetails.Enrollment_End= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_End : s.Enrollment_End).FirstOrDefault();
                        //CheckRequestDetails.CompActivityType_ID = uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => s.CompActivityType_ID).FirstOrDefault();
                        //CheckRequestDetails.CompanyActivityType = uow.Repository<CompanyActivityType>().GetData().Where(a => a.ID == CheckRequestDetails.CompActivityType_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                    }
                    else if (CheckRequestDetails.ImporterType_Id == 7)
                    {
                        CheckRequestDetails.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                        CheckRequestDetails.ImporterAddress = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                        CheckRequestDetails.ImporterType = "هيئة";
                        CheckRequestDetails.ImporterContacts = uow.Repository<Ex_ContactData>()
                  .GetData().Include(f => f.ContactType).
             Where(a => a.Exporter_ID == CheckRequestDetails.Importer_ID && a.ExporterType_Id == 7).
             Select(a => new ContactTypeDTO
             {
                 Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
                 Value = a.Value
             }).ToList();
                    }
                    else
                    {
                        CheckRequestDetails.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Name).FirstOrDefault();
                        CheckRequestDetails.ImporterAddress = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Address).FirstOrDefault();
                        CheckRequestDetails.ImporterType = "فرد";
                        CheckRequestDetails.PersonContacts = uow.Repository<Person>()
                  .GetData().
             Where(a => a.ID == CheckRequestDetails.Importer_ID).
             Select(a => new PersonContactGeneral
             {
                 Email = (lang == "1" ? a.Email : a.Email),
                 Phone = a.Phone
             }).ToList();
                    }
                    #endregion

                    #region Port



                    // the problem is the same data
                    var All_port = (from icrp in entities.Im_CheckRequest_Port
                                        //join pn1 in entities.PortNationals on icrp.Port_ID equals pn1.ID into pn1
                                    join pn1 in entities.PortNationals on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pn1.ID, b = 10 } into pn1
                                    from pn in pn1.DefaultIfEmpty()

                                    join pi9_1 in entities.Port_International on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pi9_1.ID, b = 9 } into pi9_1
                                    from pi9 in pi9_1.DefaultIfEmpty()

                                    join pi11_1 in entities.Port_International on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pi11_1.ID, b = 11 } into pi11_1
                                    from pi11 in pi11_1.DefaultIfEmpty()

                                    join c9_1 in entities.Countries on pi9.Country_ID equals c9_1.ID into c9_1
                                    from c9 in c9_1.DefaultIfEmpty()
                                    join c11_1 in entities.Countries on pi11.Country_ID equals c11_1.ID into c11_1
                                    from c11 in c11_1.DefaultIfEmpty()
                                    join pt_1 in entities.Port_Type on icrp.Port_Type_ID equals pt_1.ID into pt_1
                                    from pt in pt_1.DefaultIfEmpty()
                                    where icrp.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID
                                    select new List_PortGeneral
                                    {
                                        ReqPortType_ID = icrp.ReqPortType_ID,
                                        // الدولة المصدرة
                                        ExportCountryName = lang == "1" ? c9.Ar_Name : c9.En_Name,
                                        TransportPortType = lang == "1" ? pt.Name_Ar : pt.Name_En,
                                        TransportPortName = lang == "1" ? pi9.Name_Ar : pi9.Name_En,
                                        //العبور
                                        TransitCountry = lang == "1" ? c11.Ar_Name : c11.En_Name,
                                        TransitPortType = lang == "1" ? pt.Name_Ar : pt.Name_En,
                                        TransitPort = lang == "1" ? pi11.Name_Ar : pi11.Name_En,
                                        //الوصول
                                        ArrivePortType = lang == "1" ? pt.Name_Ar : pt.Name_En,
                                        ArrivePortName = lang == "1" ? pn.Name_Ar : pn.Name_En,
                                        govNameAR = lang == "1" ? pn.Governate.Ar_Name : pn.Governate.En_Name,
                                    }).ToList();
                    CheckRequestDetails.List_Port = All_port;
                    #endregion
                    #region Transportation
                    var shipp = uow.Repository<Shipment_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Shipment_Mean_Id).FirstOrDefault();
                    if (shipp != null)
                    {
                        CheckRequestDetails.Shipment_MeanName = shipp.Ar_Name;
                    }
                    var internationalTransport = uow.Repository<InternationalTransportation>().GetData().Where(c => c.ID == CheckRequestDetails.InternationalTransport_Id).FirstOrDefault();
                    if (internationalTransport != null)
                    {
                        CheckRequestDetails.InternationalTransport = lang == "1" ? internationalTransport.Ar_Name : internationalTransport.En_Name;
                    }
                    var transport = uow.Repository<Transport_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Transport_Mean_Id).FirstOrDefault();
                    if (transport != null)
                    {
                        CheckRequestDetails.Transport_MeanName = transport.Ar_Name;
                    }
                    //var isNationalArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID == 10).FirstOrDefault().IsNational;

                    #endregion
                    #region get companies out egypt
                    // get companies out egypt

                    var com = uow.Repository<Im_CheckRequestData_Extra>().GetData().Where(i => i.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID).Select(v => new ImportersGeneral
                    {
                        ImporterCompany = v.ImportCompany,
                        ImporterCompanyAddress = v.ImporeterCompanyAddress,
                        ImporterCompanyEn = v.ImportCompany_EN,
                        ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN
                    }).ToList();
                    CheckRequestDetails.ImportersCompanies = com;
                    //end companies 
                    #endregion
                    var shippingMethods = uow.Repository<Im_CheckRequset_Shipping_Method>().GetData().Where(c => c.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID).Select(n => new checkRequestShippingGeneral
                    {
                        ID = n.ID,
                        containers_ID = n.containers_ID,
                        containers_type_ID = n.containers_type_ID,
                        ShipholdNumber = n.ShipholdNumber,
                        ContainerNumber = n.ContainerNumber,
                        NavigationalNumber = n.NavigationalNumber,
                        Total_Weight = n.Total_Weight
                    }).ToList();
                    // distinct items for constrains
                    var initiatorsId = new List<long?>();

                    foreach (var ship in shippingMethods)
                    {
                        var container = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_ID && c.SystemCodeTypeId == 28);
                        if (container != null)
                        {
                            ship.containerName = lang == "1" ? container.ValueName : container.ValueNameEN;
                        }
                        var containertype = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_type_ID && c.SystemCodeTypeId == 29);
                        if (container != null)
                        {
                            ship.containerType = lang == "1" ? containertype.ValueName : containertype.ValueNameEN;
                        }
                        //Items
                        var itemss = uow.Repository<Im_CheckRequest_Items>().GetData().Where(i => i.Im_CheckRequset_Shipping_Method_ID == ship.ID).
                            Select(v => new Items_checkReqGeneral
                            {
                                    //eslam 8-2021
                                    //edit eslam 06-03-2023 Get ISN and QG
                                    ID = v.ID,
                                Im_Initiator_ID = v.Im_Initiator_ID,
                                ImcheckReqItem_ID = v.ID,
                                ImcheckReqshippedMethod_ID = v.Im_CheckRequset_Shipping_Method_ID,
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
                                    //QualitativeGroup_Id=v.QualitativeGroup_Id

                                }).Distinct().ToList();

                        //var ids = uow.Repository<Im_CheckRequest_Items>().GetData().Where(i => i.Im_CheckRequset_Shipping_Method_ID == ship.ID).Select(i => i.Im_Initiator_ID).Distinct().ToList();
                        //if (ids !=null)
                        //{
                        //    initiatorsId.AddRange(ids); 
                        //}
                        //end Items

                        foreach (var itt in itemss)
                        {
                            var initiatir = uow.Repository<Im_Initiator>().GetData().Include(f => f.Country)
                                .Where(u => u.ID == itt.Im_Initiator_ID && u.User_Deletion_Id == null).FirstOrDefault();
                            //itt.Item_ShortName_ID = 13;
                            if (itt.Item_ShortName_ID != null)
                            {
                                if (initiatir != null)
                                {
                                    var ism = uow.Repository<Item_ShortName>().GetData().
                                                          Where(i => i.ID == itt.Item_ShortName_ID && i.User_Deletion_Id == null).FirstOrDefault();
                                    itt.ItemShortNameAr = ism.ShortName_Ar;
                                    itt.ItemShortNameEn = ism.ShortName_En;
                                    itt.InitiatorCountry = (lang == "1" ? initiatir.Country.Ar_Name : initiatir.Country.En_Name);
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
                                        itt.ID = ism.Item.ID;
                                    }
                                    if (itt.SubPart_Name != null)
                                    {
                                        itt.ItemName = (lang == "1" ? ism.Item.Name_Ar : ism.Item.Name_En);
                                        itt.ID = ism.Item.ID;
                                    }
                                }
                                else
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
                                        itt.ID = ism.Item.ID;
                                    }
                                    if (itt.SubPart_Name != null)
                                    {
                                        itt.ItemName = (lang == "1" ? ism.Item.Name_Ar : ism.Item.Name_En);
                                        itt.ID = ism.Item.ID;
                                    }
                                }

                            }


                            //ask about qualitive group eslam
                            //get QG from items direct not from Initiator
                            var qualId = itt.QualitativeGroup_Id;
                            if (qualId != null)
                            {
                                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId && y.User_Deletion_Id == null).FirstOrDefault().Name_Ar;
                                if (initiatir != null)
                                {
                                    itt.InitiatorCountry = (lang == "1" ? initiatir.Country.Ar_Name : initiatir.Country.En_Name);
                                }
                                else
                                {
                                    itt.InitiatorCountry = "#####";
                                }
                            }
                            #region GetItemData

                            var itemShortNameId = itt.Item_ShortName_ID;//13;//
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
                            #endregion

                            //List categories And lots

                            var catAndLots = uow.Repository<Im_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Im_CheckRequest_Items_ID == itt.ImcheckReqItem_ID)
                                .Select(v => new categories_lotsGeneral
                                {
                                        //eslam
                                        ID = v.ID,
                                    Im_checkReqItems_ID = v.Im_CheckRequest_Items_ID,
                                    ItemCategory_ID = v.ItemCategory_ID,
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
                        //constrains
                        foreach (var init in itemss)
                        {
                            var ItemShortName = init.Item_ShortName_ID;
                            var qualId = init.QualitativeGroup_Id;
                            var initiatorId = init.Im_Initiator_ID;
                            var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();
                            if (initiatir != null)
                            {


                                var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData()
                                    .Include(k => k.Im_CountryConstrain_Text)
                                    .Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                                        r.Im_CountryConstrain_Text.ConstrainText_Ar

                                ).ToList();
                                var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


                                 r.Im_CountryConstrain_Text.ConstrainText_En
                                ).ToList();
                                var desAR = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                                      r.Im_CountryConstrain_Text.InSide_Certificate_Ar
                                //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                                ).ToList();
                                var desEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                                      r.Im_CountryConstrain_Text.InSide_Certificate_En
                                //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                                ).ToList();
                                var constrains = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text)
                                    .Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).ToList();
                                var constr = new constrainsGeneral();
                                constr.texts_Ar = conTextAr
    ;
                                constr.text_En = conTextEn;
                                constr.InSide_Certificate_Ar = desAR;
                                constr.InSide_Certificate_En = desEn;
                                var pp = new List<portsGeneral>();

                                if (ItemShortName != null)
                                {
                                    pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData()
                                        .Where(p => p.Item_ShortName_ID == ItemShortName && p.IsActive == true).Select(w => new portsGeneral
                                        {

                                            portId = w.Port_National_Id,
                                            portTypeId = w.Port_Type_ID
                                        }).ToList();


                                }
                                if (qualId != null)
                                {
                                    pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData()
                                        //.Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true&& p.User_Deletion_Id==null)
                                        .Select(w => new portsGeneral
                                        {

                                            portId = w.Port_National_Id,
                                            portTypeId = w.Port_Type_ID
                                        }).ToList();


                                }
                                foreach (var prt in pp)
                                {
                                    if (prt.portTypeId != null)
                                    {
                                        var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId && m.User_Deletion_Id == null && m.IsActive == true).FirstOrDefault();

                                        if (pTName != null)
                                        {
                                            prt.portType = pTName.Name_Ar;

                                        }

                                    }
                                    if (prt.portId != null)
                                    {
                                        var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId && m.User_Deletion_Id == null && m.IsActive == true).FirstOrDefault();

                                        if (pName != null)
                                        {
                                            prt.portName = pName.Name_Ar;

                                        }
                                    }
                                }
                                constr.itemConstrainPorts = pp;


                                init.Itemconstrains = constr;

                            }

                        }
                        ship.Items_checkReq = itemss;


                    }

                    CheckRequestDetails.checkRequestShipping = shippingMethods;
                    //Attachments for check requests
                    CheckRequestDetails.Attachments = uow.Repository<A_AttachmentData>().GetData()
                        .Where(v => v.RowId == CheckRequestDetails.Im_CheckRequest_ID && v.A_AttachmentTableNameId == 8 && v.User_Deletion_Id == null)
                        .Select(x => new AttachmentsGeneral
                        {
                            Attachment_Number = x.Attachment_Number,
                            AttachmentPath = x.AttachmentPath,
                            Attachment_TypeName = x.Attachment_TypeName,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            Attachment_Name = (lang == "1" ? x.A_AttachmentTableType.Ar_Name : x.A_AttachmentTableType.En_Name)

                        }).ToList();
                    //Attachments for permissions
                    CheckRequestDetails.List_Permission = uow.Repository<Im_PermissionRequest>().GetData()
                        .Where(a => a.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID)
                        .Select(z => new List_PermissionsGeneral
                        {
                            ID = z.ID,
                            ImPermission_Number = z.ImPermission_Number
                        }).ToList();
                    foreach (var item in CheckRequestDetails.List_Permission)
                    {
                        CheckRequestDetails.AttachmentPermission = uow.Repository<A_AttachmentData>().GetData()
                            .Where(v => v.RowId == item.ID && v.A_AttachmentTableNameId == 4 && v.User_Deletion_Id == null)
                            .Select(x => new AttachmentsPermissionGeneral
                            {
                                Attachment_Number = x.Attachment_Number,
                                AttachmentPath = x.AttachmentPath,
                                Attachment_TypeName = x.Attachment_TypeName,
                                StartDate = x.StartDate,
                                EndDate = x.EndDate,
                                Attachment_Name = (lang == "1" ? x.A_AttachmentTableType.Ar_Name : x.A_AttachmentTableType.En_Name)
                            }).ToList();
                    }

                    //emn
                    initiatorsId = initiatorsId.Distinct().ToList();
                    var itemsWithConstrains = new List<Items_checkReqGeneral>();
                    if (initiatorsId.Count() > 0)
                    {
                        foreach (var ids in initiatorsId)
                        {
                            var initiatir = uow.Repository<Im_Initiator>().GetData().Include(f => f.Country).Where(u => u.ID == ids).FirstOrDefault();
                            var itt = new Items_checkReqGeneral();
                            itt.InitiatorCountry = initiatir.Country.Ar_Name;
                            //ask about qualitive group
                            var qualId = initiatir.QualitativeGroup_Id;
                            if (qualId != null)
                            {
                                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                            }

                            var itemShortNameId = initiatir.Item_ShortName_ID;
                            if (itemShortNameId != null)
                            {
                                var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                                itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                                itt.ItemShortNameEn = itemShortName.ShortName_En;

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

                            //var ItemShortName = initiatir.Item_ShortName_ID;
                            //var qualId = initiatir.QualitativeGroup_Id;
                            var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>

                                 r.Im_CountryConstrain_Text.ConstrainText_Ar
                            //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                            ).ToList();
                            var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>


                             r.Im_CountryConstrain_Text.ConstrainText_En
                            ).ToList();
                            var desAR = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>

                                     r.Im_CountryConstrain_Text.InSide_Certificate_Ar
                               //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                               ).ToList();
                            var desEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>

                                  r.Im_CountryConstrain_Text.InSide_Certificate_En
                            //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                            ).ToList();
                            var constr = new constrainsGeneral();
                            constr.texts_Ar = conTextAr;
                            constr.text_En = conTextEn;
                            constr.InSide_Certificate_Ar = desAR;
                            constr.InSide_Certificate_En = desEn;
                            var pp = new List<portsGeneral>();

                            if (itemShortNameId != null)
                            {
                                pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == itemShortNameId && p.IsActive == true).Select(w => new portsGeneral
                                {

                                    portId = w.Port_National_Id,
                                    portTypeId = w.Port_Type_ID
                                }).ToList();


                            }
                            if (qualId != null)
                            {

                                pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData()
                                    .Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new portsGeneral
                                    {

                                        portId = w.Port_National_Id,
                                        portTypeId = w.Port_Type_ID
                                    }).ToList();


                            }
                            foreach (var prt in pp)
                            {
                                if (prt.portTypeId != null)
                                {
                                    var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

                                    if (pTName != null)
                                    {
                                        prt.portType = pTName.Name_Ar;


                                    }

                                }
                                if (prt.portId != null)
                                {
                                    var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                                    if (pName != null)
                                    {
                                        prt.portName = pName.Name_Ar;
                                        //govNamex = (lang == "1" ? pName.Governate.Ar_Name : pName.Governate.En_Name);
                                        //   govName = pName.Governate.Ar_Name;(lang == "1" ? v.ShippingAgency.Name_Ar : v.ShippingAgency.Name_En)
                                        //  CheckRequestDetails.govNameAR = uow.Repository<Governate>().GetData().Where(m => m.ID == pName.Govern_ID).FirstOrDefault().Ar_Name;
                                        //  CheckRequestDetails.govNameEN = uow.Repository<Governate>().GetData().Where(m => m.ID == pName.Govern_ID).FirstOrDefault().En_Name;
                                    }
                                }
                            }
                            constr.itemConstrainPorts = pp;


                            itt.Itemconstrains = constr;
                            itemsWithConstrains.Add(itt);




                        }
                    }
                    CheckRequestDetails.itemsWithConstrains = itemsWithConstrains;

                    ///////////////ESLAM///////////////


                    ///////////////ESLAM///////////////
                    var customs = uow.Repository<Im_CheckRequest_Customs_Message>()
                         .GetData().Where(i => i.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID)
                         .Select(v => new CustomsMessageGeneral
                         {
                             Im_CheckRequest_ID = v.Im_CheckRequest_ID,
                             Customs_Certificate_Number = v.Customs_Certificate_Number,
                             Shipment_Date = v.Shipment_Date,
                             Certification_Date = v.Certification_Date,
                             Arrival_Date = v.Arrival_Date,
                             Manifest_Number = v.Manifest_Number,
                             Certificate_Number_Each_Product = v.Certificate_Number_Each_Product,
                             Shipping_Agency_ID = v.Shipping_Agency_ID,
                             Shipping_Agency_Name = (lang == "1" ? v.ShippingAgency.Name_Ar : v.ShippingAgency.Name_En),
                             OperationType_ID = v.Im_OperationType,


                         }).ToList();
                    CheckRequestDetails.CustomsMessages = customs;
                    var Im_OpertaionType_id = customs.Select(c => c.OperationType_ID).FirstOrDefault();
                    CheckRequestDetails.OperationType_Name = uow.Repository<Im_OpertaionType>().GetData().Where(a => a.ID == Im_OpertaionType_id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                    ///////////////ESLAM///////////////
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
                //}

                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }


        }
        //approve request
        public Dictionary<string, object> ApproveCheckReq(Im_CheckRequestGeneralDTO dto, List<string> Device_Info)
        {
            try
            {
                Im_CheckRequest CModel = uow.Repository<Im_CheckRequest>().Findobject(dto.ID);
                CModel.IsAccepted = dto.IsAccepted;
                CModel.IsAccepted_Date = dto.IsAccepted_Date;
                CModel.IsActive = dto.IsActive;
                uow.SaveChanges();

                var empDTO = Mapper.Map<Im_CheckRequest, Im_CheckRequestGeneralDTO>(CModel);
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
        public Dictionary<string, object> InsertReasons(ReasonsListReqIdGeneralDTO dto, List<string> Device_Info)
        {
            try
            {

                Im_CheckRequest_RefuseReasonGeneralDTO rr = new Im_CheckRequest_RefuseReasonGeneralDTO();
                foreach (var id in dto.refuseReasonsIds)
                {

                    rr.Im_CheckRequest_Id = dto.checkReqId;
                    rr.Refuse_Reason_Id = id;
                    rr.User_Creation_Id = dto.User_Creation_Id;
                    rr.User_Creation_Date = dto.User_Creation_Date;
                    InsertReason(rr, Device_Info);
                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.checkReqId);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertReason(Im_CheckRequest_RefuseReasonGeneralDTO entity, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_RefuseReason_seq");
                entity.ID = idd;
                var CModel = Mapper.Map<Im_CheckRequest_RefuseReason>(entity);

                uow.Repository<Im_CheckRequest_RefuseReason>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> saveItemFees(Items_checkReqGeneral item, List<string> Device_Info)
        {
            try
            {
                Im_CheckRequest_Items CModel = uow.Repository<Im_CheckRequest_Items>().Findobject((long)item.ImcheckReqItem_ID);
                CModel.Fees = item.Fees;

                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, item.ImcheckReqItem_ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
