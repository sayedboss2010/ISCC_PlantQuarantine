using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
    public class AcceptAggreementBackDTO
    {
        public long StationID { get; set; }
        public string StationCode { get; set; }
        public Nullable<bool> StationIsApproved { get; set; }
        public Nullable<bool> StationIsActive { get; set; }
        public Nullable<bool> StationAccreditationRequestIsPaid { get; set; }
        public Nullable<bool> StationAccreditationCommitteeIsPaid { get; set; }
    }
}
