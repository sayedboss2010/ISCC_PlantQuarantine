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
    
    public partial class Company_National1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company_National1()
        {
            this.CompanyActivity1 = new HashSet<CompanyActivity1>();
        }
    
        public long ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Address_Ar { get; set; }
        public string Address_En { get; set; }
        public string TaxesRecord { get; set; }
        public string CommertialRecord { get; set; }
        public bool IsTreatment { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public string Owner_Ar { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string Owner_En { get; set; }
        public Nullable<short> Center_ID { get; set; }
        public Nullable<short> Village_ID { get; set; }
        public Nullable<short> User_Activation_Id { get; set; }
        public Nullable<System.DateTime> User_Activation_Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyActivity1> CompanyActivity1 { get; set; }
    }
}
