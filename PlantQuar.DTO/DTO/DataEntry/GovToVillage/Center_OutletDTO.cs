using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace PlantQuar.DTO.DTO.DataEntry.GovToVillage
{
    public class Center_OutletDTO
    {
        public long ID { get; set; }
        public short Center_ID { get; set; }
        public short GovID { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }

        public Nullable<short> User_Deletion_Id { get; set; }

     
    }
}