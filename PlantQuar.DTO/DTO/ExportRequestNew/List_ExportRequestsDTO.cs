using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequestNew
{
   public class List_ExportRequestsDTO
    {
        public long Row_Id { get; set; }
        public Nullable<long> CheckRequest_Id { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<System.DateTime> Ex_Creation_Date { get; set; }
        public string Outlet_Name { get; set; }
        public string Govern_Name { get; set; }
        public Nullable<long> Exporter_ID { get; set; }
        public Nullable<int> ExporterType_Id { get; set; }
        public string ExporterType_Name { get; set; }
        public string Exporter_Name { get; set; }
        public string ImportCountry_Name { get; set; }
        public Nullable<long> CommitteID { get; set; }
        public string Committe_TypeName { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> Committe_IsApproved { get; set; }
        public Nullable<bool> Committe_Status { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public Nullable<bool> CanalterGashny { get; set; }
        public Nullable<byte> Committe_Type_Id { get; set; }
        public Nullable<int> Request_Status { get; set; }
    }
}
