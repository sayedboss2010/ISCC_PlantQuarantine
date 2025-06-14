using System;
 


namespace PlantQuar.DTO.DTO.DataEntry.GovToVillage
{
    public class VillageDTO
    {
        public short ID { get; set; }
        public Nullable<short> Center_ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> Govern_ID { get; set; }

        public string Gov_Name { get; set; }
        public string Center_Name { get; set; }



        public string IsActiveName { get; set; }
    }
}