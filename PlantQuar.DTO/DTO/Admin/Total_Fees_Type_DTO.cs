using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Admin
{
    public class Total_Fees_Type_DTO
    {
        public string Description { get; set; }
        public Nullable<decimal> Amount_Total { get; set; }
        public Nullable<decimal> حسابخاص { get; set; }
        public Nullable<decimal> حسابحكومي { get; set; }
    }
}
