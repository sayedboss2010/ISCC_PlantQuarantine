using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
  public  class ExCountryConstainDisplay
    {
        public long CountryConstrain_ID { get; set; }
        public bool IsStationAccreditation { get; set; }
        public bool IsFarmAccreditation { get; set; }
        public bool IsCompanyAccreditation { get; set; }
      

     public List<ExConstrainsLabsAndTyp> Analysis { get; set; }
     public   List<ExConstrainsText> Text { get; set; }
     public   List<ExConstrainsAirPortAndCountry> Ports { get; set; }



    }
}
