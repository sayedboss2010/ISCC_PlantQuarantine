using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
    public class Farm_Committee_CheckList_DTO
    {
        public long ID { get; set; }
        public long FarmCommittee_ID { get; set; }
        public long Farm_Country_CheckList_ID { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }
       
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }

       
        public Nullable<bool> IsAccepted_Quarantine { get; set; }

    }
}
