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
    
    public partial class FumigationUnit
    {
        public long ID { get; set; }
        public Nullable<byte> UnitType_ID { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public Nullable<decimal> Capacity { get; set; }
        public bool Status { get; set; }
        public Nullable<int> Maintenance { get; set; }
    
        public virtual Outlet Outlet { get; set; }
        public virtual UnitType UnitType { get; set; }
    }
}
