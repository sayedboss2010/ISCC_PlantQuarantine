using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class Ex_CertificateAddtionUserDTO
    {
        public long ID { get; set; }
        public Nullable<long> PlantCertificatesRequestsID { get; set; }
        public string Certificate_AddtionText { get; set; }
        public Nullable<bool> IS_Client_OR_Agree { get; set; }
    }
}
