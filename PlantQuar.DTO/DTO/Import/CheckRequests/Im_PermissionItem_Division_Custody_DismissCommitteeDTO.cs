using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
   public  class Im_PermissionItem_Division_Custody_DismissCommitteeDTO
    {
        public long ID { get; set; }
        public long Im_PermissionItem_Division_Custody_Id { get; set; }
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
        public List<EmployeeDTO> com_emp { get; set; }
    }
}
