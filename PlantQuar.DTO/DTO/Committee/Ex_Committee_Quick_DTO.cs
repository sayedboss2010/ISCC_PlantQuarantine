using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Committee
{
    public class EX_RequestCommittee_QuickDTO
    {
        public long ID { get; set; }
        public Nullable<byte> EX_CommitteeCheckLocation_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
       
        public string message { get; set; }
        public List<EX_CheckRequest_Quick_DTO> ListEX_CheckRequest_Quick { get; set; }//ارقام الطلبات
        public List<CommitteeType_Quick_DTO> ListCommitteeType_Quick { get; set; }// انواع اللجان
        public List<EX_Employee_QuickDTO> com_emp { get; set; }//الموظفين
        public List<Committee_Shift_QuickDTO> List_Committee_Shift { get; set; }//النوبتجية
        public List<Committee_Eng_QuickDTO> List_Committee_Eng { get; set; }//الوردية
        public List<Committee_Items_Lot_QuickDTO> ListCommittee_Items_Lot { get; set; }//النبات واللوط


        //public List<EX_CommitteeResultDTO> List_CommitteeResult { get; set; }
        public List<EX_SampleData_QuickDTO> List_SampleData { get; set; }
        public List<EX_TreatmentMethod_QuicDTO> List_TreatmentMethod { get; set; }
        //public List<Ex_Committee_Quick_DTO> List_Ex_Request_Quick { get; set; }

    }

    public class EX_CheckRequest_Quick_DTO
    {
        public long EX_CheckRequest_ID { get; set; }

    }

    public class CommitteeType_Quick_DTO
    {
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
    }

    public class EX_Employee_QuickDTO
    {
        public long Employee_Id { get; set; }

        public Nullable<decimal> Employee_no { get; set; }
        public string Employee_name { get; set; }
        public bool ISAdmin { get; set; }
    }

    public class Committee_Shift_QuickDTO
    {
        public long ID { get; set; }
        public byte ShiftTiming_ID { get; set; }
        public Nullable<byte> Count { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<double> money { get; set; }
        public long Committee_ID { get; set; }

    }

    public class Committee_Eng_QuickDTO 
    {
        public long ID { get; set; }
        public int Ex_Fees_Type_ID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<int> Num_Eng { get; set; }
        public long Committee_ID { get; set; }

    }

    public class Committee_Items_Lot_QuickDTO
    {
        public long EX_CheckRequest_ID { get; set; }

        public long ID { get; set; }
        public long Committee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> LotData_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<byte> CommitteeResultType_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsAdminResult { get; set; }
        public string AdminFinalResult_Note { get; set; }
        public Nullable<double> QuantitySize { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        //  public string Text_Lot { get; set; }
        public long Item_ShortName_ID { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
    }

    public class Ex_Committee_Quick_DTO
    {
        public long EX_CheckRequest_ID { get; set; }
        public string EX_CheckRequest_Number { get; set; }
        public string OutLet_Name { get; set; }
        public Nullable<long> OutLet_ID { get; set; }

        public Nullable<short> ExportCountry_Id { get; set; }


        public long ID_EX_Items { get; set; }
        public string EX_ItemsName { get; set; }
        public string EX_Item_ShortName { get; set; }
        public long EX_Item_ShortName_ID { get; set; }

        public long ID_Lot { get; set; }
        public string Lot_Number { get; set; }

        public List<EX_Items_Quick> EX_Items_Quick { get; set; }

    }

    public class EX_Items_Quick
    {
        public long ID_EX_Items { get; set; }
        public string EX_ItemsName { get; set; }
        public string EX_Item_ShortName { get; set; }
        public List<EX_Items_Lot_Quick> EX_Items_Lot_Quick { get; set; }
    }
    public class EX_Items_Lot_Quick
    {
        public long ID_Lot { get; set; }
        public string Lot_Number { get; set; }
    }

    public class EX_SampleData_QuickDTO
    {
        public long ID { get; set; }
        public int AnalysisLabType_ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
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
       
        public Nullable<bool> Admin_Confirmation { get; set; }
        public Nullable<short> Admin_User { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        // Eslam Add Samples
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Fees_Actual { get; set; }
        public Nullable<int> Count_Sample { get; set; }
    }

    public class EX_TreatmentMethod_QuicDTO
    {
        public long ID { get; set; }
        public long EX_RequestCommittee_ID { get; set; }
        public long EX_Request_Item_Id { get; set; }
        public Nullable<long> EX_Request_LotData_ID { get; set; }
        public Nullable<byte> TreatmentType_ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public string Station_Place { get; set; }
        public byte TreatmentMethod_ID { get; set; }
        public Nullable<byte> TreatmentMat_ID { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> ThermalSealNumber { get; set; }
      
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<bool> IS_Total_Android { get; set; }
        public Nullable<bool> IS_From_Android { get; set; }
        public Nullable<bool> IS_Total { get; set; }
        public string Procedures { get; set; }
        //add eslam
        public Nullable<decimal> NetWeightForTreatment { get; set; }
    }
}
