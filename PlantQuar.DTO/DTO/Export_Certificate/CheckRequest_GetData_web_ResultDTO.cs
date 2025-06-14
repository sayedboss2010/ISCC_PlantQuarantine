using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public partial class CheckRequest_GetData_web_ResultDTO
    {

        /// <summary>
        /// /////////////////////////
        /// </summary>
        /// 


        //public Nullable<byte> IsExport { get; set; }

        //public string CheckRequest_Number { get; set; }
        //public string Outlet_Name { get; set; }
        //public string General_Admin_Name { get; set; }
        //public string PortNational_Shippment_Name { get; set; }
        //public Nullable<long> Exporter_ID { get; set; }
        //public Nullable<int> ExporterType_Id { get; set; }
        //public string ExporterType_Name { get; set; }
        //public string Exporter_Name { get; set; }
        //public string Exporter_Address { get; set; }
        //public string Reciever_Name { get; set; }
        //public string ImportCompany { get; set; }
        //public string ImportCompany_Address { get; set; }
        //public string ImportCountry_Name { get; set; }
        //public Nullable<long> ImportCountry { get; set; }
        //public string port_arrive_Name { get; set; }
        //public string port_transite_Name { get; set; }
        //public string c_transite_country_Name { get; set; }
        //public Nullable<long> TransitCountry { get; set; }
        //public string Shipment_Mean_Name { get; set; }
        //public string Transport_Mean_Name { get; set; }

        //public Nullable<long> Request_ApprovedUnapprovedID { get; set; }
        //public Nullable<bool> Request_ApprovedUnapprovedType { get; set; }
        //public string Request_ApprovedUnapproved_StationName { get; set; }
        //public string Request_ApprovedUnapproved_Place_Ar_Name { get; set; }
        //public string Request_ApprovedUnapproved_Place_Address_Ar { get; set; }
        //public Nullable<System.DateTime> Check_Date { get; set; }
        //public string Item_Data { get; set; }
        //public Nullable<int> CertificateNo_printed { get; set; }
        //public string Committee_Employees { get; set; }
        //public string treatments { get; set; }
        //public Nullable<int> totItems { get; set; }
        //public Nullable<int> Certificate_IsEnglish { get; set; }
        //public string Outlet_Name_En { get; set; }
        //public string General_Admin_Name_En { get; set; }
        //public string PortNational_Shippment_Name_En { get; set; }
        //public string ExporterType_Name_En { get; set; }
        //public string Exporter_Name_En { get; set; }
        //public string Exporter_Address_En { get; set; }
        //public string Reciever_Name_En { get; set; }
        //public string ImportCompany_En { get; set; }
        //public string ImportCompany_Address_En { get; set; }
        //public string ImportCountry_Name_En { get; set; }
        //public string port_arrive_Name_En { get; set; }
        //public string port_transite_Name_En { get; set; }
        //public string c_transite_country_Name_En { get; set; }
        //public string Shipment_Mean_Name_En { get; set; }
        //public string Transport_Mean_Name_En { get; set; }
        //public string Ship_Name_En { get; set; }
        //public string Request_ApprovedUnapproved_StationName_En { get; set; }
        //public string Request_ApprovedUnapproved_Place_Ar_Name_En { get; set; }
        //public string Request_ApprovedUnapproved_Place_Address_En { get; set; }




        /////////////
        ///
        //////////////////
        public Nullable<int> totItems { get; set; }
        public string treatments { get; set; }
        public string treatmentsEN { get; set; }
        public string treatment_Material_NameEN { get; set; }
        public string AttachmentTableType { get; set; }

        public Nullable<System.DateTime> CertifactionDate { get; set; }


        public string Ship_Port_Name { get; set; }
        public string Shipping_PortNameEN { get; set; }
        public long CertificateID { get; set; }
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public string CertificateNumber { get; set; }
        public int CertificateNo { get; set; }
        public Nullable<byte> Transport_Mean_Id { get; set; }
        public string Ship_Name { get; set; }
        public Nullable<long> InternationalTransportation_ID { get; set; }
        public string ImportCompany_Name_EN { get; set; }
        public string ImporeterCompanyAddress_EN { get; set; }
        public string ImportCompany_Name_AR { get; set; }
        public string ImporeterCompanyAddress_AR { get; set; }
        public Nullable<int> Shipping_Port { get; set; }
        public string Shipping_PortName { get; set; }
        public short Importing_Country { get; set; }
        public byte Port_Type_Importing_Country { get; set; }
        public Nullable<int> Port_Access { get; set; }
        public Nullable<short> Transit_Country { get; set; }
        public string Transit_CountryName { get; set; }
        public Nullable<byte> Port_Type_Transit_Country { get; set; }
        public Nullable<int> Transit_Port { get; set; }
        public string ImportCountry_Name { get; set; }
        public string ImportCountry_Name_En { get; set; }
        public string PortTypeImportCountry_Name { get; set; }
        public string PortTypeImportCountry_Name_En { get; set; }
        public string ExporterType_Name { get; set; }
        public string ExporterType_Name_En { get; set; }
        public string Importer_Exporter_Name { get; set; }
        public string Importer_Exporter_Name_En { get; set; }
        public string RejectReason { get; set; }
        public Nullable<long> Importer_Exporter_Id { get; set; }
        public string Importer_Exporter_Address { get; set; }
        public string Importer_Exporter_Address_EN { get; set; }
        public string Reciever_Name { get; set; }
        public string ImportCompany { get; set; }
        public string ImporeterCompanyAddress { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<System.DateTime> Check_Date { get; set; }
        public Nullable<bool> IS_Additional_Declaretion { get; set; }
        public Nullable<bool> IS_Containers { get; set; }
        public Nullable<bool> IS_Lot { get; set; }
        public Nullable<bool> IS_Treatment { get; set; }
        public Nullable<bool> ISAccepted { get; set; }
        public Nullable<bool> ISEnglish { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public bool ISPrint { get; set; }
        public string Ship_Name1 { get; set; }
        public string PortArriveName { get; set; }
        public string PortArriveName_EN { get; set; }

        //noura
        public Nullable<System.DateTime> User_Delegation_Date { get; set; }
        public string treatment_Material_Name { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Notes_Ar { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }

        //CertificatesFiles
        public Nullable<long> Id { get; set; }
        public Nullable<long> PlantCertificatesRequestsID { get; set; }

        public string FilePath { get; set; }

        //CertificatesAddition
        //  public long ID { get; set; }
        public string Certificate_AddtionOriginal { get; set; }
        public string Certificate_AddtionOriginalUpdate { get; set; }
        public string Certificate_AddtionUpdateAdmin { get; set; }

        public Nullable<long> AdminID { get; set; }


    }
   public partial class CommitteeEmployeeName

    {
        public long Committee_ID { get; set; }
        public long Employee_Id { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }

    }

    //public class CheckRequest_GetData_web_ResultDTO
    //{
    //    public Nullable<byte> IsExport { get; set; }
    //    public Nullable<long> CheckRequest_Id { get; set; }
    //    public string CheckRequest_Number { get; set; }
    //    public string Outlet_Name { get; set; }
    //    public string General_Admin_Name { get; set; }
    //    public string PortNational_Shippment_Name { get; set; }
    //    public Nullable<long> Exporter_ID { get; set; }
    //    public Nullable<int> ExporterType_Id { get; set; }
    //    public string ExporterType_Name { get; set; }
    //    public string Exporter_Name { get; set; }
    //    public string Exporter_Address { get; set; }
    //    public string ImportCountry_Name { get; set; }
    //    public Nullable<long> ImportCountry { get; set; }
    //    public string port_arrive_Name { get; set; }
    //    public string port_transite_Name { get; set; }
    //    public string c_transite_country_Name { get; set; }
    //    public Nullable<long> TransitCountry { get; set; }
    //    public string Shipment_Mean_Name { get; set; }
    //    public string Transport_Mean_Name { get; set; }
    //    public string Ship_Name { get; set; }
    //    public Nullable<long> Request_ApprovedUnapprovedID { get; set; }
    //    public Nullable<bool> Request_ApprovedUnapprovedType { get; set; }
    //    public string Request_ApprovedUnapproved_StationName { get; set; }
    //    public string Request_ApprovedUnapproved_Place_Ar_Name { get; set; }
    //    public string Request_ApprovedUnapproved_Place_Address_Ar { get; set; }
    //    public Nullable<System.DateTime> Check_Date { get; set; }
    //    public string Item_Data { get; set; }
    //    public string Reciever_Name { get; set; }
    //    public string ImportCompany { get; set; }
    //    public string ImportCompany_Address { get; set; }
    //    public Nullable<int> CertificateNo { get; set; }
    //    public Nullable<int> CertificateNo_printed { get; set; }
    //    public string Committee_Employees { get; set; }
    //    public string treatments { get; set; }
    //    public Nullable<int> totItems { get; set; }
    //    public Nullable<int> Certificate_IsEnglish { get; set; }
    //    public  string patternSplit { get; set; }

    //}
}
