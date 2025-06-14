 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantQuar.DTO.DTO
{
    public class CommitteeEmployeeDTO
    {
        public long Committee_ID { get; set; }
        public long Employee_Id { get; set; }
        public bool ISAdmin { get; set; }
        public int OperationType { get; set; }
        public Nullable<long> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    }
}