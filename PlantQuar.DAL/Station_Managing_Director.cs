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
    
    public partial class Station_Managing_Director
    {
        public long ID { get; set; }
        public long StationID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Managing_Director_NID { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public string Mobile { get; set; }
    
        public virtual Station Station { get; set; }
    }
}
