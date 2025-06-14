using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Employee
{
    public class Employee_TrackDTO
    {

        public Nullable<long> Outlet_ID { get; set; }
        public string FullName { get; set; }


        public long EmpId { get; set; }
        public string Name_Ar_Company { get; set; }
        public string Name_En_Company { get; set; }
        public long Importer_ID { get; set; }
        public int ImporterType_Id { get; set; }
        public string CheckRequest_Number { get; set; }
        public long Committee_ID { get; set; }
        public string ISAdmin { get; set; }
        public int OperationType { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public string Name_Committee { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<long> Company_National_Id { get; set; }
        public byte CommitteeTypes_Id { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

        public Nullable<short> Id_User { get; set; }
        public Nullable<long> EmpId_user { get; set; }
        public Nullable<long> Outlet_ID_user { get; set; }
        public Nullable<long> Employee_Id { get; set; }
    }
}
