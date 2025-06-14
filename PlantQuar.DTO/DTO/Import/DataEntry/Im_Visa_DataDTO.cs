using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Shipping
{
    public class Im_Visa_DataDTO
    {

        public long ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public bool IsActive { get; set; }

        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
    }
}
