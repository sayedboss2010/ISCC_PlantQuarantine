using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Fees
{
    public class TransferData
    {
      public TreatmentMethodDTO Dto { get; set; }
           public  List<TreatmentMaterialDTO> Dto1 { get; set; }
    }
}
