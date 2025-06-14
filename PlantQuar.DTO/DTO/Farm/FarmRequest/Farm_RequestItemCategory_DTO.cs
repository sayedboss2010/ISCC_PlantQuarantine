using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
 public   class Farm_RequestItemCategory_DTO
    {
        public long ID { get; set; }
        public Nullable<long> Farm_ID { get; set; }
        public Nullable<long> ItemCategories_ID { get; set; }
        public Nullable<double> Area_Acres { get; set; }
        public Nullable<double> Quantity_Ton { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<double> Area_Acres_Quarant { get; set; }
        public Nullable<double> Quantity_Ton__Quarant { get; set; }
        public Nullable<double> Quantity_Ton__Export { get; set; }
    }
}
