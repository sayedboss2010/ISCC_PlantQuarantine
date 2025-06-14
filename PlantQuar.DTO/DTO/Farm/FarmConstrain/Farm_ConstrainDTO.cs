using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmConstrain
{
    public class Farm_ConstrainDTO
    {
        public long ID { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public Nullable<short> Country_Id { get; set; }
        public Nullable<short> Union_Id { get; set; }
        public Nullable<long> Farm_Constrain_Text_ID { get; set; }

        public Nullable<int> AnalysisType_ID { get; set; }
        public bool Is_Preview { get; set; }
        public bool IsActive { get; set; }
        public string AnalysisType_Name { get; set; }
        public string Country_Name { get; set; }
        public string Union_Name { get; set; }

        public string Farm_Constrain_Text_Name { get; set; }
        public string Item_Name { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

        public Nullable<byte> Count_Visit { get; set; }
        // public virtual AnalysisType AnalysisType { get; set; }
        // public virtual Country Country { get; set; }
        //  public virtual Farm_Constrain_Text Farm_Constrain_Text { get; set; }
        //  public virtual Item Item { get; set; }
    }
}
