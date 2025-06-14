using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class ReasonsListReqIdDTO
    {
        public long checkReqId { get; set; }
        public string checkRequestNumber { get; set; }
        public string Notes_Reject { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public Nullable<short> User_Id { get; set; }
        public List<short> refuseReasonsIds { get; set; }
    }
}
