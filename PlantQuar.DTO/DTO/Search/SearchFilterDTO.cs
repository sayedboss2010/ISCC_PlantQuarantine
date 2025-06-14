using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Search
{
    public class SearchFilterDTO
    {
        public string SearchALL { get; set; }
        public string CheckRequest_Number { get; set; }
        public string Outlet_Name { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Name { get; set; }


    }
}
