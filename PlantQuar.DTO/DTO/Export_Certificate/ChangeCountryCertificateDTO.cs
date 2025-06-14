using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class ChangeCountryCertificateDTO
    {
        public long Ex_CheckRequest_ID { get; set; }
        public Nullable<long> Ex_CheckRequest_Data_ID { get; set; }
        public int Ex_CheckRequest_Port_ID { get; set; }
        public string CheckRequest_Number { get; set; }
        public int Port_ID { get; set; }
        public int ReqPortType_ID { get; set; }
        public byte Port_Type_ID { get; set; }
        public string Port_International_Name_Ar { get; set; }
        public string Country_Name { get; set; }
        public short Country_ID { get; set; }
        // public string msg { get; set; }
    }
}
