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
    
    public partial class Ex_CertificatesRequestsPayments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ex_CertificatesRequestsPayments()
        {
            this.Ex_CertificatesRequestsPaymentsDetailes = new HashSet<Ex_CertificatesRequestsPaymentsDetailes>();
        }
    
        public long ID { get; set; }
        public long PlantCertificatesRequestsID { get; set; }
        public Nullable<long> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<byte> Ex_CertificatesRequestsPaymentsType { get; set; }
        public Nullable<bool> IsPayment { get; set; }
    
        public virtual Ex_CertificatesRequests Ex_CertificatesRequests { get; set; }
        public virtual Ex_CertificatesRequestsPaymentsType Ex_CertificatesRequestsPaymentsType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ex_CertificatesRequestsPaymentsDetailes> Ex_CertificatesRequestsPaymentsDetailes { get; set; }
    }
}
