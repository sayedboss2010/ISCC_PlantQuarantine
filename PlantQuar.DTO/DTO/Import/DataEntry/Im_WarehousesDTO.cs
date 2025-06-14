using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.DataEntry
{
    public class Im_WarehousesDTO
    {
        public int ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string StoreArea { get; set; }
        public string Address_EN { get; set; }
        public string Address_AR { get; set; }
        public Nullable<int> WarehouseType { get; set; }
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
