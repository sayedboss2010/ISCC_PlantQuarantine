
using System;
using System.Collections.Generic;

namespace PlantQuar.DTO.DTO.Import.Committee
{
    public class Im_Check_ComitteDTO
    {
        public string Item_Name_Ar { get; set; }
        public string ShortName_Ar { get; set; }
        public string ShortName_En { get; set; }
        public Nullable<long> EmployeeId { get; set; }

        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> Status { get; set; }
        //public DateTime Date { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public double Weight { get; set; }

        public string Notes { get; set; }

        public Nullable<bool> IsAdminFinalResult { get; set; }

        public double QuantitySize { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }

        public long ID { get; set; }
        public Nullable<long> ImCheckRequest_ID { get; set; }
        public bool IsAccepted { get; set; }

        public List<Im_Execution_CommDTO> ImExecutionComm { get; set; }
        public List<empResult> empResults { get; set; }
        public List<Im_CustodyPlaceDTO> CustodyPlace { get; set; }
        public List<Im_DismissCommitteeDTO> Im_DismissCommittee { get; set; }

        public List<Im_ReceiveCommitteeDTO> Im_ReceiveCommittee { get; set; }
        // public List<Im_Check_ComitteDTO> ImCheckComitte { get; set; }
        //    public long FarmCommittee_ID { get; set; }
        //    public Nullable<long> Farm_Request_ItemCategories_ID { get; set; }
        //    public string Notes { get; set; }
        //    public Nullable<System.DateTime> EndDate { get; set; }
        //    public Nullable<System.DateTime> StartDate { get; set; }
        //    public Nullable<double> Quantity_Ton { get; set; }
        //    public Nullable<bool> IsAccepted { get; set; }
        //    public short User_Creation_Id { get; set; }
        //    public Nullable<bool> IsAdminFinalResult { get; set; }
        //    public string AdminFinalResult_Note { get; set; }
        //    public System.DateTime User_Creation_Date { get; set; }
        //    public Nullable<bool> Admin_Confirmation { get; set; }
        //    public Nullable<short> Admin_User { get; set; }
        //    public Nullable<System.DateTime> Admin_Date { get; set; }


        //    public string ItemCategories_Name_Ar { get; set; }
        //    public Nullable<bool> IsAccepted_Admin { get; set; }
        //    public Nullable<double> Area_Acres { get; set; }
        //    public List<empResult> employeeRes { get; set; }
        //    public bool IsTotalRes { get; set; }
        //    public string AdminName { get; set; }
    }
    public class empResult
    {
        public string Notes_Confirm { get; set; }
        public Nullable<bool> IsAccepted_Confirm { get; set; }
        public System.DateTime Date { get; set; }
        public short EmployeeId { get; set; }
        public string empName { get; set; }
    }
    public class Im_ReceiveCommitteeDTO
    {
        public long ID { get; set; }
        public Nullable<long> ImCheckRequest_ID { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> Dismiss_Date { get; set; }
        public Nullable<System.TimeSpan> DismissTime { get; set; }
        public long Im_PermissionItem_Division_Custody_Id { get; set; }
        public Nullable<System.DateTime> Receive_Date { get; set; }
        public Nullable<System.TimeSpan> ReceiveTime { get; set; }
    }

    public class Im_Execution_CommDTO
    {
        public long ID { get; set; }
        public long Im_RequestCommittee_Id { get; set; }
        public string Execution_Place { get; set; }
        public string Execution_Method { get; set; }
        public byte[] Execution_File { get; set; }

        public short? User_Creation_Id { get; set; }

        public Nullable<long> ImCheckRequest_ID { get; set; }

        public Nullable<System.DateTime> Delegation_Date { get; set; }

        public Nullable<System.TimeSpan> StartTime { get; set; }

        public Nullable<System.TimeSpan> EndTime { get; set; }

        public Nullable<bool> IsApproved { get; set; }

        public Nullable<bool> IsFinishedAll { get; set; }
        public bool IsAccepted { get; set; }

        public Nullable<bool> Status { get; set; }
        //public DateTime Date { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public decimal GrossWeight { get; set; }
        public Nullable<long> Im_ItemsLotDivision_ID { get; set; }
        //  public Im_ItemsLotDivision Im_ItemsLotDivision { get; set; }

        public Nullable<long> Im_PermissionItem_ID { get; set; }
        // public string Im_PermissionItems { get; set; }
    }
    public class Im_CustodyPlaceDTO
    {


        public long ID { get; set; }
        public long Im_CheckRequest_ID { get; set; }
        public string En_Desc { get; set; }
        public string Ar_Desc { get; set; }
        public byte PlaceType { get; set; }
        public double Storage_capacity { get; set; }
        public short Center_Id { get; set; }
        public string Address { get; set; }
        public string Owner_Name { get; set; }
        public string NationalID { get; set; }
        public string Phone { get; set; }
        public double PreviewQuantityDuration { get; set; }
        public Nullable<System.DateTime> DateStored { get; set; }
        public double Quantity { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CustodyPlaceType { get; set; }
        public string Center_Name { get; set; }
        public string Committee_CustodyPlace { get; set; }



    }
    public class Im_DismissCommitteeDTO
    {
        public long ID { get; set; }
        public long Im_PermissionItem_Division_Custody_Id { get; set; }
        public string Im_PermissionItem_Division_CustodyName { get; set; }
        public string Driver_Name { get; set; }
        public string Driver_Phone { get; set; }
        public string Driver_National_Id { get; set; }
        public decimal GrossWeight { get; set; }

        public long Im_RequestCommittee_Id { get; set; }
        public Nullable<System.DateTime> Dismiss_Date { get; set; }
        public Nullable<System.TimeSpan> DismissTime { get; set; }
        public bool IsApproved { get; set; }
        public bool Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string Lock_Lead { get; set; }
        public string Notes { get; set; }

        //public virtual Im_PermissionItem_Division_Custody Im_PermissionItem_Division_Custody { get; set; }
        //public virtual Im_RequestCommittee Im_RequestCommittee { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Im_PermissionItem_Division_Custody_ReceiveCommittee> Im_PermissionItem_Division_Custody_ReceiveCommittee { get; set; }
    }
}
