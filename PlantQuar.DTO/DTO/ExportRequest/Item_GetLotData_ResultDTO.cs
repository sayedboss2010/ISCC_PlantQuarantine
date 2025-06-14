using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Item_GetLotData_ResultDTO
    {
        public long Lot_Id { get; set; }
        public string Lot_Number { get; set; }
        public string packageTypeName { get; set; }
        public string materialName { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<decimal> Gross_Weight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Package_NetWeight { get; set; }
    }
}
