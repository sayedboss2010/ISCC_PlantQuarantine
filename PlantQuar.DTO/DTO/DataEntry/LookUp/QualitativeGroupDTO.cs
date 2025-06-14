using System;

namespace PlantQuar.DTO.DTO.DataEntry.LookUp
{
    public class QualitativeGroupDTO
    {
        public short Id { get; set; }
        public string Descreption_En { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Descreption_Ar { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
    }
}