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
    
    public partial class Im_Committee_CustodyPlace
    {
        public int ID { get; set; }
        public long Im_CustodyPlace_Id { get; set; }
        public Nullable<System.DateTime> Check_Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public double Weight { get; set; }
        public short Quantity { get; set; }
        public bool IsPackage { get; set; }
        public short IsApproved { get; set; }
        public bool Status { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
    
        public virtual Im_CustodyPlace_CheckRequest Im_CustodyPlace_CheckRequest { get; set; }
    }
}
