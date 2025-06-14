using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class ExportRequest_PortDTO
    {
        public int ID { get; set; }
        public Nullable<long> Export_CheckRequest_ID { get; set; }
        public Nullable<int> Port_ID { get; set; }
        public int ReqPortType_ID { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public bool IsActive { get; set; }

    }
}