using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class Farm_Get_Data_ResultDTO
    {
        public long farmId { get; set; }
        public string farmName { get; set; }
        public string farmAddress { get; set; }
        public string villageName { get; set; }
        public string centerName { get; set; }
        public string governorateName { get; set; }
        public string companyName { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        //public string plantName { get; set; }
        //public Nullable<double> area { get; set; }
        //public Nullable<double> Quantity_Ton { get; set; }
        public List<string> countryName { get; set; }
        public List<short> countryIds { get; set; }
        public long requestId { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public List<farmItemCategories> farmItemCategories { get; set; }
    }
    public class farmItemCategories
    {
        public Nullable<long> ItemCategories_ID { get; set; }
        public Nullable<double> Area_Acres { get; set; }
        public Nullable<double> Quantity_Ton { get; set; }
        public string categoryName { get; set; }

    }
}
