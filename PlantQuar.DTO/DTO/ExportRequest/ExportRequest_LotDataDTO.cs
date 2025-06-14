 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class ExportRequest_LotDataDTO
    {
        public long ID { get; set; }
        public Nullable<long> Ex_Request_Item_ID { get; set; }
        public string Lot_Number { get; set; }
        public Nullable<short> Package_Material_ID { get; set; }
        public Nullable<short> Package_Type_ID { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<long> Farm_ID { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<decimal> Gross_Weight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string PlantingPlace { get; set; }
    }
}