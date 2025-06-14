
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class Ex_RequestDetailsDTO
    {
        public long EX_CheckRequest_ID { get; set; }
        public string EXCheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }

        public List<Items_checkReq_New> itemsWithConstrains { get; set; }
    }
       

}
