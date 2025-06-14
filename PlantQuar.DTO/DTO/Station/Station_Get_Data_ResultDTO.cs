using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station
{
    public class Station_Get_Data_ResultDTO
    {
        public long StationId { get; set; }
        public string StationName { get; set; }
        public string StationAddress { get; set; }
        public string villageName { get; set; }
        public string centerName { get; set; }
        public string governorateName { get; set; }
        public string companyName { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string ItemName { get; set; }
        public string countryName { get; set; }
        public long requestId { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string TaxesRecord { get; set; }
        public string CommertialRecord { get; set; }
        public string ActiveType_Name { get; set; }
        public string Industrial_License_Num { get; set; }
    }
}
