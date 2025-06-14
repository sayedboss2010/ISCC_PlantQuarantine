

using System;
using System.Collections.Generic;


namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class EXRequestDetailsDTO
    {
        #region Details
        public Nullable<long> Ex_CheckRequestData_ID { get; set; }

        public Nullable<long> Importer_ID { get; set; }
        public Nullable<int> ImporterType_Id { get; set; }


        public Nullable<long> Ex_CheckRequest_Station_Genshi_ID { get; set; }
        public Nullable<long> Ex_CheckRequest_Station_Examination_ID { get; set; }

        public Nullable<decimal> Fees_MartyrsFees { get; set; }



        public Nullable<byte> Transport_Mean_Id { get; set; }
        public Nullable<long> InternationalTransport_Id { get; set; }
        public Nullable<byte> Shipment_Mean_Id { get; set; }

        public Nullable<short> TransitCountryId { get; set; }
        public long ID { get; set; }

        public long Ex_CheckRequest_ID { get; set; }
        public string EXCheckRequest_Number { get; set; }
        public string outletName { get; set; }
        public string Outlet_Examination { get; set; }
        public string Outlet_Gashni { get; set; }
        //eslam
        public string OperationType_Name { get; set; }
        public Nullable<byte> CompActivityType_ID { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyActivityType { get; set; }
        public Nullable<decimal> Enrollment_Number { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }

        public int? Port_Geshni_Id { get; set; }
        public string govNameAR { get; set; }
        public string govNameEN { get; set; }

        public string TransitPortType { get; set; }
        public string RefuseNotes { get; set; }
        public string TransitPort { get; set; }
        public string TransitCountry { get; set; }

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

        public string GovernateName { get; set; }
        public string CenterName { get; set; }
        public string PortName { get; set; }
        public string Station_Examination { get; set; }
        public string Station_Examination_Code { get; set; }
        public string Station_Accreditation_Name { get; set; }
        public string Station_Activity_Type_Name { get; set; }
        public string Accreditation_Type { get; set; }
        public Nullable<DateTime> Station_Accreditation_StartDate { get; set; }
        public Nullable<DateTime> Station_Accreditation_EndDate { get; set; }
        public Nullable<bool> Station_Accreditation_IsActive { get; set; }
        public string Station_Genshi_ID { get; set; }
        //////////////////
        public string OwnerName { get; set; }
        public string ComponyAddress { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsPaid { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public Nullable<long> station_agg_id { get; set; }

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
        public List<CompanyActivitysDTO> _CompanyActivitys { get; set; }
        public List<ContactTypeDTO> ExportsContacts { get; set; }
        public List<Importers> ImportersCompanies { get; set; }
        //public List<Company_Data> Company_Data { get; set; }
        public List<checkRequestShipping> checkRequestShipping { get; set; }

        //public List<Attachments> Attachments { get; set; }

        #endregion

        public List<Items_checkReq> Items_checkReqs { get; set; }

        public List<PersonContact> PersonContacts { get; set; }

        public List<List_ShiftEngineers> List_ShiftEngineers { get; set; }
        //الرسوم
        public List<Fees_Item> Fees_Item_All { get; set; }
        public List<List_Fees_Martyrs> List_Fees_Martyrs { get; set; }
        public List<Fees_Item_Shift> Fees_Item_Shift_All { get; set; }
        public Nullable<decimal> Shift_Item_All { get; set; }

        public Nullable<decimal> SUM_Shift_Fees_Item { get; set; }

        public List<List_Treatment> List_Treatment { get; set; }
        public List<List_Shift> List_Shift { get; set; }
        public List<List_Sample> List_Sample { get; set; }
        //مـــرفــقـــــــــــــات
        public List<Attachments> Attachments { get; set; }
        public string Is_Paid_Check { get; set; }
        public List<List_Port> List_Port { get; set; }
        //TransportCountryList
        public List<TransportCountryList> TransportCountryList { get; set; }
        public List<TransiteCountryList> TransiteCountryList { get; set; }
        public List<Ex_CountryConstrainDTO> Ex_CountryConstrain { get; set; }

        //اسباب الرفض
        public List<CheckRequest_RefuseReasonDTO> List__CheckRequest_RefuseReasonDTO { get; set; }


        public long? Outlet_Examination_ID { get; set; }

        public long? Station_Examination_ID { get; set; }
    }
    public class CheckRequest_RefuseReasonDTO
    {
        public string Refuse_Reason_Name { get; set; }
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
    public class TransportCountryList
    {
        public int ReqPortType_ID { get; set; }
        public string TransportPortType { get; set; }

        public string TransportPortName { get; set; }

        public Nullable<short> TransportCountryID { get; set; }








    }
    public class TransiteCountryList
    {
        public int ReqPortType_ID { get; set; }


        public Nullable<short> TransiteCountryID { get; set; }




        public string TransitPortType { get; set; }
        public string TransitPortName { get; set; }
        public string TransitCountry { get; set; }


    }

    public class ContactTypeDTO
    {

        public string Name_Ar { get; set; }
        public string Name_En { get; set; }



        public string Value { get; set; }
    }
    public class Attachments
    {
        public string AttachmentPath { get; set; }
        public string Attachment_Number { get; set; }
        public string Attachment_TypeName { get; set; }
        public string Attachment_Name { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }
    }

    public class Importers
    {
        public string ImporterCompany { get; set; }
        public string ImporterCompanyAddress { get; set; }
        public string ImporterCompanyEn { get; set; }
        public string ImporterCompanyAddressEn { get; set; }
    }

    public class CompanyActivitysDTO
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
        public string Is_Paid_Condition { get; set; }



        public Nullable<bool> IsPaidEngineers { get; set; }


    }

    public class List_Fees_Martyrs
    {
        public long Fees_Transactions_DetilesID { get; set; }
        public Nullable<short> table_name_id { get; set; }

        public string Name_Ar { get; set; }
        public Nullable<decimal> Total_Amount { get; set; }
    }

    public class List_Shift
    {
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<byte> Is_Cancel { get; set; }
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
        public string IsPaidCommitteeCondition { get; set; }

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
        public string Is_Paid_SampleCondition { get; set; }
        public Nullable<bool> Is_Paid_Sample2 { get; set; }
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
        public string Is_Paid_TreatmentConition { get; set; }
        public Nullable<bool> Is_Paid_Treatment2 { get; set; }

        public string TreatmentMethod_Name { get; set; }
        public string TreatmentType_Name { get; set; }
        public string TreatmentMat_Name { get; set; }
        public Nullable<decimal> Treatment_Amount { get; set; }



    }
    public class PersonContact
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class checkRequestShipping
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

        public List<Items_checkReq> Items_checkReq { get; set; }
    }
    public class Items_checkReq
    {//eslam
        public string Item_Type_Name { get; set; }
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
        public string QualitativeGroup_Name_Ar { get; set; }
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
        public string Scientific_Name { get; set; }
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
        public constrains Itemconstrains { get; set; }

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

        // الصنف الزراعى
        public string ItemCategories_Name { get; set; }

        //المجموعةالصنفية
        public string ItemCategories_Group_Name { get; set; }
        public Nullable<long> ItemCategories_Group_ID { get; set; }
        public Nullable<bool> CurrentStatus { get; set; }
        public Nullable<bool> IsRegister { get; set; }
        public string FarmCode { get; set; }
        public string Farm_Name_Ar { get; set; }
        public string Farm_Govern_Name { get; set; }
        public string Farm_Center_Name { get; set; }
        public string Farm_Village_Name { get; set; }
        public string Farm_Agriculture_Hand { get; set; }
        public List<categories_lots> ItemCategories_lots { get; set; }

    }

    public class constrains
    {
        public List<string> texts_Ar { get; set; }
        public List<string> text_En { get; set; }

        public List<string> InSide_Certificate_Ar { get; set; }
        public List<string> InSide_Certificate_En { get; set; }
        public List<ports> itemConstrainPorts { get; set; }
        public string govNameAR { get; set; }
        public string govNameEN { get; set; }
        public string conCountry { get; set; }

    }

    public class ports
    {
        public string portType { get; set; }
        public string portName { get; set; }
        public string portTypeEn { get; set; }
        public string portNameEn { get; set; }
        public Nullable<short> portTypeId { get; set; }
        public Nullable<int> portId { get; set; }
    }
    public class categories_lots
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
        public string ItemcategoryName { get; set; }


    }
    public class CustomsMessage
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


    //constrainsEslamMaher
    public class Ex_CountryConstrainDTO
    {
        public long ID { get; set; }
        public Nullable<short> Country_Id { get; set; }
        public Nullable<short> Union_Id { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }
        public bool IsExport { get; set; }
        public bool IsAnalysis { get; set; }
        public bool IsTreatment { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<short> CountryID { get; set; }
        public List<int> ArrivalPortList { get; set; }

        public string ConstrainOwner_Name { get; set; }
        public string TransportCountry_Name { get; set; }
        public string CountryConstrain_TypeName { get; set; }
        public bool IsActive_Action { get; set; }


        public Nullable<short> Import_Country_ID { get; set; }
        public string Import_Country_Name { get; set; }
        public Nullable<short> TransportCountry_ID { get; set; }
        public long Item_ShortName_id { get; set; }
        public string Item_Short_Name { get; set; }
        public Nullable<bool> IsStationAccreditation { get; set; }
        public Nullable<bool> IsFarmAccreditation { get; set; }
        public Nullable<bool> IsCompanyAccreditation { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> ItemCategories_ID { get; set; }

        public List<Ex_CountryConstrain_TextDTO> CountryConstrain_TextDTO { set; get; }
        public List<Ex_CountryConstrain_AnalysisLabTypeDTO> AnalysisLabType { set; get; }
        public List<Ex_CountryConstrain_ArrivalPortDTO> ConstraintAirPortInternational { set; get; }
        public List<Ex_CountryConstrain_TreatmentDTO> Constraint_Treatment { set; get; }

    }

    public class Ex_CountryConstrain_TextDTO
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        // العرض
        public Nullable<long> EX_Constrain_Text_ID { get; set; }


        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }

        public string Ar_Name_Constrain_Type { get; set; }
        public string En_Name_Constrain_Type { get; set; }
    }

    public class Ex_CountryConstrain_AnalysisLabTypeDTO
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public bool IsAcive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public int AnalysisTypeID { get; set; }

        public long ExConstrainsLabsAndTypID { get; set; }

        public string TypeName_Ar { get; set; }
        public string TypeName_En { get; set; }

    }

    public class Ex_CountryConstrain_ArrivalPortDTO
    {
        public long Id { get; set; }
        public long Ex_CountryConstrain_Id { get; set; }
        public int Port_International_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<bool> IsActive { get; set; }

        // العرض
        public long ExConstrainsAirPortAndCountryID { get; set; }
        public string CountryName_Ar { get; set; }
        public string CountryLabName_En { get; set; }
        public string AirPortName_Ar { get; set; }
        public string AirPortName_En { get; set; }

    }


    public class Ex_CountryConstrain_TreatmentDTO
    {
        public long ID { get; set; }
        public Nullable<long> EX_Choose_Treatment_ID { get; set; }
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
        public Nullable<bool> EX_Choose_Treatment_IS_Optional { get; set; }
        public byte TreatmentMethods_ID { get; set; }

        // العرض
        public string TreatmentMethod_Ar_Name { get; set; }
        public string TreatmentMethod_En_Name { get; set; }

        public string TreatmentType_Ar_Name { get; set; }
        public string TreatmentType_En_Name { get; set; }

        public string TreatmentMainType_Ar_Name { get; set; }
        public string TreatmentMainType_En_Name { get; set; }
        public string Constrain_Treatment_Already_Choosen { get; set; }

    }
}
