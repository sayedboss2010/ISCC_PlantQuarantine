using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Con_Ex_Im_ProductsDTO
    {
        public long ID { get; set; }
        public Nullable<int> ItemType_ID { get; set; }
        public Nullable<long> Con_Ex_Im_ID { get; set; }
        public Nullable<byte> ProductStatus_ID { get; set; }
        public Nullable<byte> Purpose_ID { get; set; }
        public long Product_ID { get; set; }
    }
}
