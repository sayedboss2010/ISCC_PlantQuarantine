using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
   public class CheckRequest_GetTreatment_Analsis_DTO
    {
        public int row_num { get; set; }
        public Nullable<long> checkRequest_Id { get; set; }
        public Nullable<byte> IsExport { get; set; }
        public string Item_Data { get; set; }
        public Nullable<short> Analysis_Total { get; set; }
        public Nullable<short> Treatment_Total { get; set; }
        public Nullable<short> Check_Total { get; set; }
    }
}
