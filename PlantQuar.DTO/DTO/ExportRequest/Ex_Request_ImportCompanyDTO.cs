using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Ex_Request_ImportCompanyDTO
    {
        public long ID { get; set; }
        public long CheckRequest_ID { get; set; }
        public string ImportCompany { get; set; }
        public string ImporeterCompanyAddress { get; set; }
        public string Reciever_Name { get; set; }
    }
}
