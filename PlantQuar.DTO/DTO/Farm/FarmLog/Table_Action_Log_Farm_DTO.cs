using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmLog
{
    public class Table_Action_Log_Farm_DTO
    {
        public long ID { get; set; }
        public short ID_Table_Action { get; set; }
        public Nullable<long> ID_TableActionValue { get; set; }
        public Nullable<long> Farm_ID { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public string NOTS { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<int> User_Type_ID { get; set; }
        public Nullable<int> Type_log_ID { get; set; }
    }
}
