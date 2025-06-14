using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class EX_CheckRequestDTO
    {
        public long ID { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public string CheckRequest_Number { get; set; }
        public string ExportCompany { get; set; }
        public string ExportCompanyAddress { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<short> IsStatus { get; set; }
        public string Notes_Reject { get; set; }


    }
}
