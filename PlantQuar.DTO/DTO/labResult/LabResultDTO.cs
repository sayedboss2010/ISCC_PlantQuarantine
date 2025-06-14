using PlantQuar.DTO.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO
{
    public class LabResultDTO
    {
        public string barcode { get; set; }
        public bool result { get; set; }
        public string noteAr { get; set; }
        public string noteEn { get; set; }
        public string imageResult { get; set; }
        public A_AttachmentDataDTO model { get; set; }
    }
}
