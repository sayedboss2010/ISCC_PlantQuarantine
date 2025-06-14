using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Items.Item_Descriptions
{
   public class ItemPartDTO
    {
        public long ID { get; set; }
        public long Item_ID { get; set; }
        public int SubPart_ID { get; set; }
        public int SubPart_Type_ID { get; set; }
        public bool IsAllowed { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
    }
}
