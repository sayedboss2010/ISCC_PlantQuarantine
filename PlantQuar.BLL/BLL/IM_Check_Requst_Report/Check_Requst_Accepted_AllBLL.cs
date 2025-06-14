using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.IM_Check_Requst_Report;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.IM_Check_Requst_Report
{
    public class Check_Requst_Accepted_AllBLL
    {
        private UnitOfWork uow;

        public Check_Requst_Accepted_AllBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Get_Accepet_Header
         (string ImCheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                string lang = Device_Info[2];
                var CheckRequestDetails = (from cc in entities.Im_CheckRequest
                                           join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID

                                           join cm in entities.Im_CheckRequest_Customs_Message on cc.ID equals cm.Im_CheckRequest_ID
                                           join op in entities.Im_OpertaionType on cm.Im_OperationType equals op.ID
                                           //noura ImRequestDetailsDTO.checkRequestShipping
                                           join CS in entities.Im_CheckRequset_Shipping_Method on cc.ID equals CS.Im_CheckRequest_ID
                                           // join atd in entities.A_AttachmentData on cc.ID equals atd.RowId
                                           //join Isn in entities.Item_ShortName on cc.ID equals Isn.ID
                                           join Out in entities.Outlets on cc.Outlet_ID equals Out.ID into ps
                                           join imp in entities.Company_National on rr.Importer_ID equals imp.ID
                                           // join comact in entities.CompanyActivities on rr.Importer_ID equals comact.ID
                                           join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                           from Out in ps.DefaultIfEmpty()

                                           where cc.CheckRequest_Number == ImCheckRequest_Number


                                           select new Check_Requst_Accepted_All_DTO
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
                                               Shipment_MeanName = (lang == "1" ? rr.Shipment_Mean.Ar_Name : rr.Shipment_Mean.En_Name),

                                               outletName = Out == null ? "" : Out.Ar_Name,
                                               General_Admin_Name = lang == "1" ? Out.General_Admin.Ar_Name : Out.General_Admin.En_Name,
                                               TransitCountryId = rr.TransitCountry_Id,
                                               // TransitCountry = (lang == "1" ? rr.Country.Ar_Name : rr.Country.En_Name),
                                               MessageOwner = rr.DelegateName,
                                               MessageOwnerNationalID = rr.DelegateAddress,
                                               OperationType_Name = lang == "1" ? op.Name_Ar : op.Name_En,
                                               Customs_Certificate_Number = cm.Customs_Certificate_Number,
                                               Shipment_Date = cm.Shipment_Date,
                                               Certification_Date = cm.Certification_Date,
                                               Arrival_Date = cm.Arrival_Date,
                                               Manifest_Number = cm.Manifest_Number,
                                               Certificate_Number_Each_Product = cm.Certificate_Number_Each_Product,
                                               Shipping_Agency_ID = cm.Shipping_Agency_ID,
                                               Shipping_Agency_Name = (lang == "1" ? cm.ShippingAgency.Name_Ar : cm.ShippingAgency.Name_En),
                                               OperationType_ID = cm.Im_OperationType,
                                               ImporterName = lang == "1" ? imp.Name_Ar : imp.Name_En,
                                               ImporterAddress = lang == "1" ? imp.Address_Ar : imp.Address_En,
                                               OwnerName = lang == "1" ? imp.Owner_Ar : imp.Owner_En,
                                               TaxesRecord = imp.TaxesRecord,
                                               CommertialRecord = imp.CommertialRecord,
                                               //CompActivityType__Name = lang == "1" ? comact.A_SystemCode.ValueName : comact.A_SystemCode.ValueNameEN,
                                               //Enrollment_Number = comact.Enrollment_Number,
                                               ContainerNumber = CS.ContainerNumber,
                                               NavigationalNumber = CS.NavigationalNumber,
                                               containers_ID = CS.containers_ID,

                                           }).FirstOrDefault();


                if (CheckRequestDetails != null)
                {
                    // shipmentMethod
                    var container = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == CheckRequestDetails.containers_ID && c.SystemCodeTypeId == 28);
                    if (container != null)
                    {
                        CheckRequestDetails.containerName = lang == "1" ? container.ValueName : container.ValueNameEN;

                    }
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
                                    select new List_Port_all
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
                    CheckRequestDetails.List_Port_all = All_port;






                    #endregion
                    ////arrival port
                    //Nullable<int> portArriveId = null;
                    //var portArrive = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 10 && d.User_Deletion_Id == null).FirstOrDefault();
                    //if (portArrive != null)
                    //{
                    //    portArriveId = portArrive.Port_ID;
                    //}
                    //var arrivePortName = uow.Repository<PortNational>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portArriveId).FirstOrDefault();
                    //var xx = uow.Repository<Governate>().GetData().Where(m => m.ID == arrivePortName.Govern_ID).FirstOrDefault();
                    //if (arrivePortName != null)
                    //{
                    //    CheckRequestDetails.ArrivePortName = lang == "1" ? arrivePortName.Name_Ar : arrivePortName.Name_En;
                    //    CheckRequestDetails.ArrivePortType = lang == "1" ? arrivePortName.Port_Type.Name_Ar : arrivePortName.Port_Type.Name_En;
                    //    CheckRequestDetails.govNameAR = lang == "1" ? xx.Ar_Name : xx.En_Name;

                    //}
                    ////transport port 
                    //Nullable<int> portTransId = null;
                    //var portTrans = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 9 && d.User_Deletion_Id == null).FirstOrDefault();
                    //if (portTrans != null)
                    //{
                    //    portTransId = portTrans.Port_ID;
                    //} //var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                    //var tt = uow.Repository<Port_International>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portTransId).FirstOrDefault();
                    //if (tt != null)
                    //{
                    //    CheckRequestDetails.TransportPortName = lang == "1" ? tt.Name_Ar : tt.Name_En;
                    //    CheckRequestDetails.TransportPortType = lang == "1" ? tt.Port_Type.Name_Ar : tt.Port_Type.Name_En;
                    //}

                    ////transitCountry and port transit _Eslam
                    //var transitCountry = uow.Repository<Country>().GetData().Where(d => d.ID == CheckRequestDetails.TransitCountryId && d.User_Deletion_Id == null).FirstOrDefault();
                    //if (transitCountry != null)
                    //{
                    //    CheckRequestDetails.TransitCountry = lang == "1" ? transitCountry.Ar_Name : transitCountry.En_Name;
                    //}
                    ////transite
                    //var porttransitId = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().Port_ID;
                    //if (porttransitId != null && porttransitId > 0)
                    //{
                    //    var porttransit = uow.Repository<Port_International>().GetData().Where(d => d.ID == porttransitId && d.User_Deletion_Id == null).FirstOrDefault();



                    //    if (porttransit.PortTypeID != null)
                    //    {
                    //        var portTypeTransit = uow.Repository<Port_Type>().GetData().Where(d => d.ID == porttransit.PortTypeID).FirstOrDefault();
                    //        CheckRequestDetails.TransitPortType = lang == "1" ? portTypeTransit.Name_Ar : portTypeTransit.Name_En;
                    //    }

                    //    if (porttransit != null)
                    //    {
                    //        CheckRequestDetails.TransitPort = lang == "1" ? porttransit.Name_Ar : porttransit.Name_En;

                    //    }
                    //}

                    //end
                    //Attachement

                    //Add Company Activity
                    var CompanyActivities_Details = (from ca in entities.CompanyActivities
                                                         //join cat in entities.Enrollment_type on ca.Enrollment_type_ID equals cat.ID
                                                     where ca.Company_ID == CheckRequestDetails.Importer_ID
                                                     && ca.IsActive == true

                                                     select new CompanyActivityDTO
                                                     {

                                                         CompActivityType__Name = lang == "1" ? ca.A_SystemCode.ValueName : ca.A_SystemCode.ValueNameEN,
                                                         Enrollment_Name = ca.Enrollment_Name,
                                                         Enrollment_Number = (int)ca.Enrollment_Number,
                                                         Enrollment_Start = ca.Enrollment_Start,
                                                         Enrollment_End = ca.Enrollment_End,
                                                         Enrollment_type_Name = lang == "1" ? ca.Enrollment_type.Ar_Name : ca.Enrollment_type.En_Name,

                                                     }).ToList();
                    CheckRequestDetails._CompanyActivitys = CompanyActivities_Details;
                    CheckRequestDetails.Attachments = uow.Repository<A_AttachmentData>().GetData()
                  .Where(v => v.RowId == CheckRequestDetails.Im_CheckRequest_ID && v.A_AttachmentTableNameId == 8 && v.User_Deletion_Id == null)
                  .Select(x => new DTO.DTO.IM_Check_Requst_Report.Attachments
                  {
                      Attachment_Number = x.Attachment_Number,
                      AttachmentPath = x.AttachmentPath,
                      Attachment_TypeName = x.Attachment_TypeName,
                      AttachmentStartDate = x.StartDate,
                      AttachmentEndDate = x.EndDate,


                  }).ToList();

                    // get companies out egypt

                    var com = uow.Repository<Im_CheckRequestData_Extra>().GetData()
                        .Where(i => i.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID)
                        .Select(v => new Importers
                        {
                            ImporterCompany = v.ImportCompany,
                            ImporterCompanyAddress = v.ImporeterCompanyAddress,
                            ImporterCompanyEn = v.ImportCompany_EN,
                            ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN
                        }).ToList();
                    CheckRequestDetails.ImportersCompanies = com;
                    //permissions
                    CheckRequestDetails.PermissionNumbersList = uow.Repository<Im_PermissionRequest>().GetData().Where(v => v.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID)
                        .Select(x => new PermissionNumbers { ImPermission_Number = x.ImPermission_Number.ToString() }).ToList();



                    //Items
                    var catAndLots = (from im_i in entities.Im_CheckRequest_Items
                                      join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                      join v in entities.Im_CheckRequest_Items_Lot_Category on im_i.ID equals v.Im_CheckRequest_Items_ID
                                      join pt in entities.Package_Type on v.Package_Type_ID equals pt.ID into pt1
                                      from pt in pt1.DefaultIfEmpty()
                                          //join im_cr in entities.Im_CommitteeResult on v.ID equals im_cr.LotData_ID into cr
                                          //from p in cr.DefaultIfEmpty()
                                      where im_i.Item_Permission_Number == CheckRequestDetails.ImCheckRequest_Number
                                      group v by new
                                      {
                                          im_i.Item_ShortName_ID,
                                          isn.ShortName_Ar,
                                          isn.ShortName_En,
                                          Item_ID = isn.Item.ID,
                                          ItemName_Ar = isn.Item.Name_Ar,
                                          ItemName_En = isn.Item.Name_En,
                                          categoryName = (lang == "1" ? v.ItemCategory.Name_Ar : v.ItemCategory.Name_En),
                                          InitiatorCountry = im_i.Im_Initiator.Country.Ar_Name,
                                          InitiatorCountryEn = im_i.Im_Initiator.Country.En_Name,
                                          ScientificName = isn.Item.Scientific_Name,
                                          packageType = (lang == "1" ? pt.Ar_Name : pt.En_Name)
                                      } into grp
                                      select new categories_lots_Accepted
                                      {
                                          Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                          ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,
                                          Item_ID = grp.Key.Item_ID,
                                          ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                          InitiatorCountry = lang == "1" ? grp.Key.InitiatorCountry : grp.Key.InitiatorCountryEn,
                                          categoryName = grp.Key.categoryName,
                                          GrossWeight = grp.Sum(q => q.GrossWeight),
                                          Net_Weight = grp.Sum(q => q.Net_Weight),
                                          Package_Count = grp.Sum(q => q.Package_Count),
                                          Units_Number = grp.Sum(q => q.Units_Number),
                                          Package_Based_Weight = grp.Sum(q => q.Package_Based_Weight),
                                          ScientificName = grp.Key.ScientificName,
                                          packageType = grp.Key.packageType,
                                          // Number_Wooden_Package = grp.Sum(q => long.Parse(q.Number_Wooden_Package)).ToString(),

                                      }).ToList();

                    // السحب

                    //                    select*
                    //from Im_RequestCommittee rc
                    // inner join Im_CheckRequest_SampleData sd on rc.ID = sd.Im_RequestCommittee_ID
                    // inner join  Im_CheckRequest_Items_Lot_Result Lot_R  on sd.LotData_ID = Lot_R.Im_CheckRequest_Items_Lot_Category_ID
                    // where rc.ImCheckRequest_ID = 594



                    var committee_Sample = (from rc in entities.Im_RequestCommittee
                                            join cr in entities.Im_CheckRequest_SampleData on rc.ID equals cr.Im_RequestCommittee_ID
                                            join isn in entities.Item_ShortName on cr.Item_ShortName_ID equals isn.ID
                                            join Lot_R in entities.Im_CheckRequest_Items_Lot_Result on cr.LotData_ID equals Lot_R.Im_CheckRequest_Items_Lot_Category_ID
                                            where rc.ImCheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID

                                            select new DTO.DTO.IM_Check_Requst_Report.Committee_Sample_Lot
                                            {
                                                ID = cr.ID,
                                                AnalysisLabType_ID = cr.AnalysisLabType_ID,
                                                Analysis_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisType.Name_Ar : cr.AnalysisLabType.AnalysisType.Name_En),
                                                Lab_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisLab.Name_Ar : cr.AnalysisLabType.AnalysisLab.Name_En),
                                                itemName = isn.ShortName_Ar,
                                                LotData_ID = cr.LotData_ID,
                                                //نتائج المعمل
                                                IsAccepted = cr.IsAccepted,
                                            }).Distinct().ToList();

                    CheckRequestDetails.List_Lot_Committee_Sample = committee_Sample;

                    CheckRequestDetails.ItemCategories_lots = catAndLots;

                    // لجنة الفحص
                    var committee = (from rc in entities.Im_RequestCommittee
                                     join cr in entities.Im_CommitteeResult on rc.ID equals cr.Committee_ID
                                     join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID
                                     where rc.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
                                        && rc.CommitteeType_ID == 11


                                     select new Committee_Lot_Accept
                                     {
                                         Committee_Result_Lot_ID = cr.ID,
                                         ResultTypes_Name = (lang == "1" ? cr.CommitteeResultType.Name_Ar : cr.CommitteeResultType.Name_En),
                                         EmployeeId = CE.Employee_Id,
                                         Delegation_Date = rc.Delegation_Date,
                                         StartTime = rc.StartTime,
                                         EndTime = rc.EndTime,
                                         IsApproved = rc.IsApproved,
                                         IsFinishedAll = rc.IsFinishedAll,
                                         Status = rc.Status,
                                         Date = cr.Date,
                                         Notes = cr.Notes,
                                         ISAdmin = CE.ISAdmin,
                                         Committee_ID = CE.Committee_ID,
                                         Is_Result_Finch = cr.CommitteeResultType_ID == null ? false : true,
                                         // عرض  فحص اللوطات
                                         IS_Total = cr.IS_Total,
                                         IS_TotalAndroid = cr.IS_Total_Android,
                                     }).ToList();

                    try
                    {

                        foreach (var iteme in committee)
                        {

                            if (iteme.EmployeeId != null)
                            {
                                short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    CheckRequestDetails.List_Committee_Lot_Accept = committee;

                    //الرسوم
                    var Fees_Dat = (from ch in entities.Im_CheckRequest

                                        //join ftd in entities.Fees_Transactions_Detiles  on ch.ID equals  ftd.Fees_Transactions.Table_ID 
                                        //into ftdt from ftd in ftdt.DefaultIfEmpty()
                                    where ch.CheckRequest_Number == ImCheckRequest_Number
                                    select new Fees_ALL
                                    {
                                        ID = ch.ID,
                                        Fees_CheckRequest = 10,
                                        // Is_Paid_Check=ftd.Fees_Action_ID == 4 ? "تم الدفع" : "لم يتم الدفع "    
                                    }).FirstOrDefault();


                    long ID_CheckRequest = long.Parse(Fees_Dat.ID.ToString());
                    var Fess_Check = (from ftd in entities.Fees_Transactions_Detiles
                                      where ftd.Fees_Transactions.TableName_ID == 4
                                      && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                                      && ftd.Fees_Action_ID == 4
                                      select new Fees_ALL
                                      {
                                          Is_Paid_Check = ftd.Fees_Action_ID == 4 ? "تم الدفع" : "لم يتم الدفع "
                                      }
                               ).ToList();
                    if (Fess_Check.Count > 0)
                    {

                        Fees_Dat.Is_Paid_Check = Fess_Check.FirstOrDefault().Is_Paid_Check;
                    }
                    CheckRequestDetails.Fees_ALL = Fess_Check;
                    #region رسوم Items  
                    var item_Fees = (from im_i in entities.Im_CheckRequest_Items
                                     join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                     where im_i.Item_Permission_Number == ImCheckRequest_Number
                                     group im_i by new
                                     {
                                         id = im_i.ID,
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
                                     select new DTO.DTO.IM_Check_Requst_Report.Fees_Item
                                     {
                                         ID = grp.Key.id,
                                         ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                         ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                         Fees = grp.Sum(q => q.Fees),
                                         GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                         Net_Weight = grp.Sum(q => q.Net_Weight),
                                     }).Distinct().ToList();

                    foreach (var item in item_Fees)
                    {
                        long ID_Item = long.Parse(item.ID.ToString());
                        var ddd = (from ftd in entities.Fees_Transactions_Detiles
                                   where ftd.Items_ID == ID_Item
                                   && ftd.Fees_Transactions.TableName_ID == 4
                                   && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                                   select new DTO.DTO.IM_Check_Requst_Report.Fees_Item
                                   {
                                       Is_Paid_Items = ftd.Items_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                   }
                                   ).ToList();
                        if (ddd.Count > 0)
                            item.Is_Paid_Items = ddd.FirstOrDefault().Is_Paid_Items;
                        //else
                        //    item.Is_Paid_Items = "";
                    }
                    #endregion
                    #region رسوم samples  
                    var Fees_Sample = (from sm in entities.Im_CheckRequest_SampleData

                                       where sm.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
                                       //&& sm.IS_Total==false
                                       group sm by new
                                       {
                                           ID = sm.ID,
                                           Sample_BarCode = sm.Sample_BarCode,
                                           Is_Total = sm.IS_Total,
                                           Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                           Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,
                                       } into grp
                                       select new DTO.DTO.IM_Check_Requst_Report.List_Sample
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



                    CheckRequestDetails.Fees_Item_All = item_Fees;
                    CheckRequestDetails.List_Sample = Fees_Sample44;
                    //decimal item_Fees_Total_PerShift = 0;
                    //  decimal amt = 0;
                    //decimal cnt = 0;
                    #endregion
                    #region رسوم المعالجة
                    //جزئى
                    var Fees_Treatment = (from td in entities.Im_Request_TreatmentData
                                          where td.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number

                                          group td by new
                                          {
                                              ID = td.ID,
                                              TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                          entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                                              TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                          entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                                              TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                                                          entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                                          } into grp
                                          select new List_Treatment
                                          {
                                              ID = grp.Key.ID,
                                              TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                              TreatmentType_Name = grp.Key.TreatmentType_Name,
                                              TreatmentMat_Amount = grp.Sum(c => c.TreatmentMat_Amount),
                                          }).Distinct().ToList();
                    //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();

                    foreach (var item in Fees_Treatment)
                    {
                        long ID_Item = long.Parse(item.ID.ToString());
                        var _Treatment = (from ftd in entities.Fees_Transactions_Detiles


                                          where ftd.TreatmentData_ID == ID_Item
                                          //&& ftd.Fees_Transactions.TableName_ID == 4
                                          //&& ftd.TreatmentData_ID !=null                                     
                                          select new List_Treatment
                                          {
                                              Treatment_Amount = ftd.Fees_Transactions.Amount_Total,
                                              Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
                                          }
                                   ).ToList();
                        if (_Treatment.Count > 0)
                        {
                            item.Is_Paid_Treatment = _Treatment.FirstOrDefault().Is_Paid_Treatment;
                            item.Treatment_Amount = _Treatment.FirstOrDefault().Treatment_Amount;
                        }
                        else
                        {
                            item.Is_Paid_Treatment = null;
                            item.Treatment_Amount = null;
                        }
                    }
                    CheckRequestDetails.List_Treatment = Fees_Treatment;


                    #endregion
                    CheckRequestDetails.Fees_Item_Shift_All = (from cms in entities.Im_RequestCommittee_Shift
                                                               where cms.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
                                                               select new DTO.DTO.IM_Check_Requst_Report.Fees_Item_Shift
                                                               {
                                                                   Amount_Per_Shift = cms.Amount,
                                                                   Count_Per_Shift = cms.Count,
                                                                   total_Per_Shift = (cms.Count * cms.Amount)
                                                               }).ToList();

                    var _total_Per_Shift = CheckRequestDetails.Fees_Item_Shift_All.Select(z => z.total_Per_Shift).Sum();
                    CheckRequestDetails.Shift_Item_All = _total_Per_Shift;

                    decimal item_Fees_Total = 0;


                    if (item_Fees != null)
                        item_Fees_Total = item_Fees.Select(a => a.Fees).Sum().Value;

                    CheckRequestDetails.SUM_Shift_Fees_Item = 10 + _total_Per_Shift + item_Fees_Total;
                    //visa and final result
                    var finalResult = (from crfr in entities.Im_CheckRequest_Final_Result
                                       join imfr in entities.Im_Final_Result on crfr.Im_Final_Result_ID equals imfr.ID
                                       where crfr.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID
                                       select new DTO.DTO.IM_Check_Requst_Report.FinalResult
                                       {
                                           Im_CheckRequest_FinalResult_ID = crfr.Im_CheckRequest_ID,
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
                    CheckRequestDetails.FinalResults = finalResult;

                    var CheckRequest_Visa = (from imv in entities.Im_CheckRequest_Visa
                                             where imv.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID
                                             select new DTO.DTO.IM_Check_Requst_Report.CheckRequest_Visa
                                             {
                                                 Im_CheckRequest_Visa_ID = imv.Im_CheckRequest_ID,
                                                 Date_Visa = imv.Date,
                                                 Visa_Result_EmployeeId = imv.User_Creation_Id,
                                                 Visa_Name = (lang == "1" ? imv.Im_Visa.Ar_Name : imv.Im_Visa.En_Name),
                                                 Visa_Result_Name = (lang == "1" ? imv.Im_Visa.Description_Ar : imv.Im_Visa.Description_En),
                                             }).ToList();

                    foreach (var Item_Visa in CheckRequest_Visa)
                    {
                        if (Item_Visa.Visa_Result_EmployeeId != null)
                        {

                            Item_Visa.Visa_Result_Employee_Name = priv.PR_User.Where(c => c.Id == Item_Visa.Visa_Result_EmployeeId).FirstOrDefault().FullName;
                        }
                    }
                    CheckRequestDetails.CheckRequest_Visa = CheckRequest_Visa;
                }
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
