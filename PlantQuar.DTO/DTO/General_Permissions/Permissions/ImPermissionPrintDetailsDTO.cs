using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.General_Permissions.Permissions
{
    public class ImPermissionPrintDetailsDTO
    {
        //to get ports 10 wsol 11 3bor
        public Nullable<long> Im_RequestData_ID { get; set; }
        public long Im_PermissionRequest_ID { get; set; }
        public string ImPermission_Number { get; set; }
        public Nullable<bool> IS_Print_Ar { get; set; }
        public Nullable<bool> IS_Print_EN { get; set; }
        public string UserName { get; set; }
        public string UserNameEn { get; set; }
        public string outletName { get; set; }
        public string outletName_En { get; set; }
        public string delegateName { get; set; }
        public string delegateAddress { get; set; }
        public string personIdNo { get; set; }
        //in egypt
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public string ImporterName { get; set; }
        public string ImporterAddress { get; set; }
        public string ImporterNameEn { get; set; }
        public string ImporterAddressEn { get; set; }
        public string companyActivityType { get; set; }
        public string companyActivityTypeEn { get; set; }
        public string ImporterType { get; set; }
        public string ImporterTypeEn { get; set; }
        public long Importer_ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public int ImporterType_Id { get; set; }
        public string Enrollment_Name { get; set; }
        public Nullable<decimal> Enrollment_Number { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }
        // add by eslam
        public string Enrollment_type_AR { get; set; }
        public string Enrollment_type_EN { get; set; }
        // end eslam
        public Nullable<byte> Shipment_Mean_Id { get; set; }
        public Nullable<byte> Transport_Mean_Id { get; set; }
        public string Transport_MeanName { get; set; }
        public string Shipment_MeanName { get; set; }
        public string ArrivePortName { get; set; }
        public string TransportPortName { get; set; }
        public string Ship_Name { get; set; }
        public string ExportCountryName { get; set; }
        public string ExportCountryNameEn { get; set; }
        public string Transport_MeanNameEn { get; set; }
        public string Shipment_MeanNameEn { get; set; }
        public string ArrivePortNameEn { get; set; }
        public string TransportPortNameEn { get; set; }
        public string Ship_NameEn { get; set; }

        //public List<long> Im_Initiators { get; set; }
        //out egypt from data extra
        public List<Importers> ImportersCompanies { get; set; }
        public List<Itemss> Items { get; set; }
        public List<Attachments> Attachments { get; set; }
        // public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<bool> CanPrint { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<byte> Renewal_Status { get; set; }
        public Nullable<byte> Print_Count { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }

    }
    public class Importers
    {
        public string ImporterCompany { get; set; }
        public string ImporterCompanyAddress { get; set; }
        public string ImporterCompanyEn { get; set; }
        public string ImporterCompanyAddressEn { get; set; }
    }
    public class Itemss
    {
        public Nullable<long> Im_Initiator_ID { get; set; }
        public Nullable<long> ImPermissionItem_ID { get; set; }
        public string qualitiveGroupName { get; set; }
        public string qualitiveGroupNameEn { get; set; }
        public string Item_Permission_Number { get; set; }
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
        public List<categories> ItemCategories { get; set; }
        public constrains Itemconstrains { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public string packageType { get; set; }
        public string packageTypeEn { get; set; }
        public string packageMaterial { get; set; }
        public string packageMaterialEn { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<short> packageTypeID { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public string Order_Text { get; set; }
        public List<Lots> ItemLots { get; set; }
        //Eslam add
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<short> QualitativeGroup_Id { get; set; }

    }
    public class categories
    {
        public string packageMaterial { get; set; }
        public Nullable<short> packageMaterialID { get; set; }
        public Nullable<long> Im_PermissionItems_ID { get; set; }
        public Nullable<long> Im_PermissionItemsCategory_ID { get; set; }
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

    }
    public class Lots
    {
        public string Lot_Number { get; set; }

        public Nullable<int> Package_Count { get; set; }

        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<decimal> Gross_Weight { get; set; }
        public string Container_Number { get; set; }
        public string NavigationalFluid_Number { get; set; }
        public string ShipmentPolicy_Number { get; set; }
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
}
