
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.Permissions
{
    public class ImRequestDetailsDTO
    {
        public long ID { get; set; }
        public Nullable<long> Im_CheckRequestData_ID { get; set; }
        public long Im_CheckRequest_ID { get; set; }
        public string ImCheckRequest_Number { get; set; }
        public string ImporterName { get; set; }
        public string OwnerName { get; set; }
        public string ImporterType { get; set; }
        public string ImporterAddress { get; set; }
        public string outletName { get; set; }
        //eslam


        public string OperationType_Name { get; set; }
        public Nullable<byte> CompActivityType_ID { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyActivityType { get; set; }
        public Nullable<decimal> Enrollment_Number { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }

        public long Importer_ID { get; set; }



        public string MessageOwner { get; set; }
        public string MessageOwnerNationalID { get; set; }

        public int ImporterType_Id { get; set; }
        public Nullable<byte> Shipment_Mean_Id { get; set; }
        public Nullable<byte> Transport_Mean_Id { get; set; }
        public Nullable<long> InternationalTransport_Id { get; set; }

        public string Ship_Name { get; set; }


        public string InternationalTransport { get; set; }

        public string Shipment_MeanName { get; set; }
        //public List<long> Im_Initiators { get; set; }
        //out egypt from data extra
        public List<Importers> ImportersCompanies { get; set; }
        public List<checkRequestShipping> checkRequestShipping { get; set; }
        public List<Attachments> Attachments { get; set; }
        //الاذون
        public List<List_Permissions> List_Permission { get; set; }
        public List<AttachmentsPermission> AttachmentPermission { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public List<Items_checkReq> itemsWithConstrains { get; set; }
        public List<CustomsMessage> CustomsMessages { get; set; }
        public List<CompanyActivityDTO> _CompanyActivitys { get; set; }
        public List<ContactTypeDTO> ImporterContacts { get; set; }
        public List<PersonContact> PersonContacts { get; set; }

        //الرسوم
        public List<Fees_Item> Fees_Item_All { get; set; }
        public List<Fees_Item_Shift> Fees_Item_Shift_All { get; set; }
        public Nullable<decimal> Shift_Item_All { get; set; }

        public Nullable<decimal> SUM_Shift_Fees_Item { get; set; }

        public Nullable<short> TransitCountryId { get; set; }
        public string ExportCountryName { get; set; }
        public string Transport_MeanName { get; set; }
        //noura 
        public List<List_Shift> List_Shift { get; set; }
        public List<List_Sample> List_Sample { get; set; }
        public List<List_Treatment> List_Treatment { get; set; }
        public List<List_Port> List_Port { get; set; }

        public string Is_Paid_Check { get; set; }

    }
    public class List_Port
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
    //eslam Permissions
    public class List_Permissions
    {
        public long ID { get; set; }
        public Nullable<decimal> ImPermission_Number { get; set; }

        //etc

    }
    public class AttachmentsPermission
    {
        public string AttachmentPath { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }
        public string Attachment_Name { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }
    }
    //noura
    public class List_Shift
    {
        public long? ID { get; set; }
        public string Shift_Name { get; set; }
        public Nullable<decimal> Shift_Count { get; set; }
        public Nullable<decimal> Shift_Amount { get; set; }

        public Nullable<decimal> Shift_Sum_All { get; set; }
        public string Is_Paid_Shift { get; set; }

    }

    public class List_Sample
    {
        public long? ID { get; set; }
        public string Laboratory_Name { get; set; }
        public string Sample_Name { get; set; }
        public Nullable<decimal> Sample_Amount { get; set; }
        public Nullable<decimal> Sample_Count { get; set; }
        public Nullable<decimal> Sample_Sum_All { get; set; }
        public string Sample_BarCode { get; set; }
        public string Is_Total { get; set; }
        public string Is_Paid_Sample { get; set; }
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

    }

    public class Fees_Item
    {
        public long? ID { get; set; }
        public string ItemShortName { get; set; }

        public string ItemName { get; set; }
        public Nullable<decimal> Fees { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }


        public Nullable<decimal> Shift_Item_All { get; set; }
        public Nullable<decimal> SUM_Shift_Fees_Item { get; set; }
        public string Is_Paid_Items { get; set; }

    }
    public class Fees_Item_Shift
    {

        public Nullable<decimal> total_Per_Shift { get; set; }
        public Nullable<decimal> Count_Per_Shift { get; set; }
        public Nullable<decimal> Amount_Per_Shift { get; set; }
    }

    public class PersonContact
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class checkRequestShipping
    {
        public long ID { get; set; }
        public Nullable<long> Im_CheckRequest_ID { get; set; }
        public Nullable<int> containers_ID { get; set; }
        public Nullable<int> containers_type_ID { get; set; }
        public string containerName { get; set; }
        public string containerType { get; set; }
        public string ShipholdNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<decimal> Total_Weight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }

        public List<Items_checkReq> Items_checkReq { get; set; }
    }
    public class Items_checkReq
    {//eslam
        public long ID { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<long> Im_Initiator_ID { get; set; }
        public Nullable<long> ImcheckReqItem_ID { get; set; }
        public Nullable<long> ImcheckReqshippedMethod_ID { get; set; }
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
        //eslam
        public constrains Itemconstrains { get; set; }
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
        // public List<Lots> ItemLots { get; set; }

        //الجزء النباتي
        public string SubPart_Name { get; set; }
        public List<categories_lots> ItemCategories_lots { get; set; }


        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
    }
    public class categories_lots
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
    public class CustomsMessage
    {


        public Nullable<long> Im_CheckRequest_ID { get; set; }
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

    }
}
