using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
    public class Im_RequestCommitteeDTO
    {
        public long ID { get; set; }
        public Nullable<long> ImCheckRequest_ID { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<byte> ImCommitteeCheckLocation_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public List<EmployeeDTO> com_emp { get; set; }

        public List<Im_CommitteeResultDTO> Im_CommitteeResult { get; set; }
        public List<Im_CheckRequest_SampleDataDTO> Im_CheckRequest_SampleData { get; set; }
        public List<Im_RequestCommittee_ShiftDTO> Im_RequestCommittee_Shift { get; set; }
       
    }
}
