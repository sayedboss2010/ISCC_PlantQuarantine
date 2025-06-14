using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
    public class ImCheckRequestListOutlitDTO
    {
     
        public long ID { get; set; }
        public string ImCheckRequest_Number { get; set; }     
        public long Importer_ID { get; set; }
        public int ImporterType_Id { get; set; }

        public string ExportCountryName { get; set; }
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }
        public string Outlet { get; set; }


        public string Gov_Name { get; set; }
        public string port_Name { get; set; }
        public int? Port_ID { get; set; }
        public long Outlet_ID { get; set; }
        public short Gov_ID { get; set; }


    }
}
