
using System;
using System.Collections.Generic;

namespace PlantQuar.DTO.DTO.DataEntry.Outlets
{
    public class GeneralAdminDTO
    {
        public GeneralAdminDTO()
        {
            Contacts = new List<HagrContactDTO>();
        }
        public byte ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public Nullable<int> Admin_ID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }

        public List<HagrContactDTO> Contacts { get; set; }



      
    }
}