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
    
    public partial class Farm_Committee_Constrain
    {
        public long ID { get; set; }
        public Nullable<long> Farm_Constrain_ID { get; set; }
        public Nullable<long> Farm_Committee_ID { get; set; }
    
        public virtual Farm_Committee Farm_Committee { get; set; }
        public virtual Farm_Constrain Farm_Constrain { get; set; }
    }
}
