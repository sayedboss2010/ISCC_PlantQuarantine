using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class requestCommitteeResultDTO
    {
        public List<CheckRequest_ComiteeResult_ResultDTO> check { get; set; }
        public List<CheckRequest_ComiteeResult_ResultDTO> withdrowSample { get; set; }
        public List<CheckRequest_ComiteeResult_ResultDTO> Treatment { get; set; }
    }
}
