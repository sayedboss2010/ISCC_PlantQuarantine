using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Committee
{
   
    public class Ex_Committee_DTO
    {
        public long EX_CheckRequest_ID { get; set; }
        public string EX_CheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }

        public Nullable<short> ExportCountry_Id { get; set; }
        public List<EX_Items_checkReq_New> itemsWithConstrains { get; set; }
    }
    public class EX_RequestCommitteeDTO
    {
        public long ID { get; set; }
        public Nullable<long> EX_CheckRequest_ID { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<byte> EX_CommitteeCheckLocation_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        public string message { get; set; }
        public List<EX_EmployeeDTO> com_emp { get; set; }

        public List<EX_CommitteeResultDTO> List_CommitteeResult { get; set; }
        public List<EX_CheckRequest_SampleDataDTO> List_SampleData { get; set; }
        public List<EX_RequestCommittee_ShiftDTO> List_Committee_Shift { get; set; }
        public List<EX_Checked_TreatmentMethodDTO> List_TreatmentMethod { get; set; }
        public List<Ex_Request_Fees_Eng_DTO> List_Ex_Request_Fees_Eng { get; set; }
        public List<EX_CommitteeResultDTO> List_EX_CommitteeResultDTO { get; set; }

    }

    public class EX_EmployeeDTO
    {
        public long Employee_Id { get; set; }

        public Nullable<decimal> Employee_no { get; set; }
        public string Employee_name { get; set; }
        public bool ISAdmin { get; set; }
    }
    public class EX_RequestCommittee_ShiftDTO
    {
        public long ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public byte ShiftTiming_ID { get; set; }
        public Nullable<byte> Count { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<decimal> Amount { get; set; }

        //money
        public Nullable<double> money { get; set; }
        public long Index { get; set; }

    }

    public class EX_CheckRequest_SampleDataDTO
    {
        public long ID { get; set; }
        public int AnalysisLabType_ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<System.DateTime> WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public Nullable<double> SampleSize { get; set; }
        public Nullable<double> SampleRatio { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string Notes_Ar { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }
        public string Notes_En { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<bool> Admin_Confirmation { get; set; }
        public Nullable<short> Admin_User { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        // Eslam Add Samples
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Fees_Actual { get; set; }
        public Nullable<int> Count_Sample { get; set; }
    }

    public class EX_CommitteeResultDTO
    {
        public long ID { get; set; }
        public long Committee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<byte> CommitteeResultType_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsAdminResult { get; set; }
        public string AdminFinalResult_Note { get; set; }
        public Nullable<double> QuantitySize { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        //  public string Text_Lot { get; set; }
        public long Item_ShortName_ID { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
    }

    public class EX_Checked_TreatmentMethodDTO
    {
        public long ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> EX_Request_LotData_ID { get; set; }
        public Nullable<byte> TreatmentType_ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Place { get; set; }
        public byte TreatmentMethod_ID { get; set; }
        public Nullable<byte> TreatmentMat_ID { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
        public Nullable<bool> IS_From_Android { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public string Procedures { get; set; }
        //add eslam
        public Nullable<decimal> NetWeightForTreatment { get; set; }
    }

    public class EX_CheckRequest_Committee_DTO
    {
        public long ID { get; set; }
        public Nullable<long> EX_Permission_ID { get; set; }
        public long Outlet_ID { get; set; }
        public string CheckRequest_Number { get; set; }
        public string ExportCompany { get; set; }
        public string ExportCompanyAddress { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<System.DateTime> IsAccepted_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
    }



    public class EX_Items_checkReq_New
    {
        public long? Item_ShortName_ID { get; set; }
        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }

        public long Item_ID { get; set; }
        public string ItemName_Ar { get; set; }
        public string ItemName_En { get; set; }

        public string InitiatorCountry { get; set; }
        public string InitiatorCountryEn { get; set; }

        public string qualitiveGroupName { get; set; }
        public string qualitiveGroupNameEn { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public string subPartName { get; set; }
        public List<EX_categories_lots_New> ItemCategories_lots { get; set; }

    }

    public class EX_categories_lots_New
    {

        public long ID_Lot { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> EX_checkReqItems_ID { get; set; }
        public Nullable<long> EX_checkReqItemsCategory_ID { get; set; }
        public string categoryName { get; set; }
        public string categoryNameEn { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public string packageType { get; set; }
        public Nullable<short> packageTypeID { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public string Order_Text { get; set; }
        public string RegisterReason { get; set; }
        public Nullable<bool> IsRegister { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }
        public string itemCatGroup { get; set; }
        //public List<Lots> categoryLots { get; set; }
        public string Reason_Entry { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public string Lot_Number { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string RejectReason { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }
        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }

        public string Container_Number { get; set; }
        public string NavigationalFluid_Number { get; set; }
        public string ShipmentPolicy_Number { get; set; }
        public string Number_Wooden_Package { get; set; }
        public string Grower_Number { get; set; }
        public string Waybill { get; set; }
        public int Check_Lot_Old_ID { get; set; }
        public string Check_Lot_Old_Name { get; set; }
        //بيانات النبات
        public long ID_EX_Item { get; set; }
        // بيانات الحاوية
        ////////////// ///Hadeer 24-1-2024  //////////////////
        public string FarmCode_14 { get; set; }
        public string Governate_Name { get; set; }
        public string Center_Name { get; set; }
        public string Village_Name { get; set; }
        public string Agriculture_Hand { get; set; }
        /// <summary>
        /// /////// End Hadeer
        /// </summary>
        public Nullable<long> EX_CheckRequest_ID { get; set; }

        public string containerName { get; set; }
        public string containerType { get; set; }
        public string ShipholdNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<decimal> Total_Weight { get; set; }
        public Nullable<DateTime> Delegation_Date { get; set; }
        public Nullable<byte> CommitteeResultType_ID { get; set; }

        public string Order_TextLot { get; set; }
        public string RecordedOrNot { get; set; }
        public string ItemCategoryGroup { get; set; }
        public List<EX_Items_checkReq> Items_checkReq { get; set; }
        public List<EX_Committee_Sample_Lot> list_Committee_Sample_Lot { get; set; }
        public List<EX_Committee_Treatment_Lot> list_Committee_Treatment_Lot { get; set; }

        public List<EX_Lot_Result_Status> list_Lot_Result_Status { get; set; }
        public long? EX_CommitteeResult_ID { get; set; }
        public Nullable<int> Lot_Result_Status { get; set; }


    }
    public class EX_Committee_Sample_Lot
    {

        public Nullable<long> LotData_ID { get; set; }
        public int AnalysisLabType_ID { get; set; }
        public string Analysis_Name { get; set; }
        public string Lab_Name { get; set; }


    }
    public class EX_Committee_Treatment_Lot
    {

        public long ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> EX_Request_LotData_ID { get; set; }
        public Nullable<byte> TreatmentType_ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Place { get; set; }
        public byte TreatmentMethod_ID { get; set; }
        public Nullable<byte> TreatmentMat_ID { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
        public Nullable<bool> IS_From_Android { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public string Procedures { get; set; }
        public string Is_Paid_Treatment { get; set; }

        public string TreatmentMethod_Name { get; set; }
        public string TreatmentType_Name { get; set; }
        public string TreatmentMat_Name { get; set; }
        public Nullable<decimal> Treatment_Amount { get; set; }


    }

    public class EX_Lot_Result_Status
    {

        public Nullable<long> LotData_ID { get; set; }
        public Nullable<byte> commite_No { get; set; }

        public string Status_Name { get; set; }
        public string Nots_Result_Status { get; set; }
        public Nullable<int> IS_Status { get; set; }
        public Nullable<int> IS_Status_Committee { get; set; }

        public Nullable<int> Is_Continue { get; set; }
    }

    public class EX_Items_checkReq
    {
        public long ID { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<long> EX_Initiator_ID { get; set; }
        public Nullable<long> EX_checkReqItem_ID { get; set; }
        public Nullable<long> EX_checkReqshippedMethod_ID { get; set; }
        public string qualitiveGroupName { get; set; }
        public string qualitiveGroupNameEn { get; set; }
        public string Item_checkReq_Number { get; set; }
        public Nullable<decimal> Fees { get; set; }
        public string ItemName { get; set; }
        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }

        public string ScientificNameAr { get; set; }
        public string ScientificNameEn { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string StatusEn { get; set; }
        public string PurposeEn { get; set; }
        public string subPartName { get; set; }

        public string subPartNameEn { get; set; }
        public Nullable<int> SubPart_id { get; set; }
        public string InitiatorCountry { get; set; }
        public string InitiatorCountryEn { get; set; }

        public EX_constrains Itemconstrains { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public string packageType { get; set; }
        public string packageTypeEn { get; set; }
        public string packageMaterial { get; set; }
        public string PackageMaterialEn { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<short> packageTypeID { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }

        public string Order_TextItem { get; set; }

        //الجزء النباتي
        public string SubPart_Name { get; set; }
        public List<EX_categories_lots> ItemCategories_lots { get; set; }

    }
    //public class EX_CommitteeResultDTO
    //{

    //    public long ID { get; set; }
    //    public long Committee_ID { get; set; }
    //    public long EX_Request_Item_Id { get; set; }
    //    public Nullable<long> LotData_ID { get; set; }
    //    public Nullable<long> EmployeeId { get; set; }
    //    public Nullable<byte> CommitteeResultType_ID { get; set; }
    //    public Nullable<System.DateTime> Date { get; set; }
    //    public Nullable<bool> IsAdminResult { get; set; }
    //    public string AdminFinalResult_Note { get; set; }
    //    public Nullable<double> QuantitySize { get; set; }
    //    public Nullable<double> Weight { get; set; }
    //    public string Notes { get; set; }
    //    public Nullable<bool> IS_Total { get; set; }
    //    //  public string Text_Lot { get; set; }
    //    public long Item_ShortName_ID { get; set; }
    //    public Nullable<short> User_Deletion_Id { get; set; }
    //    public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    //    public Nullable<short> User_Creation_Id { get; set; }
    //    public Nullable<System.DateTime> User_Creation_Date { get; set; }
    //    public Nullable<short> User_Updation_Id { get; set; }
    //    public Nullable<System.DateTime> User_Updation_Date { get; set; }


    //}
    //public class EX_CheckRequest_SampleDataDTO
    //{
    //    public long ID { get; set; }
    //    public int AnalysisLabType_ID { get; set; }
    //    public long EX_RequestCommittee_ID { get; set; }
    //    public Nullable<long> EX_Request_Item_Id { get; set; }
    //    public Nullable<long> LotData_ID { get; set; }
    //    public Nullable<System.DateTime> WithdrawDate { get; set; }
    //    public string Sample_BarCode { get; set; }
    //    public Nullable<double> SampleSize { get; set; }
    //    public Nullable<double> SampleRatio { get; set; }
    //    public Nullable<bool> IsAccepted { get; set; }
    //    public string Notes_Ar { get; set; }
    //    public string RejectReason_Ar { get; set; }
    //    public string RejectReason_En { get; set; }
    //    public string Notes_En { get; set; }
    //    public Nullable<System.DateTime> User_Updation_Date { get; set; }
    //    public Nullable<short> User_Deletion_Id { get; set; }
    //    public Nullable<short> User_Updation_Id { get; set; }
    //    public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    //    public short User_Creation_Id { get; set; }
    //    public System.DateTime User_Creation_Date { get; set; }
    //    public Nullable<bool> Admin_Confirmation { get; set; }
    //    public Nullable<short> Admin_User { get; set; }
    //    public Nullable<System.DateTime> Admin_Date { get; set; }
    //    public Nullable<bool> IsPrint { get; set; }
    //    public Nullable<bool> IS_Total { get; set; }
    //    public Nullable<long> Item_ShortName_ID { get; set; }
    //    // public string Text_Lot { get; set; }

    //}
    public class EX_constrains
    {
        public List<string> texts_Ar { get; set; }
        public List<string> text_En { get; set; }

        public List<string> InSide_Certificate_Ar { get; set; }
        public List<string> InSide_Certificate_En { get; set; }
        public List<EX_ports> itemConstrainPorts { get; set; }
        public string govNameAR { get; set; }
        public string govNameEN { get; set; }
        public string conCountry { get; set; }

    }
    public class EX_ports
    {
        public string portType { get; set; }
        public string portName { get; set; }
        public string portTypeEn { get; set; }
        public string portNameEn { get; set; }
        public Nullable<short> portTypeId { get; set; }
        public Nullable<int> portId { get; set; }
    }

    public class EX_categories_lots
    {
        public long ID { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> Im_checkReqItems_ID { get; set; }
        public Nullable<long> Im_checkReqItemsCategory_ID { get; set; }
        public string categoryName { get; set; }
        public string categoryNameEn { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public string packageType { get; set; }
        public Nullable<short> packageTypeID { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public string Order_TextLot { get; set; }
        public string RegisterReason { get; set; }
        //  public Nullable<bool> IsRegister { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }
        public string ItemCategory { get; set; }
        public string ItemCategoryGroup { get; set; }
        public string RecordedOrNot { get; set; }
        public string itemCatGroup { get; set; }
        //public List<Lots> categoryLots { get; set; }
        public string Reason_Entry { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Net_Weight_Final { get; set; }
        public string Lot_Number { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string RejectReason { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }
        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }

        public string Container_Number { get; set; }
        public string NavigationalFluid_Number { get; set; }
        public string ShipmentPolicy_Number { get; set; }
        public string Number_Wooden_Package { get; set; }
        public string Grower_Number { get; set; }
        public string Waybill { get; set; }


    }
    public class TreatmentMethodsDTO
    {

        public byte TreatmentMethod_ID { get; set; }
        public byte TreatmentType_ID { get; set; }
        public string Ar_Name { get; set; }

    }

    public class TreatmentTypesDTO
    {

        public byte TreatmentMainTypeId { get; set; }
        public byte TreatmentType_ID { get; set; }
        public string Ar_Name { get; set; }

    }
    public class TreatmentMainTypesDTO
    {

        public byte TreatmentMainType { get; set; }
        public string Ar_Name { get; set; }

    }
    public class AllTreatmentDataForShortnameIdDTO
    {

        public List<TreatmentMethodsDTO> TreatmentMethodsDTO { get; set; }
        public List<TreatmentTypesDTO> TreatmentTypesDTO { get; set; }
        public List<TreatmentMainTypesDTO> TreatmentMainType { get; set; }

    }



    public class Fees_ALL
    {
        public long ID { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<decimal> Fees_CheckRequest { get; set; }
        public string Is_Paid_Check { get; set; }
        public List<Fees_Item_ALL> Fees_Item_ALL { get; set; }
        public List<List_Shift> List_Shift { get; set; }
        public List<List_Sample> List_Sample { get; set; }
        public List<List_Treatment> List_Treatment { get; set; }
        public List<List_ShiftEngineers> List_ShiftEngineers { get; set; }
        public List<List_Fees_Martyrs> List_Fees_Martyrs { get; set; }

    }
    public class Fees_Item_ALL
    {
        public long? ID { get; set; }
        public string ItemShortName { get; set; }

        public string ItemName { get; set; }
        public Nullable<decimal> Fees { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }

        public Nullable<decimal> Shift_Item_All { get; set; }
        public Nullable<decimal> SUM_Shift_Fees_Item { get; set; }
        public long Fees_Action_ID { get; set; }
        public string Is_Paid_Items { get; set; }





    }
    public class List_ShiftEngineers
    {
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }

        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long? ID { get; set; }

        public int? Num_Eng { get; set; }
        public string Shift_Name { get; set; }
        public string CommitteTypeName { get; set; }


        public Nullable<decimal> Shift_Count { get; set; }
        public Nullable<decimal> Shift_Amount { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<decimal> Shift_Sum_All { get; set; }
        public string Is_Paid_Shift { get; set; }



        public Nullable<bool> IsPaidEngineers { get; set; }


    }
    public class List_Shift
    {
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long? ID { get; set; }
        public string Shift_Name { get; set; }
        public string CommitteTypeName { get; set; }
        public Nullable<decimal> Shift_Count { get; set; }
        public Nullable<decimal> Shift_Amount { get; set; }

        public Nullable<decimal> Shift_Sum_All { get; set; }
        public string Is_Paid_Shift { get; set; }
        public Nullable<bool> IsPaidCommittee { get; set; }
    }
    public class List_Fees_Martyrs
    {
        public long Fees_Transactions_DetilesID { get; set; }
        public Nullable<short> table_name_id { get; set; }

        public string Name_Ar { get; set; }
        public Nullable<decimal> Total_Amount { get; set; }
    }
    public class List_Sample
    {
        public long ID { get; set; }
        public string Laboratory_Name { get; set; }
        public string Sample_Name { get; set; }
        public Nullable<decimal> Sample_Amount { get; set; }
        public Nullable<decimal> Sample_Count { get; set; }
        public Nullable<decimal> Sample_Sum_All { get; set; }
        public string Is_Paid_Sample { get; set; }
        //public Nullable<bool> Is_Paid_Sample { get; set; }
        public string Sample_BarCode { get; set; }
        public string Is_Total { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> Is_Paid_Sample2 { get; set; }
    }

    public class List_Treatment
    {
        public long ID { get; set; }
        public long Im_RequestCommittee_ID { get; set; }
        public long Im_Request_Item_Id { get; set; }
        public Nullable<long> Im_Request_LotData_ID { get; set; }
        public Nullable<byte> TreatmentType_ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Place { get; set; }
        public byte TreatmentMethod_ID { get; set; }
        public Nullable<byte> TreatmentMat_ID { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
        public Nullable<bool> IS_From_Android { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public string Procedures { get; set; }
        public string Is_Paid_Treatment { get; set; }

        public string TreatmentMethod_Name { get; set; }
        public string TreatmentType_Name { get; set; }
        public string TreatmentMat_Name { get; set; }
        public Nullable<decimal> Treatment_Amount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> Is_Paid_Treatment2 { get; set; }
    }


    public class Ex_Request_Fees_Eng_DTO
    {
        public long ID { get; set; }
        public long Ex_RequestCommittee_ID { get; set; }
        public int Ex_Fees_Type_ID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<int> Num_Eng { get; set; }

    }
}
