using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class ExCommittee_Final_ResultDTO
    {
        public long Im_CheckRequest_ID { get; set; }
        public string ImCheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }

        public Nullable<System.DateTime> DateFinalResult { get; set; }
        public Nullable<long> Im_CheckRequest_FinalResult_ID { get; set; }
        public Nullable<short> Final_Result_EmployeeId { get; set; }
        public string Final_Result_Employee_Name { get; set; }

        public Nullable<bool> Final_Result_Status { get; set; }
        public string Final_Result_Name { get; set; }
        public Nullable<long> Count_Lots { get; set; }
        public List<CustomsMessage> CustomsMessages { get; set; }
        public List<Items_checkReq_New> itemsWithConstrains { get; set; }
        public List<FinalResult> FinalResults { get; set; }
        public List<CheckRequest_Visa> CheckRequest_Visa { get; set; }

    }
    public class Items_checkReq_New
    {


        //  public long ID { get; set; }
        public long? Item_ShortName_ID { get; set; }
        public string ItemShortNameAr { get; set; }
        public string ItemShortNameEn { get; set; }

        //public Nullable<long> ImcheckReqItem_ID { get; set; }
        //public Nullable<long> ImcheckReqshippedMethod_ID { get; set; }

        //public string Item_checkReq_Number { get; set; }
        //public Nullable<decimal> Fees { get; set; }
        public long Item_ID { get; set; }
        public string ItemName_Ar { get; set; }
        public string ItemName_En { get; set; }

        //   public Nullable<long> Im_Initiator_ID { get; set; }
        public string InitiatorCountry { get; set; }
        public string InitiatorCountryEn { get; set; }

        public string qualitiveGroupName { get; set; }
        public string qualitiveGroupNameEn { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }

        //public string ScientificNameAr { get; set; }
        //public string ScientificNameEn { get; set; }
        //public string Status { get; set; }
        //public string Purpose { get; set; }
        //public string StatusEn { get; set; }
        //public string PurposeEn { get; set; }
        //public string subPartName { get; set; }

        //public string subPartNameEn { get; set; }
        //public Nullable<int> SubPart_id { get; set; }
        //public string InitiatorCountry { get; set; }
        //public string InitiatorCountryEn { get; set; }

        //public constrains Itemconstrains { get; set; }
        //public Nullable<double> Size { get; set; }
        //public Nullable<int> Package_Count { get; set; }
        //public Nullable<decimal> Package_Weight { get; set; }
        //public Nullable<int> Units_Number { get; set; }
        //public string packageType { get; set; }
        //public string packageTypeEn { get; set; }
        //public string packageMaterial { get; set; }
        //public string packageMaterialEn { get; set; }
        //public Nullable<short> packageMaterialID { get; set; }
        //public Nullable<short> packageTypeID { get; set; }


        //public string Order_Text { get; set; }
        // public List<Lots> ItemLots { get; set; }

        public string subPartName { get; set; }
        public List<categories_lots_New> ItemCategories_lots { get; set; }

    }


    public class categories_lots_New
    {

        //eslam
        public long ID_Lot { get; set; }
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
        public Nullable<DateTime> Delegation_Date { get; set; }
        public string Container_Number { get; set; }
        public string NavigationalFluid_Number { get; set; }
        public string ShipmentPolicy_Number { get; set; }
        public string Number_Wooden_Package { get; set; }
        public string Grower_Number { get; set; }
        public string Waybill { get; set; }
        public int Check_Lot_Old_ID { get; set; }
        public string Check_Lot_Old_Name { get; set; }
        //بيانات النبات
        public long ID_IM_Item { get; set; }
        // بيانات الحاوية


        public Nullable<long> Im_CheckRequest_ID { get; set; }

        public string containerName { get; set; }
        public string containerType { get; set; }
        public string ShipholdNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<decimal> Total_Weight { get; set; }

        //اللوط اذا كانت النتيجة تعذر الفحص
        public Nullable<byte> CommitteeResultType_ID { get; set; }
        public long Im_CommitteeResult_ID { get; set; }
        //بيانات اللوط النهائى
        public Nullable<int> IS_Status_Lot_Result { get; set; }
        public string Note_Lot_Result { get; set; }

        //NOURA
        public string Order_TextLot { get; set; }
        public string RecordedOrNot { get; set; }
        public string ItemCategoryGroup { get; set; }
        //public string subPartName { get; set; }

        public List<Committee_Result_Lot> Lot_Committee_Result { get; set; }
        public List<Committee_Sample_Lot> list_Committee_Sample_Lot { get; set; }


        public List<List_AttachmentData_Im_CommitteeResult_Infection> AttachmentData_Im_CommitteeResult_Infection { get; set; }
    }
    public class Committee_Result_Lot
    {
        //Eslam
        public long Committee_Result_Lot_ID { get; set; }
        public string ResultTypes_Name { get; set; }

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
        public List<Committee_Result_Lot_Conferm> List_Committee_Result_Conferm { get; set; }

        public Nullable<bool> Is_Result_Finch { get; set; }

        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

        public List<List_Im_CommitteeResult_Infection> List_Im_CommitteeResult_Infection { get; set; }
    }

    public class Committee_Result_Lot_Conferm
    {
        public Nullable<long> ID { get; set; }
        public Nullable<long> Im_CommitteeResult_ID { get; set; }
        public System.DateTime? Date { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        // public Nullable<bool> Is_Result_Finch { get; set; }
    }

    public class Committee_Sample_Lot
    {
        public long ID { get; set; }

        public long? EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public Nullable<long> Committee_ID { get; set; }
        public Nullable<bool> ISAdmin { get; set; }

        public int AnalysisLabType_ID { get; set; }
        public string Analysis_Name { get; set; }
        public string Lab_Name { get; set; }


        public long Im_RequestCommittee_ID { get; set; }
        public long Im_Request_Item_Id { get; set; }
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


        public List<Committee_Sample_Conferm> List_Committee_Sample_Conferm { get; set; }

        public Nullable<bool> Is_Sample_Finch { get; set; }


        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

        public string Syl_ALkhatima_Number { get; set; }
    }

    public class Committee_Sample_Conferm
    {
        public long ID { get; set; }
        public long Im_CheckRequest_SampleData_ID { get; set; }
        public System.DateTime Date { get; set; }
        public long EmployeeId { get; set; }
        public string Employee_Name { get; set; }
        public string Notes { get; set; }
        public bool IsAccepted { get; set; }
    }

    public class FinalResult
    {
        public Nullable<System.DateTime> DateFinalResult { get; set; }
        public Nullable<long> Im_CheckRequest_FinalResult_ID { get; set; }
        public Nullable<short> Final_Result_EmployeeId { get; set; }
        public string Final_Result_Employee_Name { get; set; }

        public Nullable<bool> Final_Result_Status { get; set; }
        public string Final_Result_Name { get; set; }


    }

    public class CheckRequest_Visa
    {
        public Nullable<System.DateTime> Date_Visa { get; set; }
        public Nullable<long> Im_CheckRequest_Visa_ID { get; set; }
        public Nullable<short> Visa_Result_EmployeeId { get; set; }
        public string Visa_Result_Employee_Name { get; set; }
        public Nullable<bool> Visa_Result_Status { get; set; }
        public string Visa_Result_Name { get; set; }
    }
    public class Fees_ALL
    {
        public long ID { get; set; }
        public Nullable<decimal> Fees_CheckRequest { get; set; }
        public List<Fees_Item_ALL> Fees_Item_ALL { get; set; }
        public List<List_Shift> List_Shift { get; set; }
        public List<List_Sample> List_Sample { get; set; }
        public string Is_Paid_Check { get; set; }
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

    //noura



    //noura
    public class List_Im_CommitteeResult_Infection
    {
        public long ID { get; set; }

        public long Im_CommitteeResult_ID { get; set; }
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
    public class List_AttachmentData_Im_CommitteeResult_Infection
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

}