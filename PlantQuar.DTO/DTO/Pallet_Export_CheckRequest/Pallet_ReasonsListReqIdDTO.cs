using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Pallet_Export_CheckRequest
{
    public class Pallet_ReasonsListReqIdDTO
    {
        public long checkReqId { get; set; }
        public string checkRequestNumber { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public List<short> refuseReasonsIds { get; set; }
    }
}
