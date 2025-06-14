using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class Exporter_ContactDTO
    {
        public long ID { get; set; }
        public long Exporter_ID { get; set; }
        public byte ContactType_ID { get; set; }
        public int ExporterType_Id { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; } 
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short? User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public byte DeleteCheck { get; set; }
    }
}
