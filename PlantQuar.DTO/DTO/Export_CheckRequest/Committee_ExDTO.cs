
using System;
using System.Collections.Generic;


namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    
        public class Ex_RequestCommitteeDTO
        {
            public long ID { get; set; }
            public Nullable<long> Ex_CheckRequest_ID { get; set; }
            public Nullable<byte> CommitteeType_ID { get; set; }
            public Nullable<byte> Ex_CommitteeCheckLocation_ID { get; set; }
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
            public List<Ex_EmployeeDTO> com_emp { get; set; }

            public List<Ex_Committee_ResultDTO> List_CommitteeResult { get; set; }
            public List<Ex_SampleDataDTO> List_SampleData { get; set; }
            public List<Ex_ShiftDTO> List_Committee_Shift { get; set; }

        }
        public class Ex_ShiftDTO
        {
            public long ID { get; set; }
            public long Ex_RequestCommittee_ID { get; set; }
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

        public class Ex_SampleDataDTO
        {
            public long ID { get; set; }
            public int AnalysisLabType_ID { get; set; }
            public long Ex_RequestCommittee_ID { get; set; }
            public long Ex_Request_Item_Id { get; set; }
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

        public class Ex_Committee_ResultDTO
        {
            public long ID { get; set; }
            public long Committee_ID { get; set; }
            public long Ex_Request_Item_Id { get; set; }
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

        public class Ex_EmployeeDTO
        {
            public long Employee_Id { get; set; }

            public Nullable<decimal> Employee_no { get; set; }
            public string Employee_name { get; set; }
            public bool ISAdmin { get; set; }
        }
   
}
