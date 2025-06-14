using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
    public class ReasonsListReqIdDTO
    {
        public long checkReqId { get; set; }
        public string checkRequestNumber { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public List<short> refuseReasonsIds { get; set; }
    }
}
