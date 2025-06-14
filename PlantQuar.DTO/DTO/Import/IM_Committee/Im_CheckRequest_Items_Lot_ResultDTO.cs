using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.IM_Committee
{
   public class Im_CheckRequest_Items_Lot_ResultDTO
    {
        public long ID { get; set; }
        public long Im_CheckRequest_Items_Lot_Category_ID { get; set; }
        public Nullable<int> IS_Status { get; set; }
        public string Nots { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }

    }
}
