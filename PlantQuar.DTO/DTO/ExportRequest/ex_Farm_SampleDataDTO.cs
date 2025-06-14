using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
   public class ex_Farm_SampleDataDTO
    {
        public long ID { get; set; }
        public Nullable<int> AnalysisLabType_ID { get; set; }
        public long FarmCommittee_ID { get; set; }
        public Nullable<System.DateTime> WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public double SampleSize { get; set; }
        public double SampleRatio { get; set; }
        public bool IsAccepted { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string FarmCode_14 { get; set; }

        #region Android Location Data
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion

        #region Result
        public string AnalysiLab_Name_Ar { get; set; }
        public string AnalysiType_Name_Ar { get; set; }

        #endregion
    }
}
