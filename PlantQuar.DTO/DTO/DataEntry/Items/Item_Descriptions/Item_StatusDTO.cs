using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Items.Item_Descriptions
{
   public class Item_StatusDTO
    {
        public int ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Descreption_Ar { get; set; }
        public string Descreption_En { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<byte> Item_Type_ID { get; set; }
        public List<byte?> ListItem_Type_ID { get; set; }
    }
}
