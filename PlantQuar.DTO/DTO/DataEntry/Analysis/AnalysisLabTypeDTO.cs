using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Analysis
{
    public class AnalysisLabTypeDTO
    {

        public int ID { get; set; }
        public Nullable<int> AnalysisLabID { get; set; }
        public Nullable<int> AnalysisTypeID { get; set; }
        public string Name_Ar { get; set; }// for android don't change that name AnalysisLab_Name_Ar

        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

    }
}
