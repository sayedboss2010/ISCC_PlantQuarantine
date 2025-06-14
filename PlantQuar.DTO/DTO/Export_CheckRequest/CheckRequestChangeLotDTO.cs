using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class CheckRequestChangeLotDTO
    {    
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public long EX_ItemID { get; set; }
        public string ItemName { get; set; }   
        public string ItemShortName { get; set; }   
        public long LotID { get; set; }
        public string Lot_Number { get; set; }

        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<decimal> Package_Based_Weight { get; set; } //وزن العبوة القائم
        public Nullable<decimal> Package_Net_Weight { get; set; }//وزن العبوة الصافي
        public Nullable<int> Package_Count { get; set; } //عدد العبوات 
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
    }
    public class LotWeightDTO
    {

    }
    
}
