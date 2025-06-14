using System;
 

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class ExportRequest_ApprovedStationDTO
    {
        public int ID { get; set; }
        public Nullable<long> CheckRequest_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public bool IsActive { get; set; }
        
        }
}