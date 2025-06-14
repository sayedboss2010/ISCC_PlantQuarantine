using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
   public class CheckRequest_GetCommitte_Data_ResultDTO
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

        public List<EmployeeDTO> Employee_list { get; set; }
    }
}
