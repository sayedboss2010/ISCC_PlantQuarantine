using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class CertificateNewDTO
    {
       
        public long CertificateID { get; set; }
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public string CertificateNumber { get; set; }
        public int CertificateNo { get; set; }
        public string ExporterType_Name { get; set; }
        public string Importer_Exporter_Name { get; set; }
        public Nullable<long> Importer_Exporter_Id { get; set; }
        public string Importer_Exporter_Address { get; set; }
        public string Reciever_Name { get; set; }
        public string ImportCompany { get; set; }
        public string ImporeterCompanyAddress { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<bool> IS_Additional_Declaretion { get; set; }
        public Nullable<bool> IS_Containers { get; set; }
        public Nullable<bool> IS_Lot { get; set; }
        public Nullable<bool> IS_Treatment { get; set; }
        public Nullable<bool> ISAccepted { get; set; }
        public Nullable<bool> ISEnglish { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public bool ISPrint { get; set; }
        public string Ship_Name { get; set; }
        public string PortArriveName { get; set; }
        public string ImportCountry_Name { get; set; }

    }

}