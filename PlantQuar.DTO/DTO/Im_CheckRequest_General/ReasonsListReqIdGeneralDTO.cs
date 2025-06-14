using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Im_CheckRequest_General
{
  public  class ReasonsListReqIdGeneralDTO
    {


        public long checkReqId { get; set; }
        public string checkRequestNumber { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public List<short> refuseReasonsIds { get; set; }
    }
}
