using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class Ex_plants
    {

        public int Item_number { get; set; }
        public Nullable<byte> IsExport { get; set; }
        public Nullable<byte> Item_Type { get; set; }
        public Nullable<long> Item_Id { get; set; }
        public string Item_Name { get; set; }
        public string Scientific_Name { get; set; }
        public string Item_Cat_Name { get; set; }
        public string ItemPartTypeName { get; set; }
        public string Item_Strain { get; set; }
        public string ItemStatus { get; set; }
        public Nullable<int> ItemStatus_ID { get; set; }
        public string ItemPurpose { get; set; }
        public string Item_ShortName { get; set; }
        public Nullable<byte> Purpose_ID { get; set; }
        public Nullable<long> PlantCat_ID { get; set; }
        public Nullable<long> plantPart_ID { get; set; }
        public Nullable<long> plantPartType { get; set; }
        public Nullable<double> Hscode { get; set; }
        public Nullable<long> requestItemId { get; set; }
    }
}
