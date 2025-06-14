using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
    public class Refuse_ReasonsDTO
    {
        public short ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsStop { get; set; }
        public int IsExport { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<int> Refused_stopped { get; set; }
    }
}
