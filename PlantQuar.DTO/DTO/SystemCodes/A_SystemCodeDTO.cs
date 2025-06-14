using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.SystemCodes
{
    public class A_SystemCodeDTO
    {
        public int Id { get; set; }
        public string ValueName { get; set; }
        public Nullable<int> SystemCodeTypeId { get; set; }
        public Nullable<byte> Value { get; set; }
        public bool IsActive { get; set; }
    }
}
