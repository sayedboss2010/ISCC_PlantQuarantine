
//using PlantQuar.DTO.DTO.Export_CheckRequest;
using System;
using System.Collections.Generic;


namespace PlantQuar.DTO.DTO.Pallet_Export_CheckRequest
{
    public class Pallet_EXRequestDetailsDTO
    {
        #region Details
        public long ID { get; set; }
        public Nullable<long> Ex_CheckRequestData_ID { get; set; }
        public long Ex_CheckRequest_ID { get; set; }
        public string EXCheckRequest_Number { get; set; }
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
        public string govNameAR { get; set; }
        public string govNameEN { get; set; }
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
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }
        public string Owner_Name { get; set; }
        public string Shipment_MeanName { get; set; }
        public string MessageOwner { get; set; }
        public string MessageOwnerNationalID { get; set; }
        public string ImporterAddress { get; set; }
        public string Ex_CheckRequest_Examination_location { get; set; }
        public Nullable<long> Ex_CheckRequest_Station_Genshi_ID { get; set; }
        public Nullable<long> Ex_CheckRequest_Station_Examination_ID { get; set; }
        public string GovernateName { get; set; }
        public string CenterName { get; set; }
        public string PortName { get; set; }
        public string Station_Examination { get; set; }
        public string Station_Genshi_ID { get; set; }
        //////////////////
        public string OwnerName { get; set; }
        public string ComponyAddress { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }

        ////Add export Country And transite Country
        //public string ExportCountryName { get; set; }
        //public string TransportPortType { get; set; }
        //public string TransportPortName { get; set; }
        //public string TransitCountry { get; set; }
        //public string TransitPortType { get; set; }
        //public string TransitPort { get; set; }



        /// <summary>
        /// /////////////
        /// 
        /// </summary>
        public List<CompanyActivitysDTO_Pallets> _CompanyActivitys { get; set; }
        public List<ContactTypeDTO_Pallets> ExportsContacts { get; set; }
        public List<Importers_Pallets> ImportersCompanies { get; set; }
        //public List<Company_Data> Company_Data { get; set; }
        public List<checkRequestShipping_Pallets> checkRequestShipping { get; set; }

        //public List<Attachments> Attachments { get; set; }

        #endregion
        public List<Items_checkReq_Pallets> Items_checkReq_Pallets { get; set; }



        public List<PersonContact_Pallets> PersonContacts { get; set; }


        //الرسوم
        public List<Fees_Item_Pallets> Fees_Item_All_Pallets { get; set; }
        public List<Fees_Item_Shift_Pallets> Fees_Item_Shift_All_Pallets { get; set; }
        public Nullable<decimal> Shift_Item_All { get; set; }

        public Nullable<decimal> SUM_Shift_Fees_Item { get; set; }

