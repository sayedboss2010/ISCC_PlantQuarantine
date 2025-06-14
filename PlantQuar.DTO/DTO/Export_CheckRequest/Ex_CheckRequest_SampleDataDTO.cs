using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
   public  class Ex_CheckRequest_SampleDataDTO
    {
        public long ID { get; set; }
        public int AnalysisLabType_ID { get; set; }
        public long Im_RequestCommittee_ID { get; set; }
        public Nullable<long> Im_Request_Item_Id { get; set; }
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<System.DateTime> WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public Nullable<double> SampleSize { get; set; }
        public Nullable<double> SampleRatio { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string Notes_Ar { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }
        public string Notes_En { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<bool> Admin_Confirmation { get; set; }
        public Nullable<short> Admin_User { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        // public string Text_Lot { get; set; }

    }
}
