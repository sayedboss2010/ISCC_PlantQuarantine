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
    
    public partial class Station_Accreditation_CheckList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Station_Accreditation_CheckList()
        {
            this.Station_Accreditation_Committee_CheckList = new HashSet<Station_Accreditation_Committee_CheckList>();
        }
    
        public long ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public long Station_CheckList_ID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
    
        public virtual Station_Accreditation_Data Station_Accreditation_Data { get; set; }
        public virtual Station_CheckList Station_CheckList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_Committee_CheckList> Station_Accreditation_Committee_CheckList { get; set; }
    }
}
