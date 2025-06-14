using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class ExportRequest_ItemDTO
    {
        public long ID { get; set; }
        public Nullable<long> CheckRequest_ID { get; set; }
        public long ProdPlant_ID { get; set; }
        public int IsPlant { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
    }
}