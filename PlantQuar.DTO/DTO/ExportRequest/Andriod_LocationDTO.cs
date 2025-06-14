using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Andriod_LocationDTO

    {
        public long Id { get; set; }
        public long Committe_ID { get; set; }
        public bool IsExport { get; set; }
        public byte Operation_ID { get; set; }
        public long User_Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public System.DateTime Created_Date { get; set; }

     }
}
