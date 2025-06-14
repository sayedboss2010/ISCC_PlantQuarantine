using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest_New
{
    public class EX_Committee_Final_Result_QuickDTO
    {
        public long EX_CheckRequest_ID { get; set; }
        public string EX_CheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }

        public string ArrivePortName { get; set; }
        public string TransportPortName { get; set; }
        public string ArrivePortType { get; set; }
        public string govNameAR { get; set; }
        public Nullable<System.DateTime> DateFinalResult { get; set; }
        public Nullable<long> EX_CheckRequest_FinalResult_ID { get; set; }
        public Nullable<short> Final_Result_EmployeeId { get; set; }
        public string Final_Result_Employee_Name { get; set; }

        public Nullable<bool> Final_Result_Status { get; set; }
        public string Final_Result_Name { get; set; }
        public Nullable<long> Count_Lots { get; set; }
        public List<EX_CustomsMessage_Quick> CustomsMessages { get; set; }
        public List<EX_Items_checkReq_New_Quick> itemsWithConstrains { get; set; }
        public List<EX_FinalResult_Quick> FinalResults { get; set; }
        public List<EX_Request_Visa_Quick> CheckRequest_Visa { get; set; }

    }
    public class EX_Items_checkReq_New_Quick
    {


        //  public long ID { get; set; }
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

        public string categoryName { get; set; }
        public string categoryNameEn { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }

        public string subPartName { get; set; }
        public List<EX_categories_lots_New_Quick> ItemCategories_lots { get; set; }

    }

    public class EX_CustomsMessage_Quick
    {


        public Nullable<long> EX_CheckRequest_ID { get; set; }
        public string Customs_Certificate_Number { get; set; }
        public DateTime? Certification_Date { get; set; }
        public DateTime? Shipment_Date { get; set; }
        public DateTime? Arrival_Date { get; set; }
        public string Certificate_Number_Each_Product { get; set; }
        public string Manifest_Number { get; set; }
        public Nullable<long> Shipping_Agency_ID { get; set; }

        public string Shipping_Agency_Name { get; set; }

    }
    public class EX_categories_lots_New_Quick
    {/// <summary>
     /// 
     /// </summary>
        public string categoryName { get; set; }
        public string categoryNameEn { get; set; }
        public string FarmCode_14 { get; set; }
        public string Governate_Name { get; set; }
        public string Center_Name { get; set; }
        public string Village_Name { get; set; }
        public string Agriculture_Hand { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }
        //eslam
        public long ID_Lot { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> EX_checkReqItems_ID { get; set; }
        public Nullable<long> EX_checkReqItemsCategory_ID { get; set; }

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

        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<long> EX_CheckRequest_ID { get; set; }

        public string containerName { get; set; }
        public string containerType { get; set; }
        public string ShipholdNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<decimal> Total_Weight { get; set; }

        //اللوط اذا كانت النتيجة تعذر الفحص
        public Nullable<byte> CommitteeResultType_ID { get; set; }
        public long EX_CommitteeResult_ID { get; set; }

        //NOURA
        public string Order_TextLot { get; set; }
        public string RecordedOrNot { get; set; }
        public string ItemCategoryGroup { get; set; }
        //public string subPartName { get; set; }



        public List<EX_Committee_Result_Lot_Quick> Lot_Committee_Result { get; set; }
        public List<EX_Committee_Gashne_Result_Lot_Quick> Lot_Committee_Gashne_Result { get; set; }
        public List<EX_Lot_Status_Result_Quick> Lot_Status_Result { get; set; }
        public List<EX_Committee_Sample_Lot_Quick> List_Lot_Committee_Sample { get; set; }
        public List<List_Treatment_Data_Quick> List_Treatment { get; set; }



        public List<EX_List_AttachmentData_EX_CommitteeResult_Infection_Quick> AttachmentData_EX_CommitteeResult_Infection { get; set; }
    }
    public class EX_Lot_Status_Result_Quick
    {
        //بيانات اللوط النهائى

        public long Lot_Status_Result_ID { get; set; }
        public Nullable<int> IS_Status_Lot_Result { get; set; }
        public string IS_Status_Name { get; set; }
        public string Note_Lot_Result { get; set; }
        public Nullable<bool> Is_Continue { get; set; }
        public Nullable<bool> IS_Status_Committee { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public string User_Name { get; set; }
    }


    public class EX_Committee_Result_Lot_Quick
    {
        //Eslam
        public long Committee_Result_Lot_ID { get; set; }
        public string ResultTypes_Name { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<long> Committee_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<byte> CommitteeResultType_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public double Weight { get; set; }

        public string Notes { get; set; }

        public Nullable<bool> IsAdminFinalResult { get; set; }

        public Nullable<bool> ISAdmin { get; set; }

        public double QuantitySize { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public List<EX_Committee_Result_Lot_Conferm_Quick> List_Committee_Result_Conferm { get; set; }

        public Nullable<bool> Is_Result_Finch { get; set; }

        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

        public List<EX_List_CommitteeResult_Infection_Quick> List_EX_CommitteeResult_Infection { get; set; }
    }
    public class EX_Committee_Gashne_Result_Lot_Quick
    {
        //Hadeer
        public long Committee_Result_Lot_ID { get; set; }
        public string ResultTypes_Name { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<long> Committee_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> Status { get; set; }

        public Nullable<System.DateTime> Date { get; set; }

        public double Weight { get; set; }

        public string Notes { get; set; }

        public Nullable<bool> IsAdminFinalResult { get; set; }

        public Nullable<bool> ISAdmin { get; set; }

        public double QuantitySize { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public List<EX_Committee_Result_Lot_Conferm_Quick> List_Committee_Result_Conferm { get; set; }

        public Nullable<bool> Is_Result_Finch { get; set; }

        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

        public List<EX_List_CommitteeResult_Infection_Quick> List_EX_CommitteeResult_Infection { get; set; }
    }

    public class EX_Committee_Result_Lot_Conferm_Quick
    {
        public Nullable<long> ID { get; set; }
        public Nullable<long> EX_CommitteeResult_ID { get; set; }
        public System.DateTime? Date { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public long Committee_ID { get; set; }
        public long Employee_Id { get; set; }
        public bool ISAdmin { get; set; }
        public int OperationType { get; set; }

        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public string Employee_Name_Conferm { get; set; }
        // public Nullable<bool> Is_Result_Finch { get; set; }
        public string ResultTypes_Name { get; set; }
    }

    public class EX_Committee_Sample_Lot_Quick
    {
        public string ResultTypes_Name { get; set; }
        public int AnalysisLabID { get; set; }


        public long ID { get; set; }
        public string ItemName { get; set; }
        public string ItemShortName { get; set; }
        public long? EmployeeId { get; set; }
        public string Employee_Name { get; set; }

        public Nullable<long> Committee_ID { get; set; }
        public Nullable<bool> ISAdmin { get; set; }

        public int AnalysisLabType_ID { get; set; }
        public string Analysis_Name { get; set; }
        public string Lab_Name { get; set; }

        public string IS_Total_Name { get; set; }

        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<System.DateTime> WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public Nullable<double> SampleSize { get; set; }
        public Nullable<double> SampleRatio { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string Notes { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }

        public string Imge_SampleLabResult { get; set; }
        public Nullable<bool> Admin_Confirmation { get; set; }
        public Nullable<short> Admin_User { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }
        public Nullable<bool> IsPrint { get; set; }

        public Nullable<byte> CommitteeType_ID { get; set; }
        public List<EX_Committee_Sample_Conferm_Quick> List_Committee_Sample_Conferm { get; set; }


        public Nullable<bool> Is_Sample_Finch { get; set; }


        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

        public string Syl_ALkhatima_Number { get; set; }

        //بيانات اللجنة
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class EX_Committee_Sample_Conferm_Quick
    {
        public Nullable<long> ID { get; set; }
        public Nullable<long> EX_CheckRequest_SampleData_ID { get; set; }
        public System.DateTime? Date { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsAccepted { get; set; }

        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public string Employee_Name_Conferm { get; set; }
    }


    public class EX_FinalResult_Quick
    {
        public Nullable<System.DateTime> DateFinalResult { get; set; }
        public Nullable<long> EX_CheckRequest_FinalResult_ID { get; set; }
        public Nullable<short> Final_Result_EmployeeId { get; set; }
        public string Final_Result_Employee_Name { get; set; }

        public Nullable<bool> Final_Result_Status { get; set; }
        public string Final_Result_Name { get; set; }


    }

    public class EX_Request_Visa_Quick
    {
        public Nullable<System.DateTime> Date_Visa { get; set; }
        public Nullable<long> EX_CheckRequest_Visa_ID { get; set; }
        public Nullable<short> Visa_Result_EmployeeId { get; set; }
        public string Visa_Result_Employee_Name { get; set; }
        public Nullable<bool> Visa_Result_Status { get; set; }
        public string Visa_Result_Name { get; set; }
    }
    public class EX_Fees_ALL_Quick
    {
        public long ID { get; set; }
        public Nullable<decimal> Fees_CheckRequest { get; set; }
        public List<EX_Fees_Item_ALL_Quick> Fees_Item_ALL { get; set; }
        public List<EX_List_Shift_Quick> EX_List_Shift { get; set; }
        public List<EX_List_Sample_Quick> EX_List_Sample { get; set; }
        public List<EX_List_Treatment_Quick> List_Treatment { get; set; }
        public string Is_Paid_Check { get; set; }
    }
    public class EX_Fees_Item_ALL_Quick
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

    //noura
    public class EX_List_Shift_Quick
    {
        public long ID { get; set; }
        public string Shift_Name { get; set; }
        public Nullable<decimal> Shift_Count { get; set; }
        public Nullable<decimal> Shift_Amount { get; set; }

        public Nullable<decimal> Shift_Sum_All { get; set; }
        public string Is_Paid_Shift { get; set; }

    }



    public class EX_List_Sample_Quick
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
    }

    public class EX_List_Treatment_Quick
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
        public Nullable<decimal> Amount { get; set; }

    }


    //noura


    public class EX_List_CommitteeResult_Infection_Quick
    {
        public long ID { get; set; }

        public long EX_CommitteeResult_ID { get; set; }
        public long Item_ID { get; set; }
        public string Item_Name { get; set; }
        // mohamed

        public string ItemName { get; set; }

        public string Scientific_Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string FamliyName { get; set; }

        public string PhylumSubphylumName { get; set; }

        public string LevelName { get; set; }

        public string KingdomName { get; set; }
        public string Order_Name { get; set; }
        public string Secondary_Classification_Name { get; set; }
        public string Main_Classification_Name { get; set; }
        public string Group_Name { get; set; }
        public string Is_Forbidden { get; set; }
        public string Is_Forbidden_Reason { get; set; }
        public string Is_Plant_Egypt { get; set; }
        public string Is_Known_Item { get; set; }


    }
    public class EX_List_AttachmentData_EX_CommitteeResult_Infection_Quick
    {
        public long Id { get; set; }
        public long RowId { get; set; }
        public short A_AttachmentTableNameId { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }
        public string AttachmentPath { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public byte[] AttachmentPath_Binary { get; set; }

        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

    }


    //treatment noura
    public class List_Treatment_Data_Quick
    {
        public long ID { get; set; }
        public Nullable<long> Committee_ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> EX_Request_LotData_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<byte> TreatmentType_ID { get; set; }
        public string TreatmentType_Name { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public string Employee_Name { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Name { get; set; }
        public string Station_Place { get; set; }
        public byte TreatmentMethod_ID { get; set; }
        public string TreatmentMethod_Name { get; set; }
        public Nullable<byte> TreatmentMat_ID { get; set; }
        public string TreatmentMat_Name { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public int IsLot { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }

        public string Company_Name { get; set; }

        public string Procedures { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<byte> Is_Cancel { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }


        public List<List_Treatment_Data_Confirm_Quick> List_Treatment_Confirm { get; set; }


    }
    public class List_Treatment_Data_Confirm_Quick
    {
        public Nullable<long> ID { get; set; }
        public Nullable<long> EX_Request_TreatmentData_ID { get; set; }
        public System.DateTime? Date { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsAccepted { get; set; }

        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public string Employee_Name_Conferm { get; set; }

    }

}
