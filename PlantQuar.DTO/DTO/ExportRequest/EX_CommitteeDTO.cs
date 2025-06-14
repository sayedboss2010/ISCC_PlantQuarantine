using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class EX_CommitteeDTO
    {


        public long ID { get; set; }
        public long ExCheckRequest_ID { get; set; }
        public byte CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.DateTime> Check_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public int OperationType { get; set; }
        public List<EX_EmployeeDTO> com_emp { get; set; }
    }

    public class EX_EmployeeDTO
    {
        public long Employee_Id { get; set; }
        public Nullable<decimal> Employee_no { get; set; }
        public string Employee_name { get; set; }
        public bool ISAdmin { get; set; }
        public long Committee_ID { get; set; }

        public int OperationType { get; set; }
        public Nullable<long> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
    } 

    public class EX_CheckRequest_GetCommitte_Data_ResultDTO
    {
        public Nullable<long> CheckRequest_Id { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.DateTime> Check_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> number_Committes { get; set; }
        public Nullable<long> Committe_Id { get; set; }

        public List<EX_EmployeeDTO> Employee_list { get; set; }
    }

    public class EX_Station_Accreditation_CommitteeDTO
    {
        public long ID { get; set; }
        public long Station_Accreditation_ID { get; set; }
        public byte CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public bool IsPaid { get; set; }
        public int OperationType { get; set; }
        public decimal Amount_Total { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public List<EX_EmployeeDTO> com_emp { get; set; }
        public string stationcode { get; set; }


    }

    public class EX_Farm_CommitteeDTO
    {
        public long ID { get; set; }
        public long Farm_Country_Request_ID { get; set; }
        public byte CommitteeType_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public string farmcode { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }

        public decimal Amount_Total { get; set; }
        public Nullable<bool> Status { get; set; }
        public int OperationType { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public List<EX_EmployeeDTO> com_emp { get; set; }
    }
}


