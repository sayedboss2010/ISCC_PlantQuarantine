using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.StationNew
{
    public class Ex_Im_Payment_GetDataDTO
    {
        public Nullable<long> CheckRequest_Id { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public string Fees_Fixed_Data { get; set; }
        public string Fees_Constrain_Data { get; set; }
        public string Payment_data { get; set; }

        public Fees_Constrain_DataDTO fees_Constrain_DataDTO { get; set; }
        //public Fees_Fixed_DataDTO fees_Fixed_DataDTO { get; set; }
        public Payment_DataDTO Payment_DataDTO { get; set; }

    }
}
