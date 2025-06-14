using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Employee
{
   public class outletGeneralDTO
    {


        public string outLetAr_Name { get; set; }
        public string outLetEn_Name { get; set; }
        public string outLetAddress_Ar { get; set; }
        public string outLetAddress_En { get; set; }
        //public Nullable<int> Admin_ID { get; set; }
        //public bool IsActive { get; set; }
        //public Nullable<System.DateTime> User_Updation_Date { get; set; }
        //public Nullable<short> User_Deletion_Id { get; set; }
        //public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        //public short User_Creation_Id { get; set; }
        //public System.DateTime User_Creation_Date { get; set; }
        //public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<long> ID_Orcael { get; set; }
        public Nullable<long> HR_SECTOR_NO { get; set; }






       
        public Nullable<byte> GrAdmin_ID { get; set; }
        public string GrAdminAr_Name { get; set; }
        public string GrAdminEn_Name { get; set; }
        public string GrAdminAddress_Ar { get; set; }
        public string GrAdminAddress_En { get; set; }
        //public Nullable<int> Supervisor_ID { get; set; }
        //public bool IsActive { get; set; }
        //public int IsExport { get; set; }
        //public Nullable<long> User_Updation_Id { get; set; }
        //public Nullable<System.DateTime> User_Updation_Date { get; set; }
        //public Nullable<long> User_Deletion_Id { get; set; }
        //public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        //public long User_Creation_Id { get; set; }
        //public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> ID_HR { get; set; }
    }
}
