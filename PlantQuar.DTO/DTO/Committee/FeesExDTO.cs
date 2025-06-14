using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Committee
{
    public class FeesExDTO
    {
        public int Countrow { get; set; }
        public string typeFees { get; set; }
        public long Ex_CheckRequestID { get; set; }
        public long Ex_CommitteeID { get; set; }
        public Nullable<long> Ex_ShiftID { get; set; }
        public Nullable<long> Ex_EngID { get; set; }
        public Nullable<System.DateTime> dateEx { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsPaid { get; set; }
    }
}
