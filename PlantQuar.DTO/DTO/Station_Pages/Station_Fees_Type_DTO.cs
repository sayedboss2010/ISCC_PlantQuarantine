using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Fees_Type_DTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<int> Account_Type { get; set; }
        public Nullable<int> Fees_Type { get; set; }
    }
}
