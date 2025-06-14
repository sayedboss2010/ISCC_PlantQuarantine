using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Import.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.IM_Check_Requst_Report
{
    public class Check_Requst_Accepted_All_DTO
    {
        public Nullable<long> Im_CheckRequestData_ID { get; set; }
        public long Im_CheckRequest_ID { get; set; }
        public string ImCheckRequest_Number { get; set; }
        public string ImporterName { get; set; }
        public string OwnerName { get; set; }
        public string ImporterType { get; set; }
        public string ImporterAddress { get; set; }
        public string outletName { get; set; }
        public string General_Admin_Name { get; set; }
        
        //eslam

        public string CompActivityType__Name { get; set; }
        public string Enrollment_type_Name { get; set; }
        public string TaxesRecord { get; set; }
        public string CommertialRecord { get; set; }
        public Nullable<byte> CompActivityType_ID { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyActivityType { get; set; }
        public Nullable<decimal> Enrollment_Number { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }

        public long Importer_ID { get; set; }


        public string govNameAR { get; set; }
        public string govNameEN { get; set; }
        public string MessageOwner { get; set; }
        public string MessageOwnerNationalID { get; set; }
        public Nullable<short> TransitCountryId { get; set; }

        public string TransitPortType { get; set; }
        public string TransitPort { get; set; }
        public string TransitCountry { get; set; }
        public int ImporterType_Id { get; set; }
        public Nullable<byte> Shipment_Mean_Id { get; set; }
        public Nullable<byte> Transport_Mean_Id { get; set; }
        public Nullable<long> InternationalTransport_Id { get; set; }

        public string Ship_Name { get; set; }
        public string ArrivePortName { get; set; }
        public string TransportPortName { get; set; }
        public string ArrivePortType { get; set; }
        public string TransportPortType { get; set; }
        public string ExportCountryName { get; set; }
        public string Transport_MeanName { get; set; }
        public string InternationalTransport { get; set; }

        public string Shipment_MeanName { get; set; }
        //public List<long> Im_Initiators { get; set; }
        //out egypt from data extra
      
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        //customs message

        public string Customs_Certificate_Number { get; set; }
        public DateTime? Certification_Date { get; set; }
        public DateTime? Shipment_Date { get; set; }
        public DateTime? Arrival_Date { get; set; }
        public string Certificate_Number_Each_Product { get; set; }
        public string Manifest_Number { get; set; }
        public Nullable<long> Shipping_Agency_ID { get; set; }

        public string Shipping_Agency_Name { get; set; }
        public Nullable<byte> OperationType_ID { get; set; }

        public string OperationType_Name { get; set; }
        //noura

        //shipping
        public string containerName { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<int> containers_ID { get; set; }

        //fees
        public string ItemShortName { get; set; }

        public string ItemName { get; set; }
        public Nullable<decimal> Fees { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Shift_Item_All { get; set; }

        public Nullable<decimal> SUM_Shift_Fees_Item { get; set; }
        public List<Fees_Item> Fees_Item_All { get; set; }
        public List<List_Sample> List_Sample { get; set; }
        public List<List_Treatment> List_Treatment { get; set; }
        public List<Attachments> Attachments { get; set; }
        public List<categories_lots_Accepted> ItemCategories_lots { get; set; }
        public List<PermissionNumbers> PermissionNumbersList { get; set; }
 
        public List<Fees_Item_Shift> Fees_Item_Shift_All { get; set; }
        public List<Importers> ImportersCompanies { get; set; }
        public List<ImporterList> _ImporterList { get; set; }
        public List<Committee_Lot_Accept> List_Committee_Lot_Accept { get; set; }
        public List<CompanyActivityDTO> _CompanyActivitys { get; set; }
        public List<Committee_Result_Lot> Lot_Committee_Result { get; set; }
        public List<Committee_Sample_Lot> List_Lot_Committee_Sample { get; set; }
        public List<FinalResult> FinalResults { get; set; }
        public List<CheckRequest_Visa> CheckRequest_Visa { get; set; }

        public List<List_Port_all> List_Port_all { get; set; }

        public List<Fees_ALL> Fees_ALL { get; set; }
    }

    public class List_Port_all
    {
        public int ReqPortType_ID { get; set; }
        public string TransportPortType { get; set; }

        public string ExportCountryName { get; set; }

        public string ArrivePortName { get; set; }
        public string TransportPortName { get; set; }
        public string ArrivePortType { get; set; }



        public string TransitPortType { get; set; }
        public string TransitPort { get; set; }
        public string TransitCountry { get; set; }

        public string govNameAR { get; set; }
        public string govNameEN { get; set; }
    }
    public class Committee_Sample_Lot
    {
        public string itemName;

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


       

        public Nullable<bool> Is_Sample_Finch { get; set; }


        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

        public string Syl_ALkhatima_Number { get; set; }
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
      

        public Nullable<bool> Is_Result_Finch { get; set; }

        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }

     
    }
    public class Fees_ALL
    {
        public long ID { get; set; }
        public Nullable<decimal> Fees_CheckRequest { get; set; }
        
       // public List<List_Shift> List_Shift { get; set; }
       // public List<List_Sample> List_Sample { get; set; }
      //  public List<List_Treatment> List_Treatment { get; set; }
        public string Is_Paid_Check { get; set; }
    }
    public class Fees_Item
    {
        public long? ID { get; set; }
        public string ItemShortName { get; set; }
        public string Is_Paid_Items { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> Fees { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }


 
    }
    public class Fees_Item_Shift
    {

        public Nullable<decimal> total_Per_Shift { get; set; }
        public Nullable<decimal> Count_Per_Shift { get; set; }
        public Nullable<decimal> Amount_Per_Shift { get; set; }
    }
    //سحب
    public class List_Sample
    {
        public long ID { get; set; }
        public string Laboratory_Name { get; set; }
        public string Sample_Name { get; set; }
        public Nullable<decimal> Sample_Amount { get; set; }
        public Nullable<decimal> Sample_Count { get; set; }
        public Nullable<decimal> Sample_Sum_All { get; set; }
        public string Is_Paid_Sample { get; set; }
        public string Sample_BarCode { get; set; }
        public string Is_Total { get; set; }
    }

    //معالجة
    //public class List_Treatment11
    //{
    //    public long ID { get; set; }
    //    public long Im_RequestCommittee_ID { get; set; }
    //    public long Im_Request_Item_Id { get; set; }
    //    public Nullable<long> Im_Request_LotData_ID { get; set; }
    //    public Nullable<byte> TreatmentType_ID { get; set; }
    //    public Nullable<long> Company_ID { get; set; }
    //    public Nullable<long> Station_ID { get; set; }
    //    public string Station_Place { get; set; }
    //    public byte TreatmentMethod_ID { get; set; }
    //    public Nullable<byte> TreatmentMat_ID { get; set; }
    //    public Nullable<decimal> Size { get; set; }
    //    public Nullable<decimal> TreatmentMat_Amount { get; set; }
    //    public Nullable<decimal> TheDose { get; set; }
    //    public Nullable<int> Exposure_Minute { get; set; }
    //    public Nullable<int> Exposure_Hour { get; set; }
    //    public Nullable<int> Exposure_Day { get; set; }
    //    public Nullable<decimal> Temperature { get; set; }
    //    public string Note { get; set; }
    //    public Nullable<decimal> ThermalSealNumber { get; set; }
    //    public Nullable<long> User_Updation_Id { get; set; }
    //    public Nullable<System.DateTime> User_Updation_Date { get; set; }
    //    public Nullable<long> User_Deletion_Id { get; set; }
    //    public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    //    public long User_Creation_Id { get; set; }
    //    public System.DateTime User_Creation_Date { get; set; }
    //    public Nullable<long> Item_ShortName_ID { get; set; }
    //    public Nullable<bool> IS_Total_Android { get; set; }
    //    public Nullable<bool> IS_From_Android { get; set; }
    //    public Nullable<bool> IS_Total { get; set; }
    //    public string Procedures { get; set; }
    //    public string Is_Paid_Treatment { get; set; }

    //    public string TreatmentMethod_Name { get; set; }
    //    public string TreatmentType_Name { get; set; }
    //    public string TreatmentMat_Name { get; set; }
    //    public Nullable<decimal> Treatment_Amount { get; set; }

    //}
    public class PermissionNumbers
    {
        public string ImPermission_Number { get; set; }
    }
    public class ImporterList
    {
        public string ImportCompanyName { get; set; }
        public string ImporeterCompanyAddress { get; set; }
        public string Reciever_Name { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAddress { get; set; }


    }
    
    public class Attachments
    {
        public string AttachmentPath { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }

        public Nullable<DateTime> AttachmentStartDate { get; set; }

        public Nullable<DateTime> AttachmentEndDate { get; set; }
    }
  
    public class categories_lots_Accepted
    {
        public string ScientificName { get; set; }
        public long? Item_ShortName_ID { get; set; }
        public string ItemShortName { get; set; }
        public long Item_ID { get; set; }
        public string ItemName{ get; set; }

        public string InitiatorCountry { get; set; }
     
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



    }

    public class Committee_Lot_Accept
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

        public Nullable<bool> Is_Result_Finch { get; set; }

        //sayed
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<bool> IS_TotalAndroid { get; set; }
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
        public string Visa_Name { get; set; }
    }
}

