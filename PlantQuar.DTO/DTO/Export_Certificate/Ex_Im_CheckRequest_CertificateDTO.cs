using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
  public  class Ex_Im_CheckRequest_CertificateDTO
    {
        public long ID { get; set; }
        public long Index { get; set; }
        public long Ex_Im_CheckRequest_Id { get; set; }
        public bool Is_Export { get; set; }
        public int Certificate_Type_Id { get; set; }
        public string FilePath { get; set; }
        public Nullable<short> Country_Id { get; set; }
        public string Certificate_Number { get; set; }
        public Nullable<System.DateTime> Publish_Date { get; set; }

        // Im_CheckRequest_Manafest
        public long Im_Manafest { get; set; }

    }
}
