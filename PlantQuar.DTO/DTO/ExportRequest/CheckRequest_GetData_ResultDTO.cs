using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public partial class CheckRequest_GetData_ResultDTO
    {
        public long CheckRequest_Id { get; set; }
        public byte IsExport { get; set; }
        public string CheckRequest_Number { get; set; }
        public string Outlet_Name { get; set; }
        public string Govern_Name { get; set; }
        public string PortNational_Shippment_Name { get; set; }
        public Nullable<long> Exporter_ID { get; set; }
        public Nullable<int> ExporterType_Id { get; set; }
        public string ExporterType_Name { get; set; }
        public Nullable<int> Importer_Exporter_Id { get; set; }
        public string Importer_Exporter_Name { get; set; }
        public string Reciever_Name { get; set; }
        public string ImportCompany { get; set; }
        public string ImportCountry_Name { get; set; }
        public string port_arrive_Name { get; set; }
        public string port_transite_Name { get; set; }
        public string c_transite_country_Name { get; set; }
        public string Shipment_Mean_Name { get; set; }
        public string Transport_Mean_Name { get; set; }
        public string Ship_Name { get; set; }
        public Nullable<long> Request_ApprovedUnapprovedID { get; set; }
        public Nullable<bool> Request_ApprovedUnapprovedType { get; set; }
        public string Request_ApprovedUnapproved_StationName { get; set; }
        public string Request_ApprovedUnapproved_Place_Ar_Name { get; set; }
        public string Request_ApprovedUnapproved_Place_Address_Ar { get; set; }
        public string General_Admin_Name { get; set; }
        public string Committee_Type { get; set; }
        public Nullable<System.DateTime> Check_Date { get; set; }
        public string RequestCommittee_Status { get; set; }
        public string Item_Data { get; set; }
        public string Attachment_Data { get; set; }
        public string Exporter_Name { get; set; }
        public string Exporter_Address { get; set; }
        public string ImportCompany_Address { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsPaid { get; set; }

        public Nullable<long> ImportCountry { get; set; }
        public Nullable<long> TransitCountry { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public string ImportCompany_data { get; set; }
        public string Request_Fees { get; set; }
        
        //xml data
        public ExportRequest_XmlDTO ExportRequest_xml { get; set; }
        public AttachmentData_Xml AttachmentData_Xml { get; set; }
        public ExportRequest_XmlDTO Item_Data_xml { get; set; }
        public Ex_Request_ImportCompanyXML ImportCompany_xml { get; set; }
        public Ex_Request_FeesXMLDTO Request_Fees_xml { get; set; }
    }
}