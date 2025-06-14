using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
   public  class Farm_Committee_ConstrainDTO
    {
        public long ID { get; set; }
        public Nullable<long> Farm_Constrain_ID { get; set; }
        public Nullable<long> Farm_Committee_ID { get; set; }
        public Nullable<byte> Farm_type_ID { get; set; }
    }
}
