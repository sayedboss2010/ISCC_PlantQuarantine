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
    
    public partial class Im_CheckRequest_Items_Lot_Result
    {
        public long ID { get; set; }
        public long Im_CheckRequest_Items_Lot_Category_ID { get; set; }
        public string Nots { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<int> IS_Status { get; set; }
        public Nullable<bool> IS_Status_Committee { get; set; }
    
        public virtual Im_CheckRequest_Lot_Result_Status Im_CheckRequest_Lot_Result_Status { get; set; }
    }
}
