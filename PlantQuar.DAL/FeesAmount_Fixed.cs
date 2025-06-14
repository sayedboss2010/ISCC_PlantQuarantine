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
    using System.Collections.Generic;
    
    public partial class FeesAmount_Fixed
    {
        public int ID { get; set; }
        public string Name_En { get; set; }
        public string Name_Ar { get; set; }
        public byte FeesType_Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<double> WeightFrom { get; set; }
        public Nullable<double> WeightTo { get; set; }
        public Nullable<bool> IsPaidBefore { get; set; }
        public bool IsExport { get; set; }
        public bool IsActive { get; set; }
        public bool IsMandatory { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
    
        public virtual FeesType FeesType { get; set; }
    }
}
