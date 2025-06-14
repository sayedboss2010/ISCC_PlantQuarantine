//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Privilages.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PR_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PR_User()
        {
            this.PR_GroupModuleMenuPrivilage = new HashSet<PR_GroupModuleMenuPrivilage>();
            this.PR_UserWorkPlace = new HashSet<PR_UserWorkPlace>();
        }
    
        public short Id { get; set; }
        public string FullName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string JobTitleName { get; set; }
        public Nullable<System.DateTime> RegisterationDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public bool Active { get; set; }
        public Nullable<short> DomainLK_DirectorateId { get; set; }
        public long EmpId { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public string Adress_Ar { get; set; }
        public string Adress_En { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<bool> IS_Change_Password { get; set; }
        public string Governorate { get; set; }
        public string Station { get; set; }
        public string Function_Group { get; set; }
        public string Job_Code { get; set; }
        public string Carreer_Code { get; set; }
        public Nullable<bool> IS_Mail_Send { get; set; }
        public Nullable<bool> IS_Failure { get; set; }
        public string Failure_Notes { get; set; }
        public string TEL_HOME { get; set; }
        public string TEL_MOBIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PR_GroupModuleMenuPrivilage> PR_GroupModuleMenuPrivilage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PR_UserWorkPlace> PR_UserWorkPlace { get; set; }
    }
}
