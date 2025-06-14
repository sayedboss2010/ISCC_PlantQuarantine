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
    
    public partial class Farm_Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farm_Request()
        {
            this.Farm_Committee = new HashSet<Farm_Committee>();
            this.Farm_Country = new HashSet<Farm_Country>();
            this.Farm_Request_ItemCategories = new HashSet<Farm_Request_ItemCategories>();
            this.Farm_Request_Refuse_Reason = new HashSet<Farm_Request_Refuse_Reason>();
        }
    
        public long ID { get; set; }
        public Nullable<long> FarmsData_ID { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<bool> IsStatus { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<bool> IS_OnlineOffline { get; set; }
        public byte Farm_Request_Type_ID { get; set; }
        public decimal Fees { get; set; }
        public decimal Fees_Actual { get; set; }
        public Nullable<System.DateTime> End_Date_Request { get; set; }
        public Nullable<System.DateTime> Start_Date_Request { get; set; }
        public Nullable<bool> Is_Final_requst { get; set; }
        public string Print_Text { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Farm_Committee> Farm_Committee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Farm_Country> Farm_Country { get; set; }
        public virtual FarmsData FarmsData { get; set; }
        public virtual Farm_Request_Type Farm_Request_Type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Farm_Request_ItemCategories> Farm_Request_ItemCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Farm_Request_Refuse_Reason> Farm_Request_Refuse_Reason { get; set; }
    }
}
