using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class PlantCategoryDTO
    {
        //public long ID { get; set; }
        //public Nullable<long> Plant_ID { get; set; }
        //public string Name_Ar { get; set; }
        //public string Name_En { get; set; }
        //public Nullable<long> Company_ID { get; set; }
        //public string Register_NumDate { get; set; }
        //public Nullable<System.DateTime> Register_EndDate { get; set; }
        //public Nullable<System.DateTime> TimeOut { get; set; }
        //public bool IsForbidden { get; set; }
        //public bool CurrentStatus { get; set; }
        //public string Notes { get; set; }
        //public Nullable<short> User_Updation_Id { get; set; }
        //public Nullable<System.DateTime> User_Updation_Date { get; set; }
        //public Nullable<short> User_Deletion_Id { get; set; }
        //public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        //public short User_Creation_Id { get; set; }
        //public System.DateTime User_Creation_Date { get; set; }
        //public string Protect_Property { get; set; }
        //public string Protect_Property_File { get; set; }
        public long ID { get; set; }
        public Nullable<long> Plant_ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public string Register_NumDate { get; set; }
        public Nullable<System.DateTime> Register_EndDate { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public bool IsForbidden { get; set; }
        public bool CurrentStatus { get; set; }
        public string Protect_Property { get; set; }
        public string Notes { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public string Protect_Property_File { get; set; }
        public Nullable<int> Resolution_Number { get; set; }
        public int Resolution_Date { get; set; }

        //مسجل و غير مسجل في حالة المسجل
        public Nullable<bool> IsRegister { get; set; }

    }
}