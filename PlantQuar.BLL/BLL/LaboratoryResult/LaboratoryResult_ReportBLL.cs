using PlantQuar.DAL;
using PlantQuar.DTO.DTO.labResult;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.LaboratoryResult
{
    public class LaboratoryResult_ReportBLL
    {
        private UnitOfWork uow;
        public LaboratoryResult_ReportBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetLaboratoryResult_ReportData(string barcode, List<string> Device_Info)
        {
            try
            {

                var request = new LaboratoryResult_ReportDTO();

                string lang = Device_Info[2];
                if (barcode.StartsWith("78"))//المزارع
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Farm_SampleData_Item
                               join frig in entities.Farm_Request_ItemCategories on sa.Farm_Request_ItemCategories_ID equals frig.ID into frig1
                               from frig in frig1.DefaultIfEmpty()
                               join fig in entities.Farm_ItemCategories on frig.Farm_ItemCategories_ID equals fig.ID into fig1
                               from fig in fig1.DefaultIfEmpty()
                               join ig in entities.ItemCategories on fig.ItemCategories_ID equals ig.ID into ig1
                               from ig in ig1.DefaultIfEmpty()
                               join fd in entities.FarmsDatas on sa.Farm_Committee.Farm_Request.FarmsData_ID equals fd.ID
                               join it in entities.Items on fd.Item_ID equals it.ID
                               join fc in entities.Farm_Company on fd.ID equals fc.Farm_ID
                               //join co in entities.Company_National on fc.Company_ID equals co.ID

                               join co in entities.Company_National on new { a = (long?)fc.ExporterType_Id, b = (long?)fc.Company_ID } equals new { a = (long?)6, b = (long?)co.ID } into cn1
                               from co in cn1.DefaultIfEmpty()
                               join po in entities.Public_Organization on new { a = (long?)fc.ExporterType_Id, b = (long?)fc.Company_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in entities.People on new { a = (long?)fc.ExporterType_Id, b = (long?)fc.Company_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join lt in entities.AnalysisLabTypes on sa.AnalysisLabType_ID equals lt.ID
                               join la in entities.AnalysisLabs on lt.AnalysisLabID equals la.ID
                               join at in entities.AnalysisTypes on lt.AnalysisTypeID equals at.ID
                               join vil in entities.Villages on fd.Village_ID equals vil.ID into iis
                               join cen in entities.Centers on fd.Center_Id equals cen.ID
                               join gov in entities.Governates on fd.Govern_ID equals gov.ID
                               from vil in iis.DefaultIfEmpty()
                               where sa.Sample_BarCode == barcode
                               select new LaboratoryResult_ReportDTO
                               {
                                   farmSampleId = sa.ID,
                                   //update model                                
                                   IsPrint = sa.IsPrint,
                                   labName = lang == "1" ? la.Name_Ar : la.Name_En,
                                   analysisType = lang == "1" ? at.Name_Ar : at.Name_En,
                                   WithdrawDate = sa.WithdrawDate,
                                   Sample_BarCode = sa.Sample_BarCode == null ? "####" : sa.Sample_BarCode,
                                   SampleSize = sa.SampleSize,
                                   SampleRatio = sa.SampleRatio,
                                   farmCode = fd.FarmCode_14 == null ? "####" : fd.FarmCode_14,
                                   farmName = fd.Name_Ar,
                                   farmAddress = fd.Address_Ar,
                                   governate = gov.Ar_Name,
                                   center = cen.Ar_Name,
                                   village = vil.Ar_Name,
                                   itemName = lang == "1" ? it.Name_Ar : it.Name_En,
                                   itemCategoryName = lang == "1" ? ig.Name_Ar : ig.Name_En,
                                   ExaminAddress = lang == "1" ? fd.Address_Ar : fd.Address_En,
                                   //companyName = lang == "1" ? co.Name_Ar : co.Name_En
                                   companyName = fc.ExporterType_Id == 6 ? (lang == "1" ? co.Name_Ar : co.Name_En)
                                                            : fc.ExporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                            : fc.ExporterType_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                            : "",
                                   //  company_Address_Ar = fc.ExporterType_Id == 6 ? (lang == "1" ? co.Address_Ar : co.Address_En)
                                   //: fc.ExporterType_Id == 7 ? (lang == "1" ? po.Address_Ar : po.Address_En)
                                   //: fc.ExporterType_Id == 8 ? (lang == "1" ? pr.Address : pr.Address_EN)
                                   //: "",
                               }).FirstOrDefault();
                    if (request != null)
                    {
                        if (String.IsNullOrEmpty(request.ExaminAddress))
                        {
                            request.ExaminAddress = "####";

                        }
                        if (String.IsNullOrEmpty(request.analysisType))
                        {
                            request.analysisType = "####";
                        }
                        if (String.IsNullOrEmpty(request.companyName))
                        {
                            request.companyName = "####";
                        }
                    }
                }
                else if (barcode.StartsWith("74")) // الوارد
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Im_CheckRequest_SampleData
                               join irc in entities.Im_RequestCommittee on sa.Im_RequestCommittee_ID equals irc.ID
                               join icr in entities.Im_CheckRequest on irc.ImCheckRequest_ID equals icr.ID
                               join icd in entities.Im_CheckRequest_Data on icr.ID equals icd.Im_CheckRequest_ID

                               //join co in entities.Company_National on icd.Importer_ID equals co.ID

                               join co in entities.Company_National on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)6, b = (long?)co.ID } into cn1
                               from co in cn1.DefaultIfEmpty()
                               join po in entities.Public_Organization on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in entities.People on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join gov in entities.Governates on icd.Governate_ID equals gov.ID
                               join fgg in entities.Im_CheckRequest_Items_Lot_Category on sa.LotData_ID equals fgg.ID
                               join ig in entities.ItemCategories on fgg.ItemCategory_ID equals ig.ID into iis
                               join sn in entities.Item_ShortName on sa.Item_ShortName_ID equals sn.ID
                               join it in entities.Items on sn.Item_ID equals it.ID
                               from itg in iis.DefaultIfEmpty()
                               where sa.Sample_BarCode == barcode
                               select new LaboratoryResult_ReportDTO
                               {
                                   farmSampleId = sa.ID,
                                   Syl_ALkhatima_Number = sa.Syl_ALkhatima_Number,
                                   IsPrint = sa.IsPrint,
                                   labName = lang == "1" ? sa.AnalysisLabType.AnalysisLab.Name_Ar : sa.AnalysisLabType.AnalysisLab.Name_En,
                                   analysisType = lang == "1" ? sa.AnalysisLabType.AnalysisType.Name_Ar : sa.AnalysisLabType.AnalysisType.Name_En,
                                   WithdrawDate = sa.WithdrawDate,
                                   Sample_BarCode = sa.Sample_BarCode == null ? "####" : sa.Sample_BarCode,
                                   SampleSize = sa.SampleSize,
                                   SampleRatio = sa.SampleRatio,
                                   //farmCode = fd.FarmCode_14 == null ? "####" : fd.FarmCode_14,
                                   farmName = lang == "1" ? icr.Outlet.Ar_Name : icr.Outlet.En_Name,
                                   //farmAddress = fd.Address_Ar,
                                   governate = lang == "1" ? gov.Ar_Name : gov.En_Name,
                                   // center = cen.Ar_Name,
                                   // village = vil.Ar_Name,
                                   itemName = lang == "1" ? it.Name_Ar : it.Name_En,
                                   itemCategoryName = lang == "1" ? itg.Name_Ar : itg.Name_En,
                                   ShortName = lang == "1" ? sn.ShortName_Ar : sn.ShortName_En,
                                   SubPart_Name = lang == "1" ? sn.SubPart.Name_Ar : sn.SubPart.Name_En,
                                   Item_Status_Name = lang == "1" ? sn.Item_Status.Ar_Name : sn.Item_Status.En_Name,
                                   Item_Purpose_Name = lang == "1" ? sn.Item_Purpose.Ar_Name : sn.Item_Purpose.En_Name,
                                   //////ExaminAddress = lang == "1" ? fd.Address_Ar : fd.Address_En,
                                   ////companyName = lang == "1" ? co.Name_Ar : co.Name_En,
                                   companyName = icd.ImporterType_Id == 6 ? (lang == "1" ? co.Name_Ar : co.Name_En)
                                                            : icd.ImporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                            : icd.ImporterType_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                            : "",
                                   Grower_Number = fgg.Grower_Number
                               }).FirstOrDefault();

                    //if (String.IsNullOrEmpty(request.ExaminAddress))
                    //{
                    //    request.ExaminAddress = "####";

                    //}
                    if (request != null)
                    {
                        if (String.IsNullOrEmpty(request.analysisType))
                        {
                            request.analysisType = "####";
                        }
                        if (String.IsNullOrEmpty(request.companyName))
                        {
                            request.companyName = "####";
                        }
                    }
                }

                else if (barcode.StartsWith("73")) // صادر
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Ex_CheckRequest_SampleData
                               join irc in entities.Ex_RequestCommittee on sa.Ex_RequestCommittee_ID equals irc.ID
                               join icr in entities.Ex_CheckRequest on irc.ExCheckRequest_ID equals icr.ID
                               join icd in entities.Ex_CheckRequest_Data on icr.ID equals icd.Ex_CheckRequest_ID

                               //join co in entities.Company_National on icd.Importer_ID equals co.ID

                               join co in entities.Company_National on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)6, b = (long?)co.ID } into cn1
                               from co in cn1.DefaultIfEmpty()
                               join po in entities.Public_Organization on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in entities.People on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join gov in entities.Governates on icd.Governate_ID equals gov.ID into gov1
                               from gov in gov1.DefaultIfEmpty()
                               join fgg in entities.Ex_CheckRequest_Items on sa.Ex_Request_Item_Id equals fgg.ID
                               join lot in entities.Ex_CheckRequest_Items_Lot_Category on sa.LotData_ID equals lot.ID
                               join ig in entities.ItemCategories on fgg.ItemCategory_ID equals ig.ID into iis
                               join sn in entities.Item_ShortName on sa.Item_ShortName_ID equals sn.ID
                               join it in entities.Items on sn.Item_ID equals it.ID
                               from itg in iis.DefaultIfEmpty()
                               join vi in entities.Ex_List on icr.ID equals vi.Ex_CheckRequest_ID into vis
                               from vi1 in vis.DefaultIfEmpty()
                               where sa.Sample_BarCode == barcode
                               select new LaboratoryResult_ReportDTO
                               {
                                   Outlet_Name = vi1.Outlet_Examination_Name,
                                   Station_Name = vi1.Station_Examination_Name,
                                   farmSampleId = sa.ID,
                                   Syl_ALkhatima_Number = sa.Syl_ALkhatima_Number,
                                   IsPrint = sa.IsPrint,
                                   labName = lang == "1" ? sa.AnalysisLabType.AnalysisLab.Name_Ar : sa.AnalysisLabType.AnalysisLab.Name_En,
                                   analysisType = lang == "1" ? sa.AnalysisLabType.AnalysisType.Name_Ar : sa.AnalysisLabType.AnalysisType.Name_En,
                                   WithdrawDate = sa.WithdrawDate,
                                   Sample_BarCode = sa.Sample_BarCode == null ? "####" : sa.Sample_BarCode,
                                   SampleSize = sa.SampleSize,
                                   SampleRatio = sa.SampleRatio,
                                   farmCode = fgg.FarmsData.FarmCode_14 == null ? "####" : fgg.FarmsData.FarmCode_14,
                                   farmName = lang == "1" ? icr.Outlet.Ar_Name : icr.Outlet.En_Name,
                                   //farmAddress = fd.Address_Ar,
                                   governate = lang == "1" ? gov.Ar_Name : gov.En_Name,
                                   // center = cen.Ar_Name,
                                   // village = vil.Ar_Name,
                                   itemName = lang == "1" ? it.Name_Ar : it.Name_En,
                                   itemCategoryName = lang == "1" ? itg.Name_Ar : itg.Name_En,
                                   ShortName = lang == "1" ? sn.ShortName_Ar : sn.ShortName_En,
                                   SubPart_Name = lang == "1" ? sn.SubPart.Name_Ar : sn.SubPart.Name_En,
                                   Item_Status_Name = lang == "1" ? sn.Item_Status.Ar_Name : sn.Item_Status.En_Name,
                                   Item_Purpose_Name = lang == "1" ? sn.Item_Purpose.Ar_Name : sn.Item_Purpose.En_Name,
                                   //////ExaminAddress = lang == "1" ? fd.Address_Ar : fd.Address_En,
                                   ////companyName = lang == "1" ? co.Name_Ar : co.Name_En,
                                   companyName = icd.ImporterType_Id == 6 ? (lang == "1" ? co.Name_Ar : co.Name_En)
                                                            : icd.ImporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                            : icd.ImporterType_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                            : "",
                                   Grower_Number = lot.Grower_Number
                               }).FirstOrDefault();

                    //if (String.IsNullOrEmpty(request.ExaminAddress))
                    //{
                    //    request.ExaminAddress = "####";

                    //}
                    if (request != null)
                    {
                        if (String.IsNullOrEmpty(request.analysisType))
                        {
                            request.analysisType = "####";
                        }
                        if (String.IsNullOrEmpty(request.companyName))
                        {
                            request.companyName = "####";
                        }
                    }
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetLaboratoryResult_ReportDataNew(string barcode, List<string> Device_Info)
        {
            try
            {

                var request = new List<LaboratoryResult_ReportDTO>();

                string lang = Device_Info[2];
                if (barcode.StartsWith("78"))//المزارع
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Farm_SampleData_Item
                               join frig in entities.Farm_Request_ItemCategories on sa.Farm_Request_ItemCategories_ID equals frig.ID into frig1
                               from frig in frig1.DefaultIfEmpty()
                               join fig in entities.Farm_ItemCategories on frig.Farm_ItemCategories_ID equals fig.ID into fig1
                               from fig in fig1.DefaultIfEmpty()
                               join ig in entities.ItemCategories on fig.ItemCategories_ID equals ig.ID into ig1
                               from ig in ig1.DefaultIfEmpty()
                               join fd in entities.FarmsDatas on sa.Farm_Committee.Farm_Request.FarmsData_ID equals fd.ID
                               join it in entities.Items on fd.Item_ID equals it.ID
                               join fc in entities.Farm_Company on fd.ID equals fc.Farm_ID
                               //join co in entities.Company_National on fc.Company_ID equals co.ID

                               join co in entities.Company_National on new { a = (long?)fc.ExporterType_Id, b = (long?)fc.Company_ID } equals new { a = (long?)6, b = (long?)co.ID } into cn1
                               from co in cn1.DefaultIfEmpty()
                               join po in entities.Public_Organization on new { a = (long?)fc.ExporterType_Id, b = (long?)fc.Company_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in entities.People on new { a = (long?)fc.ExporterType_Id, b = (long?)fc.Company_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join lt in entities.AnalysisLabTypes on sa.AnalysisLabType_ID equals lt.ID
                               join la in entities.AnalysisLabs on lt.AnalysisLabID equals la.ID
                               join at in entities.AnalysisTypes on lt.AnalysisTypeID equals at.ID
                               join vil in entities.Villages on fd.Village_ID equals vil.ID into iis
                               join cen in entities.Centers on fd.Center_Id equals cen.ID
                               join gov in entities.Governates on fd.Govern_ID equals gov.ID
                               from vil in iis.DefaultIfEmpty()
                               where sa.Sample_BarCode == barcode
                               select new LaboratoryResult_ReportDTO
                               {
                                   farmSampleId = sa.ID,
                                   //update model                                
                                   IsPrint = sa.IsPrint,
                                   labName = lang == "1" ? la.Name_Ar : la.Name_En,
                                   analysisType = lang == "1" ? at.Name_Ar : at.Name_En,
                                   WithdrawDate = sa.WithdrawDate,
                                   Sample_BarCode = sa.Sample_BarCode == null ? "####" : sa.Sample_BarCode,
                                   SampleSize = sa.SampleSize,
                                   SampleRatio = sa.SampleRatio,
                                   farmCode = fd.FarmCode_14 == null ? "####" : fd.FarmCode_14,
                                   farmName = fd.Name_Ar,
                                   farmAddress = fd.Address_Ar,
                                   governate = gov.Ar_Name,
                                   center = cen.Ar_Name,
                                   village = vil.Ar_Name,
                                   itemName = lang == "1" ? it.Name_Ar : it.Name_En,
                                   itemCategoryName = lang == "1" ? ig.Name_Ar : ig.Name_En,
                                   ExaminAddress = lang == "1" ? fd.Address_Ar : fd.Address_En,
                                   //companyName = lang == "1" ? co.Name_Ar : co.Name_En
                                   companyName = fc.ExporterType_Id == 6 ? (lang == "1" ? co.Name_Ar : co.Name_En)
                                                            : fc.ExporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                            : fc.ExporterType_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                            : "",
                                   company_Address_Ar = fc.ExporterType_Id == 6 ? (lang == "1" ? co.Address_Ar : co.Address_En)
                                                            : fc.ExporterType_Id == 7 ? (lang == "1" ? po.Address_Ar : po.Address_En)
                                                            : fc.ExporterType_Id == 8 ? (lang == "1" ? pr.Address : pr.Address_EN)
                                                            : "",
                               }).ToList();
                    //if (request != null)
                    //{
                    //    if (String.IsNullOrEmpty(request.ExaminAddress))
                    //    {
                    //        request.ExaminAddress = "####";

                    //    }
                    //    if (String.IsNullOrEmpty(request.analysisType))
                    //    {
                    //        request.analysisType = "####";
                    //    }
                    //    if (String.IsNullOrEmpty(request.companyName))
                    //    {
                    //        request.companyName = "####";
                    //    }
                    //}
                }
                else if (barcode.StartsWith("74")) // الوارد
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Im_CheckRequest_SampleData
                               join irc in entities.Im_RequestCommittee on sa.Im_RequestCommittee_ID equals irc.ID
                               join icr in entities.Im_CheckRequest on irc.ImCheckRequest_ID equals icr.ID
                               join icd in entities.Im_CheckRequest_Data on icr.ID equals icd.Im_CheckRequest_ID

                               //join co in entities.Company_National on icd.Importer_ID equals co.ID

                               join co in entities.Company_National on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)6, b = (long?)co.ID } into cn1
                               from co in cn1.DefaultIfEmpty()
                               join po in entities.Public_Organization on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in entities.People on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join gov in entities.Governates on icd.Governate_ID equals gov.ID
                               join fgg in entities.Im_CheckRequest_Items_Lot_Category on sa.LotData_ID equals fgg.ID
                               join ig in entities.ItemCategories on fgg.ItemCategory_ID equals ig.ID into iis
                               join sn in entities.Item_ShortName on sa.Item_ShortName_ID equals sn.ID
                               join it in entities.Items on sn.Item_ID equals it.ID
                               join itt in entities.Item_Type on sn.Item_Type_ID equals itt.Id
                               //  join alt in entities.AnalysisLabTypes on sa.AnalysisLabType_ID equals alt.ID
                               //  join at in entities.AnalysisTypes on alt.AnalysisTypeID equals at.ID
                               from itg in iis.DefaultIfEmpty()
                               where sa.Sample_BarCode == barcode
                               select new LaboratoryResult_ReportDTO
                               {
                                   farmSampleId = sa.ID,
                                   Syl_ALkhatima_Number = sa.Syl_ALkhatima_Number,
                                   IsPrint = sa.IsPrint,
                                   labName = lang == "1" ? sa.AnalysisLabType.AnalysisLab.Name_Ar : sa.AnalysisLabType.AnalysisLab.Name_En,
                                   analysisType_Ar = sa.AnalysisLabType.AnalysisType.Name_Ar,
                                   analysisType_En = sa.AnalysisLabType.AnalysisType.Name_En,
                                   analysisType_ID = sa.AnalysisLabType.AnalysisType.ID,
                                   WithdrawDate = sa.WithdrawDate,
                                   Sample_BarCode = sa.Sample_BarCode == null ? "####" : sa.Sample_BarCode,
                                   SampleSize = sa.SampleSize,
                                   SampleRatio = sa.SampleRatio,
                                   //farmCode = fd.FarmCode_14 == null ? "####" : fd.FarmCode_14,
                                   farmName = lang == "1" ? icr.Outlet.Ar_Name : icr.Outlet.En_Name,
                                   //farmAddress = fd.Address_Ar,
                                   governate = lang == "1" ? gov.Ar_Name : gov.En_Name,
                                   // center = cen.Ar_Name,
                                   // village = vil.Ar_Name,
                                   itemName = lang == "1" ? it.Name_Ar : it.Name_En,
                                   Scientific_Name = it.Scientific_Name,
                                   itemCategoryName = lang == "1" ? itg.Name_Ar : itg.Name_En,
                                   ShortName = lang == "1" ? sn.ShortName_Ar : sn.ShortName_En,
                                   SubPart_Name = lang == "1" ? sn.SubPart.Name_Ar : sn.SubPart.Name_En,
                                   Item_Status_Name = lang == "1" ? sn.Item_Status.Ar_Name : sn.Item_Status.En_Name,
                                   Item_Purpose_Name = lang == "1" ? sn.Item_Purpose.Ar_Name : sn.Item_Purpose.En_Name,
                                   //////ExaminAddress = lang == "1" ? fd.Address_Ar : fd.Address_En,
                                   ////companyName = lang == "1" ? co.Name_Ar : co.Name_En,
                                   companyName = icd.ImporterType_Id == 6 ? (lang == "1" ? co.Name_Ar : co.Name_En)
                                                            : icd.ImporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                            : icd.ImporterType_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                            : "",
                                   Grower_Number = fgg.Grower_Number,
                                   item_Type_Name = itt.Name_En,
                                   FarmCode = "",

                               }).ToList();

                    //if (String.IsNullOrEmpty(request.ExaminAddress))
                    //{
                    //    request.ExaminAddress = "####";

                    //}
                    //if (request != null)
                    //{
                    //    if (String.IsNullOrEmpty(request.analysisType))
                    //    {
                    //        request.analysisType = "####";
                    //    }
                    //    if (String.IsNullOrEmpty(request.companyName))
                    //    {
                    //        request.companyName = "####";
                    //    }
                    //}
                }

                else if (barcode.StartsWith("73")) // صادر
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Ex_CheckRequest_SampleData
                               join irc in entities.Ex_RequestCommittee on sa.Ex_RequestCommittee_ID equals irc.ID
                               join icr in entities.Ex_CheckRequest on irc.ExCheckRequest_ID equals icr.ID
                               join icd in entities.Ex_CheckRequest_Data on icr.ID equals icd.Ex_CheckRequest_ID
                               join exp in entities.Ex_CheckRequest_Places on icr.ID equals exp.Ex_CheckRequest_ID
                               //join co in entities.Company_National on icd.Importer_ID equals co.ID
                               // join expp in entities.Ex_CheckRequest_Port on icd.ID equals expp.Ex_CheckRequest_Data_ID
                               join co in entities.Company_National on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)6, b = (long?)co.ID } into cn1
                               from co in cn1.DefaultIfEmpty()
                               join po in entities.Public_Organization on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in entities.People on new { a = (long?)icd.ImporterType_Id, b = (long?)icd.Importer_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join gov in entities.Governates on icd.Governate_ID equals gov.ID into gov1
                               from gov in gov1.DefaultIfEmpty()

                               join fgg in entities.Ex_CheckRequest_Items on sa.Ex_Request_Item_Id equals fgg.ID
                               // join lot in entities.Ex_CheckRequest_Items_Lot_Category on sa.LotData_ID equals lot.ID
                               join ig in entities.ItemCategories on fgg.ItemCategory_ID equals ig.ID into iis
                               join sn in entities.Item_ShortName on sa.Item_ShortName_ID equals sn.ID
                               join it in entities.Items on sn.Item_ID equals it.ID
                               //       from itg in iis.DefaultIfEmpty()
                               join vi in entities.Ex_List on icr.ID equals vi.Ex_CheckRequest_ID into vis
                               from vi1 in vis.DefaultIfEmpty()
                               join itt in entities.Item_Type on sn.Item_Type_ID equals itt.Id
                               where sa.Sample_BarCode == barcode
                               select new LaboratoryResult_ReportDTO
                               {
                                   Outlet_Name = vi1.Outlet_Examination_Name,
                                   Station_Name = vi1.Station_Examination_Name,
                                   farmSampleId = sa.ID,
                                   Syl_ALkhatima_Number = sa.Syl_ALkhatima_Number,
                                   IsPrint = sa.IsPrint,
                                   labName = lang == "1" ? sa.AnalysisLabType.AnalysisLab.Name_Ar : sa.AnalysisLabType.AnalysisLab.Name_En,
                                   analysisType = lang == "1" ? sa.AnalysisLabType.AnalysisType.Name_Ar : sa.AnalysisLabType.AnalysisType.Name_En,
                                   WithdrawDate = sa.WithdrawDate,
                                   Sample_BarCode = sa.Sample_BarCode == null ? "####" : sa.Sample_BarCode,
                                   SampleSize = sa.SampleSize,
                                   SampleRatio = sa.SampleRatio,
                                   Count_Sample = sa.Count_Sample,
                                   farmCode = fgg.FarmsData.FarmCode_14 == null ? "####" : fgg.FarmsData.FarmCode_14,
                                   farmName = lang == "1" ? icr.Outlet.Ar_Name : icr.Outlet.En_Name,
                                   //farmAddress = fd.Address_Ar,
                                   governate = lang == "1" ? gov.Ar_Name : gov.En_Name,
                                   itemName = lang == "1" ? it.Name_Ar : it.Name_En,
                                   Scientific_Name = it.Scientific_Name,
                                   //     itemCategoryName = lang == "1" ? itg.Name_Ar : itg.Name_En,
                                   ShortName = lang == "1" ? sn.ShortName_Ar : sn.ShortName_En,
                                   SubPart_Name = lang == "1" ? sn.SubPart.Name_Ar : sn.SubPart.Name_En,
                                   Item_Status_Name = lang == "1" ? sn.Item_Status.Ar_Name : sn.Item_Status.En_Name,
                                   Item_Purpose_Name = lang == "1" ? sn.Item_Purpose.Ar_Name : sn.Item_Purpose.En_Name,
                                   //////ExaminAddress = lang == "1" ? fd.Address_Ar : fd.Address_En,
                                   ////companyName = lang == "1" ? co.Name_Ar : co.Name_En,
                                   companyName = icd.ImporterType_Id == 6 ? (lang == "1" ? co.Name_Ar : co.Name_En)
                                                            : icd.ImporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En)
                                                            : icd.ImporterType_Id == 8 ? (lang == "1" ? pr.Name : pr.Name_EN)
                                                            : "",
                                   //   Grower_Number = lot.Grower_Number,
                                   company_Address_Ar = icd.ImporterType_Id == 6 ? (lang == "1" ? co.Address_Ar : co.Address_En)
                                                            : icd.ImporterType_Id == 7 ? (lang == "1" ? po.Address_Ar : po.Address_En)
                                                            : icd.ImporterType_Id == 8 ? (lang == "1" ? pr.Address : pr.Address_EN)
                                                            : "",
                                   item_Type_Name = itt.Name_En,
                                   FarmCode = fgg.FarmsData.FarmCode_14,

                                   Delegation_Date = irc.Delegation_Date,
                                   Examination_Place = exp.Station_Examination_ID != null ? exp.Station.Ar_Name
                                                         // : exp.Outlet_Exmainiation_ID != null ? entities.Outlets.Where(p => p.ID == exp.Outlet_Exmainiation_ID).Select(p =>  p.Ar_Name).ToString()
                                                         : exp.Center_ID != null ? exp.Center.Outlet.Ar_Name
                                                         : "",

                                   Origin_Countery = entities.Countries.Where(p => p.ID == fgg.Country_ID).Select(p => p.Ar_Name).FirstOrDefault(),
                                   Destination_Countery = entities.Countries.Where(p => p.ID == icd.ExportCountry_Id).Select(p => p.Ar_Name).FirstOrDefault(),
                                   Station_Code = exp.Station_Examination_ID != null ? entities.Stations.Where(p => p.ID == exp.Station_Examination_ID).Select(p => p.StationCode).FirstOrDefault() : "",

                                   // Station_Code= exp.Station_Examination_ID != null ? exp.Station.StationCode:"",
                                   //Lot_Number=lot.Lot_Number,
                                   //  Size=lot.Size,
                                   //CommitteList = (from rc in entities.Ex_RequestCommittee

                                   //                     where rc.Ex_CheckRequest.ID == icr.ID && rc.User_Deletion_Id == null //&& rc.CommitteeType_ID == 1
                                   //                     select new CommitteList
                                   //                     {
                                   //                         // ResultTypes_Name = (lang == "1" ? cr.CommitteeResultType.Name_Ar : cr.CommitteeResultType.Name_En),
                                   //                         Delegation_Date = rc.Delegation_Date,
                                   //                         CreationDate = rc.User_Creation_Date,
                                   //                         StartTime = rc.StartTime,
                                   //                         EndTime = rc.EndTime,
                                   //                         IsApproved = rc.IsApproved,
                                   //                         IsFinishedAll = rc.IsFinishedAll,
                                   //                         Status = rc.Status,      
                                   //                         Committee_ID = rc.ID,

                                   //                     }).ToList(),
                                   ItemEx_CheckRequest = (from i in entities.Ex_CheckRequest_Items
                                                          where i.Ex_CheckRequest_ID == icr.ID
                                                          select new ItemEx_CheckRequestDTO
                                                          {
                                                              ItemNameAr = lang == "1" ? i.Item_ShortName.Item.Name_Ar : i.Item_ShortName.Item.Name_En,
                                                              ItemShortNameAr = lang == "1" ? i.Item_ShortName.ShortName_Ar : i.Item_ShortName.ShortName_En,
                                                              GrossWeight = i.GrossWeight,
                                                              NetWeight = i.Net_Weight,
                                                              Agriculture_Hand = i.Agriculture_Hand,
                                                              item_lots = (from lot in entities.Ex_CheckRequest_Items_Lot_Category
                                                                           join pt in entities.Package_Type on lot.Package_Type_ID equals pt.ID

                                                                           where lot.Ex_CheckRequest_Items_ID == i.ID
                                                                           group lot by new
                                                                           {
                                                                               itemId = lot.Ex_CheckRequest_Items_ID,

                                                                               pt.Ar_Name,

                                                                               //lot.Net_Weight
                                                                               // lot.Lot_Number
                                                                           } into grp
                                                                           select new Item_lots
                                                                           {
                                                                               packageType = grp.Key.Ar_Name,
                                                                               Net_Weight_Final = grp.Sum(a => a.Net_Weight),
                                                                               Gross_Weight_Final = grp.Sum(a => a.GrossWeight),
                                                                               Package_Count = grp.Sum(a => a.Package_Count),
                                                                               //  packageMaterialName= grp.Key.
                                                                               //Lot_Number=grp.Key.Lot_Number
                                                                               //LotCount=grp.Count(a=>a.itemId)

                                                                           }).ToList(),
                                                              item_lots2 = (from lot in entities.Ex_CheckRequest_Items_Lot_Category
                                                                            join pt in entities.Package_Type on lot.Package_Type_ID equals pt.ID
                                                                            join pm in entities.Package_Material on lot.Package_Material_ID equals pm.ID
                                                                            where lot.Ex_CheckRequest_Items_ID == i.ID

                                                                            select new Item_lots2
                                                                            {
                                                                                Size = lot.Size,
                                                                                Lot_Number = lot.Lot_Number,
                                                                                Package_Type_Name = pt.Ar_Name,
                                                                                packageMaterialName = pm.Ar_Name,

                                                                            }).ToList(),
                                                          }).ToList(),
                               }).ToList();

                    //if (String.IsNullOrEmpty(request.ExaminAddress))
                    //{
                    //    request.ExaminAddress = "####";

                    //}
                    ////if (request != null)
                    ////{
                    ////    if (String.IsNullOrEmpty(request.analysisType))
                    ////    {
                    ////        request.analysisType = "####";
                    ////    }
                    ////    if (String.IsNullOrEmpty(request.companyName))
                    ////    {
                    ////        request.companyName = "####";
                    ////    }
                    ////}
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> savePrintBarcode(long? id, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Farm_SampleData>().Findobject(id);
                Cmodel.IsPrint = true;
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, id);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}
