using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
    public class Im_CheckRequest_ShowDataDTO
    {
        public Nullable<long> outlet_ID { get; set; }
        public string outlet_Name { get; set; }
        public Nullable<int> CheckRequest_Count_all { get; set; }
        public Nullable<int> CheckRequest_Count_true { get; set; }
        public Nullable<int> CheckRequest_Count_false { get; set; }
        public Nullable<int> CheckRequest_Count_nulls { get; set; }
        public Nullable<int> Closed_Request_true { get; set; }
        public Nullable<int> Closed_Request_false { get; set; }
        public Nullable<int> Closed_Request_nulls { get; set; }
    }
}
