 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantQuar.DTO.DTO.DataEntry.GovToVillage
{
    public class CenterDTO
    {
        public short ID { get; set; }
        public Nullable<short> Govern_ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public bool IsActive { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

        public string govern_Name_AR { get; set; }
        public string govern_Name_En { get; set; }


        public string IsActiveName { get; set; }

        public Nullable<long> Outlet_ID { get; set; }
    }
}