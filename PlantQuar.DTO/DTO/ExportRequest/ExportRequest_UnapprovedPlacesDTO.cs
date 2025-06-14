using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class ExportRequest_UnapprovedPlacesDTO
    {
        public int ID { get; set; }
        public Nullable<long> CheckRequest_ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }

        }
}