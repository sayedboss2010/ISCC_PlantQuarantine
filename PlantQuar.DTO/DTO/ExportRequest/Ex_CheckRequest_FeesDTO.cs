using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Ex_CheckRequest_FeesDTO
    {
        public long ID { get; set; }
        public long Ex_CheckRequest_ID { get; set; }
        public byte FixedFeesAmount_ID { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
