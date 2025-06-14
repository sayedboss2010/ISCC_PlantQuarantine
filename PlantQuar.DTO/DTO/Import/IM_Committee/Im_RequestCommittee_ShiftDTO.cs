using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.IM_Committee
{
   public class Im_RequestCommittee_ShiftDTO
    {
        public long ID { get; set; }
        public long Im_RequestCommittee_ID { get; set; }
        public byte ShiftTiming_ID { get; set; }
        public Nullable<byte> Count { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable< System.DateTime> User_Creation_Date { get; set; }
        public Nullable<decimal> Amount { get; set; }

        //money
        public Nullable<double> money { get; set; }
        public long Index { get; set; }
    }
}
