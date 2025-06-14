using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
    public class Farm_Committee_ExaminationDTO
    {
        public long ID { get; set; }
        public long FarmCommittee_ID { get; set; }
        public Nullable<long> Farm_Request_ItemCategories_ID { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<double> Quantity_Ton { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public short User_Creation_Id { get; set; }
        public Nullable<bool> IsAdminFinalResult { get; set; }
        public string AdminFinalResult_Note { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<bool> Admin_Confirmation { get; set; }
        public Nullable<short> Admin_User { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }

        public string Item_Name_Ar { get; set; }
        public string ItemCategories_Name_Ar { get; set; }
        public Nullable<bool> IsAccepted_Admin { get; set; }
        public Nullable<double> Area_AcresFarm { get; set; }
        public Nullable<double> Area_AcresAndoid { get; set; }
        public List<empResult> employeeRes { get; set; }
        public bool IsTotalRes { get; set; }
        public string AdminName { get; set; }
    }
    public class empResult
    {
        public string Notes_Confirm { get; set; }
        public Nullable<bool> IsAccepted_Confirm { get; set; }
        public System.DateTime Date { get; set; }
        public short EmployeeId { get; set; }
        public string empName { get; set; }
    }

}
