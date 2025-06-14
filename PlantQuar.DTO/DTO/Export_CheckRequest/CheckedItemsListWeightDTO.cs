using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class CheckedItemsListWeightDTO
    { //تغييير الاوزان
        public long ItemLotCatID { get; set; }
        public long LotID { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }
       
        public Nullable<int> Package_Count { get; set; } //عدد العبوات 

        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }



    }
}
