using System;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class CheckRequest_Getdata_CustomMessagesDTO
    {
        public int CustomMessages_ID { get; set; }
        public Nullable<long> Ex_CheckRequest_ID { get; set; }
        public string Customs_Certificate_Number { get; set; }
        public string Shipping_Agency { get; set; }
        public string OperationType { get; set; }
        public Nullable<System.DateTime> Certification_Date { get; set; }
        public Nullable<System.DateTime> Shipment_Date { get; set; }
        public Nullable<System.DateTime> Arrival_Date { get; set; }
        public string Certificate_Number_Each_Product { get; set; }
        public string Manifest_Number { get; set; }
        public Nullable<long> Shipping_Agency_ID { get; set; }
        public Nullable<byte> Im_OperationType { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}