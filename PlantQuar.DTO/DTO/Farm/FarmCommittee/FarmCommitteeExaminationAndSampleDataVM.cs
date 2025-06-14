using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
    public class FarmCommitteeExaminationAndSampleDataVM
    {
        public string FarmCode_14 { get; set; }
        public string print_text { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Farm_Committee_ExaminationDTO Committee_Examination { get; set; }
        public Farm_SampleDataDTO SampleData { get; set; }
        public Farm_CountryDTO Country { get; set; }

        public Farm_Committee_CheckList_ListDTO Farm_Committee_CheckList_List { get; set; }
        public Farm_Check_List_Admin_NoteDTO Farm_Check_List_Admin_Note { get; set; }
        public Farm_Check_List_All_NoteDTO Farm_Check_List_All_Note { get; set; }
    }
    public class Farm_Committee_CheckList_ListDTO
    {
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<bool> IsAcceptedAdmin { get; set; }
        public Nullable<bool> IsAcceptedConfirm { get; set; }
        public string Constrain_Ar { get; set; }
        public string AdminNameCheckList { get; set; }
        public string ConfirmName { get; set; }
        public string Notes_ArAdmin { get; set; }
        public string Notes_ArConfirm { get; set; }
        public string Farm_Country_CheckList { get; set; }
        public Nullable<long> AdminEmployeeId { get; set; }
        public Nullable<long> ConfirmEmployeeId { get; set; }
        public long FarmCommittee_ID { get; set; }
        public long CheckList_ID { get; set; }

        public long ID { get; set; }

        public long Farm_Country_CheckList_ID { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<bool> IsAccepted_Quarantine { get; set; }
        public Nullable<long> EmployeeId_Quarantine { get; set; }
        public string Employee_Name_Quarantine { get; set; }

    }

    public class Farm_Check_List_All_NoteDTO
    {
        public string Notes_Type { get; set; }
        public string Notes_Ar { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string Name { get; set; }

    }
    public class Farm_Check_List_Admin_NoteDTO
    {
        public string Notes_ArAdmin { get; set; }
        public Nullable<long> AdminEmployeeId { get; set; }
        public string AdminName { get; set; }

    }
    public class _Farm_Check_List_Confirm_NoteDTO
    {
        public string Notes_ArConfirm { get; set; }
        public Nullable<long> ConfirmEmployeeId { get; set; }
        public string ConfirmName { get; set; }

    }

    public class Farm_Check_List_AdminQuarantine_NoteDTO
    {
        public string Notes_ArAdminQuarantine { get; set; }
        public Nullable<long> AdminQuarantineEmployeeId { get; set; }
        public string QuarantineAdminName { get; set; }

    }
}