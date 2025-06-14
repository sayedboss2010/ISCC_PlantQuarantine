using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class PlantDTO
    {
        public long ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Scientific_Name { get; set; }
        public Nullable<int> Family_ID { get; set; }
        public Nullable<int> Group_ID { get; set; }
        public string Descreption_Ar { get; set; }
        public string Descreption_En { get; set; }
        public string Picture { get; set; }
        public bool IsForbidden { get; set; }
        public string ForbiddenReason { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public List<int> ListPlantPartType_Id { get; set; }
        //     public HttpPostedFileBase file_Upload { get; set; }
        // يزرع في مصر ام لا
        public Nullable<bool> IsPlantInEgypt { get; set; }

    }
}