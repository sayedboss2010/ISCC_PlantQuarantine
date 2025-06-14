using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.labResult
{
    public class Ex_SampleDataDTO
    {
        public long ID { get; set; }
        public long Committee_ID { get; set; }
        public long Ex_Request_Item_Id { get; set; }
        public Nullable<long> Ex_Request_LotData_ID { get; set; }
        public Nullable<int> AnalysisLabType_ID { get; set; }
        public System.DateTime WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public double SampleSize { get; set; }
        public double SampleRatio { get; set; }
        public bool IsAccepted { get; set; }
        public string WithDrawPlace { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }
        public Nullable<long> Lab_User_Id { get; set; }
        public Nullable<System.DateTime> Lab_User_Updation_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public string ImageResult { get; set; }
    }
}
