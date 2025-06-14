using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Committee_Station_DTO
    {
        public string Message { get; set; }
        public List<Station_Committee_Data_DTO> List_Committee_Station { get; set; }
        public List<EmployeeDTO> List_emp { get; set; }
        public List<Station_Committee_ShiftDTO> List_ShiftTiming { get; set; }
        public List<Station_Committee_CheckList_DTO> List_CheckList { get; set; }
        public List<Station_Request_Fees_DTO> List_Station_Request_Fees { get; set; }
    }

    public class Station_Committee_Data_DTO
    {
        public long ID { get; set; }
        public long Station_Request_ID { get; set; }
        public byte CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public decimal Amount_Total { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<byte> analysis_count { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public int OperationType { get; set; }
        public Nullable<byte> ShiftTiming_ID { get; set; }
    }

    public class Station_Committee_ShiftDTO
    {
        public long ID { get; set; }
        public long Station_Committee_ID { get; set; }
        public byte ShiftTiming_ID { get; set; }
        public Nullable<byte> Count { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        //money
        public Nullable<double> money { get; set; }
        public Nullable<decimal> Amount { get; set; }

    }

    public class Station_Committee_CheckList_DTO
    {
        public long ID { get; set; }
        public long StationCommittee_ID { get; set; }
        public long Station_Country_CheckList_ID { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }

        public Nullable<long> EmployeeId { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }

    }

    public class Station_Request_Fees_DTO
    {
        public long ID { get; set; }
        public long Station_ID { get; set; }
        public long Station_Accreditation_Request_ID { get; set; }
        public int Station_Fees_Type_ID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<int> Num_Eng { get; set; }

    }
}
