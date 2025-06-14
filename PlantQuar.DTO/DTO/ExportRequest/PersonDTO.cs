using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class ex_PersonDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Person_IDType { get; set; }
        public string IDNumber { get; set; }
        public Nullable<short> Country_ID { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public string Email { get; set; }
    }
}
