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
    
    public partial class Station_Fees_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Station_Fees_Type()
        {
            this.Station_Accreditation_Request_Fees = new HashSet<Station_Accreditation_Request_Fees>();
            this.Station_Accreditation_Request_Fees_ENG = new HashSet<Station_Accreditation_Request_Fees_ENG>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<int> Account_Type { get; set; }
        public Nullable<int> Fees_Type { get; set; }
        public long Fees_Action_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_Request_Fees> Station_Accreditation_Request_Fees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_Request_Fees_ENG> Station_Accreditation_Request_Fees_ENG { get; set; }
    }
}
