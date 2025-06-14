using PlantQuar.DTO.DTO.Farm.FarmRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.CheckRequests
{
   public  class Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO
    {
        public long ID { get; set; }
        public long Im_PermissionItem_Division_Custody_DismissCommittee_Id { get; set; }
        public Nullable<System.DateTime> Receive_Date { get; set; }
        public Nullable<System.TimeSpan> ReceiveTime { get; set; }
        public bool IsApproved { get; set; }
        public bool Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public decimal GrossWeight { get; set; }
        public string Notes { get; set; }
        public long Im_RequestCommittee_Id { get; set; }
        public List<EmployeeDTO> com_emp { get; set; }
    }
}
