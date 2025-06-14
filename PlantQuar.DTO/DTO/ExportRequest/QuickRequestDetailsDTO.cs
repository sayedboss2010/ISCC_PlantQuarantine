using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class QuickRequestDetailsDTO
    {
        public long Ex_CheckRequest_ID { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public string CheckRequest_Number { get; set; }
        public string ExportCompany { get; set; }
        public long Ex_CheckRequest_Items_ID { get; set; }
        public Nullable<int> SubPart_id { get; set; }
        public Nullable<short> Package_Material_ID { get; set; }
        public Nullable<short> Package_Type_ID { get; set; }
        public Nullable<int> Package_Count { get; set; }
        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<int> Units_Number { get; set; }
        public Nullable<bool> Is_LotDivision { get; set; }
        public Nullable<double> Size { get; set; }
        public string Order_Text { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public long ID { get; set; }
      //  public Nullable<long> Ex_CheckRequest_Items_ID { get; set; }
     //   public Nullable<short> Package_Material_ID { get; set; }
     //   public Nullable<short> Package_Type_ID { get; set; }
   //     public Nullable<int> Package_Count { get; set; }
    //    public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public Nullable<decimal> Based_Weight { get; set; }
//        public Nullable<decimal> Package_Weight { get; set; }
        public Nullable<decimal> Package_Based_Weight { get; set; }
        public Nullable<decimal> Package_Net_Weight { get; set; }
  //      public Nullable<int> Units_Number { get; set; }
  //      public Nullable<double> Size { get; set; }
//        public string Order_Text { get; set; }
        public string Reason_Entry { get; set; }
        public string Lot_Number { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string RejectReason { get; set; }
        public string Grower_Number { get; set; }
        public long Item_ShortName_ID { get; set; }
        public string ShortName_Ar { get; set; }
        public string ShortName_En { get; set; }
        public long Item_ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Scientific_Name { get; set; }
    }
}
