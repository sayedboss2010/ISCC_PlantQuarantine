using System;
 


namespace PlantQuar.DTO.DTO.DataEntry.Countries
{
    public class Union_CountryDTO
    {
        public short ID { get; set; }
        public long IDold { get; set; }
        public short Country_ID { get; set; }
        public short Union_ID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        
        
    }
}