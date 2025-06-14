using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class Farm_Committee_GetData_DTO
    {
        public Nullable<long> FarmCommitteeId { get; set; }
        public Nullable<long> FarmId { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IspaidCommittee { get; set; }
        public string Farm_Name { get; set; }
        public string FarmCode_14 { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<long> requestId { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public string committeetype_Name { get; set; }
        public bool hasCategories { get; set; }

        public Nullable<bool> committee_IsFinishedAll { get; set; }
        public Nullable<bool> committee_Is_Cancel { get; set; }
        public Nullable<System.DateTime> Start_Date_Request { get; set; }
        public Nullable<System.DateTime> End_Date_Request { get; set; }
 
    }
}
