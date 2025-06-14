 
using System;

namespace PlantQuar.DTO.DTO.DataEntry.Analysis
{
    public class AnalysisLabDTO
    {
        public int ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Addrees_Ar { get; set; }
        public string Addrees_En { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public Nullable<decimal> Fax { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

       
    }
}
