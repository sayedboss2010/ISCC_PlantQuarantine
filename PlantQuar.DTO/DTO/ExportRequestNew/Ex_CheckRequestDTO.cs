using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequestNew
{
   public class Ex_CheckRequestDTO
    {
        public long ID { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<short> ImportCountry_Id { get; set; }
        public Nullable<long> Exporter_ID { get; set; }
        public int ExporterType_Id { get; set; }
        public Nullable<byte> GeneralAdmin_ID { get; set; }
        public Nullable<long> Center_Outlet_ID { get; set; }
        public Nullable<byte> Transport_Mean_Id { get; set; }
        public Nullable<byte> Shipment_Mean_Id { get; set; }
        public string Certificate_Number { get; set; }
        public Nullable<int> Num_Certificate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> IsStatus { get; set; }
    }
}
