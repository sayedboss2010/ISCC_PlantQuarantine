using PlantQuar.DTO.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{



    public class Station_Company_DTO
    {
        public long StationCompany_ID { get; set; }
        public string Station_Name { get; set; }
        public string Station_Accreditation_Data_Name { get; set; }
        public Nullable<byte> Status { get; set; }
        public string FullName { get; set; }
        public string EndDate { get; set; }
        public string StartDate { get; set; }
        public long Station_ID { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        //public Nullable<long> Company_ID { get; set; }
        //public string Company_Name { get; set; }
        //public string Ar_Name { get; set; }
        //public string Station_Accreditation_Data_Name { get; set; }
        //public long Station_Accreditation_Data_ID { get; set; }
        //public long StationCompany_ID { get; set; }
        //public short User_Creation_Id { get; set; }
        //public string FullName { get; set; }
        //public string EndDate { get; set; }
        //public string StartDate { get; set; }
        //public Nullable<byte> Status_Old { get; set; }

        //public int IsExport { get; set; }
        //public long Outlet_ID { get; set; }
        //public string Outlet_Name { get; set; }
        //public Nullable<short> Outlet_Center_ID { get; set; }
        //public string Outlet_Center_Name { get; set; }
        //public Nullable<short> Outlet_Gov_Id { get; set; }
        //public string Outlet_Gov_Name { get; set; }
        //public Nullable<long> Company_ID { get; set; }
        //public string Station_Name { get; set; }
        //public long Station_ID { get; set; }
        //public long ID { get; set; }
        //public Nullable<bool> IsActive { get; set; }
        //public string Station_Accreditation_Name { get; set; }
        //public Nullable<byte> Status { get; set; }
        //public Nullable<System.DateTime> Start_Date { get; set; }
        //public Nullable<System.DateTime> End_Date { get; set; }
        //public Nullable<int> Accreditation_Type_ID { get; set; }
        //public Nullable<short> Center_Id { get; set; }
        //public Nullable<short> Gov_Id { get; set; }

        //public Nullable<short> User_Updation_Id { get; set; }
        //public Nullable<System.DateTime> User_Updation_Date { get; set; }
        //public Nullable<short> User_Deletion_Id { get; set; }
        //public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        //public short User_Creation_Id { get; set; }
        //public System.DateTime User_Creation_Date { get; set; }
        //public Nullable<long> Station_Accreditation_ID { get; set; }


        public short EmpId { get; set; }
        public string Emp_Name { get; set; }

        public List<List_Status_New_DTO> List_Status_New { get; set; }
        public List<List_Id_DTO> List_Id_new { get; set; }
    }

    public class List_Status_New_DTO
    {
        public long ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Station_Accreditation_ID { get; set; }
        public Nullable<byte> Status { get; set; }
    }

    public class List_Id_DTO
    {
        public long ID { get; set; }
    }




}
