//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlantQuar.DAL
{
    using System;
    
    public partial class Certificate_Get_Data_N_Result
    {
        public int ExporterId { get; set; }
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public Nullable<bool> IsPayment { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public string CheckRequest_Number { get; set; }
        public bool ISPrint { get; set; }
        public Nullable<bool> ISAccepted { get; set; }
        public long PlantCertificatesRequest_ID { get; set; }
        public string CertificateNumber { get; set; }
        public string Importer_Exporter_Name { get; set; }
        public string Importer_Exporter_Name_En { get; set; }
        public string ImportCountry_Name { get; set; }
        public string ImportCountry_Name_En { get; set; }
        public Nullable<System.DateTime> CertificateDate { get; set; }
        public Nullable<bool> IsPaid { get; set; }
    }
}
