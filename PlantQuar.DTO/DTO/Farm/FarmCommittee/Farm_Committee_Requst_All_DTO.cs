using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
    public class Farm_Committee_Requst_All_DTO
    {
        public List<Farm_Committee_Data_DTO> List_Committee { get; set; }
        public List<EmployeeDTO> List_emp { get; set; }
        public List<Farm_SampleData2DTO> List_Farm_SampleData { get; set; }
        public List<Farm_Committee_ShiftDTO> List_ShiftTiming { get; set; }
        public List<Farm_Committee_CheckList_DTO> List_CheckList { get; set; }
        public List<Farm_Committee_ConstrainDTO> List_Constrain_Text { get; set; }

    }

    public class Farm_Committee_Data_DTO
    {
        public long ID { get; set; }
        public long Farm_Request_ID { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public decimal Amount_Total { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<byte> analysis_count { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public int OperationType { get; set; }
        public Nullable<byte> ShiftTiming_ID { get; set; }       
    }
}
