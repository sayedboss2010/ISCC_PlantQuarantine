using System;
 


namespace PlantQuar.DTO.DTO.DataEntry.Port
{
    public class PortNationalDTO
    {
        public int ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public short Govern_ID { get; set; }
        public int PortOrgainzation_ID { get; set; }
        public byte PortTypeID { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
 
    }
}