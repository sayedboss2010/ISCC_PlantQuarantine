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
using System.Security.Policy;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.DTO.Search;

namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
    public class List_EXCheckRequest_BLL
    {
        private UnitOfWork uow;

        public List_EXCheckRequest_BLL()
        {
            uow = new UnitOfWork();
        }

        // 
        public Dictionary<string, object> GetExCheckRequestList_filter
          (long? Outlet_User_ID, long? Station_User_ID, string CanView_Outlet_Examination,
            string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi, List<string> Device_Info)
        {
            try
            {


                //PlantQuarantineEntities entities = new PlantQuarantineEntities();
                //string lang = Device_Info[2];
                ////@User_Id
                //Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                //paramters_Type.Add("CanView_Outlet_Examination", SqlDbType.Int);
                //paramters_Type.Add("CanAdd_Station_Examination", SqlDbType.Int);
                //paramters_Type.Add("CanEdit_Outlet_Genshi", SqlDbType.Int);
                //paramters_Type.Add("CanDelete_Station_Genshi", SqlDbType.Int);
                //paramters_Type.Add("Outlet_User_ID", SqlDbType.NVarChar);
                //paramters_Type.Add("Station_User_ID", SqlDbType.NVarChar);



                ////End    //Add  data Eslam
                //Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                ////paramters_Data.Add("CanView_Outlet_Examination", 1.ToString());
                ////                paramters_Data.Add("CanAdd_Station_Examination", 1.ToString());
                ////                paramters_Data.Add("CanEdit_Outlet_Genshi", 1.ToString());
                ////                paramters_Data.Add("CanDelete_Station_Genshi", 1.ToString());


                //paramters_Data.Add("CanView_Outlet_Examination", CanView_Outlet_Examination.ToString());
                //paramters_Data.Add("CanAdd_Station_Examination", CanAdd_Station_Examination.ToString());
                //paramters_Data.Add("CanEdit_Outlet_Genshi", CanEdit_Outlet_Genshi.ToString());
                //paramters_Data.Add("CanDelete_Station_Genshi", CanDelete_Station_Genshi.ToString());


                //paramters_Data.Add("Outlet_User_ID", Outlet_User_ID.ToString());
                //paramters_Data.Add("Station_User_ID", Station_User_ID.ToString());

                ////End
                //List<EXCheckRequestListDTO> requests = uow.Repository<EXCheckRequestListDTO>().CallStored("Ex_CheckRequestList_New", paramters_Type, paramters_Data, Device_Info).ToList();// entities.Ex_ALL_List().ToList();
                //if (requests.Count > 0)
                //{
                //    requests = requests.Where(a=>a.Ex_CheckRequest_ID==304).ToList();


                PlantQuarantineEntities db = new PlantQuarantineEntities();

                var list1 = db.Ex_List.ToList();
                var list2 = list1;
                var list3 = list1;
                var list4 = list1;

                if (CanView_Outlet_Examination == "1")
                {
                    list1 = db.Ex_List.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                }
                else
                { list1.Clear(); }

                if (CanAdd_Station_Examination == "1")
                {
                    list2 = db.Ex_List.Where(a => a.Station_Examination_ID == Station_User_ID).ToList();
                }
                else
                { list2.Clear(); }
                if (CanEdit_Outlet_Genshi == "1")
                {
                    list3 = db.Ex_List.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                }
                else
                { list3.Clear(); }
                if (CanDelete_Station_Genshi == "1")
                {
                    list4 = db.Ex_List.Where(a => a.Station_Genshi_ID == Station_User_ID).ToList();
                }
                else
                { list4.Clear(); }
                var union_data = list1.Union(list2).Union(list3).Union(list4).ToList();
                //union_data = union_data.Where(a => a.Ex_CheckRequest_ID == 304).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, union_data);
                //}
                //else
                //{
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);

                //}
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetImCheckRequestList_filter
            (short IsApproved, short userId, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities priv = new dbPrivilageEntities();
                var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
                var outlet = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

                // PlantQuarantineEntities entities = new PlantQuarantineEntities();
                // string lang = Device_Info[2];

                //@User_Id
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();

                paramters_Type.Add("User_Id", SqlDbType.BigInt);


                //End    //Add  data Eslam
                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();

                paramters_Data.Add("User_Id", userId.ToString());

                //End
                List<EXCheckRequestListDTO> requests = uow.Repository<EXCheckRequestListDTO>().CallStored("Ex_CheckRequestList", paramters_Type, paramters_Data, Device_Info).ToList();// entities.Ex_ALL_List().ToList();
                                                                                                                                                                                       //List <EXCheckRequestListDTO> requests = (from cc in entities.Ex_CheckRequest
                                                                                                                                                                                       //                                         join rr in entities.Ex_CheckRequest_Data on cc.ID equals rr.Ex_CheckRequest_ID
                                                                                                                                                                                       //                                         join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                                                                                                                                                                       //                                         //join exrq in entities.Ex_RequestCommittee on cc.ID equals exrq.ExCheckRequest_ID into imss
                                                                                                                                                                                       //                                         //from exrq in imss.DefaultIfEmpty() 

                //                                         join chfr in entities.Ex_CheckRequest_Final_Result on cc.ID equals chfr.Ex_CheckRequest_ID into ims
                //                                         from chfr in ims.DefaultIfEmpty()
                //                                         join excr in entities.Ex_CertificatesRequests  on cc.ID equals excr.Ex_CheckRequest_ID into excr1
                //                                         from excr in excr1.DefaultIfEmpty()
                //                                             // join exfrr in entities.Ex_Final_Result on chfr.ID equals exfrr.Ex_CheckRequest_Final_Result into frr1
                //                                             //from exfrr in frr1.DefaultIfEmpty()

                //                                             //where/* cc.IsPaid == true &&*/ cc.Outlet_ID == outlet.ID

                //                                         select  new EXCheckRequestListDTO
                //                                         {

                //                                             Ex_CheckRequest_ID = cc.ID,
                //                                             ImCheckRequest_Number = cc.CheckRequest_Number,
                //                                             Creation_Date = (DateTime)cc.User_Creation_Date,
                //                                             IsAccepted = cc.IsAccepted,
                //                                             ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                //                                             Importer_ID = rr.Importer_ID,
                //                                             ImporterType_Id = rr.ImporterType_Id,
                //                                             IsPaid= cc.IsPaid,

                //                                             Outlet_ID = cc.Outlet_ID,

                //                                             Closed_Request = chfr.Ex_Final_Result.Status,
                //                                             //Is_Cancel= exrq.Is_Cancel,
                //                                             //StatusRequestCommittee= exrq.Status,
                //                                             //CommitteeID = exrq == null? 0: exrq.ID,
                //                                             Ex_Final_ResultName= chfr.Ex_Final_Result.Ar_Name,
                //                                             Ex_Final_Result_ID= chfr.Ex_Final_Result_ID,
                //                                             Ex_CertificatesRequests_ID = excr == null ? 0 : excr.ID,
                //                                             //  CommitteeType_ID= exrq == null ?6 : exrq.CommitteeType_ID
                //                                         }).ToList();//.Skip(20).Take(20).OrderBy(a=>a.Ex_CheckRequest_ID).

                // List<EXCheckRequestListDTO> newRequests = new List<EXCheckRequestListDTO>();
                // foreach (var re in requests)
                // {
                //     //if(re.CommitteeType_ID == 0 || re.CommitteeType_ID == 11)
                //     {
                //         newRequests.Add(re);
                //     }
                // }
                // foreach (var req in newRequests)
                // {
                //     //req.PageSize = 100;

                //     req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();
                //     //req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == rr.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault(),
                //     //req.ImporterName = req.ImporterType_Id == 6 ? uow.Repository<Company_National>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault()
                //     //: req.ImporterType_Id == 7 ? uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault()
                //     //: uow.Repository<Person>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => s.Name).FirstOrDefault();

                //     //req.Closed_Request = uow.Repository<Ex_CheckRequest_Final_Result>().GetData().Where(a => a.Ex_CheckRequest_ID == req.Ex_CheckRequest_ID)
                //     //                .Select(s => s.Ex_Final_Result.Status).FirstOrDefault();
                //     //    req.Closed_Request = uow.Repository<Ex_CheckRequest_Final_Result>().GetData().Where(a => a.Ex_CheckRequest_ID == req.Ex_CheckRequest_ID)
                //     //        .Select(s =>s.Ex_Final_Result.Status).Last();
                //     //    //req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();

                //     if (req.ImporterType_Id == 6)
                //     {
                //         req.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                //     }
                //     else if (req.ImporterType_Id == 7)
                //     {
                //         req.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                //     }
                //     else
                //     {
                //         req.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => s.Name).FirstOrDefault();
                //     }
                // }

                //الطلب انتهى

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Get_User_Station(short userId, List<string> Device_Info)
        {
            try
            {
                DateTime currentDate = DateTime.Now.Date;

                var User_Station = uow.Repository<Station_Emp>().GetData().Where(o => o.Emp_Id == userId
                && o.IsActive == true && o.User_Deletion_Id == null && o.Date_To >= currentDate).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, User_Station);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //        public Dictionary<string, object> GetImCheckRequestDetails
        //       (string ImCheckRequest_Number, List<string> Device_Info)
        //        {
        //            try
        //            {
        //                PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //                string lang = Device_Info[2];
        //                var CheckRequestDetails = (from cc in entities.Ex_CheckRequest
        //                                           join rr in entities.Ex_CheckRequest_Data on cc.ID equals rr.Ex_CheckRequest_ID

        //                                           join Out in entities.Outlets on cc.Outlet_ID equals Out.ID into ps

        //                                          join co1 in entities.Countries on rr.ExportCountry_Id equals co1.ID into co
        //                                           from Out in ps.DefaultIfEmpty()
        //                                           from co1 in co.DefaultIfEmpty()

        //                                           where cc.CheckRequest_Number == ImCheckRequest_Number

        //                                           select new EXRequestDetailsDTO
        //                                           {
        //                                               Ex_CheckRequest_ID = cc.ID,
        //                                               IsAccepted = cc.IsAccepted,
        //                                               IsActive = cc.IsActive,
        //                                               IsPaid = cc.IsPaid,
        //                                               Ex_CheckRequestData_ID = rr.ID,
        //                                               ImCheckRequest_Number = cc.CheckRequest_Number,
        //                                               Ship_Name = rr.Ship_Name,
        //                                               ExportCountryName = lang == "1" ? co1.Ar_Name : co1.En_Name,
        //                                               Importer_ID = rr.Importer_ID,
        //                                               ImporterType_Id = rr.ImporterType_Id,
        //                                               Transport_Mean_Id = rr.Transport_Mean_Id,
        //                                               InternationalTransport_Id = rr.InternationalTransportation_ID,
        //                                               Shipment_Mean_Id = rr.Shipment_Mean_Id,
        //                                               //outletName = Out == null ? "" : Out.Ar_Name,
        //                                               TransitCountryId = rr.TransitCountry_Id,
        //                                               MessageOwner = rr.DelegateName,
        //                                               MessageOwnerNationalID = rr.DelegateAddress
        //                                           }).FirstOrDefault();


        //                if (CheckRequestDetails != null)
        //                {
        //                    //الرسوم
        //                    var item_Fees = (from Ex_i in entities.Ex_CheckRequest_Items
        //                                     join isn in entities.Item_ShortName on Ex_i.Item_ShortName_ID equals isn.ID
        //                                     where Ex_i.Item_Permission_Number == ImCheckRequest_Number
        //                                     group Ex_i by new
        //                                     {
        //                                         Ex_i.Item_ShortName_ID,
        //                                         isn.ShortName_Ar,
        //                                         isn.ShortName_En,
        //                                         Item_ID = isn.Item.ID,
        //                                         ItemName_Ar = isn.Item.Name_Ar,
        //                                         ItemName_En = isn.Item.Name_En,
        //                                         qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
        //                                         qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
        //                                         InitiatorCountry = Ex_i.Ex_Initiator.Country.Ar_Name,
        //                                         InitiatorCountryEn = Ex_i.Ex_Initiator.Country.En_Name,
        //                                     } into grp
        //                                     select new Fees_Item
        //                                     {

        //                                         ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

        //                                         ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
        //                                         Fees = grp.Sum(q => q.Fees),
        //                                         GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
        //                                         Net_Weight = grp.Sum(q => q.Net_Weight),
        //                                     }).Distinct().ToList();


        //                    //var Fees_Item_Sum = uow.Repository<Ex_CheckRequest_Items>().GetData()
        //                    //    .Where(i => i.Item_Permission_Number == ImCheckRequest_Number).
        //                    //    Select(v => v.Fees).Sum();

        //                    CheckRequestDetails.Fees_Item_All = item_Fees;
        //                    decimal item_Fees_Total_PerShift = 0;
        //                    //  decimal amt = 0;
        //                    //decimal cnt = 0;
        //                    CheckRequestDetails.Fees_Item_Shift_All = (from cms in entities.Ex_RequestCommittee_Shift
        //                                                               where cms.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
        //                                                               select new Fees_Item_Shift
        //                                                               {
        //                                                                   Amount_Per_Shift = cms.Amount,
        //                                                                   Count_Per_Shift = cms.Count,
        //                                                                   total_Per_Shift = (cms.Count * cms.Amount)
        //                                                               }).ToList();

        //                    var _total_Per_Shift = CheckRequestDetails.Fees_Item_Shift_All.Select(z => z.total_Per_Shift).Sum();
        //                    CheckRequestDetails.Shift_Item_All = _total_Per_Shift;

        //                    #region رسم  النوبتجية
        //                    // 
        //                    var Fees_Item_Shift = (from sh in entities.Ex_RequestCommittee_Shift

        //                                           where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
        //                                           group sh by new
        //                                           {
        //                                               ID = sh.ID,
        //                                               Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,

        //                                           } into grp
        //                                           select new List_Shift
        //                                           {
        //                                               ID = grp.Key.ID,
        //                                               Shift_Name = grp.Key.Shift_Name,
        //                                               Shift_Count = grp.Sum(q => q.Count),
        //                                               Shift_Amount = grp.Sum(q => q.Amount),
        //                                               Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
        //                                           }).Distinct().ToList();
        //                    foreach (var item in Fees_Item_Shift)
        //                    {
        //                        long ID_Item = long.Parse(item.ID.ToString());
        //                        var _Shift = (from ftd in entities.Fees_Transactions_Detiles

        //                                      where ftd.Shift_ID == ID_Item
        //                                      && ftd.Fees_Transactions.TableName_ID == 4
        //                                      && ftd.Fees_Transactions.Table_ID == CheckRequestDetails.ID
        //                                      select new List_Shift
        //                                      {
        //                                          Is_Paid_Shift = ftd.Shift_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
        //                                      }
        //                                   ).ToList();
        //                        if (_Shift.Count > 0)
        //                            item.Is_Paid_Shift = _Shift.FirstOrDefault().Is_Paid_Shift;
        //                        else
        //                            item.Is_Paid_Shift = null;
        //                    }

        //                    CheckRequestDetails.List_Shift = Fees_Item_Shift;
        //                    #endregion


        //                    #region   رسوم السحب




        //                    var Fees_Sample = (from sm in entities.Ex_CheckRequest_SampleData

        //                                       where sm.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
        //                                       group sm by new
        //                                       {
        //                                           //Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
        //                                           //Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,

        //                                           ID = sm.ID,
        //                                           Sample_BarCode = sm.Sample_BarCode,
        //                                           Is_Total = sm.IS_Total,
        //                                           Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
        //                                           Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,


        //                                       } into grp
        //                                       select new List_Sample
        //                                       {



        //                                           //Laboratory_Name = grp.Key.Laboratory_Name,
        //                                           //Sample_Name = grp.Key.Sample_Name,


        //                                           //Sample_Count = grp.Count(q => q.AnalysisLabType_ID != null),
        //                                           //Sample_Amount = 200,
        //                                           //Sample_Sum_All = grp.Count(q => q.AnalysisLabType_ID != null) * 200,


        //                                           ID = grp.Key.ID,
        //                                           Sample_BarCode = grp.Key.Sample_BarCode,
        //                                           Laboratory_Name = grp.Key.Laboratory_Name,
        //                                           Sample_Name = grp.Key.Sample_Name,
        //                                           Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
        //                                           Sample_Count = grp.Count(q => q.AnalysisLabType_ID != null),
        //                                           Sample_Amount = 200,
        //                                           Sample_Sum_All = grp.Count(q => q.AnalysisLabType_ID != null) * 200,
        //                                       }).Distinct().ToList();

        //                    var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();

        //                    foreach (var item in Fees_Sample44)
        //                    {
        //                        long ID_Item = long.Parse(item.ID.ToString());
        //                        var _Sample = (from ftd in entities.Fees_Transactions_Detiles

        //                                       where ftd.SampleData_ID == ID_Item
        //                                       && ftd.Fees_Transactions.TableName_ID == 4
        //                                       && ftd.Fees_Transactions.Table_ID == CheckRequestDetails.ID
        //                                       select new List_Sample
        //                                       {
        //                                           Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
        //                                       }
        //                                   ).ToList();
        //                        if (_Sample.Count > 0)
        //                            item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
        //                        else
        //                            item.Is_Paid_Sample = null;
        //                    }





        //                    CheckRequestDetails.List_Sample = Fees_Sample44;


        //                    //CheckRequestDetails.List_Sample = Fees_Sample;

        //                    #endregion


        //                    decimal item_Fees_Total = 0;


        //                    if (item_Fees != null)
        //                    {
        //                        item_Fees_Total = item_Fees.Select(a => a.Fees).Sum().Value;
        //                    }

        //                    var Sum_List_Sample = CheckRequestDetails.List_Sample.Select(c => c.Sample_Sum_All).Sum();



        //                    CheckRequestDetails.SUM_Shift_Fees_Item = 10 + _total_Per_Shift + item_Fees_Total + Sum_List_Sample;
        //                    // [Owner_Ar]

        //                    if (CheckRequestDetails.ImporterType_Id == 6)
        //                    {
        //                        CheckRequestDetails.ImporterType = "شركة";
        //                        CheckRequestDetails.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
        //                        CheckRequestDetails.ImporterAddress = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
        //                        CheckRequestDetails.OwnerName = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Owner_Ar : s.Owner_En).FirstOrDefault();

        //                        //Add Company Activity
        //                        var CompanyActivities_Details = (from ca in entities.CompanyActivities
        //                                                             //join cat in entities.Enrollment_type on ca.Enrollment_type_ID equals cat.ID
        //                                                         where ca.Company_ID == CheckRequestDetails.Importer_ID
        //                                                         && ca.IsActive == true

        //                                                         select new CompanyActivityDTO
        //                                                         {

        //                                                             CompActivityType__Name = lang == "1" ? ca.A_SystemCode.ValueName : ca.A_SystemCode.ValueNameEN,
        //                                                             Enrollment_Name = ca.Enrollment_Name,
        //                                                             Enrollment_Number = ca.Enrollment_Number,
        //                                                             Enrollment_Start = ca.Enrollment_Start,
        //                                                             Enrollment_End = ca.Enrollment_End,
        //                                                             Enrollment_type_Name = lang == "1" ? ca.Enrollment_type.Ar_Name : ca.Enrollment_type.En_Name,

        //                                                         }).ToList();
        //                        CheckRequestDetails._CompanyActivitys = CompanyActivities_Details;
        //                        CheckRequestDetails.ImporterContacts = uow.Repository<Ex_ContactData>()
        //                       .GetData().Include(f => f.ContactType).
        //                  Where(a => a.Exporter_ID == CheckRequestDetails.Importer_ID && a.ExporterType_Id == 6).
        //                  Select(a => new ContactTypeDTO
        //                  {
        //                      Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
        //                      Value = a.Value
        //                  }).ToList();
        //                        //CheckRequestDetails.CompanyActivity= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_Name : s.Enrollment_Name).FirstOrDefault();
        //                        //CheckRequestDetails.Enrollment_Number= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_Number : s.Enrollment_Number).FirstOrDefault();
        //                        //CheckRequestDetails.Enrollment_Start= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_Start : s.Enrollment_Start).FirstOrDefault();
        //                        //CheckRequestDetails.Enrollment_End= uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Enrollment_End : s.Enrollment_End).FirstOrDefault();
        //                        //CheckRequestDetails.CompActivityType_ID = uow.Repository<CompanyActivity>().GetData().Where(a => a.Company_ID == CheckRequestDetails.Importer_ID).Select(s => s.CompActivityType_ID).FirstOrDefault();
        //                        //CheckRequestDetails.CompanyActivityType = uow.Repository<CompanyActivityType>().GetData().Where(a => a.ID == CheckRequestDetails.CompActivityType_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
        //                    }
        //                    else if (CheckRequestDetails.ImporterType_Id == 7)
        //                    {
        //                        CheckRequestDetails.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
        //                        CheckRequestDetails.ImporterAddress = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
        //                        CheckRequestDetails.ImporterType = "هيئة";
        //                        CheckRequestDetails.ImporterContacts = uow.Repository<Ex_ContactData>()
        //                  .GetData().Include(f => f.ContactType).
        //             Where(a => a.Exporter_ID == CheckRequestDetails.Importer_ID && a.ExporterType_Id == 7).
        //             Select(a => new ContactTypeDTO
        //             {
        //                 Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
        //                 Value = a.Value
        //             }).ToList();
        //                    }
        //                    else
        //                    {
        //                        CheckRequestDetails.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Name).FirstOrDefault();
        //                        CheckRequestDetails.ImporterAddress = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Address).FirstOrDefault();
        //                        CheckRequestDetails.ImporterType = "فرد";
        //                        CheckRequestDetails.PersonContacts = uow.Repository<Person>()
        //                  .GetData().
        //             Where(a => a.ID == CheckRequestDetails.Importer_ID).
        //             Select(a => new PersonContact
        //             {
        //                 Email = (lang == "1" ? a.Email : a.Email),
        //                 Phone = a.Phone
        //             }).ToList();
        //                    }
        //                    // enter portNational or international
        //                    //var porttransitId = uow.Repository<Ex_Request_Port>().GetData().Where(d => d.Ex_RequestData_ID == CheckRequestDetails.Ex_RequestData_ID && d.ReqPortType_ID ==11).FirstOrDefault().Port_ID;
        //                    //var isNationalTransit = uow.Repository<Ex_Request_Port>().GetData().Where(d => d.Ex_RequestData_ID == CheckRequestDetails.Ex_RequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().IsNational;
        //                    Nullable<int> portArriveId = null;
        //                    var portArrive = uow.Repository<Ex_CheckRequest_Port>().GetData().Where(d => d.Ex_CheckRequest_Data_ID == CheckRequestDetails.Ex_CheckRequestData_ID && d.ReqPortType_ID == 10 && d.User_Deletion_Id == null).FirstOrDefault();
        //                    if (portArrive != null)
        //                    {
        //                        portArriveId = portArrive.Port_ID;
        //                    }
        //                    var arrivePortName = uow.Repository<PortNational>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portArriveId).FirstOrDefault();
        //                    var xx = uow.Repository<Governate>().GetData().Where(m => m.ID == arrivePortName.Govern_ID).FirstOrDefault();
        //                    if (arrivePortName != null)
        //                    {
        //                        CheckRequestDetails.ArrivePortName = lang == "1" ? arrivePortName.Name_Ar : arrivePortName.Name_En;
        //                        CheckRequestDetails.ArrivePortType = lang == "1" ? arrivePortName.Port_Type.Name_Ar : arrivePortName.Port_Type.Name_En;
        //                        CheckRequestDetails.govNameAR = lang == "1" ? xx.Ar_Name : xx.En_Name;

        //                    }
        //                    //transitCountry and port transit _Eslam
        //                    var transitCountry = uow.Repository<Country>().GetData().Where(d => d.ID == CheckRequestDetails.TransitCountryId && d.User_Deletion_Id == null).FirstOrDefault();
        //                    if (transitCountry != null)
        //                    {
        //                        CheckRequestDetails.TransitCountry = lang == "1" ? transitCountry.Ar_Name : transitCountry.En_Name;
        //                    }

        //                    var porttransitId = uow.Repository<Ex_CheckRequest_Port>().GetData().Where(d => d.Ex_CheckRequest_Data_ID == CheckRequestDetails.Ex_CheckRequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().Port_ID;
        //                    if (porttransitId != null && porttransitId != 0)
        //                    {
        //                        var porttransit = uow.Repository<Port_International>().GetData().Where(d => d.ID == porttransitId && d.User_Deletion_Id == null).FirstOrDefault();



        //                        if (porttransit.PortTypeID != null && porttransit.PortTypeID != 0)
        //                        {
        //                            var portTypeTransit = uow.Repository<Port_Type>().GetData().Where(d => d.ID == porttransit.PortTypeID).FirstOrDefault();
        //                            CheckRequestDetails.TransitPortType = lang == "1" ? portTypeTransit.Name_Ar : portTypeTransit.Name_En;
        //                        }

        //                        if (porttransit != null)
        //                        {
        //                            CheckRequestDetails.TransitPort = lang == "1" ? porttransit.Name_Ar : porttransit.Name_En;

        //                        }
        //                    }
        //                    // var portTypeId = uow.Repository<Ex_Request_Port>().GetData().Where(d => d.Ex_RequestData_ID == CheckRequestDetails.Ex_RequestData_ID).FirstOrDefault().ReqPortType_ID;


        //                    //transport port 
        //                    Nullable<int> portTransId = null;
        //                    var portTrans = uow.Repository<Ex_CheckRequest_Port>().GetData().Where(d => d.Ex_CheckRequest_Data_ID == CheckRequestDetails.Ex_CheckRequestData_ID && d.ReqPortType_ID == 9 && d.User_Deletion_Id == null).FirstOrDefault();
        //                    if (portTrans != null)
        //                    {
        //                        portTransId = portTrans.Port_ID;
        //                    } //var portTypeId = uow.Repository<Ex_Request_Port>().GetData().Where(d => d.Ex_RequestData_ID == permissionPrintDetails.Ex_RequestData_ID).FirstOrDefault().ReqPortType_ID;
        //                    var tt = uow.Repository<Port_International>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portTransId).FirstOrDefault();
        //                    if (tt != null)
        //                    {
        //                        CheckRequestDetails.TransportPortName = lang == "1" ? tt.Name_Ar : tt.Name_En;
        //                        CheckRequestDetails.TransportPortType = lang == "1" ? tt.Port_Type.Name_Ar : tt.Port_Type.Name_En;
        //                    }
        //                    //end

        //                    var shipp = uow.Repository<Shipment_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Shipment_Mean_Id).FirstOrDefault();
        //                    if (shipp != null)
        //                    {
        //                        CheckRequestDetails.Shipment_MeanName = shipp.Ar_Name;
        //                    }
        //                    var internationalTransport = uow.Repository<InternationalTransportation>().GetData().Where(c => c.ID == CheckRequestDetails.InternationalTransport_Id).FirstOrDefault();
        //                    if (internationalTransport != null)
        //                    {
        //                        CheckRequestDetails.InternationalTransport = lang == "1" ? internationalTransport.Ar_Name : internationalTransport.En_Name;
        //                    }
        //                    var transport = uow.Repository<Transport_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Transport_Mean_Id).FirstOrDefault();
        //                    if (transport != null)
        //                    {
        //                        CheckRequestDetails.Transport_MeanName = transport.Ar_Name;
        //                    }
        //                    //var isNationalArrive = uow.Repository<Ex_Request_Port>().GetData().Where(d => d.Ex_RequestData_ID == CheckRequestDetails.Ex_RequestData_ID && d.ReqPortType_ID == 10).FirstOrDefault().IsNational;
        //                    // get companies out egypt

        //                    var com = uow.Repository<Ex_CheckRequestData_Extra>().GetData().Where(i => i.Ex_CheckRequest_Data_ID == CheckRequestDetails.Ex_CheckRequestData_ID).Select(v => new Importers
        //                    {
        //                        ImporterCompany = v.ImportCompany,
        //                        ImporterCompanyAddress = v.ImporeterCompanyAddress,
        //                        ImporterCompanyEn = v.ImportCompany_EN,
        //                        ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN
        //                    }).ToList();
        //                    CheckRequestDetails.ImportersCompanies = com;
        //                    //end companies
        //                    var shippingMethods = uow.Repository<Ex_CheckRequset_Shipping_Method>().GetData().Where(c => c.Ex_CheckRequest_ID == CheckRequestDetails.Ex_CheckRequest_ID).Select(n => new checkRequestShipping
        //                    {
        //                        ID = n.ID,
        //                        containers_ID = n.containers_ID,
        //                        containers_type_ID = n.containers_type_ID,
        //                        ShipholdNumber = n.ShipholdNumber,
        //                        ContainerNumber = n.ContainerNumber,
        //                        NavigationalNumber = n.NavigationalNumber,
        //                        Total_Weight = n.Total_Weight
        //                    }).ToList();
        //                    // distinct items for constrains
        //                    var initiatorsId = new List<long?>();

        //                    foreach (var ship in shippingMethods)
        //                    {
        //                        var container = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_ID && c.SystemCodeTypeId == 28);
        //                        if (container != null)
        //                        {
        //                            ship.containerName = lang == "1" ? container.ValueName : container.ValueNameEN;
        //                        }
        //                        var containertype = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_type_ID && c.SystemCodeTypeId == 29);
        //                        if (container != null)
        //                        {
        //                            ship.containerType = lang == "1" ? containertype.ValueName : containertype.ValueNameEN;
        //                        }
        //                        //Items
        //                        var itemss = uow.Repository<Ex_CheckRequest_Items>().GetData().Where(i => i.Ex_CheckRequset_Shipping_Method_ID == ship.ID).
        //                            Select(v => new Items_checkReq
        //                            {
        //                                //eslam 8-2021
        //                                ID = v.ID,
        //                                Ex_Initiator_ID = v.Ex_Initiator_ID,
        //                                ImcheckReqItem_ID = v.ID,
        //                                ImcheckReqshippedMethod_ID = v.Ex_CheckRequset_Shipping_Method_ID,
        //                                Size = v.Size,
        //                                Package_Count = v.Package_Count,
        //                                Package_Weight = v.Package_Weight,
        //                                Units_Number = v.Units_Number,
        //                                packageTypeID = v.Package_Type_ID,
        //                                GrossWeight = v.GrossWeight,
        //                                Net_Weight = v.Net_Weight,
        //                                Fees = v.Fees,
        //                                Item_ShortName_ID = v.Item_ShortName_ID,
        //                                Order_TextItem = v.Order_Text

        //                            }).Distinct().ToList();
        //                        var ids = uow.Repository<Ex_CheckRequest_Items>().GetData().Where(i => i.Ex_CheckRequset_Shipping_Method_ID == ship.ID).Select(i => i.Ex_Initiator_ID).Distinct().ToList();
        //                        initiatorsId.AddRange(ids);
        //                        //end Items
        //                        foreach (var itt in itemss)
        //                        {
        //                            var initiatir = uow.Repository<Ex_Initiator>().GetData().Include(f => f.Country)
        //                                .Where(u => u.ID == itt.Ex_Initiator_ID && u.User_Deletion_Id == null).FirstOrDefault();
        //                            //itt.Item_ShortName_ID = 13;
        //                            if (itt.Item_ShortName_ID != null)
        //                            {
        //                                var ism = uow.Repository<Item_ShortName>().GetData().
        //                            Where(i => i.ID == itt.Item_ShortName_ID && i.User_Deletion_Id == null).FirstOrDefault();
        //                                itt.ItemShortNameAr = ism.ShortName_Ar;
        //                                itt.ItemShortNameEn = ism.ShortName_En;
        //                                itt.InitiatorCountry = (lang == "1" ? initiatir.Country.Ar_Name : initiatir.Country.En_Name);
        //                                if (itt.Purpose != null)
        //                                {
        //                                    itt.Purpose = (lang == "1" ? ism.Item_Purpose.Ar_Name : ism.Item_Purpose.En_Name);
        //                                }
        //                                if (itt.Status != null)
        //                                {
        //                                    itt.Status = (lang == "1" ? ism.Item_Status.Ar_Name : ism.Item_Status.En_Name);
        //                                }
        //                                if (itt.ItemName != null)
        //                                {
        //                                    itt.ItemName = (lang == "1" ? ism.Item.Name_Ar : ism.Item.Name_En);
        //                                    itt.ID = ism.Item.ID;
        //                                }
        //                                if (itt.SubPart_Name != null)
        //                                {
        //                                    itt.ItemName = (lang == "1" ? ism.Item.Name_Ar : ism.Item.Name_En);
        //                                    itt.ID = ism.Item.ID;
        //                                }
        //                            }


        //                            //ask about qualitive group eslam
        //                            var qualId = initiatir.QualitativeGroup_Id;
        //                            if (qualId != null)
        //                            {
        //                                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId && y.User_Deletion_Id == null).FirstOrDefault().Name_Ar;
        //                                itt.InitiatorCountry = (lang == "1" ? initiatir.Country.Ar_Name : initiatir.Country.En_Name);

        //                            }

        //                            var itemShortNameId = itt.Item_ShortName_ID;//13;//
        //                            if (itemShortNameId != null)
        //                            {
        //                                var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId && a.User_Deletion_Id == null).FirstOrDefault();
        //                                itt.ItemShortNameAr = itemShortName.ShortName_Ar;
        //                                itt.ItemShortNameEn = itemShortName.ShortName_En;
        //                                //الجزء النباتي
        //                                //itt.SubPart_Name = (lang == "1" ? itemShortName.SubPart.Name_Ar : itemShortName.SubPart.Name_En);
        //                                if (itt.ItemName != null)
        //                                {
        //                                    itt.ItemName = (lang == "1" ? itemShortName.Item.Name_Ar : itemShortName.Item.Name_En);
        //                                }
        //                                var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
        //                                if (stat != null)
        //                                {
        //                                    itt.Status = stat.Ar_Name;
        //                                }
        //                                var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
        //                                if (prp != null)
        //                                {
        //                                    itt.Purpose = prp.Ar_Name;
        //                                }
        //                                var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
        //                                if (subp != null)
        //                                {
        //                                    itt.subPartName = subp.Name_Ar;
        //                                }

        //                                itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
        //                            }

        //                            //List categories And lots

        //                            var catAndLots = uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Ex_CheckRequest_Items_ID == itt.ImcheckReqItem_ID)
        //                                .Select(v => new categories_lots
        //                                {
        //                                    //eslam
        //                                    ID = v.ID,
        //                                    Ex_checkReqItems_ID = v.Ex_CheckRequest_Items_ID,
        //                                    ItemCategory_ID = v.ItemCategory_ID,
        //                                    Size = v.Size,
        //                                    Package_Count = v.Package_Count,
        //                                    Package_Weight = v.Package_Weight,
        //                                    Units_Number = v.Units_Number,
        //                                    packageTypeID = v.Package_Type_ID,
        //                                    Order_TextLot = v.Order_Text,
        //                                    packageMaterialID = v.Package_Material_ID,
        //                                    Lot_Number = v.Lot_Number,
        //                                    Grower_Number = v.Grower_Number,
        //                                    Waybill = v.Waybill,
        //                                    Number_Wooden_Package = v.Number_Wooden_Package,
        //                                    GrossWeight = v.GrossWeight,
        //                                    Net_Weight = v.Net_Weight,
        //                                    Package_Based_Weight = v.Package_Based_Weight,
        //                                    Package_Net_Weight = v.Package_Net_Weight,
        //                                    Reason_Entry = v.Reason_Entry,
        //                                    Based_Weight = v.Based_Weight,

        //                                })
        //                              .ToList();


        //                            foreach (var ctt in catAndLots)
        //                            {
        //                                var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
        //                                if (ptypec != null)
        //                                {
        //                                    ctt.packageType = (lang == "1" ? ptypec.Ar_Name : ptypec.En_Name);//ptypec.Ar_Name;
        //                                }


        //                                var categ = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == ctt.ItemCategory_ID).FirstOrDefault();
        //                                if (categ != null)
        //                                {
        //                                    ctt.categoryName = (lang == "1" ? categ.Name_Ar : categ.Name_En);//categ.Name_Ar;
        //                                    ctt.RecordedOrNot = ((bool)categ.IsRegister ? "مسجل" : "غير مسجل");
        //                                    if (categ.ItemCategories_Group_ID == null)
        //                                    {
        //                                        ctt.ItemCategoryGroup = "لا يوجد";
        //                                    }
        //                                    else
        //                                    {
        //                                        var ccc = uow.Repository<ItemCategories_Group>().GetData().Where(d => d.ID == categ.ItemCategories_Group_ID).FirstOrDefault();
        //                                        ctt.ItemCategoryGroup = (lang == "1" ? ccc.Name_Ar : ccc.Name_En);
        //                                    }

        //                                }
        //                                var pckmtr = uow.Repository<Package_Material>().GetData().Where(g => g.ID == ctt.packageMaterialID).FirstOrDefault();
        //                                if (pckmtr != null)
        //                                {
        //                                    ctt.packageMaterialName = (lang == "1" ? pckmtr.Ar_Name : pckmtr.En_Name);
        //                                }


        //                            }
        //                            itt.ItemCategories_lots = catAndLots;

        //                        }
        //                        //constrains
        //                        foreach (var init in itemss)
        //                        {
        //                            var initiatorId = init.Ex_Initiator_ID;
        //                            var initiatir = uow.Repository<Ex_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();

        //                            var ItemShortName = initiatir.Item_ShortName_ID;
        //                            var qualId = initiatir.QualitativeGroup_Id;
        //                            var conTextAr = uow.Repository<Ex_Constrain_Initiator_Text>().GetData()
        //                                .Include(k => k.Ex_CountryConstrain_Text)
        //                                .Where(i => i.Ex_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

        //                                    r.Ex_CountryConstrain_Text.ConstrainText_Ar
        //                            // r.Ex_CountryConstrain_Text.InSide_Certificate_Ar
        //                            //    }

        //                            //text_En = r.Ex_CountryConstrain_Text.ConstrainText_En,
        //                            ).ToList();
        //                            var conTextEn = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


        //                             r.Ex_CountryConstrain_Text.ConstrainText_En
        //                            ).ToList();
        //                            var desAR = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

        //                                  r.Ex_CountryConstrain_Text.InSide_Certificate_Ar
        //                            //text_En = r.Ex_CountryConstrain_Text.ConstrainText_En,
        //                            ).ToList();
        //                            var desEn = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

        //                                  r.Ex_CountryConstrain_Text.InSide_Certificate_En
        //                            //text_En = r.Ex_CountryConstrain_Text.ConstrainText_En,
        //                            ).ToList();
        //                            var constrains = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text)
        //                                .Where(i => i.Ex_Initiator_ID == initiatorId && i.IsActive == true).ToList();
        //                            var constr = new constrains();
        //                            constr.texts_Ar = conTextAr
        //;
        //                            constr.text_En = conTextEn;
        //                            constr.InSide_Certificate_Ar = desAR;
        //                            constr.InSide_Certificate_En = desEn;
        //                            var pp = new List<ports>();

        //                            if (ItemShortName != null)
        //                            {
        //                                pp = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData()
        //                                    .Where(p => p.Item_ShortName_ID == ItemShortName && p.IsActive == true).Select(w => new ports
        //                                    {

        //                                        portId = w.Port_National_Id,
        //                                        portTypeId = w.Port_Type_ID
        //                                    }).ToList();


        //                            }
        //                            if (qualId != null)
        //                            {
        //                                pp = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData()
        //                                    //.Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true&& p.User_Deletion_Id==null)
        //                                    .Select(w => new ports
        //                                    {

        //                                        portId = w.Port_National_Id,
        //                                        portTypeId = w.Port_Type_ID
        //                                    }).ToList();


        //                            }
        //                            foreach (var prt in pp)
        //                            {
        //                                if (prt.portTypeId != null)
        //                                {
        //                                    var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId && m.User_Deletion_Id == null && m.IsActive == true).FirstOrDefault();

        //                                    if (pTName != null)
        //                                    {
        //                                        prt.portType = pTName.Name_Ar;

        //                                    }

        //                                }
        //                                if (prt.portId != null)
        //                                {
        //                                    var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId && m.User_Deletion_Id == null && m.IsActive == true).FirstOrDefault();

        //                                    if (pName != null)
        //                                    {
        //                                        prt.portName = pName.Name_Ar;

        //                                    }
        //                                }
        //                            }
        //                            constr.itemConstrainPorts = pp;


        //                            init.Itemconstrains = constr;

        //                        }
        //                        ship.Items_checkReq = itemss;
        //                        //Shift_Item_All

        //                    }

        //                    CheckRequestDetails.checkRequestShipping = shippingMethods;
        //                    //Attachments
        //                    CheckRequestDetails.Attachments = uow.Repository<A_AttachmentData>().GetData()
        //                        .Where(v => v.RowId == CheckRequestDetails.Ex_CheckRequest_ID && v.A_AttachmentTableNameId == 8 && v.User_Deletion_Id == null)
        //                        .Select(x => new Attachments
        //                        {
        //                            Attachment_Number = x.Attachment_Number,
        //                            AttachmentPath = x.AttachmentPath,
        //                            Attachment_TypeName = x.Attachment_TypeName,
        //                            StartDate = x.StartDate,
        //                            EndDate = x.EndDate,
        //                            Attachment_Name = (lang == "1" ? x.A_AttachmentTableType.Ar_Name : x.A_AttachmentTableType.En_Name)


        //                        }).ToList();

        //                    //emn
        //                    initiatorsId = initiatorsId.Distinct().ToList();
        //                    var itemsWithConstrains = new List<Items_checkReq>();
        //                    foreach (var ids in initiatorsId)
        //                    {
        //                        var initiatir = uow.Repository<Ex_Initiator>().GetData().Include(f => f.Country).Where(u => u.ID == ids).FirstOrDefault();
        //                        var itt = new Items_checkReq();
        //                        itt.InitiatorCountry = initiatir.Country.Ar_Name;
        //                        //ask about qualitive group
        //                        var qualId = initiatir.QualitativeGroup_Id;
        //                        if (qualId != null)
        //                        {
        //                            itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
        //                        }

        //                        var itemShortNameId = initiatir.Item_ShortName_ID;
        //                        if (itemShortNameId != null)
        //                        {
        //                            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
        //                            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
        //                            itt.ItemShortNameEn = itemShortName.ShortName_En;

        //                            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
        //                            if (stat != null)
        //                            {
        //                                itt.Status = stat.Ar_Name;
        //                            }
        //                            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
        //                            if (prp != null)
        //                            {
        //                                itt.Purpose = prp.Ar_Name;
        //                            }
        //                            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
        //                            if (subp != null)
        //                            {
        //                                itt.subPartName = subp.Name_Ar;
        //                            }

        //                            itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
        //                        }

        //                        //var ItemShortName = initiatir.Item_ShortName_ID;
        //                        //var qualId = initiatir.QualitativeGroup_Id;
        //                        var conTextAr = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == ids && i.IsActive == true).Select(r =>

        //                             r.Ex_CountryConstrain_Text.ConstrainText_Ar
        //                        //text_En = r.Ex_CountryConstrain_Text.ConstrainText_En,
        //                        ).ToList();
        //                        var conTextEn = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == ids && i.IsActive == true).Select(r =>


        //                         r.Ex_CountryConstrain_Text.ConstrainText_En
        //                        ).ToList();
        //                        var desAR = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == ids && i.IsActive == true).Select(r =>

        //                                 r.Ex_CountryConstrain_Text.InSide_Certificate_Ar
        //                           //text_En = r.Ex_CountryConstrain_Text.ConstrainText_En,
        //                           ).ToList();
        //                        var desEn = uow.Repository<Ex_Constrain_Initiator_Text>().GetData().Include(k => k.Ex_CountryConstrain_Text).Where(i => i.Ex_Initiator_ID == ids && i.IsActive == true).Select(r =>

        //                              r.Ex_CountryConstrain_Text.InSide_Certificate_En
        //                        //text_En = r.Ex_CountryConstrain_Text.ConstrainText_En,
        //                        ).ToList();
        //                        var constr = new constrains();
        //                        constr.texts_Ar = conTextAr;
        //                        constr.text_En = conTextEn;
        //                        constr.InSide_Certificate_Ar = desAR;
        //                        constr.InSide_Certificate_En = desEn;
        //                        var pp = new List<ports>();

        //                        if (itemShortNameId != null)
        //                        {
        //                            pp = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == itemShortNameId && p.IsActive == true).Select(w => new ports
        //                            {

        //                                portId = w.Port_National_Id,
        //                                portTypeId = w.Port_Type_ID
        //                            }).ToList();


        //                        }
        //                        if (qualId != null)
        //                        {

        //                            pp = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData()
        //                                .Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new ports
        //                                {

        //                                    portId = w.Port_National_Id,
        //                                    portTypeId = w.Port_Type_ID
        //                                }).ToList();


        //                        }
        //                        foreach (var prt in pp)
        //                        {
        //                            if (prt.portTypeId != null)
        //                            {
        //                                var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

        //                                if (pTName != null)
        //                                {
        //                                    prt.portType = pTName.Name_Ar;


        //                                }

        //                            }
        //                            if (prt.portId != null)
        //                            {
        //                                var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

        //                                if (pName != null)
        //                                {
        //                                    prt.portName = pName.Name_Ar;
        //                                    //govNamex = (lang == "1" ? pName.Governate.Ar_Name : pName.Governate.En_Name);
        //                                    //   govName = pName.Governate.Ar_Name;(lang == "1" ? v.ShippingAgency.Name_Ar : v.ShippingAgency.Name_En)
        //                                    //  CheckRequestDetails.govNameAR = uow.Repository<Governate>().GetData().Where(m => m.ID == pName.Govern_ID).FirstOrDefault().Ar_Name;
        //                                    //  CheckRequestDetails.govNameEN = uow.Repository<Governate>().GetData().Where(m => m.ID == pName.Govern_ID).FirstOrDefault().En_Name;
        //                                }
        //                            }
        //                        }
        //                        constr.itemConstrainPorts = pp;


        //                        itt.Itemconstrains = constr;
        //                        itemsWithConstrains.Add(itt);




        //                    }
        //                    CheckRequestDetails.itemsWithConstrains = itemsWithConstrains;

        //                    ///////////////ESLAM///////////////


        //                    ///////////////ESLAM///////////////
        //                    var customs = uow.Repository<Ex_CheckRequest_Customs_Message>()
        //                         .GetData().Where(i => i.Ex_CheckRequest_ID == CheckRequestDetails.Ex_CheckRequest_ID)
        //                         .Select(v => new CustomsMessage
        //                         {
        //                             Ex_CheckRequest_ID = v.Ex_CheckRequest_ID,
        //                             Customs_Certificate_Number = v.Customs_Certificate_Number,
        //                             Shipment_Date = v.Shipment_Date,
        //                             Certification_Date = v.Certification_Date,
        //                             Arrival_Date = v.Arrival_Date,
        //                             Manifest_Number = v.Manifest_Number,
        //                             Certificate_Number_Each_Product = v.Certificate_Number_Each_Product,
        //                             Shipping_Agency_ID = v.Shipping_Agency_ID,
        //                             Shipping_Agency_Name = (lang == "1" ? v.ShippingAgency.Name_Ar : v.ShippingAgency.Name_En),
        //                             OperationType_ID = v.Ex_OperationType,


        //                         }).ToList();
        //                    CheckRequestDetails.CustomsMessages = customs;
        //                    var Ex_OpertaionType_id = customs.Select(c => c.OperationType_ID).FirstOrDefault();
        //                    CheckRequestDetails.OperationType_Name = uow.Repository<Ex_OpertaionType>().GetData().Where(a => a.ID == Ex_OpertaionType_id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

        //                    ///////////////ESLAM///////////////
        //                }
        //                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
        //            }
        //            catch (Exception ex)
        //            {
        //                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //            }
        //        }
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
                #region log Action Approve for Ex_CheckRequest

                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                dto2.ID_Table_Action = 53;
                // dto2.ID_TableActionValue = checkRequestId;
                dto2.Im_CheckRequest_ID = dto.ID;
                dto2.User_Creation_Id = dto.User_Creation_Id;
                dto2.User_Creation_Date = DateTime.Now;
                dto2.NOTS = " تم الموافقه علي طلب الفحص الصادر ";
                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                dto2.Type_log_ID = 135;  //system code for Update
                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                x.save_EX_CheckRequest_Log(dto2, Device_Info);

                #endregion

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

        public Dictionary<string, object> saveItemFees(Items_checkReq item, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest_Items CModel = uow.Repository<Ex_CheckRequest_Items>().Findobject((long)item.ImcheckReqItem_ID);
                CModel.Fees = item.Fees;



                uow.SaveChanges();

                #region log Action for update export Fees

                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                dto2.ID_Table_Action = 55;
                // dto2.ID_TableActionValue = checkRequestId;
                dto2.Im_CheckRequest_ID = item.Ex_Items_checkReqID;
                dto2.User_Creation_Id = item.Accept_User_Creation_Id;
                dto2.User_Creation_Date = DateTime.Now;
                dto2.NOTS = " تم تعديل الرسوم في طلب الفحص الصادر ";
                dto2.User_Type_ID = 127;// System Code For موظف الحجر
                dto2.Type_log_ID = 135;  //system code for Update
                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                x.save_EX_CheckRequest_Log(dto2, Device_Info);

                #endregion



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, item.ImcheckReqItem_ID);
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
        public Dictionary<string, object> InsertReason(EX_CheckRequest_RefuseReasonDTO entity, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_seq");
                entity.ID = idd;
                var CModel = Mapper.Map<Ex_CheckRequest_RefuseReason>(entity);

                uow.Repository<Ex_CheckRequest_RefuseReason>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetExCheckRequestList_filter
   (short userId, long? Outlet_User_ID, long? Station_User_ID, string CanView_Outlet_Examination,
    string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi, List<string> Device_Info)
        {
            try
            {
                var dt = DateTime.Now.Date;
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                var List_Station_Emp = db.Station_Emp.Where(a => a.Emp_Id == userId
                && a.Date_To >= dt && a.IsActive == true && a.User_Deletion_Date == null).Select(a => a.Station_Id).ToList();
                var lists = db.Ex_List.ToList();
                var list1 = lists; //db.Ex_List.ToList();
                var list2 = list1;
                var list3 = list1;
                var list4 = list1;

                if (CanView_Outlet_Examination == "1")
                {
                    //list1 = db.Ex_List.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                    list1 = lists.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                }
                else
                { list1.Clear(); }

                if (CanAdd_Station_Examination == "1")
                {
                    // list2 = db.Ex_List.Where(a => List_Station_Emp.Contains( a.Station_Examination_ID)).ToList();
                    list2 = lists.Where(a => List_Station_Emp.Contains(a.Station_Examination_ID)).ToList();
                }
                else
                { list2.Clear(); }
                if (CanEdit_Outlet_Genshi == "1")
                {
                    //list3 = db.Ex_List.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                    list3 = lists.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                }
                else
                { list3.Clear(); }
                if (CanDelete_Station_Genshi == "1")
                {
                    //list4 = db.Ex_List.Where(a => List_Station_Emp.Contains(a.Station_Genshi_ID)).ToList();
                    list4 = lists.Where(a => List_Station_Emp.Contains(a.Station_Genshi_ID)).ToList();
                }
                else
                { list4.Clear(); }
                var union_data = list1.Union(list2).Union(list3).Union(list4).ToList();



                //union_data = union_data.Where(a => a.Ex_CheckRequest_ID == 304).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, union_data);
                //}
                //else
                //{
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);

                //}
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetExCheckRequestList_filterAll
(short userId, long? Outlet_User_ID, long? Station_User_ID, string CanView_Outlet_Examination,
string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi, int? radio_ID, long? Company_ID, long? Country_ID, long? StationId, string ExChechRequest_Num, string dateFrom1, string dateEnd1, List<string> Device_Info)
        {
            try
            {
                var dt = DateTime.Now.Date;
                var dateFrom = DateTime.Parse(dateFrom1);
                var dateEnd = DateTime.Parse(dateEnd1);


                PlantQuarantineEntities db = new PlantQuarantineEntities();


                var List_Station_Emp = db.Station_Emp.Where(a => a.Emp_Id == userId && a.Date_To >= dt && a.IsActive == true && a.User_Deletion_Date == null).Select(a => a.Station_Id).ToList();



                //var lists = db.Ex_List2.ToList();


                List<Ex_List2> lists = new List<Ex_List2>();
                if (radio_ID != null && (Country_ID != null || Company_ID != null || ExChechRequest_Num != null || StationId != null))
                {
                    switch (radio_ID)
                    {
                        //تم الموافقه
                        case 2:
                            lists = db.Ex_List2.Where(a => a.IsAccepted == true && (a.Creation_Date >= dateFrom.Date && a.Creation_Date <= dateEnd) && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            //    lists = db.Ex_List2.Where(a => a.IsAccepted == true && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId  || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            break;
                        //لم يتم اتخاذ اجراء 
                        case 3:
                            lists = db.Ex_List2.Where(a => a.IsAccepted == null && (a.Creation_Date >= dateFrom.Date && a.Creation_Date <= dateEnd) && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            break;
                        //مرفوضة
                        case 4:
                            lists = db.Ex_List2.Where(a => a.IsAccepted == false && (a.Creation_Date >= dateFrom.Date && a.Creation_Date <= dateEnd) && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            break;
                        //الطلب مغلق
                        case 5:
                            lists = db.Ex_List2.Where(a => a.Final_Result_ID == 9 || a.Final_Result_ID == 11 || a.Final_Result_ID == 14 || a.Final_Result_ID == 15 && (a.Creation_Date >= dateFrom.Date && a.Creation_Date <= dateEnd) && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            break;
                        //موقف الحجر الطلب يعمل
                        case 6:
                            lists = db.Ex_List2.Where(a => a.Final_Result_ID == 1 || a.Final_Result_ID == 2 || a.Final_Result_ID == 3 || a.Final_Result_ID == 4 || a.Final_Result_ID == 5 || a.Final_Result_ID == 6 && (a.Creation_Date >= dateFrom.Date && a.Creation_Date <= dateEnd) && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            break;
                        //موقف الحجر الطلب لا يعمل
                        case 7:
                            lists = db.Ex_List2.Where(a => a.Final_Result_ID == 9 || a.Final_Result_ID == 11 || a.Final_Result_ID == 14 || a.Final_Result_ID == 15 && (a.Creation_Date >= dateFrom.Date && a.Creation_Date <= dateEnd) && (a.ExportCountryID == Country_ID || a.Importer_ID == Company_ID || a.Station_Examination_ID == StationId || a.ImCheckRequest_Number == ExChechRequest_Num)).ToList();
                            break;


                    }


                }
                //if (Company_ID != null)
                //{
                //    lists = db.Ex_List2.Where(a => a.Importer_ID == Company_ID).ToList();

                //}
                //if (Country_ID != null)
                //{
                //     lists = db.Ex_List2.Where(a => a.ExportCountryID == Country_ID).ToList();

                //}

                //if (ExChechRequest_Num != null)
                //{
                //    lists = db.Ex_List2.Where(a => a.ImCheckRequest_Number == ExChechRequest_Num).ToList();

                //}


                var list1 = lists; //db.Ex_List2.ToList();
                var list2 = list1;
                var list3 = list1;
                var list4 = list1;

                if (CanView_Outlet_Examination == "1")
                {
                    //list1 = db.Ex_List2.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                    list1 = lists.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                }
                else
                { list1.Clear(); }

                if (CanAdd_Station_Examination == "1")
                {
                    // list2 = db.Ex_List2.Where(a => List_Station_Emp.Contains( a.Station_Examination_ID)).ToList();
                    list2 = lists.Where(a => List_Station_Emp.Contains(a.Station_Examination_ID)).ToList();
                }
                else
                { list2.Clear(); }
                if (CanEdit_Outlet_Genshi == "1")
                {
                    //list3 = db.Ex_List2.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                    list3 = lists.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                }
                else
                { list3.Clear(); }
                if (CanDelete_Station_Genshi == "1")
                {
                    //list4 = db.Ex_List2.Where(a => List_Station_Emp.Contains(a.Station_Genshi_ID)).ToList();
                    list4 = lists.Where(a => List_Station_Emp.Contains(a.Station_Genshi_ID)).ToList();
                }
                else
                { list4.Clear(); }
                var union_data = list1.Union(list2).Union(list3).Union(list4).ToList();
                //union_data = union_data.Where(a => a.Ex_CheckRequest_ID == 304).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, union_data);
                //}
                //else
                //{
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);

                //}
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Get_ListExportersService_filter(string ListExportersService, List<string> Device_Info)
        {
            try
            {
                var dt = DateTime.Now.Date;
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                var lists = db.Ex_List.Where(a => a.Station_Examination_ID != null || a.Station_Genshi_ID != null).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, lists);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Get_ListAll_filter(string ListAll, int page, string Search, List<string> Device_Info)
        {
            try
            {
                var dt = DateTime.Now.Date;
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                var lists = db.Ex_List.ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, lists);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetListWithPage(int CurrentPage, int pageSize, string Search)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            if (Search != null)
            {
                var ListAllEx = (from exl in db.Ex_List
                                 where exl.ImCheckRequest_Number.Contains(Search.Trim())
                                 select new Ex_ListDTO
                                 {
                                     Outlet_User_ID = exl.Outlet_User_ID,
                                     Outlet_User_Name = exl.Outlet_User_Name,
                                     Center_ID = exl.Center_ID,
                                     Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                                     ImCheckRequest_Number = exl.ImCheckRequest_Number,
                                     Creation_Date = exl.Creation_Date,
                                     IsAccepted = exl.IsAccepted,
                                     IsActive = exl.IsActive,
                                     ExportCountryName = exl.ExportCountryName,
                                     Importer_ID = exl.Importer_ID,
                                     IsPaid = exl.IsPaid,
                                     ImporterType_Id = exl.ImporterType_Id,
                                     Outlet_ID = exl.Outlet_ID,
                                     ImporterTypeName = exl.ImporterTypeName,
                                     ImporterName = exl.ImporterName,
                                     //f = exl.f,
                                     g = exl.g,
                                     Outlet_Examination_ID = exl.Outlet_Examination_ID,
                                     Outlet_Examination_Name = exl.Outlet_Examination_Name,
                                     Station_Examination_ID = exl.Station_Examination_ID,
                                     Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                                     Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                                     Station_Genshi_ID = exl.Station_Genshi_ID,
                                     Closed_Request = exl.Closed_Request,
                                     Final_Result_ID = exl.Final_Result_ID,
                                     Final_Result_Name = exl.Final_Result_Name,
                                     Station_Examination_Name = exl.Station_Examination_Name,
                                     Station_Genshi_Name = exl.Station_Genshi_Name,

                                 }).OrderByDescending(b => b.Ex_CheckRequest_ID)
                                .Skip((CurrentPage - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
                var TotalResults = db.Ex_List.Where(exl=> exl.ImCheckRequest_Number.Contains(Search.Trim())).Count();
                if (TotalResults > 0)
                {
                    ListAllEx.FirstOrDefault().TotalResults = TotalResults;

                    ListAllEx.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                }

                return uow.Repository<Object>().DataReturn((int)TotalResults, ListAllEx);
            }
            else {
                var ListAllEx = (from exl in db.Ex_List
                                 select new Ex_ListDTO
                                 {
                                     Outlet_User_ID = exl.Outlet_User_ID,
                                     Outlet_User_Name = exl.Outlet_User_Name,
                                     Center_ID = exl.Center_ID,
                                     Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                                     ImCheckRequest_Number = exl.ImCheckRequest_Number,
                                     Creation_Date = exl.Creation_Date,
                                     IsAccepted = exl.IsAccepted,
                                     IsActive = exl.IsActive,
                                     ExportCountryName = exl.ExportCountryName,
                                     Importer_ID = exl.Importer_ID,
                                     IsPaid = exl.IsPaid,
                                     ImporterType_Id = exl.ImporterType_Id,
                                     Outlet_ID = exl.Outlet_ID,
                                     ImporterTypeName = exl.ImporterTypeName,
                                     ImporterName = exl.ImporterName,
                                    // f = exl.f,
                                     g = exl.g,
                                     Outlet_Examination_ID = exl.Outlet_Examination_ID,
                                     Outlet_Examination_Name = exl.Outlet_Examination_Name,
                                     Station_Examination_ID = exl.Station_Examination_ID,
                                     Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                                     Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                                     Station_Genshi_ID = exl.Station_Genshi_ID,
                                     Closed_Request = exl.Closed_Request,
                                     Final_Result_ID = exl.Final_Result_ID,
                                     Final_Result_Name = exl.Final_Result_Name,
                                     Station_Examination_Name = exl.Station_Examination_Name,
                                     Station_Genshi_Name = exl.Station_Genshi_Name,

                                 }).OrderByDescending(b => b.Ex_CheckRequest_ID)
                                .Skip((CurrentPage - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
                var TotalResults = db.Ex_List.Count();
                if (TotalResults > 0)
                {
                    ListAllEx.FirstOrDefault().TotalResults = TotalResults;

                    ListAllEx.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                }

                return uow.Repository<Object>().DataReturn((int)TotalResults, ListAllEx);
            }
          
           
            //return new PaginatedList<Ex_ListDTO>(Gov, CurrentPage, TotalPages, TotalResults);
        }

        public Dictionary<string, object> GetListWithPage_List_filter
   (string SearchALL, short userId, long? Outlet_User_ID, long? Station_User_ID, string CanView_Outlet_Examination,
    string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi,
    int CurrentPage, int pageSize,
    List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                // محطات اليوزر
                var dt = DateTime.Now.Date;
                var List_Station_Emp = db.Station_Emp.Where(a => a.Emp_Id == userId
                && a.Date_To >= dt && a.IsActive == true && a.User_Deletion_Date == null).Select(a => a.Station_Id).ToList();



                if (SearchALL == null)
                {
                    var ListData = (from exl in db.Ex_List

                                    where List_Station_Emp.Contains(exl.Station_Examination_ID)
                                    || List_Station_Emp.Contains(exl.Station_Genshi_ID)// محطات المستخدم
                                    || exl.Outlet_Examination_ID == Outlet_User_ID
                                    || exl.Outlet_Genshi_ID == Outlet_User_ID
                                    select new Ex_ListDTO
                                    {
                                        Outlet_User_ID = exl.Outlet_User_ID,
                                        Outlet_User_Name = exl.Outlet_User_Name,
                                        Center_ID = exl.Center_ID,
                                        Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                                        ImCheckRequest_Number = exl.ImCheckRequest_Number,
                                        Creation_Date = exl.Creation_Date,
                                        IsAccepted = exl.IsAccepted,
                                        IsActive = exl.IsActive,
                                        ExportCountryName = exl.ExportCountryName,
                                        Importer_ID = exl.Importer_ID,
                                        IsPaid = exl.IsPaid,
                                        ImporterType_Id = exl.ImporterType_Id,
                                        Outlet_ID = exl.Outlet_ID,
                                        ImporterTypeName = exl.ImporterTypeName,
                                        ImporterName = exl.ImporterName,
                                        //f = exl.f,
                                        g = exl.g,
                                        Outlet_Examination_ID = exl.Outlet_Examination_ID,
                                        Outlet_Examination_Name = exl.Outlet_Examination_Name,
                                        Station_Examination_ID = exl.Station_Examination_ID,
                                        Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                                        Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                                        Station_Genshi_ID = exl.Station_Genshi_ID,
                                        Closed_Request = exl.Closed_Request,
                                        Final_Result_ID = exl.Final_Result_ID,
                                        Final_Result_Name = exl.Final_Result_Name,
                                        Station_Examination_Name = exl.Station_Examination_Name,
                                        Station_Genshi_Name = exl.Station_Genshi_Name,

                                    }).OrderByDescending(b => b.Ex_CheckRequest_ID)
               .Skip((CurrentPage - 1) * pageSize)
               .Take(pageSize)
               .ToList();
                    var TotalResults = (from exl in db.Ex_List

                                        where List_Station_Emp.Contains(exl.Station_Examination_ID)
                                        || List_Station_Emp.Contains(exl.Station_Genshi_ID)// محطات المستخدم
                                        || exl.Outlet_Examination_ID == Outlet_User_ID
                                        || exl.Outlet_Genshi_ID == Outlet_User_ID
                                        select new Ex_ListDTO
                                        {
                                            Outlet_User_ID = exl.Outlet_User_ID,
                                        }).Count();
                    //                    Outlet_User_ID = exl.Outlet_User_ID,
                    //                    var TotalResults1 = db.Ex_List.Where(exl => List_Station_Emp.Contains(exl.Station_Examination_ID)).ToList().Count();                                       
                    //var TotalResults2 = db.Ex_List.Where(exl => List_Station_Emp.Contains(exl.Station_Genshi_ID)).ToList().Count();                                       
                    //var TotalResults3 = db.Ex_List.Where(exl => exl.Outlet_Examination_ID == Outlet_User_ID).ToList().Count();                                       
                    //var TotalResults4 = db.Ex_List.Where(exl => exl.Outlet_Genshi_ID == Outlet_User_ID).ToList().Count();                                       
                                       
                   

                    //var TotalResults = TotalResults1+TotalResults2+TotalResults3+TotalResults4;
                    if (TotalResults > 0)
                    {
                        ListData.FirstOrDefault().TotalResults = TotalResults;

                        ListData.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults/ (double)pageSize);
                    }



                    return uow.Repository<Object>().DataReturn((int)TotalResults, ListData);

                }

                else
                {
                    var ListData = (from exl in db.Ex_List

                                    where (List_Station_Emp.Contains(exl.Station_Examination_ID)
                           || List_Station_Emp.Contains(exl.Station_Genshi_ID)
                           || exl.Outlet_Examination_ID == Outlet_User_ID
                           || exl.Outlet_Genshi_ID == Outlet_User_ID)
                            //|| exl.Outlet_User_Name.Contains(SearchALL.Trim())
                            && exl.ImCheckRequest_Number.Contains(SearchALL.Trim())
                                    //|| exl.ExportCountryName.Contains(SearchALL.Trim())
                                    //|| exl.Outlet_Examination_Name.Contains(SearchALL.Trim())
                                    //|| exl.Station_Genshi_Name.Contains(SearchALL.Trim())
                                    //|| exl.Final_Result_Name.Contains(SearchALL.Trim())
                                    //|| exl.Outlet_Genshi_Name.Contains(SearchALL.Trim())
                                    //|| exl.Station_Examination_Name.Contains(SearchALL.Trim())


                                    select new Ex_ListDTO
                                    {
                                        Outlet_User_ID = exl.Outlet_User_ID,
                                        Outlet_User_Name = exl.Outlet_User_Name,
                                        Center_ID = exl.Center_ID,
                                        Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                                        ImCheckRequest_Number = exl.ImCheckRequest_Number,
                                        Creation_Date = exl.Creation_Date,
                                        IsAccepted = exl.IsAccepted,
                                        IsActive = exl.IsActive,
                                        ExportCountryName = exl.ExportCountryName,
                                        Importer_ID = exl.Importer_ID,
                                        IsPaid = exl.IsPaid,
                                        ImporterType_Id = exl.ImporterType_Id,
                                        Outlet_ID = exl.Outlet_ID,
                                        ImporterTypeName = exl.ImporterTypeName,
                                        ImporterName = exl.ImporterName,
                                        //f = exl.f,
                                        g = exl.g,
                                        Outlet_Examination_ID = exl.Outlet_Examination_ID,
                                        Outlet_Examination_Name = exl.Outlet_Examination_Name,
                                        Station_Examination_ID = exl.Station_Examination_ID,
                                        Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                                        Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                                        Station_Genshi_ID = exl.Station_Genshi_ID,
                                        Closed_Request = exl.Closed_Request,
                                        Final_Result_ID = exl.Final_Result_ID,
                                        Final_Result_Name = exl.Final_Result_Name,
                                        Station_Examination_Name = exl.Station_Examination_Name,
                                        Station_Genshi_Name = exl.Station_Genshi_Name,

                                    }).OrderByDescending(b => b.Ex_CheckRequest_ID)
               .Skip((CurrentPage - 1) * pageSize)
               .Take(pageSize)
               .ToList();
                    var TotalResults = db.Ex_List.Where(exl => (List_Station_Emp.Contains(exl.Station_Examination_ID)
                           || List_Station_Emp.Contains(exl.Station_Genshi_ID)
                           || exl.Outlet_Examination_ID == Outlet_User_ID
                           || exl.Outlet_Genshi_ID == Outlet_User_ID)
                            //|| exl.Outlet_User_Name.Contains(SearchALL.Trim())
                            && exl.ImCheckRequest_Number.Contains(SearchALL.Trim())
                           //|| exl.ExportCountryName.Contains(SearchALL.Trim())
                           //|| exl.Outlet_Examination_Name.Contains(SearchALL.Trim())
                           //|| exl.Station_Genshi_Name.Contains(SearchALL.Trim())
                           //|| exl.Final_Result_Name.Contains(SearchALL.Trim())
                           //|| exl.Outlet_Genshi_Name.Contains(SearchALL.Trim())
                           //|| exl.Station_Examination_Name.Contains(SearchALL.Trim())

                           ).Count();
                    if (TotalResults > 0)
                    {
                        ListData.FirstOrDefault().TotalResults = TotalResults;

                        ListData.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                    }


                    return uow.Repository<Object>().DataReturn(0, ListData);
                }

                //     if (SearchALL != null)
                //     {
                //         ListData = ListData.Where(a => a.Outlet_User_Name.Contains( SearchALL.Trim())
                //         || a.ImCheckRequest_Number.Contains(SearchALL.Trim())
                //         || a.ExportCountryName.Contains(SearchALL.Trim())
                //         || a.Outlet_Examination_Name.Contains(SearchALL.Trim())
                //         || a.Station_Genshi_Name.Contains(SearchALL.Trim())
                //         || a.Final_Result_Name.Contains(SearchALL.Trim())
                //         || a.Outlet_Genshi_Name.Contains(SearchALL.Trim())
                //         || a.Station_Examination_Name.Contains(SearchALL.Trim())
                //         ).ToList();
                //     }

                //      ListData = (from exl in ListData
                //                     select new Ex_ListDTO
                //                     {
                //                         Outlet_User_ID = exl.Outlet_User_ID,
                //                         Outlet_User_Name = exl.Outlet_User_Name,
                //                         Center_ID = exl.Center_ID,
                //                         Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                //                         ImCheckRequest_Number = exl.ImCheckRequest_Number,
                //                         Creation_Date = exl.Creation_Date,
                //                         IsAccepted = exl.IsAccepted,
                //                         IsActive = exl.IsActive,
                //                         ExportCountryName = exl.ExportCountryName,
                //                         Importer_ID = exl.Importer_ID,
                //                         IsPaid = exl.IsPaid,
                //                         ImporterType_Id = exl.ImporterType_Id,
                //                         Outlet_ID = exl.Outlet_ID,
                //                         ImporterTypeName = exl.ImporterTypeName,
                //                         ImporterName = exl.ImporterName,
                //                         f = exl.f,
                //                         g = exl.g,
                //                         Outlet_Examination_ID = exl.Outlet_Examination_ID,
                //                         Outlet_Examination_Name = exl.Outlet_Examination_Name,
                //                         Station_Examination_ID = exl.Station_Examination_ID,
                //                         Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                //                         Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                //                         Station_Genshi_ID = exl.Station_Genshi_ID,
                //                         Closed_Request = exl.Closed_Request,
                //                         Final_Result_ID = exl.Final_Result_ID,
                //                         Final_Result_Name = exl.Final_Result_Name,
                //                         Station_Examination_Name = exl.Station_Examination_Name,
                //                         Station_Genshi_Name = exl.Station_Genshi_Name,

                //                     }).OrderByDescending(b => b.Ex_CheckRequest_ID)
                //.Skip((CurrentPage - 1) * pageSize)
                //.Take(pageSize)
                //.ToList();


                //var dt = DateTime.Now.Date;
                //PlantQuarantineEntities db = new PlantQuarantineEntities();
                //var List_Station_Emp = db.Station_Emp.Where(a => a.Emp_Id == userId
                //&& a.Date_To >= dt && a.IsActive == true && a.User_Deletion_Date == null).Select(a => a.Station_Id).ToList();
                //var lists = db.Ex_List.ToList();
                //var list1 = lists; //db.Ex_List.ToList();
                //var list2 = list1;
                //var list3 = list1;
                //var list4 = list1;

                //if (CanView_Outlet_Examination == "1")
                //{
                //    //list1 = db.Ex_List.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                //    list1 = lists.Where(a => a.Outlet_Examination_ID == Outlet_User_ID).ToList();
                //}
                //else
                //{ list1.Clear(); }

                //if (CanAdd_Station_Examination == "1")
                //{
                //    // list2 = db.Ex_List.Where(a => List_Station_Emp.Contains( a.Station_Examination_ID)).ToList();
                //    list2 = lists.Where(a => List_Station_Emp.Contains(a.Station_Examination_ID)).ToList();
                //}
                //else
                //{ list2.Clear(); }
                //if (CanEdit_Outlet_Genshi == "1")
                //{
                //    //list3 = db.Ex_List.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                //    list3 = lists.Where(a => a.Outlet_Genshi_ID == Outlet_User_ID).ToList();
                //}
                //else
                //{ list3.Clear(); }
                //if (CanDelete_Station_Genshi == "1")
                //{
                //    //list4 = db.Ex_List.Where(a => List_Station_Emp.Contains(a.Station_Genshi_ID)).ToList();
                //    list4 = lists.Where(a => List_Station_Emp.Contains(a.Station_Genshi_ID)).ToList();
                //}
                //else
                //{ list4.Clear(); }
                //var union_data = list1.Union(list2).Union(list3).Union(list4).ToList();



                ////union_data = union_data.Where(a => a.Ex_CheckRequest_ID == 304).ToList();
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, union_data);
                ////}
                ////else
                ////{
                ////    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);

                ////}
            }
             catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetListWithPage_List_filterQuick
 (string SearchALL, short userId, long? Outlet_User_ID, long? Station_User_ID, string CanView_Outlet_Examination,
  string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi,
  int CurrentPage, int pageSize,
  List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                // محطات اليوزر
                var dt = DateTime.Now.Date;
                var List_Station_Emp = db.Station_Emp.Where(a => a.Emp_Id == userId
                && a.Date_To >= dt && a.IsActive == true && a.User_Deletion_Date == null)
                    .Select(a => a.Station_Id).ToList();


                var sads = db.Ex_ListQuick.ToList();
                if (SearchALL == null)
                {
                    var ListData = (from exl in db.Ex_ListQuick

                                    where List_Station_Emp.Contains(exl.Station_Examination_ID)
                                    || List_Station_Emp.Contains(exl.Station_Genshi_ID)// محطات المستخدم
                                    || exl.Outlet_Examination_ID == Outlet_User_ID
                                    || exl.Outlet_Genshi_ID == Outlet_User_ID
                                    select new Ex_ListDTO
                                    {
                                        Outlet_User_ID = exl.Outlet_User_ID,
                                        Outlet_User_Name = exl.Outlet_User_Name,
                                        Center_ID = exl.Center_ID,
                                        Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                                        ImCheckRequest_Number = exl.ImCheckRequest_Number,
                                        Creation_Date = exl.Creation_Date,
                                        IsAccepted = exl.IsAccepted,
                                        IsActive = exl.IsActive,
                                        ExportCountryName = exl.ExportCountryName,
                                        Importer_ID = exl.Importer_ID,
                                        IsPaid = exl.IsPaid,
                                        ImporterType_Id = exl.ImporterType_Id,
                                        Outlet_ID = exl.Outlet_ID,
                                        ImporterTypeName = exl.ImporterTypeName,
                                        ImporterName = exl.ImporterName,
                                        //f = exl.f,
                                        g = exl.g,
                                        Outlet_Examination_ID = exl.Outlet_Examination_ID,
                                        Outlet_Examination_Name = exl.Outlet_Examination_Name,
                                        Station_Examination_ID = exl.Station_Examination_ID,
                                        Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                                        Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                                        Station_Genshi_ID = exl.Station_Genshi_ID,
                                        Closed_Request = exl.Closed_Request,
                                        Final_Result_ID = exl.Final_Result_ID,
                                        Final_Result_Name = exl.Final_Result_Name,
                                        Station_Examination_Name = exl.Station_Examination_Name,
                                        Station_Genshi_Name = exl.Station_Genshi_Name,

                                    }).OrderByDescending(b => b.Ex_CheckRequest_ID)
               .Skip((CurrentPage - 1) * pageSize)
               .Take(pageSize)
               .ToList();
                    var TotalResults = db.Ex_ListQuick.Where(exl => List_Station_Emp.Contains(exl.Station_Examination_ID)
                           || List_Station_Emp.Contains(exl.Station_Genshi_ID)
                           || exl.Outlet_Examination_ID == Outlet_User_ID
                           || exl.Outlet_Genshi_ID == Outlet_User_ID).Count();
                    if (TotalResults > 0)
                    {
                        ListData.FirstOrDefault().TotalResults = TotalResults;

                        ListData.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                    }



                    return uow.Repository<Object>().DataReturn((int)TotalResults, ListData);

                }

                else
                {
                    var ListData = (from exl in db.Ex_ListQuick

                                    where (List_Station_Emp.Contains(exl.Station_Examination_ID)
                           || List_Station_Emp.Contains(exl.Station_Genshi_ID)
                           || exl.Outlet_Examination_ID == Outlet_User_ID
                           || exl.Outlet_Genshi_ID == Outlet_User_ID)
                            //|| exl.Outlet_User_Name.Contains(SearchALL.Trim())
                            && exl.ImCheckRequest_Number.Contains(SearchALL.Trim())


                                    select new Ex_ListDTO
                                    {
                                        Outlet_User_ID = exl.Outlet_User_ID,
                                        Outlet_User_Name = exl.Outlet_User_Name,
                                        Center_ID = exl.Center_ID,
                                        Ex_CheckRequest_ID = exl.Ex_CheckRequest_ID,
                                        ImCheckRequest_Number = exl.ImCheckRequest_Number,
                                        Creation_Date = exl.Creation_Date,
                                        IsAccepted = exl.IsAccepted,
                                        IsActive = exl.IsActive,
                                        ExportCountryName = exl.ExportCountryName,
                                        Importer_ID = exl.Importer_ID,
                                        IsPaid = exl.IsPaid,
                                        ImporterType_Id = exl.ImporterType_Id,
                                        Outlet_ID = exl.Outlet_ID,
                                        ImporterTypeName = exl.ImporterTypeName,
                                        ImporterName = exl.ImporterName,
                                        //f = exl.f,
                                        g = exl.g,
                                        Outlet_Examination_ID = exl.Outlet_Examination_ID,
                                        Outlet_Examination_Name = exl.Outlet_Examination_Name,
                                        Station_Examination_ID = exl.Station_Examination_ID,
                                        Outlet_Genshi_ID = exl.Outlet_Genshi_ID,
                                        Outlet_Genshi_Name = exl.Outlet_Genshi_Name,
                                        Station_Genshi_ID = exl.Station_Genshi_ID,
                                        Closed_Request = exl.Closed_Request,
                                        Final_Result_ID = exl.Final_Result_ID,
                                        Final_Result_Name = exl.Final_Result_Name,
                                        Station_Examination_Name = exl.Station_Examination_Name,
                                        Station_Genshi_Name = exl.Station_Genshi_Name,

                                    }).OrderByDescending(b => b.Ex_CheckRequest_ID)
               .Skip((CurrentPage - 1) * pageSize)
               .Take(pageSize)
               .ToList();
                    var TotalResults = db.Ex_ListQuick.Where(exl => (List_Station_Emp.Contains(exl.Station_Examination_ID)
                           || List_Station_Emp.Contains(exl.Station_Genshi_ID)
                           || exl.Outlet_Examination_ID == Outlet_User_ID
                           || exl.Outlet_Genshi_ID == Outlet_User_ID)
                            //|| exl.Outlet_User_Name.Contains(SearchALL.Trim())
                            && exl.ImCheckRequest_Number.Contains(SearchALL.Trim())

                           ).Count();
                    if (TotalResults > 0)
                    {
                        ListData.FirstOrDefault().TotalResults = TotalResults;

                        ListData.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                    }


                    return uow.Repository<Object>().DataReturn(0, ListData);
                }

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}

