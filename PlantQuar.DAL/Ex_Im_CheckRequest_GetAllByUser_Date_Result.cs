//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlantQuar.DAL
{
    using System;
    
    public partial class Ex_Im_CheckRequest_GetAllByUser_Date_Result
    {
        public byte Row_Num { get; set; }
        public Nullable<byte> IsExport { get; set; }
        public string CheckRequest_Number { get; set; }
        public Nullable<long> checkRequest_Id { get; set; }
        public Nullable<long> Committee_ID { get; set; }
        public string Committee_Type_Name { get; set; }
        public Nullable<byte> Committee_Type_Id { get; set; }
        public string RequestCommittee_Status { get; set; }
        public Nullable<byte> RequestCommittee_Status_Id { get; set; }
        public string BarCode { get; set; }
        public string Emp_Committe { get; set; }
        public string Request_Treatment { get; set; }
        public Nullable<short> StatusUpdate { get; set; }
    }
}
