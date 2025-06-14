using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Ex_CheckRequest_ExtraDTO
    {
        public long CheckRequest_ID { get; set; }
        public string ImportCompany { get; set; }
        public string ImporeterCompanyAddress { get; set; }
        public string Reciever_Name { get; set; }
        public string Ship_Name { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAddress { get; set; }
        public string DelegateName { get; set; }
        public string DelegateAddress { get; set; }
    }
}
