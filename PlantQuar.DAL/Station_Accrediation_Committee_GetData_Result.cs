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
    
    public partial class Station_Accrediation_Committee_GetData_Result
    {
        public int IsExport { get; set; }
        public long Outlet_ID { get; set; }
        public string Ar_Name { get; set; }
        public short Outlet_Center_ID { get; set; }
        public string Outlet_Center_Name { get; set; }
        public short Outlet_Gov_Id { get; set; }
        public string Outlet_Gov_Name { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }
        public long stationId { get; set; }
        public Nullable<short> Station_Gov_Id { get; set; }
        public Nullable<short> Station_Center_Id { get; set; }
        public long Station_Accreditation_ID { get; set; }
        public Nullable<long> station_Committee_Id { get; set; }
        public System.DateTime station_Creation_Date { get; set; }
        public Nullable<System.DateTime> Station_Accreditation_StartDate { get; set; }
        public Nullable<System.DateTime> Station_Accreditation_EndDate { get; set; }
        public Nullable<bool> station_Committee_IsApproved { get; set; }
        public Nullable<bool> station_Committee_STATUS { get; set; }
        public string Station_Request_Type_Name { get; set; }
        public Nullable<bool> station_Committee_Is_Cancel { get; set; }
        public Nullable<bool> station_Committee_IsPaid { get; set; }
        public Nullable<System.DateTime> station_Committee_Delegation_Date { get; set; }
        public string station_Company_National { get; set; }
        public int station_Committee_Delegation_int { get; set; }
        public Nullable<long> ID { get; set; }
        public string StationActivity_Name { get; set; }
        public string station_Name { get; set; }
        public string StationCode { get; set; }
        public Nullable<bool> Station_Accreditation_IsPaid { get; set; }
        public Nullable<bool> Station_IsActive { get; set; }
        public Nullable<bool> Request_IsActive { get; set; }
        public Nullable<bool> Request_IsAccepted { get; set; }
        public Nullable<bool> Request_Is_Final_requst { get; set; }
    }
}
