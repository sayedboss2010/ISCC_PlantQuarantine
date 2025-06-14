using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmData
{
    public class FarmPlantDTO
    {
        public long ID { get; set; }
        public Nullable<long> Farm_ID { get; set; }
        public Nullable<long> Plant_ID { get; set; }
        public Nullable<long> PlantCat_ID { get; set; }
        public Nullable<double> Area_Acres { get; set; }
        public Nullable<double> Quantity_Ton { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

        public string PlantName { get; set; }
        public string CategoryName { get; set; }

        public int mainClass_Id { get; set; }
        public int secClass_Id { get; set; }
        public int group_Id { get; set; }
        public bool isKnown { get; set; }

        public string ScientificName { get; set; }

        //ItemCategories_Group_ID
        public Nullable<long> ItemCategories_Group_ID { get; set; }
    }
}
