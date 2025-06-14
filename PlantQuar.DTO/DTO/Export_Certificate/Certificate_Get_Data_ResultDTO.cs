using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class Certificate_Get_Data_ResultDTO
    {
        public Nullable<long> RequestID { get; set; }
        public Nullable<bool> IsPayment { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<bool> ISPrint { get; set; }
        public Nullable<bool> ISAccepted { get; set; }
        public long PlantCertificatesRequest_ID { get; set; }


        public string CertificateNumber { get; set; }
        public string Importer_Exporter_Name { get; set; }
        public string ImportCountry_Name { get; set; }

        public int ExporterId { get; set; }
        public long ExporterTypeID { get; set; }
       
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public string Importer_Exporter_Name_En { get; set; }
        public string ImportCountry_Name_En { get; set; }
        public Nullable<System.DateTime> CertificateDate { get; set; }
        public Nullable<bool> IsPaid { get; set; }
    }
}