        public List<List_Treatment_Pallets> List_Treatment_Pallets { get; set; }
        public List<List_Shift_Pallets> List_Shift_Pallets { get; set; }
        public List<List_Sample_Pallets> List_Sample_Pallets { get; set; }
        //مـــرفــقـــــــــــــات
        public List<Attachments_Pallets> Attachments_Pallets { get; set; }
        public string Is_Paid_Check { get; set; }
        public List<List_Port_Pallets> List_Port { get; set; }
        //TransportCountryList
        public List<TransportCountryList_Pallets> TransportCountryList { get; set; }
        public List<TransiteCountryList_Pallets> TransiteCountryList { get; set; }
       // public Ex_CountryConstrainDTO Ex_CountryConstrain { get; set; }

    }

    public class List_Port_Pallets
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
    public class TransportCountryList_Pallets
    {
        public int ReqPortType_ID { get; set; }
        public string TransportPortType { get; set; }

        public string TransportPortName { get; set; }

        public Nullable<short> TransportCountryID { get; set; }








    }
    public class TransiteCountryList_Pallets
    {
        public int ReqPortType_ID { get; set; }


        public Nullable<short> TransiteCountryID { get; set; }




        public string TransitPortType { get; set; }
        public string TransitPortName { get; set; }
        public string TransitCountry { get; set; }


    }

    public class ContactTypeDTO_Pallets
    {

        public string Name_Ar { get; set; }
        public string Name_En { get; set; }



        public string Value { get; set; }
    }
    public class Attachments_Pallets
    {
        public string AttachmentPath { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }
        public string Attachment_Name { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }
    }

    public class Importers_Pallets
    {
        public string ImporterCompany { get; set; }
        public string ImporterCompanyAddress { get; set; }
        public string ImporterCompanyEn { get; set; }
        public string ImporterCompanyAddressEn { get; set; }
    }

    public class CompanyActivitysDTO_Pallets
    {
        public long ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public int MainActivityType { get; set; }
        public Nullable<byte> CompActivityType_ID { get; set; }

        public string CompActivityType__Name { get; set; }
        public string Enrollment_type_Name { get; set; }
        public string Enrollment_Name { get; set; }
        public Nullable<decimal> Enrollment_Number { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        //Eslam
        public Nullable<byte> Enrollment_type_ID { get; set; }
        //Eslam
    }

    public class List_Shift_Pallets
    {
        public long? ID { get; set; }
        public string Shift_Name { get; set; }
        public Nullable<decimal> Shift_Count { get; set; }
        public Nullable<decimal> Shift_Amount { get; set; }

        public Nullable<decimal> Shift_Sum_All { get; set; }
        public string Is_Paid_Shift { get; set; }

    }

    public class List_Sample_Pallets
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

    public class Fees_Item_Pallets
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
    public class Fees_Item_Shift_Pallets
    {

        public Nullable<decimal> total_Per_Shift { get; set; }
        public Nullable<decimal> Count_Per_Shift { get; set; }
        public Nullable<decimal> Amount_Per_Shift { get; set; }
    }
    public class List_Treatment_Pallets
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
    public class PersonContact_Pallets
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class checkRequestShipping_Pallets
    {
        public long ID { get; set; }
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public Nullable<int> containers_ID { get; set; }
        public Nullable<int> containers_type_ID { get; set; }
        public string containerName { get; set; }
        public string containerType { get; set; }
        public string ShipholdNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }
        public Nullable<decimal> Total_Weight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }

       // public List<Items_checkReq_Pallets> Pallet_Items_checkReq { get; set; }
    }
    public class Items_checkReq_Pallets
    {//eslam

        public long? Ex_Items_checkReqID { get; set; }
        public long? ImcheckReqItem_ID { get; set; }
        public Nullable<int> SubPart_id { get; set; }
        public Nullable<short> Package_Material_ID { get; set; }
        public Nullable<short> Package_Type_ID { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public Nullable<bool> Is_LotDivision { get; set; }
        public Nullable<double> Size { get; set; }
        public string Order_Text { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public bool IsAccepted { get; set; }
        public Nullable<System.DateTime> Accept_Date { get; set; }
        public Nullable<System.DateTime> Accept_User_Updation_Date { get; set; }
        public Nullable<short> Accept_User_Creation_Id { get; set; }
        public Nullable<System.DateTime> Accept_User_Creation_Date { get; set; }
        public Nullable<short> Accept_User_Updation_Id { get; set; }
        public Nullable<short> Country_ID { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Fees { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> FarmsData_ID { get; set; }
        public string FarmsData { get; set; }
        public Nullable<long> ItemCategory_ID { get; set; }
        public string ItemCategoryName { get; set; }
        public Nullable<short> Governate_ID { get; set; }
        public Nullable<short> Center_ID { get; set; }
        public Nullable<short> Village_ID { get; set; }
        public string Agriculture_Hand { get; set; }

        public string Item_checkReq_Number { get; set; }

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

        public string InitiatorCountry { get; set; }
        public string InitiatorCountryEn { get; set; }
        //eslam
       // public constrains Itemconstrains { get; set; }

        public string packageType { get; set; }
        public string packageTypeEn { get; set; }
        public string packageMaterial { get; set; }
        public string PackageMaterialEn { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<short> packageTypeID { get; set; }


        public string Order_TextItem { get; set; }
        // public List<Lots> ItemLots { get; set; }

        //الجزء النباتي
        public string SubPart_Name { get; set; }
        public List<categories_lots_Pallets> Pallets_ItemCategories_lots { get; set; }

    }



    public class ports_Pallets
    {
        public string portType { get; set; }
        public string portName { get; set; }
        public string portTypeEn { get; set; }
        public string portNameEn { get; set; }
        public Nullable<short> portTypeId { get; set; }
        public Nullable<int> portId { get; set; }
    }
    public class categories_lots_Pallets
    {
        public long ID { get; set; }
        public string packageMaterialName { get; set; }
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> Ex_CheckRequest_Items_ID { get; set; }
        public Nullable<long> Ex_checkReqItemsCategory_ID { get; set; }
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
    public class CustomsMessage_Pallets
    {


        public Nullable<long> Ex_CheckRequest_ID { get; set; }
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


    //EslamMaher



    public class Ex_CountryConstrain_TreatmentDTO_Pallets
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public bool IsAcive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<bool> IS_Optional { get; set; }
        public byte TreatmentMethods_ID { get; set; }

        // العرض
        public string TreatmentMethod_Ar_Name { get; set; }
        public string TreatmentMethod_En_Name { get; set; }

        public string TreatmentType_Ar_Name { get; set; }
        public string TreatmentType_En_Name { get; set; }

        public string TreatmentMainType_Ar_Name { get; set; }
        public string TreatmentMainType_En_Name { get; set; }
    }
}

