using System;

namespace PlantQuar.DTO.DTO.Admin
{
    public class User
    {
        public short Id { get; set; }
        public long? Value { get; set; }
        public string FullName { get; set; }
        public string DisplayText { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string JobTitleName { get; set; }
        public Nullable<System.DateTime> RegisterationDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public bool Active { get; set; }
        public bool IS_Change_Password { get; set; }
        public Nullable<short> DomainLK_DirectorateId { get; set; }
        public long EmpId { get; set; }
        public Nullable<long> Outlet_ID { get; set; }
        public string Adress  { get; set; }
        public string Governorate { get; set; }
        public string Station { get; set; }
    }
}