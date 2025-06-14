using System;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class CheckRequest_Getdata_AdditionDec_AdminDTO
    {

        public string Certificate_AddtionUpdateAdmin { get; set; }
        public Nullable<long> AdminID { get; set; }
   
        public Nullable<bool> ISAccepted { get; set; }
        public Nullable<System.DateTime> Date_Accepted { get; set; }

        public Nullable<long> PlantCertificatesRequestsID { get; set; }
    }
}