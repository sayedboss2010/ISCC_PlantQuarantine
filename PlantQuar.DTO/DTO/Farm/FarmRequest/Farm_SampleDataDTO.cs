using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class Farm_SampleDataDTO
    {
        public long ID { get; set; }
        public Nullable<int> AnalysisLabType_ID { get; set; }
        public long FarmCommittee_ID { get; set; }
        public Nullable<long> Farm_ItemCategories_ID { get; set; }
        public Nullable<System.DateTime> WithdrawDate { get; set; }
        public string Sample_BarCode { get; set; }
        public Nullable<double> SampleSize { get; set; }
        public Nullable<double> SampleRatio { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string RejectReason_Ar { get; set; }
        public string RejectReason_En { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string FarmCode_14 { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public Nullable<bool> Admin_Confirmation { get; set; }
        public Nullable<short> Admin_User { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }

        #region Android Location Data
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion

        #region Result
        public string AnalysiLab_Name_Ar { get; set; }
        public string AnalysiType_Name_Ar { get; set; }

        #endregion

        //**********sayed**********
        public string Item_Name_Ar { get; set; }
        public string ItemCategories_Name_Ar { get; set; }
        public string AnalysisType_Name { get; set; }
        public string AnalysisLab_Name { get; set; }
        public string IsRejectedAll { get; set; }

        public Nullable<bool> IsAccepted_Confirm { get; set; }

        public string Notes_Confirm { get; set; }
        //eman

        public string imageUrl { get; set; }
        public List<empResult> employeeRes { get; set; }
        public bool IsTotalRes { get; set; }
        public string AdminName { get; set; }
    }
}
