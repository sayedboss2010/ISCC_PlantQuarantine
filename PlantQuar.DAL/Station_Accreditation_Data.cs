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
    
    public partial class Station_Accreditation_Data
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Station_Accreditation_Data()
        {
            this.Station_Accreditation = new HashSet<Station_Accreditation>();
            this.Station_Accreditation_CheckList = new HashSet<Station_Accreditation_CheckList>();
            this.Station_Accreditation_Data_Country = new HashSet<Station_Accreditation_Data_Country>();
            this.Station_Accreditation_Data_Item_ShortName = new HashSet<Station_Accreditation_Data_Item_ShortName>();
            this.Station_Accreditation_Request = new HashSet<Station_Accreditation_Request>();
        }
    
        public long ID { get; set; }
        public Nullable<byte> StationActivityType_ID { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }
        public string Name_AR { get; set; }
        public string Name_En { get; set; }
        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
        public string DescriptionMore_AR { get; set; }
        public string DescriptionMore_EN { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
    
        public virtual A_SystemCode A_SystemCode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation> Station_Accreditation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_CheckList> Station_Accreditation_CheckList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_Data_Country> Station_Accreditation_Data_Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_Data_Item_ShortName> Station_Accreditation_Data_Item_ShortName { get; set; }
        public virtual StationActivityType StationActivityType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Station_Accreditation_Request> Station_Accreditation_Request { get; set; }
    }
}
