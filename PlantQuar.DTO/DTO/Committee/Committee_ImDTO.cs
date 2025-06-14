using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Committee
{
    public class Committee_ImDTO
    {
        public class RequestCommitteeDTO
        {
            public long ID { get; set; }
            public Nullable<long> ImCheckRequest_ID { get; set; }
            public Nullable<byte> CommitteeType_ID { get; set; }
            public Nullable<byte> ImCommitteeCheckLocation_ID { get; set; }
            public Nullable<System.DateTime> Delegation_Date { get; set; }
            public System.TimeSpan StartTime { get; set; }
            public System.TimeSpan EndTime { get; set; }
            public Nullable<bool> IsFinishedAll { get; set; }
            public Nullable<bool> IsApproved { get; set; }
            public Nullable<bool> Status { get; set; }
            public Nullable<short> User_Updation_Id { get; set; }
            public Nullable<System.DateTime> User_Updation_Date { get; set; }
            public Nullable<short> User_Deletion_Id { get; set; }
            public Nullable<System.DateTime> User_Deletion_Date { get; set; }
            public short User_Creation_Id { get; set; }
            public System.DateTime User_Creation_Date { get; set; }

            public string message { get; set; }
            public List<EmployeeDTO> com_emp { get; set; }

            public List<CommitteeResultDTO> List_CommitteeResult { get; set; }
            public List<CheckRequest_SampleDataDTO> List_SampleData { get; set; }
            public List<RequestCommittee_ShiftDTO> List_Committee_Shift { get; set; }
            public List<Checked_TreatmentMethodDTO> List_TreatmentMethod { get; set; }

        }
        public class RequestCommittee_ShiftDTO
        {
            public long ID { get; set; }
            public long Im_RequestCommittee_ID { get; set; }
            public byte ShiftTiming_ID { get; set; }
            public Nullable<byte> Count { get; set; }
            public Nullable<short> User_Updation_Id { get; set; }
            public Nullable<System.DateTime> User_Updation_Date { get; set; }
            public Nullable<short> User_Deletion_Id { get; set; }
            public Nullable<System.DateTime> User_Deletion_Date { get; set; }
            public Nullable<short> User_Creation_Id { get; set; }
            public Nullable<System.DateTime> User_Creation_Date { get; set; }
            public Nullable<decimal> Amount { get; set; }

            //money
            public Nullable<double> money { get; set; }
            public long Index { get; set; }
     
        }

        public class CheckRequest_SampleDataDTO
        {
            public long ID { get; set; }
            public int AnalysisLabType_ID { get; set; }
            public long Im_RequestCommittee_ID { get; set; }
            public long Im_Request_Item_Id { get; set; }
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
        }

        public class CommitteeResultDTO
        {
            public long ID { get; set; }
            public long Committee_ID { get; set; }
            public long Im_Request_Item_Id { get; set; }
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

        public class Checked_TreatmentMethodDTO
        {
            public long ID { get; set; }
            public long Im_RequestCommittee_ID { get; set; }
            public long Im_Request_Item_Id { get; set; }
            public Nullable<long> Im_Request_LotData_ID { get; set; }
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
            public Nullable<long> User_Updation_Id { get; set; }
            public Nullable<System.DateTime> User_Updation_Date { get; set; }
            public Nullable<long> User_Deletion_Id { get; set; }
            public Nullable<System.DateTime> User_Deletion_Date { get; set; }
            public long User_Creation_Id { get; set; }
            public System.DateTime User_Creation_Date { get; set; }
            public Nullable<long> Item_ShortName_ID { get; set; }
            public Nullable<bool> IS_Total_Android { get; set; }
            public Nullable<bool> IS_From_Android { get; set; }
            public Nullable<bool> IS_Total { get; set; }
            public string Procedures { get; set; }

        }
    }
}
