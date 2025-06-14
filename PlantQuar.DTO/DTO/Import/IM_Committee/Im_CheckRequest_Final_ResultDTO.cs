using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.IM_Committee
{
   public class Im_CheckRequest_Final_ResultDTO
    {
        public long ID { get; set; }
        public Nullable<long> Im_CheckRequest_ID { get; set; }
        public Nullable<int> Im_Final_Result_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
    }
}
