using System;

namespace PlantQuar.DTO.DTO.Import.DataEntry
{
    public class FreeZoneDTO
    {
        public byte ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public Nullable<short> Gov_ID { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public Nullable<decimal> Fax { get; set; }
        public string Email { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }

        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

       


    }
}
