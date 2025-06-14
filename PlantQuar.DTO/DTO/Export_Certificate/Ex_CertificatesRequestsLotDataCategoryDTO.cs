using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class Ex_CertificatesRequestsLotDataCategory2
    {
        public long CertificatesRequest_ID { get; set; }
        public Nullable<long> LotID { get; set; }
        public string LotNumber { get; set; }
        public string Item_ShortName { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public string Scientific_Name { get; set; }
        public string ItemCategories_GroupNameAR { get; set; }
        public string ItemCategories_GroupNameEN { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public Nullable<int> Item_Status_ID { get; set; }
        public string Item_Status_Ar_Name { get; set; }
        public string Item_Name_Ar { get; set; }
        public string Item_Status_En_Name { get; set; }
        public string Item_Name_En { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> GrossWeight_Items { get; set; }
        public Nullable<decimal> Net_Weight_Items { get; set; }

        public Nullable<decimal> GrossWeight_Lots { get; set; }
        public Nullable<decimal> Net_Weight_Lots { get; set; }
        public Nullable<int> containers_ID { get; set; }
        public string containers_type{ get; set; }
        public string ShipholdNumber  { get; set; }
        public string ContainerNumber { get; set; }
        public string NavigationalNumber { get; set; }


        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }
        public Nullable<short> Package_Material_ID { get; set; }
        public Nullable<short> Package_Type_ID { get; set; }
        public string Package_Material { get; set; }
        public string Package_Type { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
    }
}
