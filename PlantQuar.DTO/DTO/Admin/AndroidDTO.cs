using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
    public class AndroidDTO
    {
        public string CommitteeTypeName_Ar { get; set; }
        public long Ex_CheckRequestID { get; set; }
        public long Ex_CommitteeID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        
public Nullable<bool> IsPaid_RequestCommittee { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        

    }
}
