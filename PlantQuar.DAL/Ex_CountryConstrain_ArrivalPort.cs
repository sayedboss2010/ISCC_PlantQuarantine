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
    
    public partial class Ex_CountryConstrain_ArrivalPort
    {
        public long Id { get; set; }
        public long Ex_CountryConstrain_Id { get; set; }
        public int Port_International_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Ex_CountryConstrain Ex_CountryConstrain { get; set; }
        public virtual Port_International Port_International { get; set; }
    }
}
