using System;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class CheckRequest_Getdata_CertifacteFeesDTO
    {
        //CertificatesFees
         public long Ex_CertificatesRequestsPaymentsID { get; set; }
        public string Ex_CertificatesRequestsPaymentsTypeValue { get; set; }
        
                  public Nullable<byte> Ex_CertificatesRequestsPaymentsTypeID { get; set; }
        public Nullable<System.DateTime> CertificatesRequestsPaymentsUser_Creation_Date { get; set; }
        public Nullable<double> CertificatesRequestsPaymentsValue { get; set; }
        public Nullable<double> AllCertificatesRequestsPaymentsValue { get; set; }
        public Nullable<bool> IsPayment { get; set; }
    }
}