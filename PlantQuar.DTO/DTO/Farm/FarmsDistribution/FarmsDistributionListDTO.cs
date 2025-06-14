using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmsDistribution
{
   public class FarmsDistributionListDTO
    {
        public long FarmID { get; set; }
        
        public string ItemName { get; set; }
        public string ItemCatgoryName { get; set; }
        public string Farm_Name_Ar { get; set; }
        public string Farm_Name_En { get; set; }
        public long Importer_ID { get; set; }
        public int ImporterType_Id { get; set; }
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }
        public double Qauntity { get; set; }
    }
}
